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
    public partial class CustomComboBox : UserControl
    {
        public static string SelectedItem { get; private set; }
        public static int SelectedIndex { get; private set; }

        public CustomComboBox()
        {
            InitializeComponent();

            foreach (string a in AppInfo.State.AccountNames)
            {
                this.DefaultComboBox.Items.Add(a);
            }

            this.DefaultComboBox.SelectedIndex = 0;
        }

        private void DefaultComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedItem = this.DefaultComboBox.SelectedItem.ToString();
            SelectedIndex = this.DefaultComboBox.SelectedIndex;
        }
    }
}
