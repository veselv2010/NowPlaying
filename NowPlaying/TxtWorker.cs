using System;
using System.Text;
using System.IO;

namespace NowPlaying
{
    public class ConfigWriter
    {
        public string WritePath { get; private set; }

        private const string WriteConfigText = "bind \"{1}\" \"say [Spotify] Now Playing: {0}\"";

        public ConfigWriter(string writePath)
        {
            WritePath = writePath;
        }

        public void Write(string text)
        {
            // if (WritePath == null) return;

            using (StreamWriter sw = new StreamWriter(WritePath, false, Encoding.GetEncoding(28591)))
                sw.WriteLine(string.Format(WriteConfigText, text, ConsoleExtensions.chatButton));
        }
    }
}