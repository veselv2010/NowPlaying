using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using System.Management;

namespace NowPlaying
{
    class SteamidLooker
    {
        public static string[] SteamAPIurls = new string[10];
        public static string RegexPattern = "\"";
        public static int linecount = File.ReadAllLines(ReadPath).Length;
        public static Regex haHAA = new Regex(RegexPattern);
        public static int ArrayCounter;
        public static string[] accounts = new string[100];
        public static string[] SteamID64 = new string[100];
        private static string ReadPath = @"";
        public static string SteamCfgPath(string name)
        {
            foreach (Process PPath in Process.GetProcessesByName(name))
            {
                ReadPath = PPath.MainModule.FileName;
            }
            int PositionOfSteamEXE = ReadPath.IndexOf("Steam.exe");
            return ReadPath = ReadPath.Remove(PositionOfSteamEXE) + @"config\loginusers.vdf";
        }
        public static void SteamCfgReader()
        {
            Console.WriteLine(linecount);
            for (int i = 2; i < linecount; i++) //id64
            {
                if (i == linecount - 1)
                {
                    break;
                }
                SteamID64[ArrayCounter] = File.ReadLines(ReadPath).Skip(i).Take(1).First().Trim().Replace(RegexPattern, "");
                i += 7;
                Console.WriteLine(SteamID64[ArrayCounter]);
                ArrayCounter++;
            }
            ArrayCounter = 0;
            for (int i = 4; i < linecount; i++) //accountname
            {
                accounts[ArrayCounter] = File.ReadLines(ReadPath).Skip(i).Take(1).First().Trim()
                                        .Replace("AccountName", "").Replace(RegexPattern, "").Trim();
                i += 7;
                Console.WriteLine(accounts[ArrayCounter]);
                ArrayCounter++;
            }
        }
        public static void urlMaker()
        {
            int g = 0;
            while (g < SteamID64.Length)
            {
                SteamAPIurls[g] = $"http://api.steampowered.com/ISteamUser/GetPlayerSummaries/v0002/?key={AppInfo.SteamAPIKey}&steamids={SteamID64[g]}";
                g++;
            }
        }
    }
}
