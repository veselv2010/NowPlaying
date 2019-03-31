using System.Windows;
using System;

namespace NowPlaying
{
    public static class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            SteamIdLooker.SteamCfgPath();
            SteamIdLooker.SteamCfgReader();

            new Application().Run(new MainWindow());
        }
    }
}