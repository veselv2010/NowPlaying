using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace NowPlaying.UI
{
    public class TrayMenu : IDisposable
    {
        public NotifyIcon Icon { get; } =
            new NotifyIcon()
            {
                Icon = new Icon("Resources/icon.ico"),
                Text = "NowPlaying",
            };

        public MenuItem NpcWorkTrayCheckBox { get; } =
            new MenuItem()
            {
                Name = "NpcWork",
                Text = "npc work",
                Checked = false,
            };

        public MenuItem TopMostCheckBox { get; } =
            new MenuItem()
            {
                Name = "TopMost",
                Text = "on top of other windows",
                Checked = false,
            };

        public ContextMenu Menu => Icon.ContextMenu;

        public Menu.MenuItemCollection Items => Menu.MenuItems;

        public TrayMenu()
        {
            Icon.ContextMenu = new ContextMenu();

            TopMostCheckBox.Click += CreateEventHandler(TopMost_Click);
            NpcWorkTrayCheckBox.Click += CreateEventHandler(NpcWork_Click);

            Items.AddRange(new MenuItem[] {
                TopMostCheckBox,
                NpcWorkTrayCheckBox,
                new MenuItem("GitHub", CreateEventHandler(OpenGitHubPage)),
            });
        }

        private void NpcWork_Click() => NpcWorkTrayCheckBox.Checked = !NpcWorkTrayCheckBox.Checked;
        private void TopMost_Click() => TopMostCheckBox.Checked = !TopMostCheckBox.Checked;
        private void OpenGitHubPage() => Process.Start("https://github.com/veselv2010/NowPlaying");

        public static EventHandler CreateEventHandler(Action func)
        {
            return new EventHandler((_sender, _args) => func());
        }

        public void Show()
        {
            Icon.Visible = true;
        }

        public void Hide()
        {
            Icon.Visible = false;
        }

        public void Dispose()
        {
            Menu.Dispose();
            Icon.Dispose();
            NpcWorkTrayCheckBox.Dispose();
            TopMostCheckBox.Dispose();
        }
    }
}
