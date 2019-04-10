using System.Windows;
using System;
using CefSharp;
using CefSharp.Wpf;

namespace NowPlaying
{
    public static class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            SteamIdLooker.UpdateSteamConfigPaths();
            SteamIdLooker.UpdateAccountsInfo();
            new Application().Run(new MainWindow());
        }
    }
}