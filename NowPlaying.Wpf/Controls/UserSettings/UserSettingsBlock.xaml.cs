using NowPlaying.Core.InputSender;
using NowPlaying.Wpf.Models;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace NowPlaying.Wpf.Controls.UserSettings
{
    public partial class UserSettingsBlock : UserControl
    {
        private readonly IKeyFormatter keyFormatter;
        public ushort CurrentVirtualKey { get; set; }

        public UserSettingsBlock()
        {
            InitializeComponent();
            keyFormatter = new KeyFormatterWindows();
        }

        public void Update(string accountName, string gameName)
        {
            var model = (UserSettingsModel)Resources["userSettingsModel"];
            model.UpdateProperties(accountName, gameName);
        }

        private void CurrentKeyControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var window = Window.GetWindow(this);
            window.KeyDown += KeyboardKeyDown;
            CurrentKeyControl.Update("press bind button");
        }

        private void KeyboardKeyDown(object sender, KeyEventArgs e)
        {
            CurrentKeyControl.MouseLeftButtonDown -= CurrentKeyControl_MouseLeftButtonDown;

            ushort virtualKey = (ushort)KeyInterop.VirtualKeyFromKey(e.Key);
            string sourceKey = keyFormatter.GetSourceKey(virtualKey);

            CurrentVirtualKey = virtualKey;
            UpdateKey(sourceKey);

            Window.GetWindow(this).KeyDown -= KeyboardKeyDown;
            CurrentKeyControl.MouseLeftButtonDown += CurrentKeyControl_MouseLeftButtonDown;
        }

        public void UpdateKey(string sourceKey)
        {
            if(CurrentVirtualKey == default)
            {
                CurrentVirtualKey = keyFormatter.GetVirtualKey(sourceKey);
            }

            CurrentKeyControl.Update(sourceKey);
            ConsolePaste.Text = $"bind \"{sourceKey}\" \"exec audio.cfg\"";
        }

        private void AutosendCheck_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            AutosendCheck.Toggle();
        }
    }
}
