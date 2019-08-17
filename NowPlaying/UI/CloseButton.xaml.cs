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
    /// <summary>
    /// Логика взаимодействия для CloseButton.xaml
    /// </summary>
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
