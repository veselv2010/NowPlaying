using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using NowPlaying.ApiResponses;
using NowPlaying.Extensions;
using MenuItem = System.Windows.Forms.MenuItem;

namespace NowPlaying.UI
{
    public partial class MainWindow : Window
    {
        private bool IsAutoTrackChangeEnabled { get; set; }
        private string CurrentKeyBind { get; set; }
        protected string LastPlayingTrackId { get; set; }

        private CancellationTokenSource _cancellationGetSpotifyUpdates;

        public MainWindow()
        {
            this.InitializeComponent();
            this.InitializeTrayMenu();

            Program.TrayMenu.Show();

            #if DEBUG

            DebugCheckBox.Visibility = Visibility.Visible;
            
            #endif
        }

        private void InitializeTrayMenu()
        {
            Program.TrayMenu.Items.AddRange(new MenuItem[]
            {
                new MenuItem("Show", TrayMenu.CreateEventHandler(ShowFromTray)),
                new MenuItem("Exit", TrayMenu.CreateEventHandler(Close)),
            });

            Program.TrayMenu.Icon.DoubleClick += TrayMenu.CreateEventHandler(ShowFromTray);
            Program.TrayMenu.NpcWorkTrayCheckBox.Click += TrayMenu.CreateEventHandler(NpcWorkCheckChange);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.Hide();

            var browserWindow = new OAuth.BrowserWindow();
            browserWindow.ShowDialog();

            if (browserWindow.ResultToken == null)
            {
                this.Close();
                return;
            }

            AppInfo.State.SpotifyAccessToken = browserWindow.ResultToken;
            AppInfo.State.SpotifyRefreshToken = browserWindow.RefreshToken;
            AppInfo.State.TokenExpireTime = DateTime.Now.AddSeconds(browserWindow.ExpireTime - 5);

            this.Show();

            if (CustomComboBox.SelectedItem == null)
            {
                MessageBox.Show("Файл loginusers.vdf пуст");
                this.Close();
                return;
            }
        }

        private void ButtonDo_Click(object sender, RoutedEventArgs e)
        {
            if (AppInfo.State.TokenExpireTime < DateTime.Now)
            {
                this.ButtonDo.Content = "spotify token expired!";
                return;
            }

            var trackResp = Requests.GetCurrentTrack(AppInfo.State.SpotifyAccessToken);

            if (trackResp == null)
                return;

            this.UpdateInterfaceTrackInfo(trackResp);

            if (CustomComboBox.SelectedItem == null)
                return;

            var cfgWriter = new ConfigWriter($@"{SteamIdLooker.UserdataPath}\{this.GetSelectedAccountId().ToString()}\730\local\cfg\audio.cfg");
            cfgWriter.RewriteKeyBinding(trackResp);
        }

        private async void ToggleSwitch_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (!this.SpotifySwitch.Toggled)
            {
                this._cancellationGetSpotifyUpdates?.Cancel();
                Program.TrayMenu.NpcWorkTrayCheckBox.Checked = false;
                return;
            }

            if (!SourceKeysExtensions.SourceEngineAllowedKeys.Contains(CustomTextBoxChatButton.CurrentText))
            {
                this.SpotifySwitch.TurnOff();
                MessageBox.Show("такой кнопки в кантре нет");
                return;
            }

            TextBoxToConsole.Text = $"bind \"{CustomTextBoxChatButton.CurrentText}\" \"exec audio.cfg\"";

            this.ButtonDo_Click(this, null); // force first request to not wait for the Thread.Sleep(1000)

            string keyboardButton = CustomComboBox.SelectedItem;
            int _SelectedAccount = CustomComboBox.SelectedIndex;
            this._cancellationGetSpotifyUpdates = new CancellationTokenSource();

            var cfgWriter = new ConfigWriter($@"{SteamIdLooker.UserdataPath}\{this.GetSelectedAccountId().ToString()}\730\local\cfg\audio.cfg");

