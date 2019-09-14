using System.Windows;
using System.Windows.Controls;


namespace NowPlaying.UI.UserControls
{
    public partial class CustomCheckBox : UserControl
    {
        public bool IsChecked { get; private set; }
        public CustomCheckBox()
        {
            InitializeComponent();
        }
        private void DefaultCheckBox_Checked(object sender, RoutedEventArgs e) => IsChecked = true;
        private void DefaultCheckBox_Unchecked(object sender, RoutedEventArgs e) => IsChecked = false;
    }
}
