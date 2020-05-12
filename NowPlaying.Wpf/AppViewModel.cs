using NowPlaying.Wpf.Auth;
using NowPlaying.Wpf.Controls.Header;
using NowPlaying.Wpf.Controls.PlayingTrack;
using NowPlaying.Wpf.Controls.UserSettings;
using NowPlaying.Wpf.Controls.UserSettings.Controls;
using NowPlaying.Wpf.Themes;
using NowPlaying.Core.Api;
using NowPlaying.Core.Api.SpotifyResponses;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NowPlaying.Wpf
{
    public class AppViewModel : ReactiveObject
    {
        [Reactive] public HeaderViewModel HeaderViewModel { get; set; } = new HeaderViewModel();

        [Reactive] public PlayingTrackViewModel PlayingTrack { get; set; } = new PlayingTrackViewModel();

        [Reactive] public UserSettingsBlockViewModel UserSettings { get; set; } = new UserSettingsBlockViewModel();

        [Reactive] public bool IsRunning { get; set; }
        [Reactive] public string SendBindKey { get; set; }
        [Reactive] public IEnumerable<string> Accounts { get; set; }
        [Reactive] public string SelectedAccount { get; set; }

        public SpotifyRequestsManager spotify;
        
        public AppViewModel()
        {
            spotify = new SpotifyRequestsManager("7633771350404368ac3e05c9cf73d187",
                "29bd9ec2676c4bf593f3cc2858099838", @"https://www.google.com/");

            spotify.StartTokenRequests(GetCode());

            HeaderViewModel.Theme = Theme.White;
            PlayingTrack.Title = "тайтл";
            PlayingTrack.Author = "автор";
            PlayingTrack.ProgressMs = 203232;
            PlayingTrack.DurationMs = 240000;
            PlayingTrack.CurrentProgress = $"{(PlayingTrack.ProgressMs / 1000 / 60)}:{(PlayingTrack.ProgressMs / 1000 % 60):00}";
            PlayingTrack.EstimatedProgress = $"{(PlayingTrack.DurationMs / 1000 / 60)}:{(PlayingTrack.DurationMs / 1000 % 60):00}";

            UserSettings.CurrentKey = "it just works";
        }

        private string GetCode()
        {
            var auth = new AuthWindow(spotify.GetAuthUrl());
            auth.ShowDialog();

            return auth.Code;
        }
    }
}
