using NowPlaying.Wpf.Controls.Header;
using NowPlaying.Wpf.Controls.PlayingTrack;
using NowPlaying.Wpf.Themes;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System.Collections.Generic;

namespace NowPlaying.Wpf
{
    public class AppViewModel : ReactiveObject
    {
        [Reactive] public HeaderViewModel HeaderViewModel { get; set; } = new HeaderViewModel();

        [Reactive] public PlayingTrackViewModel PlayingTrack { get; set; } = new PlayingTrackViewModel();

        [Reactive] public bool IsRunning { get; set; }

        [Reactive] public string SendBindKey { get; set; }

        [Reactive] public IEnumerable<string> Accounts { get; set; }
        [Reactive] public string SelectedAccount { get; set; }

        public AppViewModel()
        {
            HeaderViewModel.Theme = Theme.White;
            PlayingTrack.Title = "тайтл";
            PlayingTrack.Author = "автор";
            PlayingTrack.ProgressMs = 900;
            PlayingTrack.DurationMs = 1000;
        }
    }
}
