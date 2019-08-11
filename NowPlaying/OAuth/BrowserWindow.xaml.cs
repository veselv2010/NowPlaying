using System.Windows;
using NowPlaying.ApiResponses;
using CefSharp;
using System;
using CefSharp.Wpf;
using NowPlaying.Extensions;

namespace NowPlaying.OAuth
{
    public partial class BrowserWindow : Window
    {       
        //private string GetAuthUrl() => string.Format(authUrlTemplate, AppInfo.SpotifyClientId, AppInfo.SpotifyRedirectUri);
        //private static readonly string authUrlTemplate = @"https://accounts.spotify.com/authorize?client_id={0}"
        //                                            + "&redirect_uri={1}&response_type=code&scope=user-read-playback-state";

        private static readonly string tokenUrl = "https://accounts.spotify.com/api/token";

        private string Url { get; set; }

        public string ResultToken { get; private set; }
        public int ExpireTime { get; private set; }
        public string RefreshToken { get; private set; }

        public BrowserWindow()
        {
            var settings = new CefSettings
            {
                CachePath = "cache" //несет ли какие-нибудь последствия эта тема в плане безопасности 
            };
            Cef.Initialize(settings);
            this.InitializeComponent();
        }

        private void BrowserWindow_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Browser_LoadingStateChanged(object sender, CefSharp.LoadingStateChangedEventArgs e)
        {
            Dispatcher.BeginInvoke((Action)(() =>
            {
                Url = Browser.Address;
                if (RefreshToken != null)
                {
                    this.Browser.Dispose();
                    Cef.Shutdown();
                    this.Close();
                }
            }));
        }

        private void Browser_FrameLoadStart(object sender, FrameLoadStartEventArgs e)
        {
            if (Url.StartsWith(AppInfo.SpotifyRedirectUri + "?code="))
            {
                while (RefreshToken == null)
                {
                    string code = UriExtensions.GetPropertyValue(Url, "code");

                    var tokenReqParams = $"grant_type=authorization_code" +
                              $"&code={code}" +
                              $"&redirect_uri={AppInfo.SpotifyRedirectUri}";

                    var tokenResp = Requests.PerformUrlEncodedPostRequest<TokenResponse>(tokenUrl, tokenReqParams);
                    this.ExpireTime = tokenResp.ExpiresIn;
                    this.ResultToken = tokenResp.AccessToken;
                    this.RefreshToken = tokenResp.RefreshToken;
                    return;
                }
            }
        }
    }
}
