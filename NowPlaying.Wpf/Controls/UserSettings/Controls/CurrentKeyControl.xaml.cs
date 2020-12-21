using System.Windows.Controls;

namespace NowPlaying.Wpf.Controls.UserSettings.Controls
{
    public partial class CurrentKeyControl : UserControl
    {
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
                CurrentKeyTextBlock.Text = _currentKey;
            } 
        }
        public CurrentKeyControl()
        {
            InitializeComponent();
        }
    }
}
