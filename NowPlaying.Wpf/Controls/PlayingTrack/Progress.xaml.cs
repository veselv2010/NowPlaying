using ReactiveUI;
using System.Reactive.Disposables;

namespace NowPlaying.Wpf.Controls.PlayingTrack
{
    public class ProgressBase : ReactiveUserControl<ProgressViewModel>
    {
    }

    public partial class Progress : ProgressBase
    {
        public Progress()
        {
            ViewModel = new ProgressViewModel();
            InitializeComponent();

            this.WhenActivated(d =>
            {
                this.OneWayBind(ViewModel, vm => vm.Progress, v => v.FillingLine.Width,
                    progress => (FillingLine.Width / 100) * progress)
                    .DisposeWith(d);
            });
        }
    }
}
