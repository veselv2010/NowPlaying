using System;
using System.Linq;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NowPlaying.ApiResponses;

namespace NowPlaying
{
	static class Requests
    {
        public static CurrentTrackResponse GetCurrentTrack(string accessToken)
        {
            string resp = Requests.PerformGetRequest(@"https://api.spotify.com/v1/me/player/currently-playing?access_token=" + accessToken);

            if (string.IsNullOrEmpty(resp))
                return null;

            var currentTrackJson = JToken.Parse(resp);

            JToken trackInfo = currentTrackJson["item"];
            JToken artistsInfo = trackInfo["artists"];

            string trackId = (string) trackInfo["id"];
            string trackName = (string)trackInfo["name"];
            var artists = artistsInfo.Select(artist => (string)artist["name"]);

            long progress = (long)currentTrackJson["progress_ms"];
            long duration = (long)trackInfo["duration_ms"];

            return new CurrentTrackResponse(trackId, trackName, artists, progress, duration);
        }

        private static string PerformGetRequest(string url)
        {
            using (var wc = new WebClient() { Encoding = Encoding.UTF8 })
                return wc.DownloadString(url);
        }

        public static RespT PerformUrlEncodedPostRequest<RespT>(string url, string data)
        {
           using (var wc = new WebClient())
           {
                wc.Headers.Add(HttpRequestHeader.ContentType, "application/x-www-form-urlencoded");

                var b64str = Requests.Base64Encode($"{AppInfo.SpotifyClientId}:{AppInfo.SpotifyClientSecret}");
                wc.Headers.Add(HttpRequestHeader.Authorization, $"Basic {b64str}");
                var resp = wc.UploadString(url, data);

                return JsonConvert.DeserializeObject<RespT>(resp);
           }
        }

        private static string Base64Encode(string plainText)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(plainTextBytes);
        }
    }
}
