using NowPlaying.Wpf.Models;
using System.Windows.Controls;

namespace NowPlaying.Wpf.Controls.Common
{
    public partial class CustomCheckbox : UserControl
    {
        public bool IsToggled { get; private set; }

        public CustomCheckbox()
        {
            InitializeComponent();
        }

        public void Toggle()
        {
            var model = (CustomCheckboxModel)Resources["checkBoxModel"];
            model.IsToggled = !model.IsToggled;
            IsToggled = model.IsToggled;
        }
    }
}
