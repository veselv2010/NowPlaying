using System;
using System.Linq;
using System.Threading;

namespace NowPlaying.GameProcessHook
{
    public static class GameProcessHookHelper
    {
        /// <summary>
        /// Get if process is running.
        /// </summary>
        public static bool IsRunning(this System.Diagnostics.Process process)
        {
            try
            {
                System.Diagnostics.Process.GetProcessById(process.Id);
            }
            catch (InvalidOperationException)
            {
                return false;
            }
            catch (ArgumentException)
            {
                return false;
            }
            return true;
        }
    }
}
