namespace NowPlaying.Core.Settings
{
    public class UserSettings
    {
        public string CfgText { get; set; }
        public string LastUsedKey { get; set; }
        public bool IsAutoSendEnabled { get; set; }
        public bool IsDebugModeEnabled { get; set; }

        public PlaybackStateProvider LastProvider { get; set; }
    }
}
