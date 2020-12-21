using System;
using System.Net;
using System.Threading.Tasks;
using NowPlaying.Core.Extensions;

namespace NowPlaying.Core
{
    public class AuthServer : IDisposable
    {
        private HttpListener _server;
        public AuthServer(string redirectUrl)
        {
            _server = new HttpListener();
            _server.Prefixes.Add(redirectUrl);
            _server.Start();
        }

        public async Task<string> GetAuthCode()
        {
            while (true)
            {
                HttpListenerContext context = _server.GetContext();
                string url = context.Request.RawUrl;

                if (url.Contains("?code="))
                {
                    return UriExtensions.GetPropertyValue(url, "code");
                }

                await Task.Delay(10);
            }
        }

        public void Dispose()
        {
            _server.Close();
        }
    }
}