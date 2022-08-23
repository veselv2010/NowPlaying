namespace NowPlaying.Wpf.Models
{
    public class UserSettingsModel : PropertyNotifier
    {
        public void UpdateProperties(string accountName, string gameName, string playbackProvider)
        {
            AccountName = accountName;
            GameName = gameName;
            PlaybackProvder = playbackProvider;
        }

        private string _accountName;
        private string _gameName;
        private string _playbackProvder;

        public string PlaybackProvder
        {
            get
            {
                return _playbackProvder;
            }
            set
            {
                _playbackProvder = value;
                OnPropertyChanged(nameof(PlaybackProvder));
            }
        }

        public string AccountName 
        { 
            get 
            {
                return _accountName; 
            }

            set
            {
                _accountName = value;
                OnPropertyChanged(nameof(AccountName));
            }
        }

        public string GameName
        {
            get
            {
                return _gameName;
            }

            set
            {
                _gameName = value;
                OnPropertyChanged(nameof(GameName));
            }
        }
    }
}
