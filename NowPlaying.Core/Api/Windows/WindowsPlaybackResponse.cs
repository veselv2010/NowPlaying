using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.Control;

namespace NowPlaying.Core.Api.WindowsManager
{
    public class WindowsPlaybackResponse : IPlaybackResponse
    {
        public IEnumerable<string> Artists { get; }
        public string CoverUrl { get; }
        public long Duration { get; }
        public int DurationMinutes { get; }
        public int DurationSeconds { get; }
        public string FormattedArtists { get; }
        public string FullName { get; }
        public string Id { get; }
        public bool IsLocalFile { get; }
        public string Name { get; }
        public long Progress { get; }
        public int ProgressMinutes { get; }
        public int ProgressSeconds { get; }
        public WindowsPlaybackResponse(GlobalSystemMediaTransportControlsSessionTimelineProperties timelineProperties, GlobalSystemMediaTransportControlsSessionMediaProperties mediaProperties)
        {
            this.Id = "f";
            this.Name = mediaProperties.Title;
            this.Artists = mediaProperties.Artist.Split(',');
            this.Progress = (long)timelineProperties.Position.TotalMilliseconds;
            this.Duration = (long)timelineProperties.EndTime.TotalMilliseconds;
            this.CoverUrl = mediaProperties.Thumbnail?.ToString() ?? "";

            this.IsLocalFile = true;

            this.FullName = $"{this.GetArtistsString()} - {this.Name}";
            this.FormattedArtists = this.GetArtistsString();

            this.ProgressMinutes = (int)(this.Progress / 1000 / 60);
            this.ProgressSeconds = (int)((this.Progress / 1000) % 60);
            this.DurationMinutes = (int)(this.Duration / 1000 / 60);
            this.DurationSeconds = (int)((this.Duration / 1000) % 60);
        }

        public string GetArtistsString(string separator = ", ") => string.Join(separator, this.Artists);
    }
}
