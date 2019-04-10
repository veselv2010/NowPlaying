using System.Windows;
using NowPlaying.ApiResponses;
using CefSharp;
using CefSharp.Wpf;
using System.Threading;
using System.Threading.Tasks;
using System;
using CefSharp.SchemeHandler;


namespace NowPlaying.OAuth
{
    public partial class BrowserWindow : Window
    {
        private static readonly string authUrlTemplate = @"https://accounts.spotify.com/authorize?client_id={0}"
                                                    + "&redirect_uri={1}&response_type=code&scope=user-read-playback-state";

        private static readonly string tokenUrl = @"https://accounts.spotify.com/api/token";

        private string GetAuthUrl() => string.Format(authUrlTemplate, AppInfo.SpotifyClientId, AppInfo.SpotifyRedirectUri);

        private string url { get; set; } = "";
        public string ResultToken { get; private set; }

        public string RefreshToken { get; private set; } = "";

        public BrowserWindow()
        {
            this.InitializeComponent();
        }

        private void BrowserWindow_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Browser_LoadingStateChanged(object sender, CefSharp.LoadingStateChangedEventArgs e)
        {
            Dispatcher.BeginInvoke((Action)(() =>
            {
                HtmlBox.Text = Browser.Address;
                url = HtmlBox.Text;
                if (RefreshToken != "")
                {
                    this.Browser.Dispose();
                    Cef.Shutdown();
                    this.Close();
                }
            }));
        }


        private void Browser_FrameLoadEnd(object sender, FrameLoadEndEventArgs e)
        {
            if (url.StartsWith(AppInfo.SpotifyRedirectUri) && url.Contains("code="))
            {
                    string code = url.Remove(0, 21).Trim();

                    var tokenReqParams = $"grant_type=authorization_code" +
                              $"&code={code}" +
                              $"&redirect_uri={AppInfo.SpotifyRedirectUri}";

                    TokenResponse tokenResp = Requests.PerformUrlEncodedPostRequest<TokenResponse>(tokenUrl, tokenReqParams);

                    this.ResultToken = tokenResp.AccessToken;
                    this.RefreshToken = tokenResp.RefreshToken;



                    return;

            };
        }
    }
}
