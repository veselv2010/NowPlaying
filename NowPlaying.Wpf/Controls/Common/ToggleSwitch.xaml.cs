using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace NowPlaying.Wpf.Controls.Common
{
    public partial class ToggleSwitch : UserControl
    {
        private readonly Thickness LeftSideThickness = new Thickness(-39, 0, 0, 0);
        private readonly Thickness RightSideThickness = new Thickness(0, 0, -39, 0);
        private readonly IDictionary<bool, Thickness> _toggleMap;
        public bool IsToggled { get; set; }

        public ToggleSwitch()
        {
            InitializeComponent();

            _toggleMap = new Dictionary<bool, Thickness>
            {
                { false, LeftSideThickness },
                { true, RightSideThickness },
            };
        }

        public void Toggle(bool? newToggled = null)
        {
            IsToggled = newToggled ?? !IsToggled;
            Dot.Margin = _toggleMap[IsToggled];
        }

        private void Grid_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Toggle();
        }
    }
}
