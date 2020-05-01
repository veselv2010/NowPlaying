using System;
using System.Linq;

namespace NowPlaying.Models
{
    class AuthPresenterModel
    {
        private readonly string url;
        public AuthPresenterModel(string url)
        {
            this.url = url;
        }

        public string GetPropertyValue(string uri, string propertyName)
        {
            string[] urlParams = uri.Split(new char[] { '?', '&' });
            return urlParams.FirstOrDefault(p => p.Contains(propertyName + "="))
                                    .Split('=')[1]; // {propertyName}=*text*" split by '=', take *text*
        }
    }
}
