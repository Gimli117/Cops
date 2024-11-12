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

            loggerQueue.Enqueue($"[{City.roundCount}]\t- Report {loggerCount} -\t{thief.Name} robbed {citizen.Name} and took his {stolenItem.ItemName}.");
            
            loggerCount++;
            newEncounter = true;
        }

        public static void Arrest (Police police, Thief thief)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("🔫");

            loggerQueue.Enqueue($"[{City.roundCount}]\t- Report {loggerCount} -\t{police.Name} arrested {thief.Name}, took all his items and will put him in Prison for {thief.PrisonTime} rounds.");

            loggerCount++;
            newEncounter = true;
        }

        public static void Poor (Citizen citizen)
        {
            loggerQueue.Enqueue($"[{City.roundCount}]\t- Report {loggerCount} -\tCitizen {citizen.Name} was robbed too many times and will now be put in the Poor House for 20 rounds.");

            loggerCount++;
            newEncounter = true;
        }

        public static void Released (Thief thief)
        {
            loggerQueue.Enqueue($"[{City.roundCount}]\t- Report {loggerCount} -\tPrisoner {thief.Name} is no longer Wanted and will now be released from the Prison.");

            loggerCount++;
            newEncounter = true;
        }

        public static void PoorNoMore(Citizen citizen)
        {
            loggerQueue.Enqueue($"[{City.roundCount}]\t- Report {loggerCount} -\tCitizen {citizen.Name} is no longer Poor, was given GOLD and will now enter the City once more.");

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
                else if (log.Contains("many"))
                {
                    Console.ForegroundColor= ConsoleColor.Green;
                }
                else if (log.Contains("released") || log.Contains("enter"))
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
                //Thread.Sleep(1000);
                Console.ReadLine();
                newEncounter = false;
            }
        }
    }
}