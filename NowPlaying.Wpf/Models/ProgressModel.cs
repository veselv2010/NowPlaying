namespace NowPlaying.Wpf.Models
{
    public class ProgressModel : PropertyNotifier
    {
        public void UpdateProperties(double progressMs, double barWidth)
        {
            _oneBarWidth = barWidth / 100;
            ProgressPercentage = progressMs;
        }

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
                OnPropertyChanged(nameof(ProgressPercentage));
            }
        }
    }
}
