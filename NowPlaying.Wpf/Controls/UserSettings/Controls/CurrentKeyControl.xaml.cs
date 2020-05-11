using ReactiveUI;
using System.Windows.Media;

namespace NowPlaying.Wpf.Controls.UserSettings.Controls
{
    public partial class CurrentKeyControlBase : ReactiveUserControl<CurrentKeyControlViewModel>
    {
        
    }
    public partial class CurrentKeyControl : CurrentKeyControlBase
    {
        private static SolidColorBrush MilkyBrush = new SolidColorBrush(Color.FromRgb(0xF9, 0xF9, 0xF9));
        private static SolidColorBrush GrayBrush  = new SolidColorBrush(Color.FromRgb(0x7e, 0x7e, 0x7e));
        private static SolidColorBrush DarkBrush  = new SolidColorBrush(Color.FromRgb(0x17, 0x17, 0x17));

        public CurrentKeyControl()
        {
            ViewModel = new CurrentKeyControlViewModel();
            InitializeComponent();
        }

        private void Border_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

        }
    }
}
