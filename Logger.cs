using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TjuvPolis
{
    internal class Logger       //Use Queue here instead... 10 outputs shown and oldest output is removed when a new output is added
    {
        public static int loggerCount = 1;
        public static void Print(Police police,Thief thief)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("🔫");

            Console.CursorLeft = 0;
            Console.CursorTop = 27;
            Console.WriteLine($"Report {loggerCount} - {police.Name} took {thief.Name} to jail and also all of his items.");
            //Console.ReadLine();

            loggerCount++;
        }
        public static void Report(Thief thief, Citizen citizen, Item stolenItem)
        {
            Console.ForegroundColor= ConsoleColor.White;
            Console.Write("🔪");

            Console.CursorLeft = 0;
            Console.CursorTop = 27;
            Console.WriteLine($"Report {loggerCount} - {thief.Name } took {stolenItem.ItemName} from {citizen.Name}.");
            //Console.ReadLine();

            loggerCount++;
        }
    }
}