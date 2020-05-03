using System;
using System.Linq;

namespace NowPlaying.Extensions
{
    public class UriExtensions
    {
        public static string GetPropertyValue(string uri, string propertyName)
        {
            string[] urlParams = uri.Split(new char[] { '?', '&' });
            return urlParams.FirstOrDefault(p => p.Contains(propertyName + "="))
                                    .Split('=')[1]; // {propertyName}=*text*" split by '=', take *text*
        }
    }
}
