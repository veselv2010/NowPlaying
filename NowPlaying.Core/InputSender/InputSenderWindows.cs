using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace NowPlaying.Core.InputSender
{
    public sealed class InputSenderWindows : IInputSender
    {
        /// <summary>
        /// </summary>
        /// <param name="virtualKey">This needs to be converted by VirtualKeyFromKey(Wpf-key) (there is no System.Windows.Input.KeyInterop in netStandard2.0)</param>
        public void SendSystemInput(ushort virtualKey)
        {
            INPUT[] pInputs = new INPUT[1];
            INPUT input = new INPUT();

            input.type = 1; // 1 = Keyboard Input
            input.U.ki.wVk = virtualKey;
            input.U.ki.wScan = GetScanKey(virtualKey);
            input.U.ki.dwFlags = KEYEVENTF.SCANCODE;
            pInputs[0] = input;

            SendInput(1, pInputs, INPUT.Size);

            input.U.ki.dwFlags = KEYEVENTF.KEYUP;
            pInputs[0] = input;

            SendInput(1, pInputs, INPUT.Size);
        }

        [DllImport("user32.dll")]
        private static extern uint SendInput(uint nInputs, [MarshalAs(UnmanagedType.LPArray), In] INPUT[] pInputs, int cbSize);

        [StructLayout(LayoutKind.Sequential)]
        private struct INPUT
        {
            internal uint type;
            internal InputUnion U;
            internal static int Size
            {
                get { return Marshal.SizeOf(typeof(INPUT)); }
            }
        }

        [StructLayout(LayoutKind.Explicit)]
        private struct InputUnion
        {
            [FieldOffset(0)]
            internal MOUSEINPUT mi;
            [FieldOffset(0)]
            internal KEYBDINPUT ki;
            [FieldOffset(0)]
            internal HARDWAREINPUT hi;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct MOUSEINPUT
        {
            internal int dx;
            internal int dy;
            internal MouseEventDataXButtons mouseData;
            internal MOUSEEVENTF dwFlags;
            internal uint time;
            internal UIntPtr dwExtraInfo;
        }

        [Flags]
        private enum MouseEventDataXButtons : uint
        {
            Nothing = 0x00000000,
            XBUTTON1 = 0x00000001,
            XBUTTON2 = 0x00000002
        }

        [Flags]
        private enum MOUSEEVENTF : uint
        {
            ABSOLUTE = 0x8000,
            HWHEEL = 0x01000,
            MOVE = 0x0001,
            MOVE_NOCOALESCE = 0x2000,
            LEFTDOWN = 0x0002,
            LEFTUP = 0x0004,
            RIGHTDOWN = 0x0008,
            RIGHTUP = 0x0010,
            MIDDLEDOWN = 0x0020,
            MIDDLEUP = 0x0040,
            VIRTUALDESK = 0x4000,
            WHEEL = 0x0800,
            XDOWN = 0x0080,
            XUP = 0x0100
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct KEYBDINPUT
        {
            internal ushort wVk;
            internal ScanCodeShort wScan;
            internal KEYEVENTF dwFlags;
            internal int time;
            internal UIntPtr dwExtraInfo;
        }

        [Flags]
        private enum KEYEVENTF : uint
        {
            EXTENDEDKEY = 0x0001,
            KEYUP = 0x0002,
            SCANCODE = 0x0008,
            UNICODE = 0x0004
        }

        private enum ScanCodeShort : short
        {
            LBUTTON = 0,
            RBUTTON = 0,
            CANCEL = 70,
            MBUTTON = 0,
            XBUTTON1 = 0,
            XBUTTON2 = 0,
            BACK = 14,
            TAB = 15,
            CLEAR = 76,
            RETURN = 28,
            SHIFT = 42,
            CONTROL = 29,
            MENU = 56,
            PAUSE = 0,
            CAPITAL = 58,
            KANA = 0,
            HANGUL = 0,
            JUNJA = 0,
            FINAL = 0,
            HANJA = 0,
            KANJI = 0,
            ESCAPE = 1,
            CONVERT = 0,
            NONCONVERT = 0,
            ACCEPT = 0,
            MODECHANGE = 0,
            SPACE = 57,
            PRIOR = 73,
            NEXT = 81,
            END = 79,
            HOME = 71,
            LEFT = 75,
            UP = 72,
            RIGHT = 77,
            DOWN = 80,
            SELECT = 0,
            PRINT = 0,
            EXECUTE = 0,
            SNAPSHOT = 84,
            INSERT = 82,
            DELETE = 83,
            HELP = 99,
            KEY_0 = 11,
            KEY_1 = 2,
            KEY_2 = 3,
            KEY_3 = 4,
            KEY_4 = 5,
            KEY_5 = 6,
            KEY_6 = 7,
            KEY_7 = 8,
            KEY_8 = 9,
            KEY_9 = 10,
            KEY_A = 30,
            KEY_B = 48,
            KEY_C = 46,
            KEY_D = 32,
            KEY_E = 18,
            KEY_F = 33,
            KEY_G = 34,
            KEY_H = 35,
            KEY_I = 23,
            KEY_J = 36,
            KEY_K = 37,
            KEY_L = 38,
            KEY_M = 50,
            KEY_N = 49,
            KEY_O = 24,
            KEY_P = 25,
            KEY_Q = 16,
            KEY_R = 19,
            KEY_S = 31,
            KEY_T = 20,
            KEY_U = 22,
            KEY_V = 47,
            KEY_W = 17,
            KEY_X = 45,
            KEY_Y = 21,
            KEY_Z = 44,
            LWIN = 91,
            RWIN = 92,
            APPS = 93,
            SLEEP = 95,
            NUMPAD0 = 82,
            NUMPAD1 = 79,
            NUMPAD2 = 80,
            NUMPAD3 = 81,
            NUMPAD4 = 75,
            NUMPAD5 = 76,
            NUMPAD6 = 77,
            NUMPAD7 = 71,
            NUMPAD8 = 72,
            NUMPAD9 = 73,
            MULTIPLY = 55,
            ADD = 78,
            SEPARATOR = 0,
            SUBTRACT = 74,
            DECIMAL = 83,
            DIVIDE = 53,
            F1 = 59,
            F2 = 60,
            F3 = 61,
            F4 = 62,
            F5 = 63,
            F6 = 64,
            F7 = 65,
            F8 = 66,
            F9 = 67,
            F10 = 68,
            F11 = 87,
            F12 = 88,
            F13 = 100,
            F14 = 101,
            F15 = 102,
            F16 = 103,
            F17 = 104,
            F18 = 105,
            F19 = 106,
            F20 = 107,
            F21 = 108,
            F22 = 109,
            F23 = 110,
            F24 = 118,
            NUMLOCK = 69,
            SCROLL = 70,
            LSHIFT = 42,
            RSHIFT = 54,
            LCONTROL = 29,
            RCONTROL = 29,
            LMENU = 56,
            RMENU = 56,
            BROWSER_BACK = 106,
            BROWSER_FORWARD = 105,
            BROWSER_REFRESH = 103,
            BROWSER_STOP = 104,
            BROWSER_SEARCH = 101,
            BROWSER_FAVORITES = 102,
            BROWSER_HOME = 50,
            VOLUME_MUTE = 32,
            VOLUME_DOWN = 46,
            VOLUME_UP = 48,
            MEDIA_NEXT_TRACK = 25,
            MEDIA_PREV_TRACK = 16,
            MEDIA_STOP = 36,
            MEDIA_PLAY_PAUSE = 34,
            LAUNCH_MAIL = 108,
            LAUNCH_MEDIA_SELECT = 109,
            LAUNCH_APP1 = 107,
            LAUNCH_APP2 = 33,
            OEM_1 = 39,
            OEM_PLUS = 13,
            OEM_COMMA = 51,
            OEM_MINUS = 12,
            OEM_PERIOD = 52,
            OEM_2 = 53,
            OEM_3 = 41,
            OEM_4 = 26,
            OEM_5 = 43,
            OEM_6 = 27,
            OEM_7 = 40,
            OEM_8 = 0,
            OEM_102 = 86,
            PROCESSKEY = 0,
            PACKET = 0,
            ATTN = 0,
            CRSEL = 0,
            EXSEL = 0,
            EREOF = 93,
            PLAY = 0,
            ZOOM = 98,
            NONAME = 0,
            PA1 = 0,
            OEM_CLEAR = 0,
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct HARDWAREINPUT
        {
            internal int uMsg;
            internal short wParamL;
            internal short wParamH;
        }

        private ScanCodeShort GetScanKey(ushort key)
        {
            KeyValuePairs.TryGetValue(key, out ScanCodeShort KeyCode);
            return KeyCode;
        }

        /// <summary>
        /// Hardcoded System.Windows.Forms.Keys enum to ScanCodeShort
        /// </summary>
        private readonly Dictionary<ushort, ScanCodeShort> KeyValuePairs = new Dictionary<ushort, ScanCodeShort>
        {
            {32, ScanCodeShort.SPACE},
          //{"capslock"},
            {27, ScanCodeShort.ESCAPE},
            {112, ScanCodeShort.F1},
            {113, ScanCodeShort.F2},
            {114, ScanCodeShort.F3},
            {115, ScanCodeShort.F4},
            {116, ScanCodeShort.F5},
            {117, ScanCodeShort.F6},
            {118, ScanCodeShort.F7},
            {119, ScanCodeShort.F8},
            {120, ScanCodeShort.F9},
            {121, ScanCodeShort.F10},
            {122, ScanCodeShort.F11},
            {123, ScanCodeShort.F12},
            {19, ScanCodeShort.PAUSE},
            {192, ScanCodeShort.OEM_3},
            {189, ScanCodeShort.OEM_MINUS},
            //{"=", ScanCodeShort.},
            {8, ScanCodeShort.BACK},
            {9, ScanCodeShort.TAB},
            {221, ScanCodeShort.OEM_6},
            {219, ScanCodeShort.OEM_4},
            //{"/"},
            {186, ScanCodeShort.OEM_1},
            {222, ScanCodeShort.OEM_7},
            {226, ScanCodeShort.OEM_102},
            {16, ScanCodeShort.SHIFT},
            //{"enter"},
            //{","},
            {17, ScanCodeShort.CONTROL}, //same as below
            {18, ScanCodeShort.MENU}, //lmenu rmenu
            {49, ScanCodeShort.KEY_1},
            {50, ScanCodeShort.KEY_2},
            {51, ScanCodeShort.KEY_3},
            {52, ScanCodeShort.KEY_4},
            {53, ScanCodeShort.KEY_5},
            {54, ScanCodeShort.KEY_6},
            {55, ScanCodeShort.KEY_7},
            {56, ScanCodeShort.KEY_8},
            {57, ScanCodeShort.KEY_9},
            {48, ScanCodeShort.KEY_0},
            {65, ScanCodeShort.KEY_A},
            {66, ScanCodeShort.KEY_B},
            {67, ScanCodeShort.KEY_C},
            {68, ScanCodeShort.KEY_D},
            {69, ScanCodeShort.KEY_E},
            {70, ScanCodeShort.KEY_F},
            {71, ScanCodeShort.KEY_G},
            {72, ScanCodeShort.KEY_H},
            {73, ScanCodeShort.KEY_I},
            {74, ScanCodeShort.KEY_J},
            {75, ScanCodeShort.KEY_K},
            {76, ScanCodeShort.KEY_L},
            {77, ScanCodeShort.KEY_M},
            {78, ScanCodeShort.KEY_N},
            {79, ScanCodeShort.KEY_O},
            {80, ScanCodeShort.KEY_P},
            {81, ScanCodeShort.KEY_Q},
            {82, ScanCodeShort.KEY_R},
            {83, ScanCodeShort.KEY_S},
            {84, ScanCodeShort.KEY_T},
            {85, ScanCodeShort.KEY_U},
            {86, ScanCodeShort.KEY_V},
            {87, ScanCodeShort.KEY_W},
            {88, ScanCodeShort.KEY_X},
            {89, ScanCodeShort.KEY_Y},
            {90, ScanCodeShort.KEY_Z},
            {38, ScanCodeShort.UP},
            {40, ScanCodeShort.DOWN},
            {39, ScanCodeShort.RIGHT},
            {37, ScanCodeShort.LEFT},
            {45, ScanCodeShort.INSERT},
            {36, ScanCodeShort.HOME},
            {33, ScanCodeShort.UP},
            {34, ScanCodeShort.DOWN}, //
            {46, ScanCodeShort.DELETE},
            {35, ScanCodeShort.END},
            //{"mouse1"},
            //{"mouse2"},
            //{"mouse3"},
            {5, ScanCodeShort.XBUTTON1},
            {6, ScanCodeShort.XBUTTON2},
            //{"mwheelup"},
            //{"mwheeldown"},
            {96, ScanCodeShort.NUMPAD0},
            {97, ScanCodeShort.NUMPAD1},
            {98, ScanCodeShort.NUMPAD2},
            {99, ScanCodeShort.NUMPAD3},
            {100, ScanCodeShort.NUMPAD4},
            {101, ScanCodeShort.NUMPAD5},
            {102, ScanCodeShort.NUMPAD6},
            {103, ScanCodeShort.NUMPAD7},
            {104, ScanCodeShort.NUMPAD8},
            {105, ScanCodeShort.NUMPAD9},
            {187, ScanCodeShort.OEM_PLUS},
            //{189, ScanCodeShort.OEM_MINUS}, 
            //{"kp_slash"},
            //{46, ScanCodeShort.DELETE}, //
            //{"*"},
            //{"kp_enter"}
        };
    }
}
