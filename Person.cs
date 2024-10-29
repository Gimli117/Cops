using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
namespace TjuvPolis
{
    internal class Person
    {
        public string Name { get; set; }
        protected Position Pos { get; set; }
        private static Random random = new Random();    
        public Person(string name)
        {
            Pos = new Position();
            Pos.X = random.Next(0, 100);
            Pos.Y = random.Next(0, 25);
            Name = name;
        }
        public int ShowPositionX()
        {
            return Pos.X;
        }
        public int ShowPositionY()
        {
            return Pos.Y;
        }
        public void UpdatePos(int direction)
        {
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
        public ConsoleColor CitizenColor = ConsoleColor.Green;
        private List<Item> Possessions { get; set; }
        public Citizen(string name) : base(name)
        {
                           // char citizen = 'C';   Console.ForegroundColor = ConsoleColor.Green;
            CreateList(); // Slipper kalla på funktionen i man
        }
        
        private void CreateList()         //Skapar en lista med 4 items
        {
            Possessions.Add(new Item("Phone"));
            Possessions.Add(new Item("Watch"));
            Possessions.Add(new Item("Money"));
            Possessions.Add(new Item("Wallet"));

            foreach(Item item in Possessions)       //Skriv ut listan (just nu för synes skull...)
            {
                Console.WriteLine(item.ItemName);
            }
        }

        public void GiveUp()
        {
            // Inga items kvar, hamna på fattighem...
        }
        public List<Item> GiveItem()
        {
            return this.Possessions;
        }
    }


    class Police : Person
    {
        public Police(string name) : base(name)
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
        public bool Wanted;
        private List<Item> Booty { get; set; }
        public Thief(string name) : base(name)
        {
            Booty = new List<Item>();
            // bool isWanted = false;
            // char thief = 'T';    Console.ForegroundColor = ConsoleColor.Red;
        }
        public void Scan(List<Person> population) 
        {
            foreach(var person in population)
            {
                if(this.Pos.X == person.ShowPositionX() && 
                   this.Pos.Y == person.ShowPositionY())
                {
                    if(person is Citizen)
                    {
                        var persAsCitizen = person as Citizen;
                        this.Steal(persAsCitizen);
                    }
                }
            }
        }
        private void Steal(Citizen citizen)
        {
            this.Wanted = true;
            Item Stolen = citizen.GiveItem().First();
            this.Booty.Add(Stolen);
            citizen.GiveItem().Remove(Stolen);
        }
    }
}