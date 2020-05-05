using System.Threading;
using System.Threading.Tasks;
using System.Net.Http;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace NowPlaying.Core.Api
{
    public abstract class RequestsManager
    {
        protected readonly HttpClient client = new HttpClient { Timeout = Timeout.InfiniteTimeSpan };

        protected async Task<string> Get(string url)
        {
            using (var resp = await client.GetAsync(url))
            {
                var content = resp.Content;
                return await content.ReadAsStringAsync();
            }
        }

        protected async Task<RespT> UrlEncodedPost<RespT>(string url, IDictionary<string, string> reqParams = null,
            string authorization = null)
        {
            using (var postRequest = new HttpRequestMessage(HttpMethod.Post, url))
            {
                postRequest.Content = new FormUrlEncodedContent(reqParams);

                if (authorization != null)
                {
                    postRequest.Headers.Add("Authorization", authorization);
                }

                using (var resp = await client.SendAsync(postRequest))
                {
                    var respContent = await resp.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<RespT>(respContent);
                }
            }
        }
    }
}