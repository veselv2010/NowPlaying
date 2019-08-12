using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace NowPlaying
{
    class TrayMenuHelper
    {
        public static ContextMenu TrayMenu = new ContextMenu();
        public static NotifyIcon TrayMenuIcon = new NotifyIcon();

        public TrayMenuHelper()
        {
            TrayMenuIcon.Icon = new System.Drawing.Icon("icon.ico");
            TrayMenuIcon.Visible = true;
            TrayMenuIcon.Text = "NowPlaying";

            TrayMenu.MenuItems.Add("GitHub", new EventHandler((_sender, _args) => OpenGitHubPage()));
            TrayMenuIcon.ContextMenu = TrayMenu;
        }

        private void OpenGitHubPage() => Process.Start("https://github.com/veselv2010/NowPlaying");
    }
}
