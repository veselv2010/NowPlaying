using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace NowPlaying.ApiResponses
{
    public class CurrentTrackResponse : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private string _Name;
        private string _FormattedArtists;
        private string _ProgressFormatted;
        private string _DurationFormatted;
        private bool _IsLocalFile;
        public string Id { get; }

        /// <summary>
        /// Track name.
        /// </summary>
        public string Name
        {
            get { return _Name; }
            set
            {
                _Name = value;
                OnPropertyChanged("_Name");
            }
        }

        /// <summary>
        /// Artists list.
        /// </summary>
        public IEnumerable<string> Artists { get; }

        /// <summary>
        /// Track progress in milliseconds.
        /// </summary>
        public long Progress { get; }

        /// <summary>
        /// Track duration in milliseconds.
        /// </summary>
        public long Duration { get; }

        public string FullName { get; }
        public string FormattedArtists
        {
            get
            {
                return _FormattedArtists;
            }
            set
            {
                _FormattedArtists = value;
                OnPropertyChanged("FormattedArtists");
            }
        }
        public string ProgressFormatted
        {
            get
            {
                return _ProgressFormatted;
            }
            set
            {
                _ProgressFormatted = value;
                OnPropertyChanged("ProgressFormatted");
            }
        }
        public string DurationFormatted
        {
            get
            {
                return _DurationFormatted;
            }
            set
            {
                _DurationFormatted = value;
                OnPropertyChanged("DurationFormatted");
            }
        }

        public bool IsLocalFile
        {
            get
            {
                return _IsLocalFile;
            }
            set
            {
                _IsLocalFile = value;
                OnPropertyChanged("IsLocalFile");
            }
        }

        public CurrentTrackResponse(string trackId, string trackName, IEnumerable<string> artists, long progress, long duration)
        {
            this.Id = trackId;
            this.Name = trackName ?? throw new ArgumentNullException(nameof(trackName));
            this.Artists = artists ?? throw new ArgumentNullException(nameof(artists));
            this.Progress = progress;
            this.Duration = duration;

            this.IsLocalFile = this.Id == null;
            this.FullName = $"{this.GetArtistsString()} - {this.Name}";
            this.FormattedArtists = this.GetArtistsString();
            this.ProgressFormatted = $"{((int)(this.Progress / 1000 / 60)).ToString()}:{(int)((this.Progress / 1000) % 60):00}";
            this.DurationFormatted = $"{((int)(this.Duration / 1000 / 60)).ToString()}:{(int)((this.Duration / 1000) % 60):00}";
        }

        /// <summary>
        /// Get artists separated by some string. Ex: XTENTACILION xxx AK47 xxx LANA DEL RAY.
        /// </summary>
        public string GetArtistsString(string separator = ", ") => string.Join(separator, this.Artists);
    }
}