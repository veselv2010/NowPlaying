using System.Linq;

namespace NowPlaying
{
    public static class TrackNameFormatter
    {
        public static string FormatForWriting(string name)
        {
            return GlobalVariables.Ru2En.Aggregate(name, (current, value) => current.Replace(value.Key, value.Value));
        }

    }
}