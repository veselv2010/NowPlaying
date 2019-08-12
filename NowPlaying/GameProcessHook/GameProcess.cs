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

        protected override string ThreadName => nameof(GameProcess);
        protected override TimeSpan ThreadFrameSleep { get; set; } = new TimeSpan(0, 0, 0, 0, 500);

        /// <summary>
        /// Game process.
        /// </summary>
        public Process Process { get; private set; }

        /// <summary>
        /// Game window handle.
        /// </summary>
        private IntPtr WindowHwnd { get; set; }

        /// <summary>
        /// Whether game window is active.
        /// </summary>
        private bool WindowActive { get; set; }

        /// <summary>
        /// Is game process valid?
        /// </summary>
        public bool IsValid => WindowActive && !(Process is null);

        public override void Dispose()
        {
            InvalidateWindow();
            InvalidateModules();

            base.Dispose();
        }


        /// <inheritdoc />
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

        /// <summary>
        /// Invalidate all game modules.
        /// </summary>
        private void InvalidateModules()
        {
            Process?.Dispose();
            Process = default;
        }

        /// <summary>
        /// Invalidate game window.
        /// </summary>
        private void InvalidateWindow()
        {
            WindowHwnd = IntPtr.Zero;
            WindowActive = false;
        }

        /// <summary>
        /// Ensure game process and modules.
        /// </summary>
        private bool EnsureProcessAndModules()
        {
            if (Process is null)
            {
                Process = Process.GetProcessesByName(ProcessName).FirstOrDefault();
            }
            if (Process is null || !Process.IsRunning())
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Ensure game window.
        /// </summary>
        private bool EnsureWindow()
        {
            WindowHwnd = WinAPIUser32Methods.FindWindow(null, ProcessWindowName);
            if (WindowHwnd == IntPtr.Zero)
            {
                return false;
            }

            WindowActive = WindowHwnd == WinAPIUser32Methods.GetForegroundWindow();

            return WindowActive;
        }
    }
}
