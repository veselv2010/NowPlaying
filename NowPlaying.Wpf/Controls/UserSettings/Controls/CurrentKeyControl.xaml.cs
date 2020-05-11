using ReactiveUI;

namespace NowPlaying.Wpf.Controls.UserSettings.Controls
{
    public partial class CurrentKeyControlBase : ReactiveUserControl<CurrentKeyControlViewModel>
    {
        
    }

    public partial class CurrentKeyControl : CurrentKeyControlBase
    {
        public CurrentKeyControl()
        {
            ViewModel = new CurrentKeyControlViewModel();
            InitializeComponent();
        }

        private void KeyControlMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            
        }
    }
}
