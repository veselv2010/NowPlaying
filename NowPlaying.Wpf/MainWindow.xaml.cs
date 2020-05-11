using NowPlaying.Wpf.Themes;
using ReactiveUI;
using System.Reactive.Disposables;
using System.Windows.Media;

namespace NowPlaying.Wpf
{
    public partial class MainWindow : ReactiveWindow<AppViewModel>
    {
        public MainWindow()
        {
            ViewModel = new AppViewModel();

            InitializeComponent();

            HeaderBlock.ViewModel = ViewModel.HeaderViewModel;
            PlayingTrackControl.ViewModel = ViewModel.PlayingTrack;

            this.WhenActivated(d => {
                this.OneWayBind(ViewModel, vm => vm.HeaderViewModel.Theme, v => v.Background, ThemeToBrush)
                    .DisposeWith(d);
            });
        }

        // TODO: custom colors
        private SolidColorBrush ThemeToBrush(Theme theme) => theme == Theme.Black ? ColorsConstants.AlmostBlack : ColorsConstants.White;

        private void HeaderBlock_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            OnMouseLeftButtonDown(e);
            this.DragMove();
        }
    }
}
