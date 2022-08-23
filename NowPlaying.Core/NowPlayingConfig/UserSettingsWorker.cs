using System.IO;
using Newtonsoft.Json;

namespace NowPlaying.Core.Settings
{
    public class UserSettingsWorker
    {
        private readonly string ConfigName = "NowPlayingConfig.json";

        public void SaveConfigFile(UserSettings settingsOnExit)
        {
            File.WriteAllText(ConfigName, JsonConvert.SerializeObject(settingsOnExit));
        }

        public UserSettings ReadConfigFile()
        {
            if (!File.Exists(ConfigName))
                CreateDefaultConfig();

            return JsonConvert.DeserializeObject<UserSettings>(File.ReadAllText(ConfigName));
        }
        private void CreateDefaultConfig()
        {
            var settings = new UserSettings
            {
                CfgText = @"say [Spotify] Now Playing: {0}",
                LastUsedKey = "",
                IsAutoSendEnabled = false,
                IsDebugModeEnabled = false,
                LastProvider = PlaybackStateProvider.WINDOWSRT
            };

            File.WriteAllText(ConfigName, JsonConvert.SerializeObject(settings));
        }
    }
}
