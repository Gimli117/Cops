using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
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
        private static int personNum;
        public Person(string name)

        {
            Pos = new Position();

            Pos.X = random.Next(1, 99);
            Pos.Y = random.Next(1, 24);

            Name = name;

            //WritePosition();
        }

        int direction = random.Next(0,9);
        int directionCounter = 0;
        public void DrawPerson()
        {
            Console.CursorLeft = Pos.X;
            Console.CursorTop = Pos.Y;

            Console.Write(ToString());

            if (directionCounter == 4)
            {
                direction = random.Next(0, 9);
                directionCounter = 0;
            }
            UpdatePos(direction);
            directionCounter++;
        }

        public void CheckCollision()   // Kollision vid en vägg
        {
            // byt direction...
        }

#pragma warning disable CS0114
        public virtual string ToString()
#pragma warning restore CS0114
        {
            return "X";
        }


        protected virtual void WritePosition()
        {
            Console.WriteLine($"Person{personNum} med X:{Pos.X}, Y:{Pos.Y}");
            personNum++;
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

                case 2:         //personen ska röra sig neråt 
                    Pos.Y++;
                    break;

                case 3:         //personen ska röra sig uppåt
                    Pos.Y--;
                    break;

                case 4:         //personen ska röra sig snett ner till vänster
                    Pos.X--;
                    Pos.Y++;
                    break;

                case 5:         //personen ska röra sig snett upp till vänster
                    Pos.X--;
                    Pos.Y--;
                    break;

                case 6:         //personen ska röra sig snett upp till höger
                    Pos.X++;
                    Pos.Y--;
                    break;

                case 7:         //personen ska röra sig snett ner höger
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
        public static readonly ConsoleColor CitizenColor = ConsoleColor.Green;
        private List<Item> Possessions { get; set; }
        public Citizen(string name) : base(name)
        {
            this.Possessions = new List<Item>();

            CreateList(); // Slipper kalla på funktionen i main
        }

        protected override void WritePosition()
        {
            Console.ForegroundColor = CitizenColor;
            Console.WriteLine($"{Name} med X:{Pos.X}, Y:{Pos.Y}");
            Console.ForegroundColor = ConsoleColor.White;
        }

        private void CreateList()         //Skapar en lista med 4 items
        {
            Possessions.Add(new Item("Phone"));
            Possessions.Add(new Item("Watch"));
            Possessions.Add(new Item("Money"));
            Possessions.Add(new Item("Wallet"));

        }

        public void GiveUp()
        {
            // Inga items kvar, hamna på fattighem...
        }

        public List<Item> GiveItem()
        {
            return this.Possessions;
        }
        public override string ToString()
        {
            Console.ForegroundColor = CitizenColor;

            return "C";
        }
    }

    class Police : Person                                                           //Police
    {
        public static readonly ConsoleColor PoliceColor = ConsoleColor.Blue;
        public Police(string name) : base(name)
        {
            List<Item> seizedGoods = new List<Item>();           
        }
        public override string ToString()
        {
            Console.ForegroundColor = PoliceColor;

            return "P";
        }

        protected override void WritePosition()
        {
            Console.ForegroundColor = PoliceColor;
            Console.WriteLine($"{Name} med X:{Pos.X}, Y:{Pos.Y}");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public void SeizeItems()
        {
            // Tar tjuvens items och sätter han i fängelse...
        }
    }

    class Thief : Person                                                            //Thief
    {
        public static readonly ConsoleColor ThiefColor = ConsoleColor.Red;

        public bool Wanted;
        private List<Item> Booty { get; set; }
        public Thief(string name) : base(name)
        {
            Booty = new List<Item>();
        }
        public override string ToString()
        {
            Console.ForegroundColor = ThiefColor;

            return "T";
        }

        protected override void WritePosition()
        {
            Console.ForegroundColor = ThiefColor;
            Console.WriteLine($"{Name} med X:{Pos.X}, Y:{Pos.Y}");
            Console.ForegroundColor = ConsoleColor.White;
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
                        this.Steal(persAsCitizen!);
                    }
                }
            }
        }
        private void Steal(Citizen citizen)                                     //Citizen
        {
            this.Wanted = true;
            Item Stolen = citizen.GiveItem().First();
            this.Booty.Add(Stolen);
            citizen.GiveItem().Remove(Stolen);
        }
    }
}