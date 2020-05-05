namespace NowPlaying.Core.Steam
{
    public interface ISteamService
    {
        SteamInfo GetSteamInfo();
        string GetSteamFullPath();
        string GetSteamLastAccount();
    }
}
