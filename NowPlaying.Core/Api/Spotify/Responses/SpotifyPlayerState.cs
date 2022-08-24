using Newtonsoft.Json;
using System.Collections.Generic;

namespace NowPlaying.Core.Api.Spotify.Responses
{
    internal class Actions
    {
        [JsonProperty("disallows")]
        public Disallows Disallows { get; set; }
    }

    internal class Album
    {
        [JsonProperty("album_type")]
        public string AlbumType { get; set; }

        [JsonProperty("artists")]
        public IReadOnlyList<Artist> Artists { get; set; }

        [JsonProperty("available_markets")]
        public IReadOnlyList<string> AvailableMarkets { get; set; }

        [JsonProperty("external_urls")]
        public ExternalUrls ExternalUrls { get; set; }

        [JsonProperty("href")]
        public string Href { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("images")]
        public IReadOnlyList<Image> Images { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("release_date")]
        public string ReleaseDate { get; set; }

        [JsonProperty("release_date_precision")]
        public string ReleaseDatePrecision { get; set; }

        [JsonProperty("total_tracks")]
        public int TotalTracks { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("uri")]
        public string Uri { get; set; }
    }

    internal class Artist
    {
        [JsonProperty("external_urls")]
        public ExternalUrls ExternalUrls { get; set; }

        [JsonProperty("href")]
        public string Href { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("uri")]
        public string Uri { get; set; }
    }

    internal class Context
    {
        [JsonProperty("external_urls")]
        public ExternalUrls ExternalUrls { get; set; }

        [JsonProperty("href")]
        public string Href { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("uri")]
        public string Uri { get; set; }
    }

    internal class Disallows
    {
        [JsonProperty("resuming")]
        public bool Resuming { get; set; }

        [JsonProperty("skipping_prev")]
        public bool SkippingPrev { get; set; }
    }

    internal class ExternalIds
    {
        [JsonProperty("isrc")]
        public string Isrc { get; set; }
    }

    internal class ExternalUrls
    {
        [JsonProperty("spotify")]
        public string Spotify { get; set; }
    }

    internal class Image
    {
        [JsonProperty("height")]
        public int Height { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("width")]
        public int Width { get; set; }
    }

    internal class Item
    {
        [JsonProperty("album")]
        public Album Album { get; set; }

        [JsonProperty("artists")]
        public IReadOnlyList<Artist> Artists { get; set; }

        [JsonProperty("available_markets")]
        public IReadOnlyList<string> AvailableMarkets { get; set; }

        [JsonProperty("disc_number")]
        public int DiscNumber { get; set; }

        [JsonProperty("duration_ms")]
        public long DurationMs { get; set; }

        [JsonProperty("explicit")]
        public bool Explicit { get; set; }

        [JsonProperty("external_ids")]
        public ExternalIds ExternalIds { get; set; }

        [JsonProperty("external_urls")]
        public ExternalUrls ExternalUrls { get; set; }

        [JsonProperty("href")]
        public string Href { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("is_local")]
        public bool IsLocal { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("popularity")]
        public int Popularity { get; set; }

        [JsonProperty("preview_url")]
        public object PreviewUrl { get; set; }

        [JsonProperty("track_number")]
        public int TrackNumber { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("uri")]
        public string Uri { get; set; }
    }

    internal class SpotifyPlayerState
    {
        [JsonProperty("timestamp")]
        public long Timestamp { get; set; }

        [JsonProperty("context")]
        public Context Context { get; set; }

        [JsonProperty("progress_ms")]
        public long ProgressMs { get; set; }

        [JsonProperty("item")]
        public Item Item { get; set; }

        [JsonProperty("currently_playing_type")]
        public string CurrentlyPlayingType { get; set; }

        [JsonProperty("actions")]
        public Actions Actions { get; set; }

        [JsonProperty("is_playing")]
        public bool IsPlaying { get; set; }
    }
}
