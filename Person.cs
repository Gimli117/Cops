using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TjuvPolis
{
    internal class Person
    {
        public int[,] StartPos { get; set; }
        public int[,] CurrentPos { get; set; }
        public int RandomDirection { get; set; }

        public Person(int[,] startPos, int[,] currentPos, int randomDirection)
        {
            StartPos = startPos;
            CurrentPos = currentPos;
            RandomDirection = randomDirection;
        }
    }

    class Citizen : Person
    {
        public Citizen(int[,] startPos, int[,] currentPos, int randomDirection) : base(startPos, currentPos, randomDirection)
        {
            // char citizen = 'C';   Console.ForegroundColor = ConsoleColor.Green;
        }

        public static void CreateList()         //Skapar en lista med 4 items
        {
            List<Item> possessions = new List<Item>();

            possessions.Add(new Item("Phone"));
            possessions.Add(new Item("Watch"));
            possessions.Add(new Item("Money"));
            possessions.Add(new Item("Wallet"));

            foreach(Item item in possessions)       //Skriv ut listan (just nu för synes skull...)
            {
                Console.WriteLine(item.ItemName);
            }
        }

        public void GiveUp()
        {
            // Inga items kvar, hamna på fattighem...
        }
    }

    class Police : Person
    {
        public Police(int[,] startPos, int[,] currentPos, int randomDirection) : base(startPos, currentPos, randomDirection)
        {
            List<Item> seizedGoods = new List<Item>();

            // char police = 'P';   Console.ForegroundColor = ConsoleColor.Blue;
        }

        public void SeizeItems()
        {
            // Tar tjuvens items och sätter han i fängelse...
        }
    }

    class Thief : Person
    {
        public Thief(int[,] startPos, int[,] currentPos, int randomDirection) : base(startPos, currentPos, randomDirection)
        {
            List<Item> stolenGoods = new List<Item>();

            // bool isWanted = false;
            // char thief = 'T';    Console.ForegroundColor = ConsoleColor.Red;
        }

        public void StealItems ()
        {
            // Tar ett random item från en Citizen och blir Wanted...
        }
    }
}