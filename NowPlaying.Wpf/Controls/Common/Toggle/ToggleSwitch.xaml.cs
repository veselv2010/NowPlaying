using ReactiveUI;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Windows;
using System.Windows.Media;

namespace NowPlaying.Wpf.Controls.Common.Toggle
{
    public partial class ToggleSwitchBase : ReactiveUserControl<ToggleSwitchViewModel>
    {
        // https://reactiveui.net/api/reactiveui/reactiveusercontrol_1/#Remarks
    }

    public partial class ToggleSwitch : ToggleSwitchBase
    {
        static readonly Thickness LeftSideThickness = new Thickness(-39, 0, 0, 0);
        static readonly Thickness RightSideThickness = new Thickness(0, 0, -39, 0);

        static readonly SolidColorBrush Off = new SolidColorBrush(Color.FromRgb(255, 64, 64));
        static readonly SolidColorBrush On = new SolidColorBrush(Color.FromRgb(29, 185, 84));

        public ToggleSwitch()
        {
            ViewModel = new ToggleSwitchViewModel();
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

        public bool IsToggled { get => ViewModel.IsToggled; set => Toggle(value); }

        public void Toggle(bool? newToggled = null)
        {
            ViewModel.IsToggled = newToggled ?? !ViewModel.IsToggled;

            System.Console.WriteLine(ViewModel.IsToggled);
        }

        private void Grid_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Toggle();
        }
    }
}
