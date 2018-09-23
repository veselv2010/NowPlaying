using System;
using System.Diagnostics;
using System.Linq;

namespace NowPlaying
{
    public class SpotifyProcessWorker
    {
        public static string GetCurrentTrack()
        {
            Process spotifyProcess = Process.GetProcessesByName("Spotify")
                              .FirstOrDefault(p => !string.IsNullOrWhiteSpace(p.MainWindowTitle));

            if (spotifyProcess == null)
                return "Spotify is not running!";

            if (string.Equals(spotifyProcess.MainWindowTitle, "Spotify", StringComparison.InvariantCultureIgnoreCase))
                return "Paused";

            return spotifyProcess.MainWindowTitle;
        }
    }
}