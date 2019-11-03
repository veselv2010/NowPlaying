using System;
using System.Collections.Generic;
using NowPlaying.Api.SpotifyResponses;

namespace NowPlaying
{
    internal static class AppInfo
    {
        public static readonly string SpotifyClientId = "7633771350404368ac3e05c9cf73d187";
        public static readonly string SpotifyClientSecret = "29bd9ec2676c4bf593f3cc2858099838";
        public static readonly string SpotifyRedirectUri = "https://www.google.com/"; // Random link.
        public static readonly string AssemblyDirectory = AppDomain.CurrentDomain.BaseDirectory;

        internal static class State
        {
            public static string SpotifyAccessToken { get; set; }

            public static string SpotifyRefreshToken { get; set; }

            public static DateTime TokenExpireTime { get; set; }

            public static IList<string> AccountNames { get; } = new List<string>();

            public static IDictionary<string, int> AccountNameToSteamId3 { get; } = new Dictionary<string, int>();

            public static string WindowHandle { get; set; }

            public static void UpdateToken(string accessToken, int expiresIn)
            {
                SpotifyAccessToken = accessToken;
                TokenExpireTime = DateTime.Now.AddSeconds(expiresIn - 5);
            }

            public static void UpdateToken(TokenResponse tokenResp)
            {
                UpdateToken(tokenResp.AccessToken, tokenResp.ExpiresIn);
            }
        }
    }
}
