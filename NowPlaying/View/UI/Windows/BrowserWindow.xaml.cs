using System.Windows;
using CefSharp;
using CefSharp.Wpf;
using NowPlaying.Api;
using NowPlaying.Extensions;

namespace NowPlaying.UI.Windows
{
    public partial class BrowserWindow : Window
    {
        private string Url { get; set; }
        public string ResultToken { get; private set; }
        public int ExpireTime { get; private set; }
        public string RefreshToken { get; private set; }

        private SpotifyRequestsManager _spotify;

        internal BrowserWindow(SpotifyRequestsManager spotify)
        {
            _spotify = spotify;

            var settings = new CefSettings
            {
                CachePath = "cache",
            };
            CefSharpSettings.SubprocessExitIfParentProcessClosed = true;
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

                    var tokenResp = _spotify.GetToken(code, AppInfo.SpotifyRedirectUri);

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
