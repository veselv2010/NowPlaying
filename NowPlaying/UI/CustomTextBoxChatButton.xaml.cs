using System;
using System.Windows.Controls;
using System.Windows.Media;

namespace NowPlaying.UI
{
    public partial class CustomTextBoxChatButton : UserControl
    {
        public static string CurrentText { get; private set; }
        public CustomTextBoxChatButton()
        {
            InitializeComponent();

            DefaultTextBox.Background = new SolidColorBrush(Color.FromRgb(249, 249, 249)); //#F9F9F9
            DefaultTextBox.Foreground = new SolidColorBrush(Color.FromRgb(126, 126, 126)); //7e7e7e
        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e) => CurrentText = this.DefaultTextBox.Text;

        public void NightModeEnable()
        {
            DefaultTextBox.Background = new SolidColorBrush(Color.FromArgb(255, 44, 44, 44)); //#2C2C2C
            DefaultTextBox.Foreground = new SolidColorBrush(Color.FromArgb(255, 249, 249, 249)); //#F9F9F9
        }

        public void NightModeDisable()
        {
            DefaultTextBox.Background = new SolidColorBrush(Color.FromRgb(249, 249, 249)); //#F9F9F9
            DefaultTextBox.Foreground = new SolidColorBrush(Color.FromRgb(126, 126, 126)); //7e7e7e
        }
    }
}
