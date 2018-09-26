using System.Collections.Generic;
using System.IO;
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
            JToken currentTrackJson;
            try
            {
                currentTrackJson = PerformApiRequest(@"https://api.spotify.com/v1/me/player/currently-playing?access_token=" + accessToken);
            }
            catch (JsonReaderException)
            {
                return null; // Nothing is playing.
            }

            JToken trackInfo = currentTrackJson["item"];
            JToken artistsInfo = trackInfo["artists"];

            string trackName = (string)trackInfo["name"];
            IEnumerable<string> artists = artistsInfo.Select(artist => (string)artist["name"]);

            long progress = (long)currentTrackJson["progress_ms"];
            long duration = (long)trackInfo["duration_ms"];

            return new CurrentTrackResponse(trackName, artists, progress, duration);
        }

        private static JToken PerformApiRequest(string url)
        {
            string resp;
            using (WebClient wc = new WebClient() { Encoding = Encoding.UTF8 })
                resp = wc.DownloadString(url);

            return JToken.Parse(resp);
        }
    }
}
