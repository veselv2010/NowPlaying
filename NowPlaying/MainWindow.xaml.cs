using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using NowPlaying.ApiResponses;
using NowPlaying.Extensions;

namespace NowPlaying
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

            foreach (string a in AppInfo.State.AccountNames)
                this.AccountsList.Items.Add(a);
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

            if (this.AccountsList.SelectedItem == null)
                return;

            var cfgWriter = new ConfigWriter($@"{SteamIdLooker.UserdataPath}\{this.GetSelectedAccountId().ToString()}\730\local\cfg\audio.cfg");
            cfgWriter.RewriteKeyBinding(trackResp);
        }

        private async void ToggleSwitch_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (!this.SpotifySwitch.Toggled)
            {
                this.ChangeUIState(MainWindowUIState.Idle);
                this._cancellationGetSpotifyUpdates?.Cancel();
                return;
            }

            if (this.AccountsList.SelectedItem == null)
            {
                this.SpotifySwitch.TurnOff();
                MessageBox.Show("Выберите аккаунт");
                return;
            }

            if (!SourceKeysExtensions.SourceEngineAllowedKeys.Contains(this.TextBoxKeyBind.Text.ToLower()))
            {
                this.SpotifySwitch.TurnOff();
                MessageBox.Show("такой кнопки в кантре нет");
                return;
            }

            TextBoxToConsole.Text = $"bind \"{TextBoxKeyBind.Text}\" \"exec audio.cfg\"";

            this.ButtonDo_Click(this, null); // force first request to not wait for the Thread.Sleep(1000)

            this.ChangeUIState(MainWindowUIState.NpcWork);

            string keyboardButton = this.AccountsList.SelectedItem.ToString();

            this._cancellationGetSpotifyUpdates = new CancellationTokenSource();

            var cfgWriter = new ConfigWriter($@"{SteamIdLooker.UserdataPath}\{this.GetSelectedAccountId().ToString()}\730\local\cfg\audio.cfg");

            await Task.Factory.StartNew((/* сюда серануть keyboardButton как нибудь*/) =>
            {                           // чтобы потом его можно было использовать внутри этого блока
                while (true)
                {
                    Thread.Sleep(1000);

                    CurrentTrackResponse trackResp = Requests.GetCurrentTrack(AppInfo.State.SpotifyAccessToken);

                    this.Dispatcher.Invoke(() => this.UpdateInterfaceTrackInfo(trackResp));
                    this.Dispatcher.Invoke(() => LabelWindowHandle.Content = AppInfo.State.WindowHandle);

                    if (trackResp?.Id != this.LastPlayingTrackId)
                    {
                        cfgWriter.RewriteKeyBinding(trackResp);
                        this.LastPlayingTrackId = trackResp.Id;
                        if (IsAutoTrackChangeEnabled && Program.GameProcess.IsValid)
                        {
                            KeySender.SendInputWithAPI(CurrentKeyBind);
                        }
                    }

                    if (this._cancellationGetSpotifyUpdates.IsCancellationRequested)
                        return;

                    if (AppInfo.State.TokenExpireTime < DateTime.Now)
                    {
                        cfgWriter.RewriteKeyBinding("say \"spotify token expired!\"");
                        return;
                    }
                }
            });
        }

        private void UpdateInterfaceTrackInfo(CurrentTrackResponse trackResp)
        {
            this.IsAutoTrackChangeEnabled = CheckBoxAutoSend.IsChecked.Value;
            this.CurrentKeyBind = TextBoxKeyBind.Text;
            this.LabelWithButton.Content = this.TextBoxKeyBind.Text;

            if (trackResp == null)
            {
                this.LabelFormatted.Content = "";
                this.ButtonDo.Content = "Nothing is playing!";
                return;
            }

            if (trackResp.IsLocalFile)
                this.LabelLocalFilesWarning.Visibility = Visibility.Visible;
            else
                this.LabelLocalFilesWarning.Visibility = Visibility.Collapsed;

            this.LabelFormatted.Content = trackResp.FormattedName;
            this.ButtonDo.Content =
                $"{trackResp.FullName} | " +
                $"{trackResp.ProgressMinutes}:{trackResp.ProgressSeconds:00}/" +
                $"{trackResp.DurationMinutes}:{trackResp.DurationSeconds:00}";
        }

        private int GetSelectedAccountId()
        {
            return AppInfo.State.AccountNameToSteamId3[this.AccountsList.SelectedItem.ToString()];
        }

        private void ButtonPath_Click(object sender, RoutedEventArgs e)
        {
            this.TextBoxPath.Text = SteamIdLooker.UserdataPath
                                  + $@"\{this.GetSelectedAccountId().ToString()}\730\local\cfg\audio.cfg";
        }

        private void AccountsList_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (this.SpotifySwitch.Toggled && this._cancellationGetSpotifyUpdates != null)
            {
                this.SpotifySwitch.TurnOff();
                this._cancellationGetSpotifyUpdates?.Cancel();
            }
        }


        private void ChangeUIState(MainWindowUIState idle)
        {
            switch(idle)
            {
                case MainWindowUIState.NpcWork:
                {
                    this.LabelNpcDisclaimer.Visibility = Visibility.Collapsed;
                    this.LabelFormatted.Visibility     = Visibility.Visible;
                }
                break;

                case MainWindowUIState.Idle:
                {
                    this.LabelNpcDisclaimer.Visibility = Visibility.Visible;
                    this.LabelFormatted.Visibility     = Visibility.Collapsed;
                }
                break;
            }
        }

        private void ButtonSourceKeys_Click(object sender, RoutedEventArgs e)
        {
            if (!SourceKeysExtensions.TryOpenSourceKeysFile())
                MessageBox.Show("не найден файл с биндами (SourceKeys.txt)");
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Program.GameProcess.Dispose();
        }
    }
}

//-_=ICON BY SCOUTPAN_=