namespace NowPlaying.Wpf.Models
{
    public class UserSettingsModel : PropertyNotifier
    {
        public void UpdateProperties(string accountName, string gameName)
        {
            AccountName = accountName;
            GameName = gameName;
        }

        private string _accountName;
        private string _gameName;

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
