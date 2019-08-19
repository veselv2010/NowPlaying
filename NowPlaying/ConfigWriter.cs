using System.IO;
using NowPlaying.ApiResponses;

namespace NowPlaying
{
    internal class ConfigWriter
    {
        private string WritePath { get; set; }

        private const string WriteConfigText = "say \"[Spotify] Now Playing: {0}\"";

        public ConfigWriter(string writePath)
        {
            this.WritePath = writePath;

            int indexOfAudioCfg = this.WritePath.IndexOf(@"\audio.cfg");
            Directory.CreateDirectory(this.WritePath.Remove(indexOfAudioCfg));

            if (!File.Exists(this.WritePath))
                File.CreateText(this.WritePath).Dispose();
        }

        public void RewriteKeyBinding(CurrentTrackResponse currentTrack)
        {
            string strForWriting = string.Format(ConfigWriter.WriteConfigText, currentTrack.FullName);

            this.RewriteKeyBinding(strForWriting);
        }

        public void RewriteKeyBinding(string line)
        {
            using (var sw = new StreamWriter(this.WritePath))
                sw.WriteLine(line);
        }
    }
}