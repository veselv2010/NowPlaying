using System.Diagnostics;
using System.Linq;

public class ProcessChecker
{
    public static bool IsCSGORunning()
    {
        Process CSGOprocess = Process.GetProcessesByName("csgo")
                                        .FirstOrDefault(p => !string.IsNullOrWhiteSpace(p.MainWindowTitle));
        return (CSGOprocess != null);

    }

    public static bool IsSpotifyRunning()
    {
        Process spotifyProcess = Process.GetProcessesByName("Spotify")
                              .FirstOrDefault(p => !string.IsNullOrWhiteSpace(p.MainWindowTitle));

        return (spotifyProcess != null);
    }
}