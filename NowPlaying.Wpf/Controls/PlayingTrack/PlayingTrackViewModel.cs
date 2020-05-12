using ReactiveUI.Fody.Helpers;

namespace NowPlaying.Wpf.Controls.PlayingTrack
{
    public class PlayingTrackViewModel
    {
        [Reactive] public string Title { get; set; }
        [Reactive] public string Author { get; set; }
        [Reactive] public int DurationMs { get; set; }
        [Reactive] public int ProgressMs { get; set; }
        [Reactive] public string CurrentProgress { get; set; }
        [Reactive] public string EstimatedProgress { get; set; }
    }
}
