using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using NowPlaying.Core.GameProcessHook;
using NowPlaying.Core.Api;
using NowPlaying.Core.Api.Spotify;
using NowPlaying.Core.Steam;
using NowPlaying.Core.Config;
using NowPlaying.Core.InputSender;
using System.Linq;

namespace NowPlaying.Cli
{
    class Program
    {
        private static GameProcess process;
        private static SpotifyRequestsManager requestsManager;
        private static ISteamService steamService;
        private static PathResolver pathResolver;
        private static ConfigWriter configWriter;
        private static IInputSender keySender;
        private static IKeyFormatter keyFormatter;
        private static ITrackInfoUpdater trackInfoUpdater;

        private static SteamContext steamContext;
        private static ushort currentKeyVirtual;
        private static string currentKey;

        static async Task Main()
        {
            AppDomain.CurrentDomain.ProcessExit += new EventHandler(onClose);

            steamService = OperatingSystem.IsWindows() ? new SteamServiceWindows() : new SteamServiceLinux();
            string redirectUrl = @"http://localhost:8888/";
            keySender = new InputSenderWindows();
            keyFormatter = new KeyFormatterWindows();
            pathResolver = new PathResolver();

            requestsManager = new SpotifyRequestsManager("7633771350404368ac3e05c9cf73d187",
                "29bd9ec2676c4bf593f3cc2858099838", redirectUrl);

            process = new GameProcess();
            process.Start();

            steamContext = steamService.GetSteamContext();
            var accounts = steamContext.GetAccounts();

            Console.WriteLine("Awaiting user authorization...");
            var server = new AuthServer(redirectUrl);

            string authUrl = requestsManager.GetAuthUrl().Replace("&", "^&");
            Process.Start(new ProcessStartInfo("cmd", $"/c start {authUrl}") { CreateNoWindow = true });
            string code = await server.GetAuthCode();

            await requestsManager.StartTokenRequests(code);

            int accSteamId3 = accounts.FirstOrDefault((x) => x.Name == steamContext.LastAccount).SteamId3;

            string writePath = pathResolver.GetWritePath(process.CurrentProcess, steamContext.UserdataPath, accSteamId3.ToString());

            configWriter = new ConfigWriter(writePath);

            Console.WriteLine("Press the bind key");
            var consoleInput = Console.ReadKey(true);
            currentKeyVirtual = (ushort)consoleInput.Key;
            currentKey = keyFormatter.GetSourceKey(currentKeyVirtual);

            trackInfoUpdater = new SpotifyTrackUpdater(requestsManager);
            trackInfoUpdater.OnPlaybackStateUpdate += onPlaybackStateUpdate;
            trackInfoUpdater.StartPlaybackUpdate();
        }

        private static string lastTrackId = string.Empty;

        private static void onClose(object sender, EventArgs args)
        {
            process?.Dispose();
            requestsManager?.Dispose();
            trackInfoUpdater?.Dispose();
        }

        private static void onPlaybackStateUpdate(IPlaybackResponse resp)
        {
            if (resp == null)
            {
                Console.Clear();
                Console.WriteLine("Nothing is playing!");
                return;
            }

            Console.Clear();
            Console.WriteLine($"{resp.FullName} ({resp.ProgressMinutes}:{resp.ProgressSeconds:00})");
            Console.WriteLine("Current account: " + steamContext.LastAccount);
            Console.WriteLine("Current key: " + currentKey);

            if (resp.Id != lastTrackId)
            {
                if (process.IsValid)
                {
                    keySender.SendSystemInput(currentKeyVirtual);
                }

                lastTrackId = resp.Id;
                configWriter.RewriteKeyBinding(resp);
            }
        }
    }
}
