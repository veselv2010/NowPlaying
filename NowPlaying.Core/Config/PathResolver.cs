using NowPlaying.Core.Steam;
using NowPlaying.Core.GameProcessHook;

namespace NowPlaying.Core.Config
{
    public class PathResolver
    {
        public string GetWritePath(GameProcessInfo processInfo,
            string userdataPath, string steamId3)
        {
            //switch (processInfo.Process)
            //{
            //    case SupportedProcess.csgo:
            //        return $"{steamInfo.UserdataPath}\\{steamId32}\\730\\local\\cfg\\audio.cfg";
            //    case SupportedProcess.hl2:
            //        return $"{processInfo.ProcessPath.Replace("hl2.exe", "")}\\tf\\cfg\\audio.cfg";
            //    default:
            //        throw new NotImplementedException();
            //}

            return $"{userdataPath}\\{steamId3}\\730\\local\\cfg\\audio.cfg";
        }
    }
}
