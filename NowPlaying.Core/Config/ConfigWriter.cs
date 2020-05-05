using System.IO;
using NowPlaying.Core.Api.SpotifyResponses;

namespace NowPlaying.Core.Config
{
    public class ConfigWriter
    {
        private readonly string writePath;
        private readonly string writeConfigText;

        public ConfigWriter(string writePath,
            string writeConfigText = "say [Spotify] Now Playing: {0}")
        {
            this.writePath = writePath;
            this.writeConfigText = writeConfigText;
        }

        public void RewriteKeyBinding(CurrentTrackResponse currentTrack)
        {
            int indexOfAudioCfg = this.writePath.IndexOf(@"\audio.cfg");
            Directory.CreateDirectory(this.writePath.Remove(indexOfAudioCfg));

            if (!File.Exists(this.writePath))
                File.CreateText(this.writePath).Dispose();

            string strForWriting = string.Format(this.writeConfigText, currentTrack.FullName);

            this.RewriteKeyBinding(strForWriting);
        }

        private void RewriteKeyBinding(string line)
        {
            using (var sw = new StreamWriter(this.writePath))
                sw.WriteLine(line);
        }
    }
}