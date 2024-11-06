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
    public class CitySize
    {
        public static int Width = 100;
        public static int Height = 25;
    }
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
        }

        int direction = random.Next(0, 9);
        int directionCounter = 0;
        public void DrawPerson(AreaSize city)
        {
            CheckCollision(city);

            UpdatePos(direction);

            Console.CursorLeft = Pos.X;
            Console.CursorTop = Pos.Y;

            Console.Write(ToString());

            if (directionCounter == 5)           // Byter håll var femte turn
            {
                direction = random.Next(0, 9);
                directionCounter = 0;
            }
            directionCounter++;
        }

        public void CheckCollision(AreaSize size)             // Kollision vid en vägg
        {
            switch (direction)
            {
                case 0:                                     // Left
                    if (Pos.X - 1 == size.MinWidthX)                     // Left Wall Collision
                    {
                        direction = 2;  //Right
                    }
                    break;

                case 1:                                     // Up
                    if (Pos.Y - 1 == size.MinHeightY)                     // Ceiling Collision
                    {
                        direction = 3;  //Down
                    }
                    break;

                case 2:                                     // Right
                    if (Pos.X + 1 == size.MaxWidthX)        // Right Wall Collision
                    {
                        direction = 0;  //Left
                    }
                    break;

                case 3:                                     // Down
                    if (Pos.Y + 1 == size.MaxHeightY)       // Bottom Collision
                    {
                        direction = 1;  //Up
                    }
                    break;

                case 4:                                                    // Left+Down
                    if (Pos.X - 1 == size.MinWidthX)                                    // Left Wall Collision
                    {
                        direction = 7;  //Right+Down
                    }
                    if (Pos.Y + 1 == size.MaxHeightY)                      // Bottom Collision
                    {
                        direction = 5;  //Left+Up
                    }
                    if (Pos.X - 1 == size.MinWidthX && Pos.Y + 1 == size.MaxHeightY)    // CORNER collision
                    {
                        direction = 6;  //Right+Up
                        Console.BackgroundColor = ConsoleColor.DarkYellow;
                    }
                    break;

                case 5:                                                    // Left+Up
                    if (Pos.X - 1 == size.MinWidthX)                                    // Left Wall collision
                    {
                        direction = 6;  //Right+Up
                    }
                    if (Pos.Y - 1 == size.MinHeightY)                                    // Ceiling collision
                    {
                        direction = 4;  //Left+Down
                    }
                    if (Pos.X - 1 == size.MinWidthX && Pos.Y - 1 == size.MinHeightY)                  // CORNER collision
                    {
                        direction = 7;  //Right+Down
                        Console.BackgroundColor = ConsoleColor.DarkYellow;
                    }
                    break;

                case 6:                                                    // Right+Up
                    if (Pos.X + 1 == size.MaxWidthX)                       // Right Wall collision
                    {
                        direction = 5;  //Left+Up
                    }
                    if (Pos.Y - 1 == size.MinHeightY)                                    // Ceiling collision
                    {
                        direction = 7;  //Right+Down
                    }
                    if (Pos.X + 1 == size.MaxWidthX && Pos.Y - 1 == size.MinHeightY)     // CORNER collision
                    {
                        direction = 4;  //Left+Down
                        Console.BackgroundColor = ConsoleColor.DarkYellow;
                    }
                    break;

                case 7:                                                    // Right+Down
                    if (Pos.X + 1 == size.MaxWidthX)                       // Right Wall collision
                    {
                        direction = 4;  //Left+Down
                    }
                    if (Pos.Y + 1 == size.MaxHeightY)                      // Bottom collision
                    {
                        direction = 6;  //Right+Up
                    }
                    if (Pos.X + 1 == size.MaxWidthX && Pos.Y + 1 == size.MaxHeightY)     // CORNER collision
                    {
                        direction = 5;  //Left+Up
                        Console.BackgroundColor = ConsoleColor.DarkYellow;
                    }
                    break;

                default:                                      // Freeze...
                    break;
            }
        }
    

        new public virtual string ToString()
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

    class Citizen : Person                                                          // Citizen
    {
        public static readonly ConsoleColor CitizenColor = ConsoleColor.Green;
        public bool isPoor = false;
        private static Random poorPos = new Random();
        private List<Item> Possessions { get; set; }
        public Citizen(string name) : base(name)
        {
            this.Possessions = new List<Item>();

            CreateList();                 // Slipper kalla på funktionen i main
        }

        protected override void WritePosition()
        {
            Console.ForegroundColor = CitizenColor;
            Console.WriteLine($"{Name} med X:{Pos.X}, Y:{Pos.Y}");
            Console.ForegroundColor = ConsoleColor.White;
        }

        private void CreateList()         // Skapar en lista med 4 items
        {
            Possessions.Add(new Item("Phone"));
            Possessions.Add(new Item("Watch"));
            Possessions.Add(new Item("Money"));
            Possessions.Add(new Item("Wallet"));
        }

        public List<Item> GiveItem()
        {
            return this.Possessions;
        }

        public void GiveUp()        //Inga items kvar, hamna på fattighuset
        {
            if (this.Possessions.Count <= 0 && !this.isPoor)
            {
                this.isPoor = true;

                this.Pos.X = poorPos.Next(106, 129);
                this.Pos.Y = poorPos.Next(16, 24);

                Logger.Poor(this);
            }
        }

        public override string ToString()
        {
            Console.ForegroundColor = CitizenColor;

            return "👨";
        }
    }

    class Police : Person                                                           // Police
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

            return "👮‍";
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
        private void SeizeItems(Thief thief)                                        // Tar tjuvens items och sätter han i fängelse...
        {
            if (thief.Wanted)
            {
                this.seizedGoods.AddRange(thief.LoseItems());
                thief.LoseItems().Clear();

                thief.GoToJail();

                Console.CursorLeft = ShowPositionX();
                Console.CursorTop = ShowPositionY();

                Logger.Arrest(this, thief);
            }
        }
    }
    class Thief : Person                                                            // Thief
    {
        public static readonly ConsoleColor ThiefColor = ConsoleColor.Red;

        public bool Wanted = false;
        public bool Prisonized = false;
        private static Random prisonPos = new Random();
        private List<Item> Booty { get; set; }
        public Thief(string name) : base(name)
        {
            Booty = new List<Item>();
        }
        public override string ToString()
        {
            Console.ForegroundColor = ThiefColor;

            if (Wanted)
            {
                return "🦊";
            }
            else
            {
                return "🦝";
            }
        }

        public List<Item> LoseItems() 
        {
            return Booty;
        }

        public void GoToJail()      //Tagen av polisen, hamna i fängelset...
        {
                Prisonized = true;

                this.Pos.X = prisonPos.Next(106, 129);
                this.Pos.Y = prisonPos.Next(1, 9);
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
        {
            if (!citizen.isPoor)
            {
                this.Wanted = true;

                List<Item> items = citizen.GiveItem();

                Item Stolen = items.ElementAt(Random.Shared.Next(0, items.Count));      //Takes a random item from the citizen
                this.Booty.Add(Stolen);

                citizen.GiveItem().Remove(Stolen);

                Console.CursorLeft = ShowPositionX();
                Console.CursorTop = ShowPositionY();

                Logger.Robbery(this, citizen, Stolen);
            }
        }   
    }
}