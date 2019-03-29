using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System;
using NowPlaying.ApiResponses;

namespace NowPlaying
{
    public partial class MainWindow : Window
    {
        private CancellationTokenSource _cancellationGetSpotifyUpdates;

        protected string LastPlayingTrackId { get; set; }

        public MainWindow()
        {
            this.InitializeComponent();

            for (int i = 0; i < SteamIdLooker.Accounts.Count; i++)
                this.AccountsList.Items.Add(SteamIdLooker.Accounts[i]);
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

            AppInfo.SpotifyAccessToken = browserWindow.ResultToken;
            this.Show();
        }

        private void ButtonDo_Click(object sender, RoutedEventArgs e)
        {
            CurrentTrackResponse trackResp = Requests.GetCurrentTrack(AppInfo.SpotifyAccessToken);

            if (trackResp == null)
                return;

            this.UpdateInterfaceTrackInfo(trackResp);

            if (this.AccountsList.SelectedItem == null)
                return;

            var cfgWriter = new ConfigWriter(this.KeyBind.Text, $@"{SteamIdLooker.UserdataPath}\{this.GetSelectedAccountId().ToString()}\730\local\cfg\audio.cfg");
            cfgWriter.RewriteKeyBinding(trackResp);
        }

        private async void ToggleSwitch_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (!this.SpotifySwitch.Toggled)
            {
                this._cancellationGetSpotifyUpdates?.Cancel();
                return;
            }

            if (this.AccountsList.SelectedItem == null)
            {
                this.SpotifySwitch.TurnOff();
                MessageBox.Show("Выберите аккаунт"); // Здесь просто MessageBox какой-нибудь пользователю алертнуть мол он в край ебнулся
                return;
            }

            if (this.KeyBind.Text == "")
            {
                this.SpotifySwitch.TurnOff();
                MessageBox.Show("Не назначена клавиша чата");
                return;
            }


            this.ButtonDo_Click(this, null); // force first request to not wait for the Thread.Sleep(1000)

            string keyboardButton = this.AccountsList.SelectedItem.ToString();
            this._cancellationGetSpotifyUpdates = new CancellationTokenSource();

            var cfgWriter = new ConfigWriter(this.KeyBind.Text, $@"{SteamIdLooker.UserdataPath}\{this.GetSelectedAccountId().ToString()}\730\local\cfg\audio.cfg");

            await Task.Factory.StartNew((/* сюда серануть keyboardButton как нибудь*/) =>
            {                           // чтобы потом его можно было использовать внутри этого блока
                while (true)
                {
                    Thread.Sleep(1000);

                    CurrentTrackResponse trackResp = Requests.GetCurrentTrack(AppInfo.SpotifyAccessToken);

                    this.Dispatcher.Invoke(() => this.UpdateInterfaceTrackInfo(trackResp));

                    if (trackResp?.Id != this.LastPlayingTrackId)
                    {
                        cfgWriter.RewriteKeyBinding(trackResp);
                        this.LastPlayingTrackId = trackResp.Id;
                    }

                    if (this._cancellationGetSpotifyUpdates.IsCancellationRequested)
                        return;
                }
            });
        }

        private void UpdateInterfaceTrackInfo(CurrentTrackResponse trackResp)
        {
            this.LabelWithButton.Content = this.KeyBind.Text;

            if (trackResp == null)
            {
                this.LabelFormatted.Content = "";
                this.ButtonDo.Content = "Nothing is playing!";
                return;
            }
            
            this.LabelFormatted.Content = trackResp.FormattedName;
            this.ButtonDo.Content = $"{trackResp.FullName} | {trackResp.Progress / 1000/ 60}:{trackResp.Progress / 1000 % 60}/{trackResp.Duration / 1000 / 60}:{trackResp.Duration / 1000 % 60}";
        }

        private int GetSelectedAccountId()
        {
            SteamIdLooker.AccountNameToSteamid3.TryGetValue(this.AccountsList.SelectedItem.ToString(), out int selectedAccountId);
            return selectedAccountId;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            this.PathTextBox.Text = SteamIdLooker.UserdataPath + $@"\{this.GetSelectedAccountId().ToString()}\730\local\cfg\audio.cfg";
        }

        private void AccountsList_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (this.SpotifySwitch.Toggled && this._cancellationGetSpotifyUpdates != null)
            {
                this.SpotifySwitch.TurnOff();
                this._cancellationGetSpotifyUpdates?.Cancel();
            }
        }
    }

}

//-_=ICON BY SCOUTPAN_=