using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace NowPlaying.Core.GameProcessHook
{
    public enum SupportedProcess
    {
        csgo,
        hl2
    }

    public class GameProcess : ThreadComponent
    {
        private GameProcessInfo[] gameInfo = new GameProcessInfo[]
        {
            new GameProcessInfo(SupportedProcess.csgo, "Counter-Strike: Global Offensive"),
            // new GameProcessInfo(SupportedProcess.hl2, "Team Fortress 2")
        };

        protected override string ThreadName => nameof(GameProcess);
        protected override TimeSpan ThreadFrameSleep { get; set; } = new TimeSpan(0, 0, 0, 0, 500);

        private Process Process { get; set; }

        private IntPtr WindowHwnd { get; set; }

        private SupportedProcess currentProcess;

        private bool WindowActive { get; set; }

        public bool IsValid => WindowActive
                            && Process != null;

        public GameProcessInfo CurrentProcess { get; set; }
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

            //if (CurrentProcess.Process.ToString() != Process.ProcessName)
            //{
            //    CurrentProcess = gameInfo[GetGameInfoIndex(currentProcess)];
            //    CurrentProcess.ProcessPath = Process.MainModule.FileName;
            //}
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
            //foreach(var s in gameInfo)
            //{
            //    Process = Process.GetProcessesByName(nameof(s.Process)).FirstOrDefault();
            //    currentProcess = (Process == null) ? 0 : s.Process;
            //}

            Process = Process.GetProcessesByName("csgo").FirstOrDefault();

            if (Process == null || !Process.IsRunning())
                return false;

            return true;
        }

        private bool EnsureWindow()
        {
            foreach (var s in gameInfo)
                WindowHwnd = WinAPIUser32Methods.FindWindow(null, s.WindowName);

            if (WindowHwnd == IntPtr.Zero)
                return false;

            WindowActive = WindowHwnd == WinAPIUser32Methods.GetForegroundWindow();

            return WindowActive;
        }

        private int GetGameInfoIndex(SupportedProcess process)
        {
            int i = 0;
            foreach (var elem in gameInfo)
                if (elem.Process == process)
                    return i;
                else
                    i++;

            throw new KeyNotFoundException();
        }
    }
}
