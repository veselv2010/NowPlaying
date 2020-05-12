using NowPlaying.Wpf.Controls.Header;
using NowPlaying.Wpf.Controls.PlayingTrack;
using NowPlaying.Wpf.Controls.UserSettings;
using NowPlaying.Wpf.Controls.UserSettings.Controls;
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

        [Reactive] public UserSettingsBlockViewModel UserSettings { get; set; } = new UserSettingsBlockViewModel();
        [Reactive] public CurrentKeyControlViewModel CurrentKeyViewModel { get; set; } = new CurrentKeyControlViewModel();

        [Reactive] public bool IsRunning { get; set; }

        [Reactive] public string SendBindKey { get; set; }

        [Reactive] public IEnumerable<string> Accounts { get; set; }
        [Reactive] public string SelectedAccount { get; set; }

        public AppViewModel()
        {
            HeaderViewModel.Theme = Theme.White;
            PlayingTrack.Title = "тайтл";
            PlayingTrack.Author = "автор";
            PlayingTrack.ProgressMs = 203232;
            PlayingTrack.DurationMs = 240000;
            PlayingTrack.CurrentProgress = $"{(PlayingTrack.ProgressMs / 1000 / 60)}:{(PlayingTrack.ProgressMs / 1000 % 60):00}";
            PlayingTrack.EstimatedProgress = $"{(PlayingTrack.DurationMs / 1000 / 60)}:{(PlayingTrack.DurationMs / 1000 % 60):00}";
            
            CurrentKeyViewModel.CurrentKey = "kp_5";
        }
    }
}
