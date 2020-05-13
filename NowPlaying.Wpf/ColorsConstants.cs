using System.Windows.Media;

namespace NowPlaying.Wpf
{
    public static class ColorsConstants
    {
        public static readonly SolidColorBrush SpotifyGreen = new SolidColorBrush(Color.FromArgb(0xFF, 0x1D, 0xB9, 0x54));
        public static readonly SolidColorBrush BrightRed = new SolidColorBrush(Color.FromArgb(0xFF, 0xFF, 0x40, 0x40));
        public static readonly SolidColorBrush Transparent = new SolidColorBrush(Color.FromArgb(0x00, 0x00, 0x00, 0x00));
        public static readonly SolidColorBrush BlackThemeBackground = new SolidColorBrush(Color.FromRgb(0x17, 0x17, 0x17));

        /// <summary>
        /// White theme background, Black theme "npc work", "Current track", textbox text color
        /// </summary>
        public static readonly SolidColorBrush White = new SolidColorBrush(Color.FromRgb(0xF9, 0xF9, 0xF9));

        /// <summary>
        /// White theme borders color
        /// </summary>
        public static readonly SolidColorBrush MilkyGrayBorder = new SolidColorBrush(Color.FromRgb(0xB3, 0xB3, 0xB3));

        /// <summary>
        /// Black theme font color
        /// </summary>
        public static readonly SolidColorBrush MilkyGray = new SolidColorBrush(Color.FromRgb(0xB2, 0xB2, 0xB2));

        /// <summary>
        /// White theme font color
        /// </summary>
        public static readonly SolidColorBrush DarkGray = new SolidColorBrush(Color.FromRgb(0x7E, 0x7E, 0x7E));

        /// <summary>
        /// White theme "npc work", "Current track", textbox text color
        /// </summary>
        public static readonly SolidColorBrush AlmostBlack = new SolidColorBrush(Color.FromRgb(0x46, 0x46, 0x46));
    }
}
