using System.Windows.Controls;

namespace NowPlaying.Wpf.Controls.PlayingTrack
{
    public partial class Progress : UserControl
    {
        private double _progressPercentage;
        private double _oneBarWidth;

        /// <summary>
        /// 0 <= Progress <= 100
        /// </summary>
        public double ProgressPercentage
        {
            get
            {
                return _progressPercentage;
            }
            set 
            {
                _progressPercentage = _oneBarWidth * value;
                Dispatcher.Invoke(() => FillingLine.Width = _progressPercentage);
            } 
        }
        public Progress()
        {
            InitializeComponent();
            _oneBarWidth = this.Width / 100;
        }
    }
}
