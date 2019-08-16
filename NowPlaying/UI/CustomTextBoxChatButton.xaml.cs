using System;
using System.Windows.Controls;

namespace NowPlaying.UI
{
    public partial class CustomTextBoxChatButton : UserControl
    {
        public static string CurrentText { get; private set; }
        public CustomTextBoxChatButton()
        {
            InitializeComponent();
        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e) => CurrentText = this.DefaultTextBox.Text;
    }
}
