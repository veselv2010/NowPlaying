using System;
using System.Linq;

namespace NowPlaying
{
    internal static class UriExt
    {
        public static string GetPropertyValue(this Uri uri, string propertyName)
        {
            string[] urlParams = uri.ToString().Split(new char[] { '?', '&' });
            return urlParams.Single(p => p.Contains(propertyName + "="))
                                    .Split('=')[1]; // {propertyName}=*text*" split by '=', take *text*
        }

    }
}
