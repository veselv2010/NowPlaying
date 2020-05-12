using System;
using System.Windows;
using NowPlaying.Core.Extensions;
using CefSharp;
using CefSharp.Wpf;
using System.Threading.Tasks;

namespace NowPlaying.Wpf.Auth
{
    public partial class AuthWindow : Window
    {
        private delegate void AddressChanged();
        private event AddressChanged addressChanged;
        private string url;
        public string Code;
        public AuthWindow(string url)
        {
            var cefSettings = new CefSettings
            {
                CachePath = "cache"
            };
            Cef.Initialize(cefSettings);
            InitializeComponent();

            BrowserControl.Address = url;

            BrowserControl.LoadingStateChanged += LoadingStateChanged;
            addressChanged += GetCode;
        }

        private void LoadingStateChanged(object sender, LoadingStateChangedEventArgs e)
        {
            string currentUrl = string.Empty;
            Dispatcher.Invoke(() => currentUrl = BrowserControl.Address);

            if(currentUrl != this.url)
            {
                this.url = currentUrl;
                addressChanged.Invoke();
            }         
        }

        private void GetCode()
        {
            if (this.url.Contains("?code="))
            {
                Code = UriExtensions.GetPropertyValue(this.url, "code");
                Dispatcher.Invoke(() => this.BrowserControl.Dispose());
                Dispatcher.Invoke(() => this.Close());
            }
        }
    }
}
