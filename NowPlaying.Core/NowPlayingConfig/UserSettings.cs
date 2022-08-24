using Newtonsoft.Json;

namespace NowPlaying.Core.Settings
{
    public class UserSettings
    {
        [JsonProperty("CfgText")]
        public string CfgText { get; set; }
        [JsonProperty("LastUsedKey")]
        public string LastUsedKey { get; set; }
        [JsonProperty("IsAutoSendEnabled")]
        public bool IsAutoSendEnabled { get; set; }
        [JsonProperty("IsDebugModeEnabled")]
        public bool IsDebugModeEnabled { get; set; }
        [JsonProperty("LastProvider")]
        public PlaybackStateProvider LastProvider { get; set; }
    }
}
