using System;
using System.Diagnostics;
using System.Threading.Tasks;
using NowPlaying.Core.GameProcessHook;
using NowPlaying.Core.Api;
using NowPlaying.Core.Api.Spotify;
using NowPlaying.Core.Steam;
using NowPlaying.Core.Config;
using NowPlaying.Core.InputSender;
using System.Linq;
using NowPlaying.Core.Settings;
using NowPlaying.Core.Api.WindowsManager;

namespace NowPlaying.Cli
{
    class Program
    {
        private static GameProcess process;
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
            var appConfigWorker = new UserSettingsWorker();
            var appConfig = appConfigWorker.ReadConfigFile();

            steamService = OperatingSystem.IsWindows() ? new SteamServiceWindows() : new SteamServiceLinux();
            keySender = new InputSenderWindows();
            keyFormatter = new KeyFormatterWindows();
            pathResolver = new PathResolver();

            Console.WriteLine("Awaiting user authorization...");

            trackInfoUpdater = await new PlaybackStateProviderResolver()
                .ResolveTrackInfoUpdater(appConfig.LastProvider);

            process = new GameProcess();
            process.Start();

            steamContext = steamService.GetSteamContext();
            var accounts = steamContext.GetAccounts();
            int accSteamId3 = accounts.FirstOrDefault((x) => x.Name == steamContext.LastAccount).SteamId3;

            string writePath = pathResolver
                .GetWritePath(process.CurrentProcess, steamContext.UserdataPath, accSteamId3.ToString());

            configWriter = new ConfigWriter(writePath);

            Console.WriteLine("Press the bind key");
            var consoleInput = Console.ReadKey(true);
            currentKeyVirtual = (ushort)consoleInput.Key;
            currentKey = keyFormatter.GetSourceKey(currentKeyVirtual);


            trackInfoUpdater.OnPlaybackStateUpdate += onPlaybackStateUpdate;
            trackInfoUpdater.StartPlaybackUpdate();
        }

        private static string lastTrackId = string.Empty;

        private static void onClose(object sender, EventArgs args)
        {
            process?.Dispose();
            trackInfoUpdater?.Dispose();
        }

        private static void onPlaybackStateUpdate(IPlaybackResponse resp)
        {
            Console.SetCursorPosition(0, 0);

            if (resp == null)
            {
                Console.WriteLine("Nothing is playing!");
                return;
            }

            var consoleMessage = getMessage(resp);
            Console.WriteLine(consoleMessage);

            // Из-за WindowsMediaManager больше нет возможности проверять айди треков
            if (lastTrackId != resp.FullName)
            {
                if (process.IsValid)
                {
                    keySender.SendSystemInput(currentKeyVirtual);
                }

                lastTrackId = resp.FullName;
                configWriter.RewriteKeyBinding(resp);
            }
        }

        private static string getMessage(IPlaybackResponse response)
        {
            return $"{response.FullName} ({response.ProgressMinutes}:{response.ProgressSeconds:00})\n" +
                   $"Current account: {steamContext.LastAccount}\n" +
                   $"Current key: {currentKey}";
        }
    }
}
