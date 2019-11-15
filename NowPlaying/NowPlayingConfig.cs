using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;

namespace NowPlaying
{
    class NowPlayingConfigWorker
    {
        private readonly string ConfigName = "NowPlayingConfig.json";

        public NowPlayingConfig Config;
        public NowPlayingConfigWorker()
        {
            ReadConfigFile();
            if (!Config.CfgText.Contains("{0}"))
            {
                Config.CfgText = "say [Spotify] Now Playing: {0}";
            }
        }
        private void MakeConfigFile()
        {
            dynamic Settings = new JObject();
            Settings.CfgText = @"say [Spotify] Now Playing: {0}";
            Settings.LastUsedKey = "";
            Settings.IsAutoSendEnabled = false;
            Settings.IsNightModeEnabled = false;
            Settings.IsDebugModeEnabled = false;

            File.WriteAllText(ConfigName, Settings.ToString());
        }

        public void SaveConfigFile(NowPlayingConfig SettingsOnExit)
        {
            dynamic Settings = new JObject();
            Settings.CfgText = Config.CfgText;
            Settings.IsDebugModeEnabled = Config.IsDebugModeEnabled;
            Settings.LastUsedKey = SettingsOnExit.LastUsedKey;
            Settings.IsAutoSendEnabled = SettingsOnExit.IsAutoSendEnabled;
            Settings.IsNightModeEnabled = SettingsOnExit.IsNightModeEnabled;

            File.WriteAllText(ConfigName, Settings.ToString());
        }

        private void ReadConfigFile()
        {
            if (File.Exists(ConfigName))
            {
                Config = JsonConvert.DeserializeObject<NowPlayingConfig>(File.ReadAllText(ConfigName));
            }
            else
            {
                MakeConfigFile();
                Config = JsonConvert.DeserializeObject<NowPlayingConfig>(File.ReadAllText(ConfigName));
            }
        }
    }

    public class NowPlayingConfig
    {
        public string CfgText { get; set; }
        public string LastUsedKey { get; set; }
        public bool IsAutoSendEnabled { get; set; }
        public bool IsNightModeEnabled { get; set; }
        public bool IsDebugModeEnabled { get; set; }
    }
}
