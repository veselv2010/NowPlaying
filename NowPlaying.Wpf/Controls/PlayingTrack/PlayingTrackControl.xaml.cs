using ReactiveUI;
using System.Reactive.Disposables;

namespace NowPlaying.Wpf.Controls.PlayingTrack
{
    public class PlayingTrackControlBase : ReactiveUserControl<PlayingTrackViewModel>
    {
        //
    }

    public partial class PlayingTrackControl : PlayingTrackControlBase
    {
        public PlayingTrackControl()
        {
            ViewModel = new PlayingTrackViewModel();

            InitializeComponent();

            this.WhenActivated(d => {
                this.OneWayBind(ViewModel,
                        vm => vm.ProgressMs, v => v.Progress.ViewModel.Progress,
                        progressMs => progressMs / (ViewModel.DurationMs / 100))
                    .DisposeWith(d);
            });
        }
    }
}
