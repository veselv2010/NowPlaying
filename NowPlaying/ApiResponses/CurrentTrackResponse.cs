using System;
using System.Collections.Generic;

namespace NowPlaying.ApiResponses
{
    internal class CurrentTrackResponse
    {
        public string Id { get; private set; }

        /// <summary>
        /// Track name.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Artists list.
        /// </summary>
        public IEnumerable<string> Artists { get; private set; }

        /// <summary>
        /// Track progress in milliseconds.
        /// </summary>
        public long Progress { get; private set; }

        /// <summary>
        /// Track duration in milliseconds.
        /// </summary>
        public long Duration { get; private set; }

        public string FullName { get; private set; }

        public string FormattedName { get; private set; }

        public CurrentTrackResponse(string trackId, string trackName, IEnumerable<string> artists, long progress, long duration)
        {
            this.Id = trackId ?? throw new ArgumentNullException(nameof(trackId));
            this.Name = trackName ?? throw new ArgumentNullException(nameof(trackName));
            this.Artists = artists ?? throw new ArgumentNullException(nameof(artists));
            this.Progress = progress;
            this.Duration = duration;

            this.FullName = $"{this.GetArtistsString()} - {this.Name}";
            this.FormattedName = TrackNameFormatter.ToLatin(this.FullName);
        }

        /// <summary>
        /// Get artists separated by some string. Ex: XTENTACILION xxx AK47 xxx LANA DEL RAY.
        /// </summary>
        public string GetArtistsString(string separator = " ") => string.Join(separator, this.Artists);
    }
}