using NowPlaying.Core.Api.SpotifyResponses;
using System;
using System.Windows.Controls;
using NowPlaying.Wpf.Models;

namespace NowPlaying.Wpf.Controls.PlayingTrack
{
    public partial class PlayingTrackControl : UserControl
    {
        public PlayingTrackControl()
        {
            InitializeComponent();
        }

        public void Update(CurrentTrackResponse resp)
        {
            var currentTrack = (PlayingTrackModel)Resources["currentTrack"];
            currentTrack.UpdateProperties(resp);
            Progress.Update(GetProgess(resp.Progress, resp.Duration));
        }

        private double GetProgess(long progressMs, long durationMs) =>
            durationMs != 0 ? (Convert.ToDouble(progressMs) / Convert.ToDouble(durationMs)) * 100 : 0;
    }
}
