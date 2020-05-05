namespace NowPlaying.Core.Steam
{
    public static class SteamIdConversionExtensions
    {
        private const long steamId32Mask = 76561197960265728;

        public static int GetSteamId32(long steamId64)
        {
            return (int)(steamId64 - steamId32Mask);
        }
    }
}