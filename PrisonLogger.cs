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

        public static Stack<Thief> prisoners = new Stack<Thief>();
        public static void AddPrisonInfo(Thief thief)
        {
            if (!prisoners.Contains(thief) && thief.Prisonized)
            {
                prisoners.Push(thief);
            }
            else if (!thief.Prisonized && thief.PrisonTime > 0)
            {
                prisoners.Pop();
            }

            PrisonInfo();
        }
        public static void PrisonInfo()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.CursorTop = 1;

            foreach (var info in prisoners)
            {
                Console.CursorLeft = 135;

                    Console.WriteLine($"Prisoner {info.Name} will be released in rounds: {info.PrisonTime}");
                    Console.WriteLine();
                }
            }

        public static void PoorHouseInfo()
        {

        }
    }
}
