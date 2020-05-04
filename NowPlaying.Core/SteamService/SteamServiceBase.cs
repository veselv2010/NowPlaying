using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace NowPlaying.Core.SteamService
{
    public abstract class SteamServiceBase
    {
        public abstract SteamInfo GetSteamInfo();

        private const long steamId32Mask = 76561197960265728;
        public IDictionary<string, int> GetSteamAccounts(SteamInfo info)
        {
            var accounts = new Dictionary<string, int>();
            string line;
            int currentSteamId32 = 0;

            var regexSteamId64 = new Regex(@"(765611)\d+");
            var regexAcc = new Regex(@"AccountName""\s*""(\w+)");

            using (var reader = new StreamReader(info.LoginUsersPath))
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
                            throw new Exception(); //fileformatex

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
