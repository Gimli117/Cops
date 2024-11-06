using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace TjuvPolis
{
    internal class Logger
    {
        public static int loggerCount = 1;

        public static bool newEncounter;

        public static Queue<string> loggerQueue = new Queue<string>();

        public static void Robbery (Thief thief, Citizen citizen, Item stolenItem)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("🔪");

            loggerQueue.Enqueue($"Report {loggerCount} - {thief.Name} took {stolenItem.ItemName} from {citizen.Name}.");
            loggerCount++;
            newEncounter = true;
        }

        public static void Arrest (Police police, Thief thief)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("🔫");

            loggerQueue.Enqueue($"Report {loggerCount} - {police.Name} took {thief.Name} to jail and also all of his items.");
            loggerCount++;
            newEncounter = true;
        }

        public static void Poor (Citizen citizen)
        {
            loggerQueue.Enqueue($"Report {loggerCount} - Citizen {citizen.Name} was robbed too many times and is now in the Poor House.");
            loggerCount++;
            newEncounter = true;
        }

        public static void PrintQueue ()
        {
            Console.CursorTop = 31;

            while (loggerQueue.Count > 10)
            {
                loggerQueue.Dequeue();
            }

            foreach (string log in loggerQueue.Reverse())
            {
                if (log.Contains("jail"))
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                }
                else if (log.Contains("Poor"))
                {
                    Console.ForegroundColor= ConsoleColor.Green;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                Console.CursorLeft = 1;

                Console.WriteLine(log);
                Console.WriteLine();
            }
            if (newEncounter)
            {
                Console.ReadLine();
                newEncounter = false;
            }
        }
    }
}