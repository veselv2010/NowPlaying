using System;

namespace NowPlaying
{
    public class ConsoleExtensions
    {
        public static string chatButton;

        private static string enter = "\n";

        public static void WelcomeMessage()
        {
            Console.WriteLine("process status: " + ProcessChecker.IsCSGORunning());
            Console.Write("кнопка exec: ");
            string exec_button = Console.ReadLine();

            Console.WriteLine(enter + $"bind \"{exec_button}\" \"exec audio\"");
            Console.Write(enter + "кнопка чата: ");
            chatButton = Console.ReadLine();

            Console.WriteLine(enter + "1. veselv2010" 
                               + enter + "2. scoutpan" + enter + "3. smurf acc by vasilvs" + enter + "4. jigido" + enter + "5. jigido no cheat");
            Console.Write("Выбери бойца: ");
        }

        public static string SelectConfigDirectoryMessage()
        {
            string temp = Console.ReadLine();
            if (temp == "1")
            {
                return @"C:\Program Files (x86)\Steam\userdata\73467495\730\local\cfg\audio.cfg";
            }
            if (temp == "2")
            {
                return @"D:\Steam\userdata\88427647\730\local\cfg\audio.cfg";
            }
            if (temp == "3")
            {
                return @"C:\Program Files (x86)\Steam\userdata\223309473\730\local\cfg\audio.cfg";
            }
            if (temp == "4")
            {
                return @"C:\Program Files (x86)\Steam\userdata\899887627\730\local\cfg\audio.cfg";
            }
            if (temp == "5")
            {
                return @"C:\Program Files (x86)\Steam\userdata\882950809\730\local\cfg\audio.cfg";
            }
            return null;
        }
    }
}

//=_-icon by SCOUTPAN-_=