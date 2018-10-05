using System.Windows;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Threading;
using System;

namespace NowPlaying
{
    public static class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            SteamIdLooker.SteamCfgPath("steam");
            SteamIdLooker.SteamCfgReader();

            new Application().Run(new MainWindow());
            /*

            if (!ProcessChecker.IsCSGORunning())
            {
                Console.WriteLine("process status: no csgo no work");
                Console.ReadKey();
                return;
            }

            ConsoleExtensions.WelcomeMessage();
            while (ProcessChecker.IsCSGORunning())
            {
                string currentTrack = Spotify.GetCurrentTrack();
                string currentTrackFormatted = TrackNameFormatter.FormatForWriting(currentTrack);
                if (currentTrackFormatted != null)
                {
                    ConfigWriter.Write(currentTrackFormatted);
                    Console.WriteLine("original: " + currentTrack);
                    Console.WriteLine("formatted: " + "[Spotify] Now Playing: " + currentTrackFormatted);
                }
                Thread.Sleep(1000);
                Console.Clear();
            }

            */
        }
        private static Action EmptyDelegate = delegate () { };

        public static void Refresh(this UIElement uiElement)
        {
            uiElement.Dispatcher.Invoke(DispatcherPriority.Render, EmptyDelegate);
        }
    }
}