            await Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    if (CustomComboBox.SelectedIndex != _SelectedAccount)
                        this.Dispatcher.Invoke(() => OnAccountsListSelectionChanged());

                    Thread.Sleep(1000);

                    if (AppInfo.State.TokenExpireTime < DateTime.Now)
                    {
                        AppInfo.State.RefreshToken();
                        cfgWriter.RewriteKeyBinding("say \"spotify token expired!\"");
                    }

                    var trackResp = Requests.GetCurrentTrack(AppInfo.State.SpotifyAccessToken);

                    this.Dispatcher.Invoke(() => this.UpdateInterfaceTrackInfo(trackResp));
                    this.Dispatcher.Invoke(() => LabelWindowHandle.Content = AppInfo.State.WindowHandle);

                    if (trackResp != null && trackResp.Id != this.LastPlayingTrackId)
                    {
                        cfgWriter.RewriteKeyBinding(trackResp);
                        this.LastPlayingTrackId = trackResp.Id;

                        if (IsAutoTrackChangeEnabled && Program.GameProcess.IsValid)
                            KeySender.SendInputWithAPI(CurrentKeyBind);
                    }

                    if (this._cancellationGetSpotifyUpdates.IsCancellationRequested)
                        return;
                }
            });
        }

        private void UpdateInterfaceTrackInfo(CurrentTrackResponse trackResp)
        {
            this.IsAutoTrackChangeEnabled = CustomCheckBox.IsChecked;
            this.CurrentKeyBind = CustomTextBoxChatButton.CurrentText;
            this.LabelWithButton.Content = CustomTextBoxChatButton.CurrentText;

            if (trackResp == null)
            {
                this.LabelArtist.Content = "NowPlaying";
                this.LabelFormatted.Content = "Nothing is playing!";
                return;
            }

            if (trackResp.IsLocalFile)
                this.LabelLocalFilesWarning.Visibility = Visibility.Visible;
            else
                this.LabelLocalFilesWarning.Visibility = Visibility.Collapsed;

            this.LabelArtist.Content = $"{trackResp.FormattedArtists}";
            this.LabelFormatted.Content = $"{trackResp.Name}";
            this.LabelCurrentTime.Content = $"{trackResp.ProgressMinutes.ToString()}:{trackResp.ProgressSeconds:00}";
            this.LabelEstimatedTime.Content = $"{trackResp.DurationMinutes.ToString()}:{trackResp.DurationSeconds:00}";
        }

        private int GetSelectedAccountId()
        {
            return AppInfo.State.AccountNameToSteamId3[CustomComboBox.SelectedItem];
        }

        private void OnAccountsListSelectionChanged()
        {
            if (this.SpotifySwitch.Toggled && this._cancellationGetSpotifyUpdates != null)
            {
                this.SpotifySwitch.TurnOff();
                this._cancellationGetSpotifyUpdates?.Cancel();
            }
        }

        private void LabelSourceKeysClick(object sender, RoutedEventArgs e)
        {
            if (!SourceKeysExtensions.TryOpenSourceKeysFile())
                MessageBox.Show("не найден файл с биндами (SourceKeys.txt)");
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Program.Dispose();
        }

        private void Window_StateChanged(object sender, EventArgs e)
        {
            if (WindowState == WindowState.Minimized) //костыль для работы трея из форм в впфе
                this.Hide();
        }

        private void ShowFromTray()
        {
            this.Show();
            WindowState = WindowState.Normal;
        }

        private void NpcWorkCheckChange()
        {
            this.SpotifySwitch.Toggle();
            ToggleSwitch_MouseLeftButtonDown(null, null);
        }

        private void LabelHelpClick(object sender, RoutedEventArgs e) 
            => Process.Start("https://github.com/veselv2010/NowPlaying/blob/master/README.md");
    }
}

//-_=ICON BY SCOUTPAN_=