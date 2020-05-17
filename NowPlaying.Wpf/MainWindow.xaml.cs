using NowPlaying.Wpf.Controls.UserSettings;
using NowPlaying.Wpf.Controls.UserSettings.Controls;
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
            UserSettingsBlock.ViewModel = ViewModel.UserSettings;

            this.WhenActivated(d => {
                this.OneWayBind(ViewModel, vm => vm.HeaderViewModel.Theme, v => v.Background, ThemeToBrush)
                    .DisposeWith(d);
            });
        }

        // TODO: custom colors
        private SolidColorBrush ThemeToBrush(Theme theme) => theme == Theme.Black ? ColorsConstants.BlackThemeBackground : ColorsConstants.White;

        private void HeaderBlock_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            try 
            {
                OnMouseLeftButtonDown(e);
                this.DragMove();
            }
            catch (System.InvalidOperationException)
            {
                return;
            }
        }

        private async void WindowLoaded(object sender, System.Windows.RoutedEventArgs e)
        {
            AcrylicMaterial.EnableBlur(this);
            this.Hide();
            await this.ViewModel.SpotifyRequestInitialize();
            this.Show();
        }
    }
}
