using ReactiveUI.Fody.Helpers;

namespace NowPlaying.Wpf.Controls.PlayingTrack
{
    public class ProgressViewModel
    {
        /// <summary>
        /// 0 <= Progress <= 100
        /// </summary>
        [Reactive] public long Progress { get; set; }
    }
}