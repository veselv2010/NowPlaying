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

namespace NowPlaying
{
    /// <summary>
    /// Логика взаимодействия для ToggleSwitch.xaml
    /// </summary>
    public partial class ToggleSwitch : UserControl
    {
        Thickness LeftSide = new Thickness(-125, 0, 0, 0);
        Thickness RightSide = new Thickness(0, 0, -120, 0);
        SolidColorBrush Off = new SolidColorBrush(Color.FromRgb(155, 32, 32));
        SolidColorBrush On = new SolidColorBrush(Color.FromRgb(39, 185, 39));
        public static bool Toggled = false;


        public ToggleSwitch()
        {
            InitializeComponent();
            Background.Fill = Off;
            Toggled = false;
            Dot.Margin = LeftSide;
        }

        private void Dot_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!Toggled)
            {
                Background.Fill = On;
                Toggled = true;
                Dot.Margin = RightSide;
            }
            else
            {
                Background.Fill = Off;
                Toggled = false;
                Dot.Margin = LeftSide;
            }
        }

        private void Background_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!Toggled)
            {
                Background.Fill = On;
                Toggled = true;
                Dot.Margin = RightSide;
            }
            else
            {
                Background.Fill = Off;
                Toggled = false;
                Dot.Margin = LeftSide;
            }
        }
    }
}
