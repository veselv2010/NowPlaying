using System;
using System.Timers;

namespace NowPlaying.Core.Api.Spotify
{
    public sealed class SpotifyTrackUpdater : ITrackInfoUpdater
    {
        public event StateUpdate OnPlaybackStateUpdate;

        private readonly SpotifyRequestsManager _spotify;
        private readonly Timer _timer;

        public SpotifyTrackUpdater(SpotifyRequestsManager requestsManager)
        {
            _spotify = requestsManager;
            _timer = new Timer { Interval = 1000, AutoReset = true };
            _timer.Elapsed += onTimeElapsed;
        }

        public void StartPlaybackUpdate()
        {
            _timer.Start();
        }

        private async void onTimeElapsed(object sender, ElapsedEventArgs e)
        {
            OnPlaybackStateUpdate.Invoke(await _spotify.GetCurrentTrack());
        }

        public void Dispose()
        {
            _timer.Stop();
            _timer.Dispose();
        }
    }
}
