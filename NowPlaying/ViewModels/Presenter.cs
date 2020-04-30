using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using NowPlaying.Api;
using NowPlaying.Api.SpotifyResponses;

namespace NowPlaying.ViewModels
{
    class Presenter : ObservableObject
    {
        private CurrentTrackResponse resp;
        public CurrentTrackResponse Resp
        {
            get { return resp; }
            set
            {
                resp = value;
                RaisePropertyChangedEvent("resp");
            }
        }

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
