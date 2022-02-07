﻿using Newtonsoft.Json;

namespace NowPlaying.Core.Api.Spotify.Responses
{
    public class SpotifyTokenResponse
    {
        [JsonProperty(PropertyName = "access_token")]
        public string AccessToken { get; private set; }

        [JsonProperty(PropertyName = "refresh_token")]
        public string RefreshToken { get; private set; }

        [JsonProperty(PropertyName = "scope")]
        public string Scope { get; private set; }

        [JsonProperty(PropertyName = "expires_in")]
        public int ExpiresIn { get; private set; }
    }
}
