using System;
using System.Linq;
using System.Diagnostics;
using System.Collections.Generic;
using System.Threading;
using NowPlaying.Core.GameProcessHook;
using NowPlaying.Core.Api;
using NowPlaying.Core.SteamService;
using NowPlaying.Core.Config;
using NowPlaying.Core.NowPlayingConfig;
using NowPlaying.Core.InputSender;

namespace NowPlaying.CLI
{
    class Program
    {
        private static GameProcess process;
        private static SpotifyRequestsManager requestsManager;
        private static SteamServiceWindows steamService;
        private static ConfigWorker config;
        private static PathResolver pathResolver;
        private static ConfigWriter configWriter;
        private static KeySender keySender;

        private static SteamInfo steamInfo;
        private static IDictionary<string, int> accounts;
        static void Main()
        {
            process = new GameProcess();
            process.Start();

            steamService = new SteamServiceWindows();
            steamInfo = steamService.GetSteamInfo();
            accounts = steamService.GetSteamAccounts(steamInfo);
            
            requestsManager = new SpotifyRequestsManager("7633771350404368ac3e05c9cf73d187",
                "29bd9ec2676c4bf593f3cc2858099838", @"https://www.google.com/");

            string authUrl = requestsManager.GetAuthUrl();
            authUrl = authUrl.Replace("&", "^&");
            Process.Start(new ProcessStartInfo("cmd", $"/c start {authUrl}") { CreateNoWindow = true });

            Console.Write("code = ");
            string code = Console.ReadLine();

            requestsManager.StartTokenRequests(code);

            keySender = new KeySender();
            pathResolver = new PathResolver();

            int accSteamId3 = accounts[steamInfo.LastAccount];

            string writePath = pathResolver.GetWritePath(process.CurrentProcess, steamInfo, accSteamId3.ToString());
            configWriter = new ConfigWriter(writePath);

            string lastTrackCached = string.Empty;
            while (true)
            {
                var resp = requestsManager.GetCurrentTrack();

                if (resp.FullName != lastTrackCached)
                {
                    Console.Clear();
                    Console.WriteLine(resp.FormattedArtists + " - " + resp.FullName);
                    Console.WriteLine("Current account: " + steamInfo.LastAccount);
                    if (process.IsValid)
                    {
                        keySender.SendInputWithAPI("kp_5");
                    }

                    lastTrackCached = resp.FullName;
                    configWriter.RewriteKeyBinding(resp);
                }
                Thread.Sleep(1000);
            }          
        }
    }
}
