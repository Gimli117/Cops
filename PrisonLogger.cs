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
        public static void AddPrisonInfo(Thief thief)
        {
            if (!prisoners.Any(t => t.Name == thief.Name) && thief.Prisonized)
            {
                prisoners.Add(thief);
            }

            ReleasePrisoner();

            PrisonInfo();
        }

        private static void ReleasePrisoner()
        {
            for (int i = 0; i < prisoners.Count; i++)
            {
                if (prisoners[i].PrisonTime <= 0)
                {
                    prisoners.RemoveAt(i);
                }
            }         
        }

        private static void PrisonInfo()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.CursorTop = 1;

            foreach (var thief in prisoners)
            {
                Console.CursorLeft = 135;

                Console.WriteLine($"Prisoner {thief.Name} will be released on round {thief.PrisonTime + City.roundCount}.");

                Console.WriteLine();
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