using System.Windows.Controls;

namespace NowPlaying.Wpf.Controls.Header.Buttons
{
    public partial class CrossButton : UserControl
    {
        public CrossButton()
        {
            InitializeComponent();
        }

        private void CrossButtonMouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            this.Cross1.Fill = ColorsConstants.BrightRed;
            this.Cross2.Fill = ColorsConstants.BrightRed;
        }

        private void CrossButtonMouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            this.Cross1.Fill = ColorsConstants.MilkyGray;
            this.Cross2.Fill = ColorsConstants.MilkyGray;
        }
    }
}
