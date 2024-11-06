using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TjuvPolis
{
    internal class PrisonLogger
    {

        public static Stack<string> prisoners = new Stack<string>();
        public static void AddPrisonInfo(Thief thief)
        {
            if (!prisoners.Contains($"Prisoner {thief.Name} will be released in rounds: ") && thief.Prisonized)
            {
                prisoners.Push($"Prisoner {thief.Name} will be released in rounds: ");
            }
            else if (!thief.Prisonized && thief.PrisonTime > 0)
            {
                prisoners.Pop();
            }

            PrisonInfo(thief);
        }
        public static void PrisonInfo(Thief thief)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.CursorTop = 1;

            foreach (string info in prisoners)
            {
                Console.CursorLeft = 135;

                if (prisoners.Contains($"Prisoner {thief.Name} will be released in rounds: "))
                {
                    Console.WriteLine($"{info} {thief.PrisonTime}");
                    Console.WriteLine();
                }
            }
        }

        public static void PoorHouseInfo()
        {

        }
    }
}
