using System.IO;
using System.Collections.Generic;
using Microsoft.Win32;
using System.Text.RegularExpressions;

namespace NowPlaying.Core
{
    public class SteamIdLooker
    {
        private const long steamId32Mask = 76561197960265728;
        private const string steamRegistryPath = @"HKEY_CURRENT_USER\Software\Valve\Steam";

        private string steamFullPathCached;
        private string SteamFullPath
        {
            get
            {
                if (steamFullPathCached != null)
                    return steamFullPathCached;

                var path = Registry.GetValue(steamRegistryPath, "SteamPath", "") as string;

                if (string.IsNullOrEmpty(path))
                    throw new DirectoryNotFoundException("Unable to locate the steam folder");

                return steamFullPathCached = path;
            }
        }
        private string steamLastAccountCached;
        public string SteamLastAccount
        {
            get
            {
                if (steamLastAccountCached != null)
                    return steamLastAccountCached;

                var account = Registry.GetValue(steamRegistryPath, "AutoLoginUser", "") as string;

                if (string.IsNullOrEmpty(account))
                    throw new DirectoryNotFoundException("Unable to locate last logged-on account");

                return steamLastAccountCached = account;
            }
        }

        public string UserdataPath => SteamFullPath + @"\userdata";
        private string loginUsersPath => SteamFullPath + @"\config\loginusers.vdf";

        public IDictionary<string, int> GetSteamAccounts()
        {
            var accounts = new Dictionary<string, int>();
            string line;
            int currentSteamId32 = 0;

            var regexSteamId64 = new Regex(@"(765611)\d+");
            var regexAcc = new Regex(@"AccountName""\s*""(\w+)");

            using (var reader = new StreamReader(loginUsersPath))
            {
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
            }

            return accounts;
        }

        private int GetSteamId32(long steamId64)
        {
            return (int)(steamId64 - steamId32Mask);
        }
    }
}
