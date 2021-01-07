namespace NowPlaying.Core.Steam
{
    public interface ISteamService
    {
        SteamContext GetSteamContext();
        string GetSteamFullPath();
        string GetSteamLastAccount();
    }
}
