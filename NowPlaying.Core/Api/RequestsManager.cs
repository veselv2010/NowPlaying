using System;
using System.Net;
using System.Text;
using Newtonsoft.Json;

namespace NowPlaying.Core.Api
{
    public abstract class RequestsManager
    {
        private WebClient CreateWebClient()
        {
            return new WebClient() { Encoding = Encoding.UTF8 };
        }

        protected string Get(string url)
        {
            using (var wc = CreateWebClient())
                return wc.DownloadString(url);
        }

        protected RespT UrlEncodedPost<RespT>(string url, string data = "", string authorization = null)
        {
           using (var wc = CreateWebClient())
           {
                wc.Headers.Add(HttpRequestHeader.ContentType, "application/x-www-form-urlencoded");

                if (authorization != null)
                {
                    wc.Headers.Add(HttpRequestHeader.Authorization, authorization);
                }

                var resp = wc.UploadString(url, data);

                return JsonConvert.DeserializeObject<RespT>(resp);
           }
        }
    }
}