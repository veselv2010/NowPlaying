using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace NowPlaying.Core.Settings
{
    public class UserSettingsWorker
    {
        private readonly string ConfigName = "NowPlayingConfig.json";

        public void SaveConfigFile(UserSettings settingsOnExit)
        {
            dynamic settings = new JObject();
            settings.CfgText = settingsOnExit.CfgText;
            settings.IsDebugModeEnabled = settingsOnExit.IsDebugModeEnabled;
            settings.LastUsedKey = settingsOnExit.LastUsedKey;
            settings.IsAutoSendEnabled = settingsOnExit.IsAutoSendEnabled;

            File.WriteAllText(ConfigName, settings.ToString());
        }

        public UserSettings ReadConfigFile()
        {
            if (!File.Exists(ConfigName))
                CreateDefaultConfig();         

            return JsonConvert.DeserializeObject<UserSettings>(File.ReadAllText(ConfigName));
        }
        private void CreateDefaultConfig()
        {
            dynamic settings = new JObject();
            settings.CfgText = @"say [Spotify] Now Playing: {0}";
            settings.LastUsedKey = "";
            settings.IsAutoSendEnabled = false;
            settings.IsDebugModeEnabled = false;

            File.WriteAllText(ConfigName, settings.ToString());
        }
    }
}
