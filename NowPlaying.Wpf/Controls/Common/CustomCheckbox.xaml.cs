using NowPlaying.Wpf.Models;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;

namespace NowPlaying.Wpf.Controls.Common
{
    public partial class CustomCheckbox : UserControl
    {
        public bool IsToggled { get; private set; }

        public CustomCheckbox()
        {
            InitializeComponent();
        }

        private void StackPanel_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            BorderRectangle.Stroke = ColorsConstants.SpotifyGreen;
        }

        private void StackPanel_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            BorderRectangle.Stroke = ColorsConstants.MilkyGrayBorder;
        }

        public void Toggle()
        {
            var model = (CustomCheckboxModel)Resources["checkBoxModel"];
            model.IsToggled = !model.IsToggled;
            IsToggled = model.IsToggled;
        }
    }
}
