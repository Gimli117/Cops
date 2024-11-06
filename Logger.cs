using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.ComponentModel.Design;

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

            loggerQueue.Enqueue($"Report {loggerCount} - {police.Name} put {thief.Name} in prison for {thief.PrisonTime} turns and also took all of his items.");
            loggerCount++;
            newEncounter = true;
        }

        public static void Poor (Citizen citizen)
        {
            loggerQueue.Enqueue($"Report {loggerCount} - Citizen {citizen.Name} was robbed too many times and is now in the Poor House.");
            loggerCount++;
            newEncounter = true;
        }

        public static void Released (Thief thief)
        {
            loggerQueue.Enqueue($"Report {loggerCount} - Prisoner {thief.Name} was released from the Prison and is no longer Wanted.");
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
                if (log.Contains("items"))
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                }
                else if (log.Contains("robbed"))
                {
                    Console.ForegroundColor= ConsoleColor.Green;
                }
                else if (log.Contains("released"))
                {
                    Console.ForegroundColor = ConsoleColor.White;
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