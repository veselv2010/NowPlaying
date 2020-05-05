using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace NowPlaying.Core.Steam
{
    public class LoginUsersReader
    {
        private readonly string _loginUsersPath;
        private readonly Regex regexAcc = new Regex(@"AccountName""\s*""(\w+)");
        private readonly Regex regexSteamId64 = new Regex(@"(765611)\d+");

        public LoginUsersReader(string loginUsersVdfPath)
        {
            _loginUsersPath = loginUsersVdfPath;
        }

        private bool TryReadSteamId64(string line, out long steamId64)
        {
            var steamId64Match = regexSteamId64.Match(line);

            if (!steamId64Match.Success)
            {
                steamId64 = default;
                return false;
            }

            steamId64 = long.Parse(steamId64Match.Value);
            return true;
        }

        private bool TryReadAccountName(string line, out string accName)
        {
            var accMatch = regexAcc.Match(line);

            if (!accMatch.Success)
            {
                accName = null;
                return false;
            }

            accName = accMatch.Groups[1].Value;
            return true;
        }

        public IDictionary<string, int> Read()
        {
            var accounts = new Dictionary<string, int>();
            string line;
            int currentSteamId32 = 0;

            using (var reader = new StreamReader(_loginUsersPath))
            {
                while ((line = reader.ReadLine()) != null)
                {
                    if (TryReadSteamId64(line, out var steamId64))
                    {
                        currentSteamId32 = SteamIdConversionExtensions.GetSteamId32(steamId64);
                        continue;
                    }

                    if (TryReadAccountName(line, out var accName))
                    {
                        if (currentSteamId32 == 0)
                            throw new Exception(); //fileformatex

                        accounts.Add(accName, currentSteamId32);
                        continue;
                    }
                }
            }

            return accounts;
        }
    }
}