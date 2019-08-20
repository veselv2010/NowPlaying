using System;
using System.Windows;
using System.Windows.Shapes;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace NowPlaying.UI.UserControls
{
    public partial class ToggleSwitch : UserControl
    {
        private readonly Storyboard ActivesbFillAnimation;
        private readonly Storyboard InActivesbFillAnimation;
        private readonly Thickness LeftSide = new Thickness(7, 7, 0, 0);
        private readonly Thickness RightSide = new Thickness(58, 7, 0, 0);
        private readonly SolidColorBrush Off = new SolidColorBrush(Color.FromRgb(255, 64, 64));
        private readonly SolidColorBrush On = new SolidColorBrush(Color.FromRgb(29, 185, 84));
        private readonly ThicknessAnimation AnimThickness = new ThicknessAnimation();

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
            AnimThickness.Duration = TimeSpan.FromSeconds(0.2);
            Dot.BeginAnimation(Ellipse.MarginProperty, AnimThickness);

            ActivesbFillAnimation.Begin();

            Program.TrayMenu.NpcWorkTrayCheckBox.Checked = false;
            this.Toggled = false;
        }

        public void TurnOn()
        {
            AnimThickness.From = LeftSide;
            AnimThickness.To = RightSide;
            AnimThickness.Duration = TimeSpan.FromSeconds(0.2);
            Dot.BeginAnimation(Ellipse.MarginProperty, AnimThickness);

            InActivesbFillAnimation.Begin();

            Program.TrayMenu.NpcWorkTrayCheckBox.Checked = true;
            this.Toggled = true;
        }

        public void Toggle()
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

        private void Dot_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Toggle();
        }

        private void Background_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Toggle();
        }

        public void NightModeEnable()
        {
            Dot.Fill = new SolidColorBrush(Color.FromRgb(0x17, 0x17, 0x17));
        }

        public void NightModeDisable()
        {
            Dot.Fill = new SolidColorBrush(Color.FromRgb(249, 249, 249));
        }

        private Storyboard ActiveCreateFillAnimationStoryboard()
        {
            var sb = new Storyboard()
            {
                Duration = TimeSpan.FromSeconds(0.3),
                BeginTime = TimeSpan.Zero
            };

            var colAnim = new ColorAnimation
            {
                From = On.Color,
                To = Off.Color,
                Duration = new Duration(TimeSpan.FromSeconds(0.3)),
                AutoReverse = false
            };

            sb.Children.Add(colAnim);

            Storyboard.SetTarget(colAnim, SwitchBackground);
            Storyboard.SetTargetProperty(colAnim, new PropertyPath("Fill.Color"));

            return sb;
        }

        private Storyboard InActiveCreateFillAnimationStoryboard()
        {
            var sb = new Storyboard()
            {
                Duration = TimeSpan.FromSeconds(0.3),
                BeginTime = TimeSpan.Zero
            };

            var colAnim = new ColorAnimation
            {
                From = Off.Color,
                To = On.Color,
                Duration = new Duration(TimeSpan.FromSeconds(0.3)),
                AutoReverse = false
            };

            sb.Children.Add(colAnim);

            Storyboard.SetTarget(colAnim, SwitchBackground);
            Storyboard.SetTargetProperty(colAnim, new PropertyPath("Fill.Color"));

            return sb;
        }
    }
}
