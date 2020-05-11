using ReactiveUI;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using NowPlaying.Wpf.Controls.Common.Toggle;

namespace NowPlaying.Wpf.Controls.Common
{
    public partial class CustomCheckboxBase : ToggleBase
    {

    }

    public partial class CustomCheckbox : CustomCheckboxBase
    {
        static readonly SolidColorBrush ToggledOn = new SolidColorBrush(Color.FromArgb(0xFF, 0x1D, 0xB9, 0x54));
        static readonly SolidColorBrush ToggledOff = new SolidColorBrush(Color.FromArgb(0x00, 0x1D, 0xB9, 0x54));

        static readonly SolidColorBrush MouseEnterColor = new SolidColorBrush(Color.FromRgb(0x1D, 0xB9, 0x54));
        static readonly SolidColorBrush MouseLeaveColor = new SolidColorBrush(Color.FromRgb(0xB3, 0xB3, 0xB3));

        public CustomCheckbox()
        {
            ViewModel = new ToggleViewModel();
            InitializeComponent();

            var map = new Dictionary<bool, SolidColorBrush>
            {
                { false, ToggledOff },
                { true, ToggledOn },
            };

            this.WhenActivated(d =>
            {
                this.OneWayBind(ViewModel, vm => vm.IsToggled, view => view.CheckedRectangle.Fill, isToggled => map[isToggled])
                    .DisposeWith(d);
            });
        }

        private void StackPanel_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Toggle();
        }

        private void StackPanel_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            BorderRectangle.Stroke = MouseEnterColor;
        }

        private void StackPanel_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            BorderRectangle.Stroke = MouseLeaveColor;
        }
    }
}
