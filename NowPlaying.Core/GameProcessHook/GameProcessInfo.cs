namespace NowPlaying.Core.GameProcessHook
{
    public class GameProcessInfo
    {
        public GameProcessInfo(SupportedProcess process, string windowName)
        {
            this.Process = process;
            this.WindowName = windowName;
        }
        public SupportedProcess Process { get; }
        public string WindowName { get;  }
        public string ProcessPath { get; set; }
    }
}
