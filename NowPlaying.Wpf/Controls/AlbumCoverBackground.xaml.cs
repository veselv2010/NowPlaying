using NowPlaying.Wpf.Models;
using System.Windows.Controls;
namespace NowPlaying.Wpf.Controls
{
    public partial class AlbumCoverBackground : UserControl
    {
        public AlbumCoverBackground()
        {
            InitializeComponent();
        }

        public void Update(string coverUrl)
        {
            var model = (AlbumCoverBackgroundModel)Resources["albumCoverBackgroundModel"];
            model.UpdateProperties(coverUrl);
        }
    }
}
