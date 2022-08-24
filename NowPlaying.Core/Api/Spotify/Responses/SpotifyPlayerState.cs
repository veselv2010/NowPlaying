using Newtonsoft.Json;
using System.Collections.Generic;

namespace NowPlaying.Core.Api.Spotify.Responses
{
    internal class Actions
    {
        [JsonConstructor]
        public Actions(
            [JsonProperty("disallows")] Disallows disallows
        )
        {
            this.Disallows = disallows;
        }

        [JsonProperty("disallows")]
        public Disallows Disallows { get; }
    }

    internal class Album
    {
        [JsonConstructor]
        public Album(
            [JsonProperty("album_type")] string albumType,
            [JsonProperty("artists")] List<Artist> artists,
            [JsonProperty("available_markets")] List<string> availableMarkets,
            [JsonProperty("external_urls")] ExternalUrls externalUrls,
            [JsonProperty("href")] string href,
            [JsonProperty("id")] string id,
            [JsonProperty("images")] List<Image> images,
            [JsonProperty("name")] string name,
            [JsonProperty("release_date")] string releaseDate,
            [JsonProperty("release_date_precision")] string releaseDatePrecision,
            [JsonProperty("total_tracks")] int totalTracks,
            [JsonProperty("type")] string type,
            [JsonProperty("uri")] string uri
        )
        {
            this.AlbumType = albumType;
            this.Artists = artists;
            this.AvailableMarkets = availableMarkets;
            this.ExternalUrls = externalUrls;
            this.Href = href;
            this.Id = id;
            this.Images = images;
            this.Name = name;
            this.ReleaseDate = releaseDate;
            this.ReleaseDatePrecision = releaseDatePrecision;
            this.TotalTracks = totalTracks;
            this.Type = type;
            this.Uri = uri;
        }

        [JsonProperty("album_type")]
        public string AlbumType { get; }

        [JsonProperty("artists")]
        public IReadOnlyList<Artist> Artists { get; }

        [JsonProperty("available_markets")]
        public IReadOnlyList<string> AvailableMarkets { get; }

        [JsonProperty("external_urls")]
        public ExternalUrls ExternalUrls { get; }

        [JsonProperty("href")]
        public string Href { get; }

        [JsonProperty("id")]
        public string Id { get; }

        [JsonProperty("images")]
        public IReadOnlyList<Image> Images { get; }

        [JsonProperty("name")]
        public string Name { get; }

        [JsonProperty("release_date")]
        public string ReleaseDate { get; }

        [JsonProperty("release_date_precision")]
        public string ReleaseDatePrecision { get; }

        [JsonProperty("total_tracks")]
        public int TotalTracks { get; }

        [JsonProperty("type")]
        public string Type { get; }

        [JsonProperty("uri")]
        public string Uri { get; }
    }

    internal class Artist
    {
        [JsonConstructor]
        public Artist(
            [JsonProperty("external_urls")] ExternalUrls externalUrls,
            [JsonProperty("href")] string href,
            [JsonProperty("id")] string id,
            [JsonProperty("name")] string name,
            [JsonProperty("type")] string type,
            [JsonProperty("uri")] string uri
        )
        {
            this.ExternalUrls = externalUrls;
            this.Href = href;
            this.Id = id;
            this.Name = name;
            this.Type = type;
            this.Uri = uri;
        }

        [JsonProperty("external_urls")]
        public ExternalUrls ExternalUrls { get; }

        [JsonProperty("href")]
        public string Href { get; }

        [JsonProperty("id")]
        public string Id { get; }

        [JsonProperty("name")]
        public string Name { get; }

        [JsonProperty("type")]
        public string Type { get; }

        [JsonProperty("uri")]
        public string Uri { get; }
    }

    internal class Context
    {
        [JsonConstructor]
        public Context(
            [JsonProperty("external_urls")] ExternalUrls externalUrls,
            [JsonProperty("href")] string href,
            [JsonProperty("type")] string type,
            [JsonProperty("uri")] string uri
        )
        {
            this.ExternalUrls = externalUrls;
            this.Href = href;
            this.Type = type;
            this.Uri = uri;
        }

        [JsonProperty("external_urls")]
        public ExternalUrls ExternalUrls { get; }

        [JsonProperty("href")]
        public string Href { get; }

        [JsonProperty("type")]
        public string Type { get; }

        [JsonProperty("uri")]
        public string Uri { get; }
    }

    internal class Disallows
    {
        [JsonConstructor]
        public Disallows(
            [JsonProperty("resuming")] bool resuming,
            [JsonProperty("skipping_prev")] bool skippingPrev
        )
        {
            this.Resuming = resuming;
            this.SkippingPrev = skippingPrev;
        }

        [JsonProperty("resuming")]
        public bool Resuming { get; }

        [JsonProperty("skipping_prev")]
        public bool SkippingPrev { get; }
    }

    internal class ExternalIds
    {
        [JsonConstructor]
        public ExternalIds(
            [JsonProperty("isrc")] string isrc
        )
        {
            this.Isrc = isrc;
        }

