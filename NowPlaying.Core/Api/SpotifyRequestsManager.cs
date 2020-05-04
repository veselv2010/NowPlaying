using System;
using System.Timers;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;
using NowPlaying.Core.Api.SpotifyResponses;

namespace NowPlaying.Core.Api
{
    public class SpotifyRequestsManager : RequestsManager
    {
        private class SpotifyApiUrls
        {
            public const string Auth = "https://accounts.spotify.com/authorize";

            public const string Token = "https://accounts.spotify.com/api/token";

            public const string CurrentlyPlaying = "https://api.spotify.com/v1/me/player/currently-playing";
        }

        private Timer tokenRefreshTimer;
        private readonly string authorization;
        private TokenResponse lastTokenResponse;

        public SpotifyRequestsManager(string spotifyClientId, string spotifyClientSecret)
        {
            authorization = "Basic " + Base64Encode($"{spotifyClientId}:{spotifyClientSecret}");
        }

        private RespT SpotifyPost<RespT>(string url, string data = "")
        {
            return UrlEncodedPost<RespT>(url, data, authorization);
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

        private void GetRefreshedToken(object source, ElapsedEventArgs e)
        {
            var tokenReqParams = "grant_type=refresh_token" +
                                 $"&refresh_token={lastTokenResponse.RefreshToken}";

            var resp = SpotifyPost<TokenResponse>(SpotifyApiUrls.Token, tokenReqParams);

            lastTokenResponse = resp;
        }

        public void StartTokenRequests(string code, string redirectUrl)
        {
            var tokenReqParams = 
                              $"grant_type=authorization_code" +
                              $"&code={code}" +
                              $"&redirect_uri={redirectUrl}";

            var resp = SpotifyPost<TokenResponse>(SpotifyApiUrls.Token, tokenReqParams);

            lastTokenResponse = resp;

            tokenRefreshTimer = new Timer((resp.ExpiresIn - 2) * 1000);
            tokenRefreshTimer.Elapsed += GetRefreshedToken;
        }

        public string GetAuthUrl(string clientId, string redirectUrl)
        {
            return $"{SpotifyApiUrls.Auth}" +
                $"?client_id={clientId}" +
                $"&redirect_uri={redirectUrl}" +
                $"&response_type=code" +
                $"&scope=user-read-playback-state";
        }
    }
}