using System;
using System.Windows.Controls;
using System.Windows.Media;

namespace NowPlaying.UI.Controls
{
    public partial class CustomTextBoxChatButton : UserControl
    {
        public static string CurrentText { get; private set; }

        private static SolidColorBrush MilkyBrush { get; } = new SolidColorBrush(Color.FromRgb(0xF9, 0xF9, 0xF9));
        
        private static SolidColorBrush GrayBrush { get; } = new SolidColorBrush(Color.FromRgb(0x7e, 0x7e, 0x7e));

        private static SolidColorBrush DarkBrush { get; } = new SolidColorBrush(Color.FromRgb(0x2C, 0x2C, 0x2C));


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
