using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using NowPlaying.ApiResponses;

namespace NowPlaying
{
    public partial class MainWindow : Window
    {
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

            SteamIdLooker.AccountNameToSteamid3.TryGetValue(this.AccountsList.SelectedItem.ToString(), out int selectedAccId);

            var cfgWriter = new ConfigWriter(this.TextBox.Text, $@"{SteamIdLooker.UserdataPath}\{selectedAccId.ToString()}\730\local\cfg\audio.cfg");
            cfgWriter.RewriteKeyBinding(trackResp);
        }

        private async void ToggleSwitch_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (!ToggleSwitch.Toggled)
            {
                // Сюда серануть с .Cancel()
                return;
            }

            if (this.AccountsList.SelectedItem == null)
            {
                // Здесь просто MessageBox какой-нибудь пользователю алертнуть мол он в край ебнулся
                return;
            }

            string keyboardButton = this.AccountsList.SelectedItem.ToString();

            await Task.Factory.StartNew((/* сюда серануть keyboardButton как нибудь*/) => 
            {                           // чтобы потом его можно было использовать внутри этого блока
                while (true)
                {
                    Thread.Sleep(1000);

                    CurrentTrackResponse trackResp = Requests.GetCurrentTrack(AppInfo.SpotifyAccessToken);

                    if (trackResp == null)
                        continue;

                    this.Dispatcher.Invoke(() => this.UpdateInterfaceTrackInfo(trackResp));

                    // if (trackResp.Id != this.LastPlayingTrackId)
                    // { cfgWriter.Write(trackResp); }
                }
            }/*, сюда вторым аргументом серануть чето про CancellationToken */);
        }

        private void UpdateInterfaceTrackInfo(CurrentTrackResponse trackResp)
        {
            this.LabelWithButton.Content = this.TextBox.Text;
            this.LabelFormatted.Content = trackResp.FormattedName;
            this.ButtonDo.Content = $"{trackResp.FullName} | {trackResp.Progress / 1000}/{trackResp.Duration / 1000}";
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            SteamIdLooker.AccountNameToSteamid3.TryGetValue(this.AccountsList.SelectedItem.ToString(), out int selectedaccountid);
            this.PathTextBox.Text = SteamIdLooker.UserdataPath + $@"\{selectedaccountid.ToString()}\730\local\cfg\audio.cfg";
        }


        private void textBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
        }

        private void AccountsList_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
        }

        private void ToggleSwitch_Loaded(object sender, RoutedEventArgs e)
        {
        }
    }

}

//-_=ICON BY SCOUTPAN_=