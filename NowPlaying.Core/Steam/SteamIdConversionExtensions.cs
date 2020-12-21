namespace NowPlaying.Core.Steam
{
    internal static class SteamIdConversionExtensions
    {
        private const long steamId32Mask = 76561197960265728;

        internal static int GetSteamId32(long steamId64)
        {
            return (int)(steamId64 - steamId32Mask);
        }
    }
}