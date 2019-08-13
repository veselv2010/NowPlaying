using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace NowPlaying
{
    class TrayMenuHelper
    {
        public static ContextMenu TrayMenu = new ContextMenu();
        public static NotifyIcon TrayMenuIcon = new NotifyIcon();
        public static MenuItem NpcWorkTrayCheckBox = new MenuItem()
        {
            Name = "NpcWork",
            Checked = false,
            Text= "npc work"
        };
        public TrayMenuHelper()
        {
            TrayMenuIcon.Icon = new System.Drawing.Icon("icon.ico");
            TrayMenuIcon.Visible = true;
            TrayMenuIcon.Text = "NowPlaying";

            NpcWorkTrayCheckBox.Click += new EventHandler((_sender, _args) => NpcWork_Click());

            TrayMenu.MenuItems.Add(NpcWorkTrayCheckBox);
            TrayMenu.MenuItems.Add("GitHub", new EventHandler((_sender, _args) => OpenGitHubPage()));
            TrayMenuIcon.ContextMenu = TrayMenu;
        }

        private void NpcWork_Click() => NpcWorkTrayCheckBox.Checked = !NpcWorkTrayCheckBox.Checked;
        private void OpenGitHubPage() => Process.Start("https://github.com/veselv2010/NowPlaying");

    }
}
