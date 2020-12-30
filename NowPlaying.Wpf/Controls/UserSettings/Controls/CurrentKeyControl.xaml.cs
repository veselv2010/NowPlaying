using System.Windows.Controls;
using NowPlaying.Wpf.Models;

namespace NowPlaying.Wpf.Controls.UserSettings.Controls
{
    public partial class CurrentKeyControl : UserControl
    {
        public CurrentKeyControl()
        {
            InitializeComponent();
        }

        public void Update(string key)
        {
            var currentKeyModel = (CurrentKeyControlModel)Resources["currentKeyModel"];
            currentKeyModel.UpdateProperties(key);
        }
    }
}
