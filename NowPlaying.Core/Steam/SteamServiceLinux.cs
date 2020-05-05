using System;

namespace NowPlaying.Core.Steam
{
    public sealed class SteamServiceLinux : ISteamService
    {
        public string GetSteamFullPath()
        {
            throw new NotImplementedException();
        }

        public string GetSteamLastAccount()
        {
            throw new NotImplementedException();
        }

        public SteamInfo GetSteamInfo()
        {
            return new SteamInfo(GetSteamFullPath(), GetSteamLastAccount());
        }
    }
}
