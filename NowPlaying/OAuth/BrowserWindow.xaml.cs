using System.Windows;
using NowPlaying.ApiResponses;
using CefSharp;
using CefSharp.Wpf;



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
        private void BrowserWindow_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Browser_LoadingStateChanged(object sender, CefSharp.LoadingStateChangedEventArgs e)
        {

         /*   string url = Browser.Address;

            if (url.StartsWith(AppInfo.SpotifyRedirectUri) && url.Contains("code="))
            {
                string code = Browser.Address.Substring(33, 184);

                var tokenReqParams = $"grant_type=authorization_code" +
                          $"&code={code}" +
                          $"&redirect_uri={AppInfo.SpotifyRedirectUri}";

                TokenResponse tokenResp = Requests.PerformUrlEncodedPostRequest<TokenResponse>(tokenUrl, tokenReqParams);

                this.ResultToken = tokenResp.AccessToken;
                this.RefreshToken = tokenResp.RefreshToken;
                Cef.Shutdown();
                this.Close();
                this.Browser.Dispose();
                return;
            } */
        }

        private void Browser_FrameLoadEnd(object sender, FrameLoadEndEventArgs e)
        {

        }
    }
}
