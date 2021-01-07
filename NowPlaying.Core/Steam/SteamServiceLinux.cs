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

        public SteamContext GetSteamContext()
        {
            return new SteamContext(GetSteamFullPath(), GetSteamLastAccount());
        }
    }
}
