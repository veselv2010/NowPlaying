using System.Windows;
using System;
using NowPlaying.GameProcessHook;

namespace NowPlaying
{
    public class Program : Application, IDisposable
    {
        [STAThread]
        public static void Main(string[] args)
        {
            SteamIdLooker.UpdateAccountsInfo();
            Ctor();
            new Application().Run(new MainWindow());
        }

        public static GameProcess GameProcess { get; set; }

        public Program()
        {
            Startup += (sender, args) => Ctor();
            Exit += (sender, args) => Dispose();
        }

        public static void Ctor() //constructor
        {
            GameProcess = new GameProcess();
            GameProcess.Start();
        }

        public void Dispose()
        {
            GameProcess.Dispose();
            GameProcess = default;
        }
    }
}