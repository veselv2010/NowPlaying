using System;
using System.Linq;

namespace NowPlaying.Models
{
    class AuthPresenterModel
    {
        private readonly string authPageAddress;
        //private readonly string url;
        public AuthPresenterModel(string url, string clientId, string redirectUrl)
        {
            //this.url = url;
            this.authPageAddress = $"https://accounts.spotify.com/authorize" +
                $"?client_id={clientId}" +
                $"&redirect_uri={redirectUrl}" +
                $"&response_type=code" +
                $"&scope=user-read-playback-state";
        }

        public string GetPropertyValue(string uri, string propertyName = "code")
        {
            string[] urlParams = uri.Split(new char[] { '?', '&' });
            return urlParams.FirstOrDefault(p => p.Contains(propertyName + "="))
                                    .Split('=')[1]; // {propertyName}=*text*" split by '=', take *text*
        }
    }
}
