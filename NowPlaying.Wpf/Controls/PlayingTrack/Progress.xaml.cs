using NowPlaying.Wpf.Models;
using System.Windows.Controls;

namespace NowPlaying.Wpf.Controls.PlayingTrack
{
    public partial class Progress : UserControl
    {
        public Progress()
        {
            InitializeComponent();
        }

        public void Update(double progressMs)
        {
            var progress = (ProgressModel)Resources["progressModel"];
            progress.UpdateProperties(progressMs, 360); //TODO
        }
    }
}
