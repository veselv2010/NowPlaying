using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Win32;
using System.Text.RegularExpressions;

namespace NowPlaying
{
    class SteamIdLooker
    {
        //public static List<string> SteamAPIurls = new List<string>();
        public static List<string> Accounts = new List<string>();
        public static List<string> SteamIds64 = new List<string>();
        public static List<int> UserdataNumbers = new List<int>();

        public static Dictionary<string, int> AccountNameToSteamid3 = new Dictionary<string, int>();

        private static string LoginUsersPath = @"";
        public static string UserdataPath = @"";

        public static string SteamCfgPath()
        { 
            var steamFullPath = Registry.GetValue(@"HKEY_CURRENT_USER\Software\Valve\Steam", "SteamPath", "") as string;

            if (String.IsNullOrEmpty(steamFullPath))
                throw new InvalidOperationException("Unable to locate the steam folder");

            UserdataPath = steamFullPath + @"\userdata";
            return LoginUsersPath = steamFullPath + @"\config\loginusers.vdf";
        }

        public static void SteamCfgReader()
        {
            string RegexPatternId64 = @"(765611)\w+";
            string RegexPatternAcc = "(Acc)\\w+" + '"' + "\\s+" + '"' + "([A-z-0-9])+";
            Regex id64Looker = new Regex(RegexPatternId64);
            Regex accountLooker = new Regex(RegexPatternAcc);
            string[] fileLines = File.ReadAllLines(LoginUsersPath);
            int temp32;
            long temp64;
            for (int lineIndex = 2; lineIndex < fileLines.Length - 1; lineIndex++) //id64 + userdata(steamid3/32)
            {
                var currentSteamId64 = id64Looker.Match(fileLines[lineIndex]);
                long.TryParse(currentSteamId64.ToString(), out temp64);
                if (temp64 != 0)
                {
                    var currentSteamId32 = temp64 - 76561197960265728; //steamid64 - 76561197960265728 = steamid3/32
                    int.TryParse(currentSteamId32.ToString(), out temp32);
                    SteamIds64.Add(temp64.ToString());
                    UserdataNumbers.Add(temp32);
                }
                var currentAccountName = accountLooker.Match(fileLines[lineIndex]).ToString(); //accountname
                if (currentAccountName != "")
                    Accounts.Add(currentAccountName.Remove(0, 12).Trim().Remove(0, 1));
            }

            for (int PositionInDictionary = 0; PositionInDictionary < Accounts.Count; PositionInDictionary++)
            {
                AccountNameToSteamid3.Add(Accounts[PositionInDictionary], UserdataNumbers[PositionInDictionary]);
            }
        }
        /*public static void MakeUrls() //future feature
        {
            for (int i = 0; i < SteamIds64.Count; i++)
            {
                SteamAPIurls[i] = "http://api.steampowered.com/ISteamUser/GetPlayerSummaries/v0002/" +
                    $"?key={AppInfo.SteamAPIKey}" +
                    $"&steamids={SteamIds64[i]}";
            }
        }*/
    }
}
