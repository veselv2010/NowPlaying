using System;

namespace NowPlaying.Core.NowPlayingConfig
{
    public class Config
    {
        public string CfgText { get; set; }
        public string LastUsedKey { get; set; }
        public bool IsAutoSendEnabled { get; set; }
        public bool IsNightModeEnabled { get; set; }
        public bool IsDebugModeEnabled { get; set; }
    }
}
