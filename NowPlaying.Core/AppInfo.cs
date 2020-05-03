using System;
using System.Collections.Generic;
using NowPlaying.Api.SpotifyResponses;

namespace NowPlaying
{
    internal static class AppInfo
    {
        internal static class State
        {
            public static string SpotifyAccessToken { get; set; }

            public static string SpotifyRefreshToken { get; set; }

            public static DateTime TokenExpireTime { get; set; }

            public static IList<string> AccountNames { get; } = new List<string>();

            public static IDictionary<string, int> AccountNameToSteamId3 { get; } = new Dictionary<string, int>();
            private static string _WindowName;
            public static string WindowName
            {
                get
                {
                    if(_WindowName != null)
                        return _WindowName;

                    return string.Empty;
                }
                set
                {
                    if (value == null || _WindowName == value)
                        return;

                    _WindowName = value;
                }
            }

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
