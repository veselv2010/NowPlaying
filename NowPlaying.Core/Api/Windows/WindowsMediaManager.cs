// https://github.com/DubyaDude/WindowsMediaController

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Windows.Media.Control;

namespace NowPlaying.Core.Api.WindowsManager
{
    public class WindowsMediaManager : IDisposable, ITrackInfoUpdater
    {
        public event StateUpdate OnPlaybackStateUpdate;

        internal ReadOnlyDictionary<string, MediaSession> CurrentMediaSessions => new ReadOnlyDictionary<string, MediaSession>(_CurrentMediaSessions);
        private readonly Dictionary<string, MediaSession> _CurrentMediaSessions = new Dictionary<string, MediaSession>();

        public bool IsStarted { get => _IsStarted; }
        private bool _IsStarted;


        public GlobalSystemMediaTransportControlsSessionManager WindowsSessionManager { get => _WindowsSessionManager; }
        private GlobalSystemMediaTransportControlsSessionManager _WindowsSessionManager;

        public void StartPlaybackUpdate()
        {
            if (!_IsStarted)
            {
                _WindowsSessionManager = GlobalSystemMediaTransportControlsSessionManager.RequestAsync().GetAwaiter().GetResult();
                onSessionsChanged(_WindowsSessionManager);
                _WindowsSessionManager.SessionsChanged += onSessionsChanged;
                _IsStarted = true;
            }
            else
            {
                throw new InvalidOperationException("MediaManager already started");
            }
        }

        private void onSessionsChanged(GlobalSystemMediaTransportControlsSessionManager winSessionManager, SessionsChangedEventArgs args = null)
        {
            var controlSessionList = winSessionManager.GetSessions();

            foreach (var controlSession in controlSessionList)
            {
                if (!CurrentMediaSessions.ContainsKey(controlSession.SourceAppUserModelId))
                {
                    var mediaSession = new MediaSession(controlSession, this);
                    _CurrentMediaSessions[controlSession.SourceAppUserModelId] = mediaSession;

                    mediaSession.OnSongChange(controlSession);
                }
            }

            //Checking if a source fell off the session list without doing a proper Closed event (*cough* spotify *cough*)
            var controlSessionIds = controlSessionList.Select(x => x.SourceAppUserModelId);
            var sessionsToRemove = new List<MediaSession>();

            foreach (var session in CurrentMediaSessions)
            {
                if (!controlSessionIds.Contains(session.Key))
                {
                    sessionsToRemove.Add(session.Value);
                }
            }

            sessionsToRemove.ForEach(x => x.Dispose());
        }


        internal bool RemoveSource(MediaSession mediaSession)
        {
            if (_CurrentMediaSessions.ContainsKey(mediaSession.Id))
            {
                _CurrentMediaSessions.Remove(mediaSession.Id);

                return true;
            }
            return false;
        }

        public void Dispose()
        {
            OnPlaybackStateUpdate = null;

            var keys = CurrentMediaSessions.Keys.ToList();
            foreach (var key in keys)
            {
                CurrentMediaSessions[key].Dispose();
            }
            _CurrentMediaSessions?.Clear();

            _IsStarted = false;
            _WindowsSessionManager.SessionsChanged -= onSessionsChanged;
            _WindowsSessionManager = null;
        }

        internal class MediaSession
        {
            public GlobalSystemMediaTransportControlsSession ControlSession { get => _ControlSession; }
            private GlobalSystemMediaTransportControlsSession _ControlSession;

            public readonly string Id;

            internal WindowsMediaManager MediaManagerInstance;

            internal MediaSession(GlobalSystemMediaTransportControlsSession controlSession, WindowsMediaManager mediaMangerInstance)
            {
                MediaManagerInstance = mediaMangerInstance;
                _ControlSession = controlSession;
                Id = _ControlSession.SourceAppUserModelId;

                _ControlSession.MediaPropertiesChanged += OnSongChange;
                //_ControlSession.TimelinePropertiesChanged TODO: windows playback timeline
            }

            internal async void OnSongChange(GlobalSystemMediaTransportControlsSession controlSession, MediaPropertiesChangedEventArgs args = null)
            {
                var mediaProperties = await controlSession.TryGetMediaPropertiesAsync();
                var timelineProperties = controlSession.GetTimelineProperties();

                var resp = new WindowsPlaybackResponse(timelineProperties, mediaProperties);

                try { MediaManagerInstance.OnPlaybackStateUpdate?.Invoke(resp); } catch { }
            }

            internal void Dispose()
            {
                if (MediaManagerInstance.RemoveSource(this))
                {
                    _ControlSession.MediaPropertiesChanged -= OnSongChange;
                    _ControlSession = null;
                }
            }
        }
    }

}
