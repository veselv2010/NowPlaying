using System.IO;
using NowPlaying.ApiResponses;

namespace NowPlaying
{
	internal class ConfigWriter
    {
        public string WritePath { get; set; }
        public string BindKey { get; set; } = "";
        private const string WriteConfigText = "bind \"{1}\" \"say [Spotify] Now Playing: {0}\"";

        public ConfigWriter(string bindKey, string writePath)
        {
            this.BindKey = bindKey;
            this.WritePath = writePath;

            int IndexOfAudioCfg = this.WritePath.IndexOf(@"\audio.cfg");
            Directory.CreateDirectory(this.WritePath.Remove(IndexOfAudioCfg));

            if (!File.Exists(this.WritePath))
                File.CreateText(this.WritePath).Dispose();
        }

        public void RewriteKeyBinding(CurrentTrackResponse currentTrack)
        {
            string strForWriting = string.Format(WriteConfigText, currentTrack.FormattedName, this.BindKey);

            using (var sw = new StreamWriter(this.WritePath))
                sw.WriteLine(strForWriting);
        }
    }
}