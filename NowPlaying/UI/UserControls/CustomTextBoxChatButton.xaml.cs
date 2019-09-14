using System;
using System.Windows.Controls;
using System.Windows.Media;

namespace NowPlaying.UI.UserControls
{
    public partial class CustomTextBoxChatButton : UserControl
    {
        public string CurrentText { get; private set; }

        private static SolidColorBrush MilkyBrush { get; } = new SolidColorBrush(Color.FromRgb(0xF9, 0xF9, 0xF9));

        private static SolidColorBrush GrayBrush { get; } = new SolidColorBrush(Color.FromRgb(0x7e, 0x7e, 0x7e));

        private static SolidColorBrush DarkBrush { get; } = new SolidColorBrush(Color.FromRgb(0x17, 0x17, 0x17));

        public CustomTextBoxChatButton()
        {
            InitializeComponent();

            DefaultTextBox.Background = MilkyBrush;
            DefaultTextBox.Foreground = GrayBrush;
        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e) => CurrentText = this.DefaultTextBox.Text;

        public void NightModeEnable()
        {
            DefaultTextBox.Background = DarkBrush;
            DefaultTextBox.Foreground = MilkyBrush;
        }

        public void NightModeDisable()
        {
            DefaultTextBox.Background = MilkyBrush;
            DefaultTextBox.Foreground = GrayBrush;
        }
    }
}
