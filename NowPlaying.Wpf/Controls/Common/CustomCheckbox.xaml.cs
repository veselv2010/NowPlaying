using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;

namespace NowPlaying.Wpf.Controls.Common
{
    public partial class CustomCheckbox : UserControl
    {
        public bool IsToggled { get; set; }
        IDictionary<bool, SolidColorBrush> colorMap;

        public CustomCheckbox()
        {

            InitializeComponent();

            colorMap = new Dictionary<bool, SolidColorBrush>
            {
                { false, ColorsConstants.Transparent },
                { true, ColorsConstants.SpotifyGreen },
            };
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

        public void Toggle(bool? newToggled = null)
        {
            IsToggled = newToggled ?? !IsToggled;
            CheckedRectangle.Fill = colorMap[IsToggled];
        }
    }
}
