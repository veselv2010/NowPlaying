using System.IO;
using System.Collections.Generic;
using Microsoft.Win32;
using System.Text.RegularExpressions;

namespace NowPlaying
{
    public class SteamIdLooker
    {
        private string steamFullPathCached;
        private string SteamFullPath
        {
            get
            {
                if (steamFullPathCached != null)
                    return steamFullPathCached;

                var path = Registry.GetValue(@"HKEY_CURRENT_USER\Software\Valve\Steam", "SteamPath", "") as string;

                if (string.IsNullOrEmpty(path))
                    throw new DirectoryNotFoundException("Unable to locate the steam folder");

                return steamFullPathCached = path;
            }
        }
        private string steamLasAccountCached;
        public string SteamLastAccount
        {
            get
            {
                if (steamLasAccountCached != null)
                    return steamLasAccountCached;

                var account = Registry.GetValue(@"HKEY_CURRENT_USER\Software\Valve\Steam", "AutoLoginUser", "") as string;

                if (string.IsNullOrEmpty(account))
                    throw new DirectoryNotFoundException("Unable to locate last logged-on account");

                return steamLasAccountCached = account;
            }
        }

        public string UserdataPath => SteamFullPath + @"\userdata";
        private string loginUsersPath => SteamFullPath + @"\config\loginusers.vdf";

        public IDictionary<string, int> GetSteamAccounts()
        {
            IDictionary<string, int> accounts = new Dictionary<string, int>();
            string line;
            int currentSteamId32 = 0;

            var regexSteamId64 = new Regex(@"(765611)\d+");
            var regexAcc = new Regex(@"AccountName""\s*""(\w+)");

            using (StreamReader reader = new StreamReader(loginUsersPath)) 

            while ((line = reader.ReadLine()) != null)
            {
                var steamId64Match = regexSteamId64.Match(line);

                if (steamId64Match.Success)
                {
                    long steamId64 = long.Parse(steamId64Match.Value);
                    currentSteamId32 = GetSteamId32(steamId64);
                    continue;
                }

                var accMatch = regexAcc.Match(line);

                if (accMatch.Success)
                {
                    if (currentSteamId32 == 0)
                        throw new FileFormatException();

                    accounts.Add(accMatch.Groups[1].Value, currentSteamId32);
                    continue;
                }
            }

            return accounts;
        }

        private int GetSteamId32(long steamId64) //steamid64 - 76561197960265728 = steamid3/32
        {
            return (int)(steamId64 - 76561197960265728);
        }
    }
}
