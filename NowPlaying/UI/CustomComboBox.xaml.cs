using System;
using System.Windows.Controls;

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

            this.DefaultComboBox.SelectedItem = SteamIdLooker.SteamLastLoggedOnAccount;
        }

        private void DefaultComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedItem = this.DefaultComboBox.SelectedItem.ToString();
            SelectedIndex = this.DefaultComboBox.SelectedIndex;
        }
    }
}
