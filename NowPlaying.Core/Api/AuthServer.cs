using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using NowPlaying.Core.Extensions;

namespace NowPlaying.Core.Api
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

                var resp = context.Response;
                string url = context.Request.RawUrl;

                if (url.Contains("?code="))
                {
                    string redirectHtml = "<script>window.close()</script>";
                    byte[] buffer = Encoding.UTF8.GetBytes(redirectHtml);
                    resp.ContentLength64 = buffer.Length;
                    resp.OutputStream.Write(buffer, 0, buffer.Length);
                    resp.Close();

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