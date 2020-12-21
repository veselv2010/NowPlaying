using NowPlaying.Core.InputSender;
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

        private void CurrentKeyControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var window = Window.GetWindow(this);
            window.KeyDown += KeyboardKeyDown;
            CurrentKeyControl.CurrentKey = "press bind button";
        }

        private void KeyboardKeyDown(object sender, KeyEventArgs e)
        {
            CurrentKeyControl.MouseLeftButtonDown -= CurrentKeyControl_MouseLeftButtonDown;

            ushort virtualKey = (ushort)KeyInterop.VirtualKeyFromKey(e.Key);
            string sourceKey = keyFormatter.GetSourceKey(virtualKey);

            CurrentKeyControl.CurrentKey = sourceKey;
            CurrentVirtualKey = virtualKey;

            Window.GetWindow(this).KeyDown -= KeyboardKeyDown;
            CurrentKeyControl.MouseLeftButtonDown += CurrentKeyControl_MouseLeftButtonDown;
        }
    }
}
