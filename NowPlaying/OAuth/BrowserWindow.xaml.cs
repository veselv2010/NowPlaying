using System.Windows;
using System.Windows.Navigation;
using mshtml;
using NowPlaying.ApiResponses;
using System.IO;
using System.Text.RegularExpressions;
using System.Net;
using System.Text;


namespace NowPlaying.OAuth
{
    public partial class BrowserWindow : Window
    {
        private static readonly string authUrlTemplate = @"https://accounts.spotify.com/authorize?client_id={0}"
                                                    + "&redirect_uri={1}&response_type=code&scope=user-read-playback-state";

        private static readonly string tokenUrl = @"https://accounts.spotify.com/api/token";

        private string GetAuthUrl() => string.Format(authUrlTemplate, AppInfo.SpotifyClientId, AppInfo.SpotifyRedirectUri);

        private string HtmlWithJs { get; set; }

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
            string FileWithJs = "index.2a0d8f60176c3d43e699.js";
            string FileWithHtml = "auth.html";
            string jsAddress = "https://accounts.scdn.co/js/index.2a0d8f60176c3d43e699.js";
            string url = e.Uri.ToString();

            using (var wc = new WebClient() { Encoding = Encoding.UTF8 })
            {
                HtmlWithJs = wc.DownloadString(url);
                wc.DownloadFile(jsAddress, FileWithJs);               
            }
            string temp = File.ReadAllText(FileWithJs);
            string NormalFunc = @"const r=function(e,t) {return new function(Promise(function(n) {return {const r=function() {return {t.test(document.readyState)&&(document.removeEventListener(e,r),n();};});};};};";
            string ArrowFunc = @"const r = (e, t) => new Promise(n => {const r= () => { t.test(document.readyState) && (document.removeEventListener(e, r), n())};";
            temp.Replace(ArrowFunc, NormalFunc);
            HtmlWithJs.Replace("src =\"https://accounts.scdn.co/js/index.2a0d8f60176c3d43e699.js\"", "src = \"index.2a0d8f60176c3d43e699.js\"");
            File.WriteAllText(FileWithHtml, HtmlWithJs);
            string Dir = Directory.GetCurrentDirectory();
            this.Browser.Navigate(@"file:///{Dir}/{FileWithHtml}");
            
            url = e.Uri.ToString();

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
