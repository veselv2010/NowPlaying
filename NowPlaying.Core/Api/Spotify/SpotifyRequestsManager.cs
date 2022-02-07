using System;
using System.Threading.Tasks;
using System.Timers;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;
using NowPlaying.Core.Api.Spotify.Responses;
using System.Collections.Generic;
using System.Net.Http;

namespace NowPlaying.Core.Api.Spotify
{
    public sealed class SpotifyRequestsManager : RequestsManager
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

        private Timer _tokenRefreshTimer;
        private SpotifyTokenResponse _lastTokenResponse;

        private bool isAuthorized => _lastTokenResponse == null ? false : true;

        private readonly string authorization;
        private readonly string clientId;
        private readonly string redirectUrl;

        public SpotifyRequestsManager(string clientId, string clientSecret,
            string redirectUrl, HttpClient httpClient = null) : base(httpClient)
        {
            this.authorization = "Basic " + Utils.Base64Encode($"{clientId}:{clientSecret}");
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

        /// <summary>
        /// Returns null if nothing is playing rn (or there is no access token)
        /// </summary>
        /// <returns></returns>
        public async Task<IPlaybackResponse> GetCurrentTrack()
        {
            if (!isAuthorized)
                throw new System.Security.Authentication.AuthenticationException();

            string resp = await SpotifyGet(SpotifyApiUrls.CurrentlyPlaying, _lastTokenResponse.AccessToken);

            if (string.IsNullOrEmpty(resp))
                return null;

            var currentTrackJson = JToken.Parse(resp);

            JToken trackInfo = currentTrackJson["item"];
            JToken artistsInfo = trackInfo["artists"];
            JToken albumCover = trackInfo["album"];

            string trackId = (string) trackInfo["id"];
            string trackName = (string)trackInfo["name"];
            var artists = artistsInfo.Select(artist => (string)artist["name"]);

            long progress = (long)currentTrackJson["progress_ms"];
            long duration = (long)trackInfo["duration_ms"];

            string coverUrl = albumCover["images"].Count() == 0 ? "" : (string)albumCover["images"][0]["url"];

            
            return new SpotifyPlaybackResponse(trackId, trackName, coverUrl, artists, progress, duration);
        }

        private async void GetRefreshedToken(object source, ElapsedEventArgs e)
        {
            var tokenReqParams = CreateTokenReqParams(RequestType.Refresh);

            var resp = await SpotifyPost<SpotifyTokenResponse>(SpotifyApiUrls.Token, tokenReqParams);

            _lastTokenResponse = resp;
        }

        public async Task StartTokenRequests(string code)
        {
            var resp = await GetInitialResponse(code);

            _lastTokenResponse = resp;

            _tokenRefreshTimer = new Timer((resp.ExpiresIn - 2) * 1000);
            _tokenRefreshTimer.Elapsed += GetRefreshedToken;
            _tokenRefreshTimer.Start();
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
                    { "refresh_token", this._lastTokenResponse.RefreshToken }
                };
            }

            throw new NotImplementedException();
        }

        private async Task<SpotifyTokenResponse> GetInitialResponse(string code)
        {
            var tokenReqParams = CreateTokenReqParams(RequestType.Auth, code);

            return await SpotifyPost<SpotifyTokenResponse>(SpotifyApiUrls.Token, tokenReqParams);
        }

        public override void Dispose()
        {
            base.Dispose();
            _tokenRefreshTimer.Dispose();          
        }
    }
}