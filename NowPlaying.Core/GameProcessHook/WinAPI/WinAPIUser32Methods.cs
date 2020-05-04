using System;
using System.Runtime.InteropServices;

namespace NowPlaying.Core.GameProcessHook
{
    class WinAPIUser32Methods
    {
        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
    }
}
