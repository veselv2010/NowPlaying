using System;
using System.Linq;

namespace NowPlaying
{
    internal static class UriExt
    {
        public static string GetPropertyValue(string uri, string propertyName)
        {
            string[] urlParams = uri.Split(new char[] { '?', '&' });
            return urlParams.FirstOrDefault(p => p.Contains(propertyName + "="))
                                    .Split('=')[1]; // {propertyName}=*text*" split by '=', take *text*
        }

    }
}
