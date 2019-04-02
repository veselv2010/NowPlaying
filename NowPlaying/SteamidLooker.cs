using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Win32;
using System.Text.RegularExpressions;

namespace NowPlaying
{
    internal static class SteamIdLooker
    {
		const string RegexPatternId64 = @"(765611)\w+";
        const string RegexPatternAcc = "(Acc)\\w+\"\\s+\"([A-z-0-9])+";
        private static Regex Regex64 { get; } = new Regex(RegexPatternId64);
        private static Regex RegexAcc { get; } = new Regex(RegexPatternAcc);

        //public static List<string> SteamAPIurls = new List<string>();
        public static IList<string> Accounts { get; } = new List<string>();
        private static IList<string> SteamIds64 { get; } = new List<string>();
        private static IList<int> UserdataNumbers { get; } = new List<int>();

        public static IDictionary<string, int> AccountNameToSteamid3 { get; } = new Dictionary<string, int>();

        private static string LoginUsersPath = @"";
        public static string UserdataPath = @"";

        public static string UpdateSteamConfigPaths()
        { 
            var steamFullPath = Registry.GetValue(@"HKEY_CURRENT_USER\Software\Valve\Steam", "SteamPath", "") as string;

            if (String.IsNullOrEmpty(steamFullPath))
                throw new InvalidOperationException("Unable to locate the steam folder");

            UserdataPath = steamFullPath + @"\userdata";
            return LoginUsersPath = steamFullPath + @"\config\loginusers.vdf";
        }

        public static void UpdateAccountsInfo()
        {
            string[] fileLines = File.ReadAllLines(LoginUsersPath);
			for (int lineIndex = 2; lineIndex < fileLines.Length - 1; lineIndex++) //id64 + userdata(steamid3/32)
            {
                var currentSteamId64 = Regex64.Match(fileLines[lineIndex]);
                long.TryParse(currentSteamId64.ToString(), out long temp64);
                if (temp64 != 0)
                {
                    var currentSteamId32 = temp64 - 76561197960265728; //steamid64 - 76561197960265728 = steamid3/32
                    int.TryParse(currentSteamId32.ToString(), out int temp32 );
                    SteamIds64.Add(temp64.ToString());
                    UserdataNumbers.Add(temp32);
                }
                var currentAccountName = RegexAcc.Match(fileLines[lineIndex]).ToString(); //accountname
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
