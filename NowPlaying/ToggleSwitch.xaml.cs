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
    public partial class ToggleSwitch : UserControl
    {
        Thickness LeftSide = new Thickness(-125, 0, 0, 0);
        Thickness RightSide = new Thickness(0, 0, -120, 0);
        readonly SolidColorBrush Off = new SolidColorBrush(Color.FromRgb(155, 32, 32));
        readonly SolidColorBrush On = new SolidColorBrush(Color.FromRgb(39, 185, 39));
        public bool Toggled = false;

        public ToggleSwitch()
        {
            this.InitializeComponent();
            this.TurnOff();
        }

        public void TurnOff()
        {
            this.Background.Fill = this.Off;
            this.Toggled = false;
            this.Dot.Margin = this.LeftSide;
        }

        public void TurnOn()
        {
            this.Background.Fill = this.On;
            this.Toggled = true;
            this.Dot.Margin = this.RightSide;
        }

        private void Dot_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!this.Toggled)
            {
                this.TurnOn();
            }
            else
            {
                this.TurnOff();
            }
        }

        private void Background_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!this.Toggled)
            {
                this.TurnOn();
            }
            else
            {
                this.TurnOff();
            }
        }
    }
}
