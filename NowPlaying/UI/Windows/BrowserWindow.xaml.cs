using System.Windows;
using NowPlaying.ApiResponses;
using CefSharp;
using System;
using CefSharp.Wpf;
using NowPlaying.Extensions;

namespace NowPlaying.UI.Windows
{
    public partial class BrowserWindow : Window
    {
        private static readonly string tokenUrl = "https://accounts.spotify.com/api/token";

        private string Url { get; set; }

        public string ResultToken { get; private set; }
        public int ExpireTime { get; private set; }
        public string RefreshToken { get; private set; }

        public BrowserWindow()
        {
            var settings = new CefSettings
            {
                CachePath = "cache", //несет ли какие-нибудь последствия эта тема в плане безопасности
            };
            settings.CefCommandLineArgs.Remove("process-per-tab");
            Cef.Initialize(settings);
            this.InitializeComponent();
        }

        private void Browser_LoadingStateChanged(object sender, CefSharp.LoadingStateChangedEventArgs e)
        {
            Dispatcher.Invoke(() => Url = Browser.Address);

            if (Url != null && Url.StartsWith(AppInfo.SpotifyRedirectUri + "?code="))
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
                    Dispatcher.Invoke(() => this.Browser.Dispose());
                    Dispatcher.Invoke(() => this.Close());
                }
            }
        }
    }
}
