using System;
using System.Text;
using System.IO;

namespace NowPlaying
{
    public class ConfigWriter
    {
        public static string WritePath = ConsoleExtensions.SelectConfigDirectoryMessage();

        private const string WriteConfigText = "bind \"{1}\" \"say [Spotify] Now Playing: {0}\"";
        
        public static void Write()
        {
            // if (WritePath == null) return;

            string formattedTrackName = TrackNameFormatter.FormatForWriting(Spotify.CurrentTrack());

            using (StreamWriter sw = new StreamWriter(WritePath, false, Encoding.GetEncoding(28591)))
                sw.WriteLine(string.Format(WriteConfigText, formattedTrackName, ConsoleExtensions.chatButton));
        }
    }
}


//=_-icon by SCOUTPAN-_=