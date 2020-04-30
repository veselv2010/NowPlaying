using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace NowPlaying.UI.UserControls
{
    public partial class MinimizeWindowButton : UserControl
    {
        public MinimizeWindowButton()
        {
            InitializeComponent();
        }

        private void Rectangle_MouseEnter(object sender, MouseEventArgs e)
        {
            Rectangle1.Fill = new SolidColorBrush(Color.FromRgb(29, 185, 84));
        }

        private void Rectangle_MouseLeave(object sender, MouseEventArgs e)
        {
            Rectangle1.Fill = new SolidColorBrush(Color.FromRgb(126, 126, 126));
        }
    }
}
