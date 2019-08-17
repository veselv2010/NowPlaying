using System;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;


namespace NowPlaying.UI
{
    public partial class CloseButton : UserControl
    {
        public CloseButton()
        {
            InitializeComponent();
        }

        private void Rectangle_MouseEnter(object sender, MouseEventArgs e)
        {
            Rectangle1.Fill = new SolidColorBrush(Color.FromRgb(255, 64, 64));
        }

        private void Rectangle_MouseLeave(object sender, MouseEventArgs e)
        {
            Rectangle1.Fill = new SolidColorBrush(Color.FromRgb(126, 126, 126));
        }
    }
}
