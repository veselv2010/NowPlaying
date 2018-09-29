using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.IO;
using System.Diagnostics;

namespace NowPlaying
{
    class SteamIdLooker
    {
        public static List<string> SteamAPIurls = new List<string>();
        public static List<string> accounts = new List<string>();
        public static List<string> SteamIds64 = new List<string>();
        public static List<int> userdataNumbers = new List<int>();

        private static Regex haHAA = new Regex("\"");
        private static string LoginUsersPath = @"";
        public static string userdataPath = @"";


        public static string SteamCfgPath(string processName)
        {
            string steamFullPath = Process.GetProcessesByName(processName)[0].MainModule.FileName;
            int IndexOfSteamEXE = steamFullPath.IndexOf("Steam.exe");
            userdataPath = steamFullPath.Remove(IndexOfSteamEXE) + @"userdata";
            return LoginUsersPath = steamFullPath.Remove(IndexOfSteamEXE) + @"config\loginusers.vdf";
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

                accounts.Add(currentAcc);
                Console.WriteLine(accounts.Last());
            }
            foreach(string s in Directory.GetDirectories(userdataPath)) //userdata steamid3
            {
                string temp = s.Remove(0, userdataPath.Length + 1);
                int tempint = int.Parse(temp);
                userdataNumbers.Add(tempint);
            }
            userdataNumbers.Sort();
            for (int PositionInDictionary = 0; PositionInDictionary < accounts.Count; PositionInDictionary++)
            {
                AccountNameToSteamid3.Add(accounts[PositionInDictionary], userdataNumbers[PositionInDictionary]);
            }
        }
        public static Dictionary<string, int> AccountNameToSteamid3 = new Dictionary<string, int>
        {

        };

        public static void MakeUrls() //future feature
        {
            for (int i = 0; i < SteamIds64.Count; i++)
            {
                SteamAPIurls[i] = "http://api.steampowered.com/ISteamUser/GetPlayerSummaries/v0002/" +
                    $"?key={AppInfo.SteamAPIKey}" +
                    $"&steamids={SteamIds64[i]}";
            }
        }
    }
}
