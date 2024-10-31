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

        int direction = random.Next(0, 9);
        int directionCounter = 0;
        public void DrawPerson()
        {
            CheckCollision();

            Console.CursorLeft = Pos.X;
            Console.CursorTop = Pos.Y;

            UpdatePos(direction);

            Console.Write(ToString());

            if (directionCounter == 5)          //Byter håll var femte turn
            {
                direction = random.Next(0, 9);
                directionCounter = 0;
            }
            directionCounter++;

        }

        public void CheckCollision()             // Kollision vid en vägg
        {
            switch (direction)
            {
                case 0:                                     // Left
                    if (Pos.X - 1 == 0)                     // Left Wall Collision
                    {
                        direction = 2;  //Right
                    }
                    break;

                case 1:                                     // Up
                    if (Pos.Y - 1 == 0)                     // Ceiling Collision
                    {
                        direction = 3;  //Down
                    }
                    break;

                case 2:                                     // Right
                    if (Pos.X + 1 == CitySize.Width)        // Right Wall Collision
                    {
                        direction = 0;  //Left
                    }
                    break;

                case 3:                                     // Down
                    if (Pos.Y + 1 == CitySize.Height)       // Bottom Collision
                    {
                        direction = 1;  //Up
                    }
                    break;

                case 4:                                                    // Left+Down
                    if (Pos.X - 1 == 0)                                    // Left Wall Collision
                    {
                        direction = 7;  //Right+Down
                    }
                    if (Pos.Y + 1 == CitySize.Height)                      // Bottom Collision
                    {
                        direction = 5;  //Left+Up
                    }
                    if (Pos.X - 1 == 0 && Pos.Y + 1 == CitySize.Height)    // CORNER collision
                    {
                        direction = 6;  //Right+Up
                        Console.BackgroundColor = ConsoleColor.DarkYellow;
                    }
                    break;

                case 5:                                                    // Left+Up
                    if (Pos.X - 1 == 0)                                    // Left Wall collision
                    {
                        direction = 6;  //Right+Up
                    }
                    if (Pos.Y - 1 == 0)                                    // Ceiling collision
                    {
                        direction = 4;  //Left+Down
                    }
                    if (Pos.X - 1 == 0 && Pos.Y - 1 == 0)                  // CORNER collision
                    {
                        direction = 7;  //Right+Down
                        Console.BackgroundColor = ConsoleColor.DarkYellow;
                    }
                    break;

                case 6:                                                    // Right+Up
                    if (Pos.X + 1 == CitySize.Width)                       // Right Wall collision
                    {
                        direction = 5;  //Left+Up
                    }
                    if (Pos.Y - 1 == 0)                                    // Ceiling collision
                    {
                        direction = 7;  //Right+Down
                    }
                    if (Pos.X + 1 == CitySize.Width && Pos.Y - 1 == 0)     // CORNER collision
                    {
                        direction = 4;  //Left+Down
                        Console.BackgroundColor = ConsoleColor.DarkYellow;
                    }
                    break;

                case 7:                                                    // Right+Down
                    if (Pos.X + 1 == CitySize.Width)                       // Right Wall collision
                    {
                        direction = 4;  //Left+Down
                    }
                    if (Pos.Y + 1 == CitySize.Height)                      // Bottom collision
                    {
                        direction = 6;  //Right+Up
                    }
                    if (Pos.X + 1 == CitySize.Width && Pos.Y + 1 == CitySize.Height)     // CORNER collision
                    {
                        direction = 5;  //Left+Up
                        Console.BackgroundColor = ConsoleColor.DarkYellow;
                    }
                    break;

                default:                                      // Freeze...
                    break;
            }
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

                case 1:         //personen ska röra sig uppåt
                    Pos.Y--;
                    break;

                case 2:         //personen ska röra sig höger
                    Pos.X++;
                    break;

                case 3:         //personen ska röra sig neråt
                    Pos.Y++;
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

            return "👨";
        }
    }

    class Police : Person                                                           //Police
    {
        public static readonly ConsoleColor PoliceColor = ConsoleColor.Blue;
        public List<Item> seizedGoods { get; set; }

        public Police(string name) : base(name)
        {
            seizedGoods = new List<Item>();
        }
        public override string ToString()
        {
            Console.ForegroundColor = PoliceColor;

            return "👮";
        }

        protected override void WritePosition()
        {
            Console.ForegroundColor = PoliceColor;
            Console.WriteLine($"{Name} med X:{Pos.X}, Y:{Pos.Y}");
            Console.ForegroundColor = ConsoleColor.White;
        }
        public void Scan(List<Person> population)
        {
            foreach (var person in population)
            {
                if (this.Pos.X == person.ShowPositionX() &&
                   this.Pos.Y == person.ShowPositionY())
                {
                    Thief persAsThief;
                    if (person is Thief && (persAsThief = (person as Thief)!).Wanted)

                    {
                        
                        this.SeizeItems(persAsThief!);

                    }
                }
            }
        }
        private void SeizeItems(Thief thief)
        {
            // Tar tjuvens items och sätter han i fängelse...
            this.seizedGoods.AddRange(thief.LoseItems());
            thief.LoseItems().Clear();
            


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

            if (Wanted)
                return "🦊";
            else
                return "🦝";
        }
        public List<Item> LoseItems() 
        { 
            return Booty;
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
        private void Steal(Citizen citizen)         
           //Citizen
        {
            this.Wanted = true;
            Item Stolen = citizen.GiveItem().First();
            this.Booty.Add(Stolen);
            citizen.GiveItem().Remove(Stolen);
            Console.CursorLeft= ShowPositionX();
            Console.CursorTop = ShowPositionY();
            Logger.Report(this,citizen);



        }   

    }


}