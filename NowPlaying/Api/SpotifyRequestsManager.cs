using System;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;
using NowPlaying.Api.SpotifyResponses;

namespace NowPlaying.Api
{
    internal class SpotifyRequestsManager : RequestsManager
    {
        private class SpotifyApiUrls
        {
            public const string Token = "https://accounts.spotify.com/api/token";

            public const string CurrentlyPlaying = "https://api.spotify.com/v1/me/player/currently-playing";
        }

        private readonly string _authorization;

        public SpotifyRequestsManager(string spotifyClientId, string spotifyClientSecret)
        {
            _authorization = "Basic " + Base64Encode($"{spotifyClientId}:{spotifyClientSecret}");
        }

        private RespT SpotifyPost<RespT>(string url, string data = "")
        {
            return UrlEncodedPost<RespT>(url, data, _authorization);
        }

        private string SpotifyGet(string url, string accessToken)
        {
            string combinedUrl = url + "?access_token=" + accessToken;

            string resp = Get(combinedUrl);

            return resp;
        }

        private string Base64Encode(string plainText)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(plainTextBytes);
        }

        public CurrentTrackResponse GetCurrentTrack(string accessToken)
        {
            string resp = SpotifyGet(SpotifyApiUrls.CurrentlyPlaying, accessToken);

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

        public TokenResponse GetRefreshedToken(string refreshToken)
        {
            var tokenReqParams = "grant_type=refresh_token" +
                                 $"&refresh_token={refreshToken}";

            var resp = SpotifyPost<TokenResponse>(SpotifyApiUrls.Token, tokenReqParams);

            return resp;
        }

        public TokenResponse GetToken(string code, string redirectUrl)
        {
            var tokenReqParams = 
                              $"grant_type=authorization_code" +
                              $"&code={code}" +
                              $"&redirect_uri={redirectUrl}";

            var resp = SpotifyPost<TokenResponse>(SpotifyApiUrls.Token, tokenReqParams);

            return resp;
        }
    }
}