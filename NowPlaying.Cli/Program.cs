using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using NowPlaying.Core.GameProcessHook;
using NowPlaying.Core.Api;
using NowPlaying.Core.Steam;
using NowPlaying.Core.Config;
using NowPlaying.Core.NowPlayingConfig;
using NowPlaying.Core.InputSender;

namespace NowPlaying.Cli
{
    class Program
    {
        private static GameProcess process;
        private static SpotifyRequestsManager requestsManager;
        private static ISteamService steamService;
        private static ConfigWorker config;
        private static PathResolver pathResolver;
        private static ConfigWriter configWriter;
        private static KeySender keySender;

        static async Task Main()
        {
            process = new GameProcess();
            process.Start();

            steamService = new SteamServiceWindows();
            var steamInfo = steamService.GetSteamInfo();

            var loginUsersReader = new LoginUsersReader(steamInfo.LoginUsersPath);
            var accounts = loginUsersReader.Read();

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

            string lastTrackId = null;
            string currentKey = "kp_5";

            while (true)
            {
                var resp = await requestsManager.GetCurrentTrack();

                if (resp != null)
                {
                    Console.Clear();
                    Console.WriteLine($"{resp.FullName} ({resp.ProgressMinutes}:{resp.ProgressSeconds})");
                    Console.WriteLine("Current account: " + steamInfo.LastAccount);
                    Console.WriteLine("Current key: " + currentKey);

                    if (resp.Id != lastTrackId)
                    {
                        if (process.IsValid)
                        {
                            keySender.SendInputWithAPI(currentKey);
                        }

                        lastTrackId = resp.Id;
                        configWriter.RewriteKeyBinding(resp);
                    }
                }

                Thread.Sleep(1000);
            }
        }
    }
}
