using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
namespace TjuvPolis
{
    internal class Person
    {
        protected Position Pos { get; set; }
        private static Random random = new Random();    

        public int RandomDirection { get; set; }
        
        public Person(int[,] startPos, int[,] currentPos, int randomDirection)
        {
            Pos = new Position();
            Pos.X = random.Next(0, 100);
            Pos.Y = random.Next(0, 25);



           
            RandomDirection = randomDirection;
            Console.WriteLine();
        }

        protected void CreateList(List<Item> possession)
        {
            List<Item> items = [
                new Item( "Phone"),
                new Item( "Watch"),
                new Item( "Money"),
                new Item("Wallet")
            ];
            possession.AddRange(items);
        }
        public void UpdatePos(int direction)
        {
            this.Pos.X += x;
            this.Pos.Y += y;
            switch (direction) 
            {
                case 0:         //personen ska röra sig till vänster           
                    Pos.X--;
                    break;

                case 1:         //personen ska röra sig till höger
                    Pos.X++;
                    break;

                case 2:         //personen ska röra sig nereåt 
                    Pos.Y++;
                    break;

                case 3:         //personen ska röra sig uppåt
                    Pos.Y--;
                    break;


                case 4:         //personen ska röra sig snitt ner till vänster
                    Pos.X--;
                    Pos.Y++;
                    break;

                case 5:         //personen ska röra sig snitt upp till vänster 
                    Pos.X--;
                    Pos.Y--;
                    break;


                case 6:         //persone ska röra sig snitt upp till höger
                    Pos.X++;
                    Pos.Y--;
                    break;

                case 7:         //personen ska röra sig snitt ner höger
                    Pos.X++;
                    Pos.Y++;
                    break;

                default:         //personen står still
                    break;
                 
            }
           
          

        }


    }

    class Citizen : Person
    {
        private List<Item> possessions;
        public Citizen(
            int[,] startPos,
            int[,] currentPos,
            int randomDirection)
            : base(startPos, currentPos, randomDirection)
        {
            // char citizen = 'C';   Console.ForegroundColor = ConsoleColor.Green;
            base.CreateList(this); // Slipper kalla på funktionen i main
        }
        
        private static void CreateList()         //Skapar en lista med 4 items
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