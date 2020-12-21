using NowPlaying.Core.Api.SpotifyResponses;
using System;
using System.Windows.Controls;

namespace NowPlaying.Wpf.Controls.PlayingTrack
{
    public partial class PlayingTrackControl : UserControl
    {
        private CurrentTrackResponse _currentTrack;
        public CurrentTrackResponse CurrentTrack 
        { 
            get
            {
                return _currentTrack;
            } 

            set
            {
                _currentTrack = value;
                Dispatcher.Invoke(() =>
                {
                    DataContext = CurrentTrack;
                    Progress.ProgressPercentage = GetProgess(_currentTrack.Progress, _currentTrack.Duration);
                });
            }
        }
        public PlayingTrackControl()
        {
            InitializeComponent();
            DataContext = CurrentTrack;
        }

        private double GetProgess(long progressMs, long durationMs) =>
            durationMs != 0 ? (Convert.ToDouble(progressMs) / Convert.ToDouble(durationMs)) * 100 : 0;
        
    }
}
