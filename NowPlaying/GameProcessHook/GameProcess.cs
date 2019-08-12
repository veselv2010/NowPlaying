using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;

namespace NowPlaying.GameProcessHook
{
    public class GameProcess : ThreadComponent
    {
        private const string ProcessName = "csgo";
        private const string ProcessWindowName = "Counter-Strike: Global Offensive";

        protected override string ThreadName => "GameProcess";
        protected override TimeSpan ThreadFrameSleep { get; set; } = new TimeSpan(0, 0, 0, 0, 500);

        public Process Process { get; private set; }

        private IntPtr WindowHwnd { get; set; }

        private bool WindowActive { get; set; }

        public bool IsValid => WindowActive && !(Process is null);

        public override void Dispose()
        {
            InvalidateWindow();
            InvalidateModules();

            base.Dispose();
        }

        protected override void FrameAction()
        {
            if (!EnsureProcessAndModules())
            {
                InvalidateModules();
            }

            if (!EnsureWindow())
            {
                InvalidateWindow();
            }

            AppInfo.State.WindowHandle =  IsValid ? $"0x{(int)Process.Handle:X8}" : null;  
        }

        private void InvalidateModules()
        {
            Process?.Dispose();
            Process = default;
        }

        private void InvalidateWindow()
        {
            WindowHwnd = IntPtr.Zero;
            WindowActive = false;
        }

        private bool EnsureProcessAndModules()
        {
            if (Process == null)
                Process = Process.GetProcessesByName(ProcessName).FirstOrDefault();

            if (Process == null || !Process.IsRunning())
                return false;

            return true;
        }

        private bool EnsureWindow()
        {
            WindowHwnd = WinAPIUser32Methods.FindWindow(null, ProcessWindowName);

            if (WindowHwnd == IntPtr.Zero)
                return false;

            WindowActive = WindowHwnd == WinAPIUser32Methods.GetForegroundWindow();

            return WindowActive;
        }
    }
}
