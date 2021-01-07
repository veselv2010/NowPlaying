namespace NowPlaying.Wpf.Models
{
    public class CurrentKeyControlModel : PropertyNotifier
    {
        public void UpdateProperties(string key)
        {
            CurrentKey = key;
        }

        private string _currentKey;
        public string CurrentKey
        {
            get
            {
                return _currentKey;
            }
            set
            {
                _currentKey = value;
                OnPropertyChanged(nameof(CurrentKey));
            }
        }
    }
}
