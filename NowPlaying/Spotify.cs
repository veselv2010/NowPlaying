using System;
using System.Diagnostics;
using System.Linq;

namespace NowPlaying
{
    public class Spotify
    {
        public static string CurrentTrack()
        {
            var proc = Process.GetProcessesByName("Spotify")
                              .FirstOrDefault(p => !string.IsNullOrWhiteSpace(p.MainWindowTitle));

            if (proc == null)
                return "Spotify is not running!";

            if (string.Equals(proc.MainWindowTitle, "Spotify", StringComparison.InvariantCultureIgnoreCase))
                return "Paused";

            return proc.MainWindowTitle;
        }
    }
}

//=_-icon by SCOUTPAN-_=