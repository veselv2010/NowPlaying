using System.IO;
using System.Net;
using Newtonsoft.Json;

namespace NowPlaying
{
    static class SimpleRequest
    {
        public static string Execute(string url, string json, string method = "GET")
        {
            var httpRequest = (HttpWebRequest)WebRequest.Create(url);
            httpRequest.ContentType = "application/json";
            httpRequest.Method = method;

            using (var streamWriter = new StreamWriter(httpRequest.GetRequestStream()))
                streamWriter.Write(json);

            var httpResponse = (HttpWebResponse)httpRequest.GetResponse();

            string result;
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                result = streamReader.ReadToEnd();

            return result;
        }

        public static object Execute<TT>(string url, string json, string method = "GET")
        {
            var result = Execute(url, json, method);

            return JsonConvert.DeserializeObject<TT>(result);
        }

    }
}
