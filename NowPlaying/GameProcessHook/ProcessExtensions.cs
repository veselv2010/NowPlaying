using System;
using System.Diagnostics;

namespace NowPlaying.GameProcessHook
{
    public static class ProcessExtensions
    {
        /// <summary>
        /// Check if process is running.
        /// </summary>
        public static bool IsRunning(this Process process)
        {
            try
            {
                Process.GetProcessById(process.Id);
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
