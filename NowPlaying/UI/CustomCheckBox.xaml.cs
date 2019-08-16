using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NowPlaying.UI
{
    public partial class CustomCheckBox : UserControl
    {
        public static bool IsChecked { get; private set; }
        public CustomCheckBox()
        {
            InitializeComponent();
        }

        private void DefaultCheckBox_Checked(object sender, RoutedEventArgs e) => IsChecked = true;
        private void DefaultCheckBox_Unchecked(object sender, RoutedEventArgs e) => IsChecked = false;
    }
}
