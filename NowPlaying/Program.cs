using System.Windows;
using System;
using NowPlaying.GameProcessHook;
using NowPlaying.UI.Windows;
using NowPlaying.UI;

namespace NowPlaying
{
    public class Program : Application
    {
        [STAThread]
        public static void Main()
        {
            SteamIdLooker.UpdateAccountsInfo();
            InitializeGameProcess();
            new Application().Run(new MainWindow());
        }

        public static TrayMenu TrayMenu { get; } = new TrayMenu();

        public static GameProcess GameProcess { get; private set; }

        public Program()
        {
            Startup += (sender, args) => InitializeGameProcess();
            Exit += (sender, args) => Dispose();
        }

        public static void InitializeGameProcess()
        {
            GameProcess = new GameProcess();
            GameProcess.Start();
        }

        public static void Dispose()
        {
            GameProcess.Dispose();
            TrayMenu.Dispose();
        }
    }
}