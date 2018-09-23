using System.Diagnostics;
using System.Linq;

public class ProcessChecker
{
    public static bool IsCSGORunning()
    {
        var CSGOprocess = Process.GetProcessesByName("csgo")
                                        .FirstOrDefault(p => !string.IsNullOrWhiteSpace(p.MainWindowTitle));
        return (CSGOprocess != null);

    }
}


//=_-icon by SCOUTPAN-_=