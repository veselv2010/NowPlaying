using System;
using System.Collections.Generic;

namespace NowPlaying.ApiResponses
{
    internal class CurrentTrackResponse
    {
        /// <summary>
        /// Track name.
        /// </summary>
        public string TrackName { get; private set; }

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

        public CurrentTrackResponse(string trackName, IEnumerable<string> artists, long progress, long duration)
        {
            this.TrackName = trackName ?? throw new ArgumentNullException(nameof(trackName));
            this.Artists = artists ?? throw new ArgumentNullException(nameof(artists));
            this.Progress = progress;
            this.Duration = duration;
        }

        /// <summary>
        /// Get artists separated by some string. Ex: XTENTACILION xxx AK47 xxx LANA DEL RAY.
        /// </summary>
        public string GetArtistsString(string separator = " ") => string.Join(separator, Artists);
    }
}