using System;
using System.Collections.Generic;
using System.Text; 

namespace NowPlaying.Core.SteamService
{
    public class SteamInfo
    {
        public string FullPath { get; set; }

        public string LastAccount { get; set; }

        public string UserdataPath => FullPath + @"\userdata";

        public string LoginUsersPath => FullPath + @"\config\loginusers.vdf";

    }

    public interface ISteamService
    {
        string GetSteamLastAccount();
        string GetSteamFullPath();
    }
}
