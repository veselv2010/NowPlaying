using System;
using System.Collections.Generic;

namespace NowPlaying
{
    public static class AppInfo
    {
        public static readonly string SpotifyClientId = "7633771350404368ac3e05c9cf73d187";
        public static readonly string SpotifyClientSecret = "29bd9ec2676c4bf593f3cc2858099838";
        public static readonly string SpotifyRedirectUri = "https://vk.com/"; // Random link.

        public static class State
        {
            public static string SpotifyAccessToken { get; set; }
            
            public static string SpotifyRefreshToken { get; set; }

            public static DateTime TokenExpireTime { get; set; }

            public static IList<string> AccountNames { get; } = new List<string>();

            public static IDictionary<string, int> AccountNameToSteamId3 { get; } = new Dictionary<string, int>();
        }
    }
}
