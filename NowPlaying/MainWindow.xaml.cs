using System.Windows;
using System.Threading.Tasks;
using System.Threading;
using NowPlaying.ApiResponses;

namespace NowPlaying
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            for (int i = 0; i < SteamIdLooker.accounts.Count; i++)
            {
                AccountsList.Items.Add(SteamIdLooker.accounts[i]);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.Hide();

            var browserWindow = new OAuth.BrowserWindow();
            browserWindow.ShowDialog();

            AppInfo.SpotifyAccessToken = browserWindow.ResultToken;

            this.Show();
        }
        public static string formattedTrackName;
        private void ButtonDo_Click(object sender, RoutedEventArgs e)
        {
                var getTrackTask = new Task<CurrentTrackResponse>(() => Requests.GetCurrentTrack(AppInfo.SpotifyAccessToken));
                getTrackTask.Start();
                getTrackTask.Wait();

                CurrentTrackResponse resp = getTrackTask.Result;

                if (resp == null)
                    return;

                ConfigWriter.ButtonToWrite = textBox.Text;
                LabelWithButton.Content = ConfigWriter.ButtonToWrite;

                string originalTrackName = $"{resp.GetArtistsString()} - {resp.TrackName}";
                string formattedTrackName = TrackNameFormatter.FormatForWriting(originalTrackName);

                ButtonDo.Content = $"{originalTrackName} | {(int)resp.Progress/1000}/{(int)resp.Duration/1000}";
                LabelFormatted.Content = formattedTrackName;
                int selectedaccountid;
                SteamIdLooker.AccountNameToSteamid3.TryGetValue(AccountsList.SelectedItem.ToString(), out selectedaccountid);
                var cfgWriter = new ConfigWriter(SteamIdLooker.userdataPath + $@"\{selectedaccountid.ToString()}\730\local\cfg\audio.cfg");
                cfgWriter.Write(formattedTrackName);
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
        private void ToggleSwitch_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (ToggleSwitch.Toggled == true)
            {
                string LastTrack;
                int selectedaccountid;
                SteamIdLooker.AccountNameToSteamid3.TryGetValue(AccountsList.SelectedItem.ToString(), out selectedaccountid);
                var cfgWriter = new ConfigWriter(SteamIdLooker.userdataPath + $@"\{selectedaccountid.ToString()}\730\local\cfg\audio.cfg");

                var getTrackTask = new Task<CurrentTrackResponse>(() => Requests.GetCurrentTrack(AppInfo.SpotifyAccessToken));
                getTrackTask.Start();
                getTrackTask.Wait();

                CurrentTrackResponse resp = getTrackTask.Result;

                if (resp == null)
                    return;

                ConfigWriter.ButtonToWrite = textBox.Text;
                LabelWithButton.Content = ConfigWriter.ButtonToWrite;

                string originalTrackName = $"{resp.GetArtistsString()} - {resp.TrackName}";
                string formattedTrackName = TrackNameFormatter.FormatForWriting(originalTrackName);

                ButtonDo.Content = $"{originalTrackName} | {resp.Progress/1000}/{resp.Duration/1000}";
                LabelFormatted.Content = formattedTrackName;
                cfgWriter.Write(formattedTrackName);
                LastTrack = formattedTrackName;
                for (int i = 0; i < resp.Duration/1000; i++)
                {
                    getTrackTask = new Task<CurrentTrackResponse>(() => Requests.GetCurrentTrack(AppInfo.SpotifyAccessToken));
                    getTrackTask.Start();
                    getTrackTask.Wait();
                    resp = getTrackTask.Result;
                    originalTrackName = $"{resp.GetArtistsString()} - {resp.TrackName}";
                    formattedTrackName = TrackNameFormatter.FormatForWriting(originalTrackName);
                    ButtonDo.Content = $"{originalTrackName} | {resp.Progress / 1000}/{resp.Duration / 1000}";
                    LabelFormatted.Content = formattedTrackName;
                    if (formattedTrackName == LastTrack)
                    {
                        Thread.Sleep(1000);
                    }
                    else
                    {
                        cfgWriter.Write(formattedTrackName);
                        LastTrack = formattedTrackName;
                        Thread.Sleep(1000);
                    }
                }

            }
        }
    }

}

//-_=ICON BY SCOUTPAN_=