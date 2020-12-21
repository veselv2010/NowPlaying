using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Media;

namespace NowPlaying.Wpf.Controls.Header
{
    public partial class HeaderBlock : UserControl
    {
        public HeaderBlock()
        {
            InitializeComponent();

            var toggleColors = new Dictionary<bool, SolidColorBrush>
            {
                { false, ColorsConstants.MilkyGray },
                { true, ColorsConstants.DarkGray },
            };
        }

        private void CloseButton_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void HelpTextBlock_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Process.Start("https://github.com/veselv2010/NowPlaying");
        }
    }
}
