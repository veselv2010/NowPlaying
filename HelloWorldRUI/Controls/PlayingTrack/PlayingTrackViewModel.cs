using ReactiveUI.Fody.Helpers;

namespace HelloWorldRUI.Controls.PlayingTrack
{
    public class PlayingTrackViewModel
    {
        [Reactive] public string Title { get; set; }
        [Reactive] public int DurationMs { get; set; }
        [Reactive] public int ProgressMs { get; set; }
    }
}
