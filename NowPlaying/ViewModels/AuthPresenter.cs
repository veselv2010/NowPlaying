using System;

namespace NowPlaying.ViewModels
{
    class AuthPresenter : ObservableObject
    {
        private string currentUrl;
        public string CurrentUrl
        {
            get { return currentUrl; }
            set
            {
                currentUrl = value;
                RaisePropertyChangedEvent(nameof(currentUrl));
            }
        }
    }
}
