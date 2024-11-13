using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TjuvPolis
{
    internal class PrisonLogger
    {

        private static List<Thief> prisoners = new List<Thief>();
        private static List<Citizen> poorGuys = new List<Citizen>();

        private static Dictionary<Thief, int> prisonPrintLine = new Dictionary<Thief, int>();
        private static Dictionary<Citizen, int> poorPrintLine = new Dictionary<Citizen, int>();
        public static void AddPrisonInfo(Thief thief)
        {
            if (!prisoners.Any(t => t.Name == thief.Name) && thief.Prisonized)
            {
                prisoners.Add(thief);

                prisonPrintLine.Add(thief, 1);
            }

            PrisonInfo();

            ReleasePrisoner();
        }

        private static void ReleasePrisoner()
        {
            for (int i = 0; i < prisoners.Count; i++)
            {
                if (prisoners[i].PrisonTime <= 0)
                {
                    Console.CursorLeft = 135;
                    Console.CursorTop = prisonPrintLine[prisoners[i]];
                    Console.Write("|                                        |");

                    prisonPrintLine.Remove(prisoners[i]);

                    prisoners.RemoveAt(i);

                    if (prisoners.Count > 1)        //Om det finns mer än en tjuv i listan, ta bort raden under
                    {
                        Console.CursorTop++;
                        Console.Write("|                                        |");
                    }
                }
            }
        }

        private static void PrisonInfo()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.CursorTop = 0;

            foreach (var thief in prisoners)
            {
                Console.CursorLeft = 135;

                Console.CursorTop += 1;

                Console.Write($"Prisoner {thief.Name} will be released on round {thief.PrisonTime + City.roundCount}.");

                prisonPrintLine[thief] = Console.CursorTop;
            }
        }

        public static void AddPoorHouseInfo(Citizen citizen)
        {
            if (!poorGuys.Any(c => c.Name == citizen.Name) && citizen.IsPoor)
            {
                poorGuys.Add(citizen);
            }

            CitizenNoPoorAnymore();

            PoorHouseInfo();
        }

        private static void CitizenNoPoorAnymore()
        {
            for (int i = 0; i < poorGuys.Count; i++)
            {
                if (poorGuys[i].PoorTime <= 0)
                {
                    poorGuys.RemoveAt(i);
                }
            }
        }

        private static void PoorHouseInfo()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.CursorTop = 16;

            foreach (var poorGuy in poorGuys)
            {
                Console.CursorLeft = 135;

                Console.WriteLine($"Citizen {poorGuy.Name} will be back in the city on round {poorGuy.PoorTime + City.roundCount}.");

                Console.WriteLine();
            }
        }
    }
}