using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace NowPlaying.Core.NowPlayingConfig
{
    public class ConfigWorker
    {
        private readonly string ConfigName = "NowPlayingConfig.json";

        public void SaveConfigFile(Config settingsOnExit)
        {
            dynamic settings = new JObject();
            settings.CfgText = settingsOnExit.CfgText;
            settings.IsDebugModeEnabled = settingsOnExit.IsDebugModeEnabled;
            settings.LastUsedKey = settingsOnExit.LastUsedKey;
            settings.IsAutoSendEnabled = settingsOnExit.IsAutoSendEnabled;
            settings.IsNightModeEnabled = settingsOnExit.IsNightModeEnabled;

            File.WriteAllText(ConfigName, settings.ToString());
        }

        public Config ReadConfigFile()
        {
            if (!File.Exists(ConfigName))
                CreateDefaultConfig();         

            return JsonConvert.DeserializeObject<Config>(File.ReadAllText(ConfigName));
        }
        private void CreateDefaultConfig()
        {
            dynamic Settings = new JObject();
            Settings.CfgText = @"say [Spotify] Now Playing: {0}";
            Settings.LastUsedKey = "";
            Settings.IsAutoSendEnabled = false;
            Settings.IsNightModeEnabled = false;
            Settings.IsDebugModeEnabled = false;

            File.WriteAllText(ConfigName, Settings.ToString());
        }
    }
}
