﻿using System;
using System.Collections.Generic;

namespace NowPlaying.Core.Api.Spotify.Responses
{
    internal class SpotifyPlaybackResponse : IPlaybackResponse
    {
        public string Id { get; }

        /// <summary>
        /// Track name.
        /// </summary>
        public string Name { get; }

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

        /// <summary>
        /// Album cover url.
        /// </summary>
        public string CoverUrl { get; }

        public string FullName { get; }
        public string FormattedArtists { get; }
        public int ProgressSeconds { get; }
        public int ProgressMinutes { get; }
        public int DurationMinutes { get; }
        public int DurationSeconds { get; }

        public bool IsLocalFile { get; }

        public SpotifyPlaybackResponse(string trackId, string trackName, string coverUrl, IEnumerable<string> artists, long progress, long duration)
        {
            this.Id = trackId;
            this.Name = trackName ?? throw new ArgumentNullException(nameof(trackName));
            this.Artists = artists ?? throw new ArgumentNullException(nameof(artists));
            this.Progress = progress;
            this.Duration = duration;
            this.CoverUrl = coverUrl;

            this.IsLocalFile = this.Id == null;

            this.FullName = $"{this.GetArtistsString()} - {this.Name}";
            this.FormattedArtists = this.GetArtistsString();

            this.ProgressMinutes = (int)(this.Progress / 1000 / 60);
            this.ProgressSeconds = (int)((this.Progress / 1000) % 60);
            this.DurationMinutes = (int)(this.Duration / 1000 / 60);
            this.DurationSeconds = (int)((this.Duration / 1000) % 60);
        }

        /// <summary>
        /// Get artists separated by some string. Ex: XTENTACILION xxx AK47 xxx LANA DEL RAY.
        /// </summary>
        public string GetArtistsString(string separator = ", ") => string.Join(separator, this.Artists);
    }
}