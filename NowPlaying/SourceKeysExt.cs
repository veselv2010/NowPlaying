using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace NowPlaying
{
    public class SourceKeysExt
    {
        public static bool TryOpenSourceKeysFile()
        {
            if (File.Exists("SourceKeys.txt"))
            {
                Process.Start("SourceKeys.txt");
                return true;
            }

            return false;
        }

        #region Source Keys

        public static List<string> SourceEngineAllowedKeys = new List<string>() //todo
        {
            {""},
            {"space"},
            {"capslock"},
            {"escape"},
            {"f1"},
            {"f2"},
            {"f3"},
            {"f4"},
            {"f5"},
            {"f6"},
            {"f7"},
            {"f8"},
            {"f9"},
            {"f10"},
            {"f11"},
            {"f12"},
            {"pause"},
            {"`"},
            {"-"},
            {"="},
            {"backspace"},
            {"tab"},
            {"]"},
            {"["},
            {"/"},
            {"semicolon"},
            {"'"},
            {"\\"},
            {"shift"},
            {"enter"},
            {","},
            {"ctrl"},
            {"alt"},
            {"1"},
            {"2"},
            {"3"},
            {"4"},
            {"5"},
            {"6"},
            {"7"},
            {"8"},
            {"9"},
            {"0"},
            {"a"},
            {"b"},
            {"c"},
            {"d"},
            {"e"},
            {"f"},
            {"g"},
            {"h"},
            {"i"},
            {"j"},
            {"k"},
            {"l"},
            {"m"},
            {"n"},
            {"o"},
            {"p"},
            {"q"},
            {"r"},
            {"s"},
            {"t"},
            {"u"},
            {"v"},
            {"w"},
            {"x"},
            {"y"},
            {"z"},
            {"uparrow"},
            {"downarrow"},
            {"rightarrow"},
            {"leftarrow"},
            {"ins"},
            {"home"},
            {"pgup"},
            {"pgdn"},
            {"del"},
            {"end"},
            {"mouse1"},
            {"mouse2"},
            {"mouse3"},
            {"mouse4"},
            {"mouse5"},
            {"mwheelup"},
            {"mwheeldown"},
            {"kp_end"},
            {"kp_downarrow"},
            {"kp_pgdn"},
            {"kp_leftarrow"},
            {"kp_5"},
            {"kp_rightarrow"},
            {"kp_home"},
            {"kp_uparrow"},
            {"kp_pgup"},
            {"kp_ins"},
            {"kp_plus "},
            {"kp_minus"},
            {"kp_slash"},
            {"kp_del"},
            {"*"},
            {"kp_enter"}
        };

        #endregion
    }
}