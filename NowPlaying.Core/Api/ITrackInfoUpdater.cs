using System;

namespace NowPlaying.Core.Api
{
    public delegate void StateUpdate(IPlaybackResponse state);
    public interface ITrackInfoUpdater : IDisposable
    {
        void StartPlaybackUpdate();
        event StateUpdate OnPlaybackStateUpdate;
    }
}
