using System;

namespace NowPlaying.Wpf.Models
{
    public class AlbumCoverBackgroundModel : PropertyNotifier
    {
        public void UpdateProperties(string coverUrl)
        {
            var image = new Uri(coverUrl, UriKind.Absolute);
            Cover = image;
        }

        private Uri _cover;

        public Uri Cover 
        { 
            get
            {
                return _cover;
            }
            set
            {
                _cover = value;
                OnPropertyChanged(nameof(Cover));
            }
        }
    }
}