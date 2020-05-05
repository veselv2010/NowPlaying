using System;
using System.Threading.Tasks;
using System.Timers;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;
using NowPlaying.Core.Api.SpotifyResponses;
using System.Collections.Generic;

namespace NowPlaying.Core.Api
{
    public class SpotifyRequestsManager : RequestsManager
    {
        private enum RequestType
        {
            Auth,
            Refresh
        }

        private class SpotifyApiUrls
        {
            public const string Auth = "https://accounts.spotify.com/authorize";

            public const string Token = "https://accounts.spotify.com/api/token";

            public const string CurrentlyPlaying = "https://api.spotify.com/v1/me/player/currently-playing";
        }

        private Timer tokenRefreshTimer;
        private TokenResponse lastTokenResponse;

        private readonly string authorization;
        private readonly string clientId;
        private readonly string redirectUrl;

        public SpotifyRequestsManager(string clientId, string clientSecret,
            string redirectUrl)
        {
            this.authorization = "Basic " + Base64Encode($"{clientId}:{clientSecret}");
            this.clientId = clientId;
            this.redirectUrl = redirectUrl;
        }

        private async Task<RespT> SpotifyPost<RespT>(string url, IDictionary<string, string> reqParams = null)
        {
            return await UrlEncodedPost<RespT>(url, reqParams, authorization);
        }

        private async Task<string> SpotifyGet(string url, string accessToken)
        {
            string combinedUrl = url + "?access_token=" + accessToken;

            string resp = await Get(combinedUrl);

            return resp;
        }

        private string Base64Encode(string plainText)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(plainTextBytes);
        }
        /// <summary>
        /// Returns null if nothing is playing rn
        /// </summary>
        /// <returns></returns>
        public async Task<CurrentTrackResponse> GetCurrentTrack()
        {
            string resp = await SpotifyGet(SpotifyApiUrls.CurrentlyPlaying, lastTokenResponse.AccessToken);

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

        private async void GetRefreshedToken(object source, ElapsedEventArgs e)
        {
            var tokenReqParams = CreateTokenReqParams(RequestType.Refresh);

            var resp = await SpotifyPost<TokenResponse>(SpotifyApiUrls.Token, tokenReqParams);

            lastTokenResponse = resp;
        }

        public async Task StartTokenRequests(string code)
        {
            var tokenReqParams = CreateTokenReqParams(RequestType.Auth, code);

            var resp = await SpotifyPost<TokenResponse>(SpotifyApiUrls.Token, tokenReqParams);

            lastTokenResponse = resp;

            tokenRefreshTimer = new Timer((resp.ExpiresIn - 2) * 1000);
            tokenRefreshTimer.Elapsed += GetRefreshedToken;
            tokenRefreshTimer.Start();
        }

        public string GetAuthUrl()
        {
            return $"{SpotifyApiUrls.Auth}" +
                $"?client_id={this.clientId}" +
                $"&redirect_uri={this.redirectUrl}" +
                $"&response_type=code" +
                $"&scope=user-read-playback-state";
        }

        private IDictionary<string, string> CreateTokenReqParams(RequestType type, string code = null)
        {
            if (type == RequestType.Auth)
            {
                return new Dictionary<string, string>
                {
                    { "grant_type", "authorization_code" },
                    { "code", code },
                    { "redirect_uri", this.redirectUrl }
                };
            }

            if (type == RequestType.Refresh)
            {
                return new Dictionary<string, string>
                {
                    { "grant_type", "refresh_token" },
                    { "refresh_token", this.lastTokenResponse.RefreshToken }
                };
            }

            throw new NotImplementedException();
        }
    }
}