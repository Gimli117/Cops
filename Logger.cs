using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TjuvPolis
{
    internal class Logger
    {
        public static void Print(Police police,Thief thief)
        {
            Console.CursorLeft = 0;
            Console.CursorTop = 27;
            Console.WriteLine($"{police.Name} tar tjuven {thief.Name} och alla hans items");
            Console.ReadLine();
        }
        public static void Report(Thief thief, Citizen citizen)
        {
            Console.ForegroundColor= ConsoleColor.White;
            Console.Write("🔪");

            Console.CursorLeft = 0;
            Console.CursorTop = 27;
            Console.WriteLine($"{thief.Name } tar ett item från {citizen.Name}");
            Console.ReadLine();      
        }
    }
}