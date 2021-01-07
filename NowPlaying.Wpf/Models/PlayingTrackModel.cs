using NowPlaying.Core.Api.SpotifyResponses;

namespace NowPlaying.Wpf.Models
{
    public class PlayingTrackModel : PropertyNotifier
    {
        public void UpdateProperties(CurrentTrackResponse resp)
        {
            FormattedArtists = resp.FormattedArtists;
            Name = resp.Name;
            ProgressMinutes = resp.ProgressMinutes;
            ProgressSeconds = resp.ProgressSeconds;
            DurationMinutes = resp.DurationMinutes;
            DurationSeconds = resp.DurationSeconds;
        }

        private string _formattedArtists;
        private string _name;
        private int _progressMinutes;
        private int _progressSeconds;
        private int _durationMintutes;
        private int _durationSeconds;

        public string FormattedArtists 
        { 
            get 
            {
                return _formattedArtists;
            }
            set 
            {
                _formattedArtists = value;
                OnPropertyChanged(nameof(FormattedArtists));
            } 
        }

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public int ProgressMinutes
        {
            get
            {
                return _progressMinutes;
            }
            set
            {
                _progressMinutes = value;
                OnPropertyChanged(nameof(ProgressMinutes));
            }
        }

        public int ProgressSeconds
        {
            get
            {
                return _progressSeconds;
            }
            set
            {
                _progressSeconds = value;
                OnPropertyChanged(nameof(ProgressSeconds));
            }
        }

        public int DurationMinutes
        {
            get
            {
                return _durationMintutes;
            }
            set
            {
                _durationMintutes = value;
                OnPropertyChanged(nameof(DurationMinutes));
            }
        }

        public int DurationSeconds
        {
            get
            {
                return _durationSeconds;
            }
            set
            {
                _durationSeconds = value;
                OnPropertyChanged(nameof(DurationSeconds));
            }
        }
    }
}
