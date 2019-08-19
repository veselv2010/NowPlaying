using System;
using System.Windows;
using System.Windows.Shapes;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace NowPlaying.UI.Controls
{
    public partial class ToggleSwitchNightMode : UserControl
    {
        private readonly Storyboard ActivesbFillAnimation;
        private readonly Storyboard InActivesbFillAnimation;
        private readonly Thickness LeftSide = new Thickness(7, 7, 0, 0);
        private readonly Thickness RightSide = new Thickness(58, 7, 0, 0);

        private readonly Color SpotifyLightGray = Color.FromRgb(217, 217, 217);
        private readonly Color SpotifyDarkGray = Color.FromRgb(102, 102, 102);

        private readonly SolidColorBrush SpotifyGreenBrush = new SolidColorBrush(Color.FromRgb(29, 185, 84));
        private readonly SolidColorBrush SpotifyGrayNightBrush = new SolidColorBrush(Color.FromRgb(126, 126, 126));
        ThicknessAnimation AnimThickness = new ThicknessAnimation();

        public bool IsNightModeToggled = false;

        public ToggleSwitchNightMode()
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

            this.LabelDayMode.Foreground = SpotifyGreenBrush;
            this.LabelNightMode.Foreground = SpotifyGrayNightBrush;
            Dot.Fill = new SolidColorBrush(Color.FromRgb(249, 249, 249));
            this.IsNightModeToggled = false;
        }

        public void TurnOn()
        {
            AnimThickness.From = LeftSide;
            AnimThickness.To = RightSide;
            AnimThickness.Duration = TimeSpan.FromSeconds(0.3);
            Dot.BeginAnimation(Ellipse.MarginProperty, AnimThickness);

            InActivesbFillAnimation.Begin();

            this.LabelDayMode.Foreground = SpotifyGrayNightBrush;
            this.LabelNightMode.Foreground = SpotifyGreenBrush;
            Dot.Fill = new SolidColorBrush(Color.FromRgb(44, 44, 44));
            this.IsNightModeToggled = true;
        }

        public void Toggle()
        {
            if (!this.IsNightModeToggled)
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

        private Storyboard ActiveCreateFillAnimationStoryboard()
        {
            var sb = new Storyboard()
            {
                Duration = TimeSpan.FromSeconds(0.5),
                BeginTime = TimeSpan.Zero
            };

            var colAnim = new ColorAnimation
            {
                From = SpotifyDarkGray,
                To = SpotifyLightGray,
                Duration = new Duration(TimeSpan.FromSeconds(0.5)),
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
                From = SpotifyLightGray,
                To = SpotifyDarkGray,
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