        [JsonProperty("isrc")]
        public string Isrc { get; }
    }

    internal class ExternalUrls
    {
        [JsonConstructor]
        public ExternalUrls(
            [JsonProperty("spotify")] string spotify
        )
        {
            this.Spotify = spotify;
        }

        [JsonProperty("spotify")]
        public string Spotify { get; }
    }

    internal class Image
    {
        [JsonConstructor]
        public Image(
            [JsonProperty("height")] int height,
            [JsonProperty("url")] string url,
            [JsonProperty("width")] int width
        )
        {
            this.Height = height;
            this.Url = url;
            this.Width = width;
        }

        [JsonProperty("height")]
        public int Height { get; }

        [JsonProperty("url")]
        public string Url { get; }

        [JsonProperty("width")]
        public int Width { get; }
    }

    internal class Item
    {
        [JsonConstructor]
        public Item(
            [JsonProperty("album")] Album album,
            [JsonProperty("artists")] List<Artist> artists,
            [JsonProperty("available_markets")] List<string> availableMarkets,
            [JsonProperty("disc_number")] int discNumber,
            [JsonProperty("duration_ms")] int durationMs,
            [JsonProperty("explicit")] bool @explicit,
            [JsonProperty("external_ids")] ExternalIds externalIds,
            [JsonProperty("external_urls")] ExternalUrls externalUrls,
            [JsonProperty("href")] string href,
            [JsonProperty("id")] string id,
            [JsonProperty("is_local")] bool isLocal,
            [JsonProperty("name")] string name,
            [JsonProperty("popularity")] int popularity,
            [JsonProperty("preview_url")] object previewUrl,
            [JsonProperty("track_number")] int trackNumber,
            [JsonProperty("type")] string type,
            [JsonProperty("uri")] string uri
        )
        {
            this.Album = album;
            this.Artists = artists;
            this.AvailableMarkets = availableMarkets;
            this.DiscNumber = discNumber;
            this.DurationMs = durationMs;
            this.Explicit = @explicit;
            this.ExternalIds = externalIds;
            this.ExternalUrls = externalUrls;
            this.Href = href;
            this.Id = id;
            this.IsLocal = isLocal;
            this.Name = name;
            this.Popularity = popularity;
            this.PreviewUrl = previewUrl;
            this.TrackNumber = trackNumber;
            this.Type = type;
            this.Uri = uri;
        }

        [JsonProperty("album")]
        public Album Album { get; }

        [JsonProperty("artists")]
        public IReadOnlyList<Artist> Artists { get; }

        [JsonProperty("available_markets")]
        public IReadOnlyList<string> AvailableMarkets { get; }

        [JsonProperty("disc_number")]
        public int DiscNumber { get; }

        [JsonProperty("duration_ms")]
        public long DurationMs { get; }

        [JsonProperty("explicit")]
        public bool Explicit { get; }

        [JsonProperty("external_ids")]
        public ExternalIds ExternalIds { get; }

        [JsonProperty("external_urls")]
        public ExternalUrls ExternalUrls { get; }

        [JsonProperty("href")]
        public string Href { get; }

        [JsonProperty("id")]
        public string Id { get; }

        [JsonProperty("is_local")]
        public bool IsLocal { get; }

        [JsonProperty("name")]
        public string Name { get; }

        [JsonProperty("popularity")]
        public int Popularity { get; }

        [JsonProperty("preview_url")]
        public object PreviewUrl { get; }

        [JsonProperty("track_number")]
        public int TrackNumber { get; }

        [JsonProperty("type")]
        public string Type { get; }

        [JsonProperty("uri")]
        public string Uri { get; }
    }

    internal class SpotifyPlayerState
    {
        [JsonConstructor]
        public SpotifyPlayerState(
            [JsonProperty("timestamp")] long timestamp,
            [JsonProperty("context")] Context context,
            [JsonProperty("progress_ms")] int progressMs,
            [JsonProperty("item")] Item item,
            [JsonProperty("currently_playing_type")] string currentlyPlayingType,
            [JsonProperty("actions")] Actions actions,
            [JsonProperty("is_playing")] bool isPlaying
        )
        {
            this.Timestamp = timestamp;
            this.Context = context;
            this.ProgressMs = progressMs;
            this.Item = item;
            this.CurrentlyPlayingType = currentlyPlayingType;
            this.Actions = actions;
            this.IsPlaying = isPlaying;
        }

        [JsonProperty("timestamp")]
        public long Timestamp { get; }

        [JsonProperty("context")]
        public Context Context { get; }

        [JsonProperty("progress_ms")]
        public long ProgressMs { get; }

        [JsonProperty("item")]
        public Item Item { get; }

        [JsonProperty("currently_playing_type")]
        public string CurrentlyPlayingType { get; }

        [JsonProperty("actions")]
        public Actions Actions { get; }

        [JsonProperty("is_playing")]
        public bool IsPlaying { get; }
    }
}
