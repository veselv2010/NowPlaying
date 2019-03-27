using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.IO;
using System.Diagnostics;
using Microsoft.Win32;

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
            string[] fileLines = File.ReadAllLines(LoginUsersPath);
            
            for (int lineIndex = 2; lineIndex < fileLines.Length - 1; lineIndex += 8) //id64
            {
                string currentSteamId64 = fileLines.Skip(lineIndex).Take(1).First().Trim().Replace("\"", "");

                SteamIds64.Add(currentSteamId64);
                Console.WriteLine(SteamIds64.Last());
            }

            for (int lineIndex = 4; lineIndex < fileLines.Length; lineIndex += 8) //accountname
            {
                string currentAcc = fileLines.Skip(lineIndex).Take(1).First().Trim()
                                            .Replace("AccountName", "").Replace("\"", "").Trim();

                Accounts.Add(currentAcc);
                Console.WriteLine(Accounts.Last());
            }
            foreach(string s in Directory.GetDirectories(UserdataPath)) //userdata steamid3
            {
                string temp = s.Remove(0, UserdataPath.Length + 1);
                int tempint = int.Parse(temp);
                UserdataNumbers.Add(tempint);
            }
            UserdataNumbers.Sort();
            for (int PositionInDictionary = 0; PositionInDictionary < Accounts.Count - 1; PositionInDictionary++)
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
