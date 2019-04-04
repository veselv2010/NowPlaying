using System.Windows;
using System.Windows.Navigation;
using mshtml;
using NowPlaying.ApiResponses;

namespace NowPlaying.OAuth
{
    public partial class BrowserWindow : Window
    {
        private static readonly string authUrlTemplate = @"https://accounts.spotify.com/authorize?client_id={0}"
                                                    + "&redirect_uri={1}&response_type=code&scope=user-read-playback-state";

        private static readonly string tokenUrl = @"https://accounts.spotify.com/api/token";

        private string GetAuthUrl() => string.Format(authUrlTemplate, AppInfo.SpotifyClientId, AppInfo.SpotifyRedirectUri);


        public string ResultToken { get; private set; }

        public string RefreshToken { get; private set; }

        public BrowserWindow()
        {
            this.InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.Browser.Navigate(this.GetAuthUrl());
        }

        private void Browser_LoadCompleted(object sender, NavigationEventArgs e)
        {
            // Hide scrollbar
            (this.Browser.Document as IHTMLDocument2).body.parentElement.style.overflow = "hidden";
        }

        private void Browser_Navigating(object sender, NavigatingCancelEventArgs e)
        {
            string url = e.Uri.ToString();

            if (url.StartsWith(AppInfo.SpotifyRedirectUri) && url.Contains("code="))
            {
                string code = e.Uri.GetPropertyValue("code");

                var tokenReqParams = $"grant_type=authorization_code" +
                          $"&code={code}" +
                          $"&redirect_uri={AppInfo.SpotifyRedirectUri}";

                TokenResponse tokenResp = Requests.PerformUrlEncodedPostRequest<TokenResponse>(tokenUrl, tokenReqParams);

                this.ResultToken = tokenResp.AccessToken;
                this.RefreshToken = tokenResp.RefreshToken;
                this.Close();
				this.Browser.Dispose();
                return;
            }
        }
    }
}
