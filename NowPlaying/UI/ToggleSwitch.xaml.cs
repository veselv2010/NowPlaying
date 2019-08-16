using System;
using System.Windows;
using System.Windows.Shapes;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace NowPlaying.UI
{
    public partial class ToggleSwitch : UserControl
    {
        private Storyboard ActivesbFillAnimation;
        private Storyboard InActivesbFillAnimation;
        private readonly Thickness LeftSide = new Thickness(7, 7, 0, 0);
        private readonly Thickness RightSide = new Thickness(58, 7, 0, 0);
        private readonly SolidColorBrush Off = new SolidColorBrush(Color.FromRgb(255, 64, 64));
        private readonly SolidColorBrush On = new SolidColorBrush(Color.FromRgb(29, 185, 84));
        ThicknessAnimation AnimThickness = new ThicknessAnimation();
 
        public bool Toggled = false;

        public ToggleSwitch()
        {
            this.InitializeComponent();
            ActivesbFillAnimation = ActiveCreateFillAnimationStoryboard();
            InActivesbFillAnimation = InActiveCreateFillAnimationStoryboard();
            this.TurnOff();
        }

        public void TurnOff()
        {
            AnimThickness.From = RightSide;
            AnimThickness.To = LeftSide;
            AnimThickness.Duration = TimeSpan.FromSeconds(0.3);
            Dot.BeginAnimation(Ellipse.MarginProperty, AnimThickness);

            ActivesbFillAnimation.Begin();

            Program.TrayMenu.NpcWorkTrayCheckBox.Checked = false;
            this.SwitchBackground.Fill = this.Off;
            this.Toggled = false;
        }

        public void TurnOn()
        {
            AnimThickness.From = LeftSide;
            AnimThickness.To = RightSide;
            AnimThickness.Duration = TimeSpan.FromSeconds(0.3);
            Dot.BeginAnimation(Ellipse.MarginProperty, AnimThickness);

            InActivesbFillAnimation.Begin();

            Program.TrayMenu.NpcWorkTrayCheckBox.Checked = true;
            this.Toggled = true;
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

        private Storyboard ActiveCreateFillAnimationStoryboard()
        {
            Storyboard sb = new Storyboard() { Duration = TimeSpan.FromSeconds(0.5), BeginTime = TimeSpan.Zero };

            ColorAnimation colAnim = new ColorAnimation();
            colAnim.From = On.Color;
            colAnim.To = Off.Color;
            colAnim.Duration = new Duration(TimeSpan.FromSeconds(0.5));
            colAnim.AutoReverse = false;

            sb.Children.Add(colAnim);

            Storyboard.SetTarget(colAnim, SwitchBackground);
            Storyboard.SetTargetProperty(colAnim, new PropertyPath("Fill.Color"));

            return sb;
        }

        private Storyboard InActiveCreateFillAnimationStoryboard()
        {
            Storyboard sb = new Storyboard() { Duration = TimeSpan.FromSeconds(0.3), BeginTime = TimeSpan.Zero };

            ColorAnimation colAnim = new ColorAnimation();
            colAnim.From = Off.Color;
            colAnim.To = On.Color;
            colAnim.Duration = new Duration(TimeSpan.FromSeconds(0.3));
            colAnim.AutoReverse = false;

            sb.Children.Add(colAnim);

            Storyboard.SetTarget(colAnim, SwitchBackground);
            Storyboard.SetTargetProperty(colAnim, new PropertyPath("Fill.Color"));

            return sb;
        }
    }
}
