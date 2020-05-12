using ReactiveUI;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Windows;
using System.Windows.Media;
using NowPlaying.Wpf.Controls.Common.Toggle;

namespace NowPlaying.Wpf.Controls.Common
{
    public partial class ToggleSwitchBase : ToggleBase
    {
        // https://reactiveui.net/api/reactiveui/reactiveusercontrol_1/#Remarks
    }

    public partial class ToggleSwitch : ToggleSwitchBase
    {
        private readonly Thickness LeftSideThickness = new Thickness(-39, 0, 0, 0);
        private readonly Thickness RightSideThickness = new Thickness(0, 0, -39, 0);

        public ToggleSwitch()
        {
            ViewModel = new ToggleViewModel();
            InitializeComponent();

            var map = new Dictionary<bool, Thickness>
            {
                { false, LeftSideThickness },
                { true, RightSideThickness },
            };

            this.WhenActivated(d =>
            {
                this.OneWayBind(ViewModel, vm => vm.IsToggled, view => view.Dot.Margin, isToggled => map[isToggled])
                    .DisposeWith(d);
            });
        }

        private void Grid_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Toggle();
        }
    }
}
