using System.IO;
using System.Diagnostics;
using System.Linq;
using NowPlaying.Api.SpotifyResponses;

namespace NowPlaying
{
    internal class ConfigWriter
    {
        private string writePath { get; set; }
        private string writeConfigText { get; set; }

        public ConfigWriter(string writePath, string writeConfigText = "say [Spotify] Now Playing: {0}")
        {
            Process process = Process.GetProcessesByName("hl2").FirstOrDefault(); //прямой пример технического долга

            if (process != null)
                this.writePath = process.MainModule.FileName.Replace("hl2.exe", "") + "tf\\cfg\\audio.cfg";
            else
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

        public void RewriteKeyBinding(string line)
        {
            using (var sw = new StreamWriter(this.writePath))
                sw.WriteLine(line);
        }
    }
}