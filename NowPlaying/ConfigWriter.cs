using System.Text;
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
            if (!File.Exists(WritePath))
                File.CreateText(WritePath).Dispose();
        }

        public void RewriteKeyBinding(CurrentTrackResponse currentTrack)
        {
            string strForWriting = string.Format(WriteConfigText, currentTrack.FormattedName, this.BindKey);

            using (StreamWriter sw = new StreamWriter(this.WritePath))
                sw.WriteLine(strForWriting);
        }
    }
}