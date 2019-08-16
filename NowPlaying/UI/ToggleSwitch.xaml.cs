using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace NowPlaying.UI
{
    public partial class ToggleSwitch : UserControl
    {
        Thickness LeftSide = new Thickness(7, 7, 0, 0);
        Thickness RightSide = new Thickness(58, 7, 0, 0);
        readonly SolidColorBrush Off = new SolidColorBrush(Color.FromRgb(255, 64, 64));
        readonly SolidColorBrush On = new SolidColorBrush(Color.FromRgb(29, 185, 84));
        public bool Toggled = false;

        public ToggleSwitch()
        {
            this.InitializeComponent();
            this.TurnOff();
        }

        public void TurnOff()
        {
            TrayMenuHelper.NpcWorkTrayCheckBox.Checked = false;
            this.SwitchBackground.Fill = this.Off;
            this.Toggled = false;
            this.Dot.Margin = this.LeftSide;
        }

        public void TurnOn()
        {
            TrayMenuHelper.NpcWorkTrayCheckBox.Checked = true;
            this.SwitchBackground.Fill = this.On;
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
