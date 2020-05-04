using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace NowPlaying.Core.GameProcessHook
{
    public class GameProcess : ThreadComponent
    {
        public enum SupportedProcess
        {
            csgo,
            hl2
        }

        private IDictionary<SupportedProcess, string> gameInfo = new Dictionary<SupportedProcess, string>
        {
            { SupportedProcess.csgo, "Counter-Strike: Global Offensive" },
            { SupportedProcess.hl2, "Team Fortress 2" }
        };

        protected override string ThreadName => "GameProcess";
        protected override TimeSpan ThreadFrameSleep { get; set; } = new TimeSpan(0, 0, 0, 0, 500);

        private Process Process { get; set; }

        private IntPtr WindowHwnd { get; set; }

        private bool WindowActive { get; set; }

        public bool IsValid => WindowActive
                            && Process != null;

        public string CurrentProcess;

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

            CurrentProcess = Process.ProcessName;
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
            foreach(var s in gameInfo)
                Process = Process.GetProcessesByName(nameof(s.Key)).FirstOrDefault();          

            if (Process == null || !Process.IsRunning())
                return false;

            return true;
        }

        private bool EnsureWindow()
        {
            foreach (var s in gameInfo)
                WindowHwnd = WinAPIUser32Methods.FindWindow(null, s.Value);

            if (WindowHwnd == IntPtr.Zero)
                return false;

            WindowActive = WindowHwnd == WinAPIUser32Methods.GetForegroundWindow();

            return WindowActive;
        }
    }
}
