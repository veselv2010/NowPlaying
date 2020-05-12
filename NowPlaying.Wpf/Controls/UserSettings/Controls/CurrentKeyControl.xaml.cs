using ReactiveUI;
using System.Reactive.Disposables;

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

            this.WhenActivated(d => {
                this.OneWayBind(ViewModel, vm => vm.CurrentKey, v => v.CurrentKeyTextBlock.Text)
                    .DisposeWith(d);
            });
        }

        private void KeyControlMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            
        }
    }
}
