using System;
using System.Diagnostics;
using System.Windows;

namespace NowPlaying
{
    class TrayMenuHelper
    {
        public static System.Windows.Forms.ContextMenu TrayMenu = new System.Windows.Forms.ContextMenu();
        public static System.Windows.Forms.NotifyIcon TrayMenuIcon = new System.Windows.Forms.NotifyIcon();

        public TrayMenuHelper()
        {
            TrayMenuIcon.Icon = new System.Drawing.Icon("mwvra5f4q0y_JnS_icon.ico");
            TrayMenuIcon.Visible = true;
            TrayMenuIcon.Text = "NowPlaying";

            TrayMenu.MenuItems.Add("GitHub", new EventHandler((_sender, _args) => OpenGitHubPage()));
            TrayMenuIcon.ContextMenu = TrayMenu;
        }

        private void OpenGitHubPage() => Process.Start("https://github.com/veselv2010/NowPlaying");
    }
}
