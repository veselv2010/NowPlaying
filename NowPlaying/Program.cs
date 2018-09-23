using System;
using System.Threading;

namespace NowPlaying
{
    public class MainProgram
    {
        public static void Main(string[] args)
        {
            Console.Title = "NowPlaying";

            if (!ProcessChecker.IsCSGORunning())
            {
                Console.WriteLine("process status: no csgo no work");
                Console.ReadKey();
                return;
            }

            ConsoleExtensions.WelcomeMessage();
            while (ProcessChecker.IsCSGORunning())
            {
                string currentTrack = Spotify.CurrentTrack();
                string currentTrackFormatted = TrackNameFormatter.FormatForWriting(currentTrack);
                ConfigWriter.Write();
                Console.WriteLine("original: " + currentTrack);
                Console.WriteLine("formatted: " + "[Spotify] Now Playing: " + currentTrackFormatted);
                Thread.Sleep(1000);
                Console.Clear();
            }
        }
    }
}

//=_-icon by SCOUTPAN-_=