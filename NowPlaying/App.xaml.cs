using System;
using System.Windows;
using NowPlaying.GameProcessHook;
using NowPlaying.Models;
using NowPlaying.UI.Windows;
using NowPlaying.UI;
using NowPlaying.Views;

namespace NowPlaying
{
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            InitializeGameProcess();
        }

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            Dispose();
        }

        [STAThread]
        public static void Main(string[] args)
        {
            App app = new App();

            app.InitializeComponent();
            app.Run();

            var steamWorker = new SteamIdLooker();
            var steamAccounts = steamWorker.GetSteamAccounts();

            var browser = new AuthControlView();

        }

        public static TrayMenu TrayMenu { get; } = new TrayMenu();

        public static GameProcess GameProcess { get; private set; }

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
