using System.Linq;
using System.Windows;
using System.Windows.Navigation;
using mshtml;

namespace NowPlaying.OAuth
{
    public partial class BrowserWindow : Window
    {
        private const string authLinkTemplate = @"https://accounts.spotify.com/en/authorize?client_id={0}"
                                                    + "&redirect_uri={1}&response_type=token&scope=user-read-playback-state";

        private static string AuthLink => string.Format(authLinkTemplate, AppInfo.SpotifyClientId, AppInfo.SpotifyRedirectUri);

        public string ResultToken { get; private set; }

        public BrowserWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Browser.Navigate(AuthLink);
        }

        private void Browser_LoadCompleted(object sender, NavigationEventArgs e)
        {
            // Hide scrollbar
            (Browser.Document as IHTMLDocument2).body.parentElement.style.overflow = "hidden";
        }

        private void Browser_Navigating(object sender, NavigatingCancelEventArgs e)
        {
            string currentUrl = e.Uri.ToString();
            if (currentUrl.Contains("access_token="))
            {
                var urlParams = currentUrl.Split(new char[] { '?', '&' });
                string token = urlParams.Single(p => p.Contains("access_token"))
                                        .Split('=')[1]; // "access_token=*text*" split by '=', take *text*

                this.ResultToken = token;

                this.Browser.Dispose();
                this.Close();
            }
        }
    }
}
