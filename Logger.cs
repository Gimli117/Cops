using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TjuvPolis
{
    internal class Logger
    {
        public static void Print()
        {
            Console.CursorLeft = 0;
            Console.CursorTop = 30;

            Console.WriteLine("Logger says Bu...");
        }
    }
}
