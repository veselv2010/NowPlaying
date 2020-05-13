using System.Windows.Controls;

namespace NowPlaying.Wpf.Controls.Header.Buttons
{
    public partial class LineButton : UserControl
    {
        public LineButton()
        {
            InitializeComponent();
        }

        private void LineButtonMouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            this.Line.Fill = ColorsConstants.MilkyGray;
        }

        private void LineButtonMouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            this.Line.Fill = ColorsConstants.DarkGray;
        }
    }
}
