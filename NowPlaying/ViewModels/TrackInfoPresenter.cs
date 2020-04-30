using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using NowPlaying.Api;
using NowPlaying.Api.SpotifyResponses;

namespace NowPlaying.ViewModels
{
    class TrackInfoPresenter : ObservableObject
    {
        private CurrentTrackResponse resp;

        public string Artists
        {
            get
            {
                return resp.FormattedArtists;
            }
        }

        public string SongName
        {
            get
            {
                return resp.FullName;
            }
        }

        public string CurrentTime
        {
            get
            {
                return $"{resp.ProgressMinutes.ToString()}:{resp.ProgressSeconds:00}";
            }
        }

        public string EstmatedTime
        {
            get
            {
                return $"{resp.DurationMinutes.ToString()}:{resp.DurationSeconds:00}";
            }
        }

        public double Progress
        {
            get
            {
                return resp.Progress / 1000;
            }
        }
    }
}
