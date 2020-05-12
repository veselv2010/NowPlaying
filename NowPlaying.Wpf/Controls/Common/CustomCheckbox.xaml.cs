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
        public CustomCheckbox()
        {
            ViewModel = new ToggleViewModel();
            InitializeComponent();

            var map = new Dictionary<bool, SolidColorBrush>
            {
                { false, ColorsConstants.Transparent },
                { true, ColorsConstants.SpotifyGreen },
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
            BorderRectangle.Stroke = ColorsConstants.SpotifyGreen;
        }

        private void StackPanel_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            BorderRectangle.Stroke = ColorsConstants.MilkyGrayBorder;
        }
    }
}
