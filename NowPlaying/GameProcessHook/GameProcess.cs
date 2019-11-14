using System;
using System.Diagnostics;
using System.Linq;

namespace NowPlaying.GameProcessHook
{
    public class GameProcess : ThreadComponent
    {
        private const string ProcessNameCsgo = "csgo";
        private const string ProcessNameTf2 = "hl2";
        private const string ProcessWindowNameCsgo = "Counter-Strike: Global Offensive";
        private const string ProcessWindowNameTf2 = "Team Fortress 2";

        protected override string ThreadName => "GameProcess";
        protected override TimeSpan ThreadFrameSleep { get; set; } = new TimeSpan(0, 0, 0, 0, 500);

        public Process Process { get; private set; }

        private IntPtr WindowHwnd { get; set; }

        private bool WindowActive { get; set; }

        public bool IsValid => WindowActive
                            && Process != null;

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

            AppInfo.State.WindowHandle = IsValid ? $"0x{(int)Process.Handle:X8}" : null;
        }

        private void InvalidateModules()
        {
            if (Process == null)
                return;

            Process.Dispose();
            Process = null;
        }

        private void InvalidateWindow()
        {
            WindowHwnd = IntPtr.Zero;
            WindowActive = false;
        }

        private bool EnsureProcessAndModules()
        {
            if (Process == null)
                Process = Process.GetProcessesByName(ProcessNameCsgo).FirstOrDefault();

            if (Process == null)
                Process = Process.GetProcessesByName(ProcessNameTf2).FirstOrDefault();

            if (Process == null || !Process.IsRunning())
                return false;

            return true;
        }

        private bool EnsureWindow()
        {
            WindowHwnd = WinAPIUser32Methods.FindWindow(null, ProcessWindowNameCsgo);

            if (WindowHwnd == IntPtr.Zero)
                WindowHwnd = WinAPIUser32Methods.FindWindow(null, ProcessWindowNameTf2);

            if (WindowHwnd == IntPtr.Zero)
                return false;

            WindowActive = WindowHwnd == WinAPIUser32Methods.GetForegroundWindow();

            return WindowActive;
        }
    }
}
