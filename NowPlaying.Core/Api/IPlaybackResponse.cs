using System.Collections.Generic;

namespace NowPlaying.Core.Api
{
    public interface IPlaybackResponse
    {
        IEnumerable<string> Artists { get; }
        string CoverUrl { get; }
        long Duration { get; }
        int DurationMinutes { get; }
        int DurationSeconds { get; }
        string FormattedArtists { get; }
        string FullName { get; }
        string Id { get; }
        bool IsLocalFile { get; }
        string Name { get; }
        long Progress { get; }
        int ProgressMinutes { get; }
        int ProgressSeconds { get; }

        string GetArtistsString(string separator = ", ");
    }
}