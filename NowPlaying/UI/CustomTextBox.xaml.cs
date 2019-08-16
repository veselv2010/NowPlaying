using System;
using System.Windows.Controls;

namespace NowPlaying.UI
{
    public partial class CustomTextBox : UserControl
    {
        public static string CurrentText { get; private set; }
        public CustomTextBox()
        {
            InitializeComponent();
        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e) => CurrentText = this.DefaultTextBox.Text; 
    }
}
