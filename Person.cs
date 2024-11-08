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
        /// <summary>
        /// är prooety som hämtar och sätter puplic namn på personer.
        /// </summary>
        public string Name { get; set; }
        //det är propety som också hämtar och sätter vad personierna exakt i city
        protected Position Pos { get; set; }
        //vi laggt till random och det konstant i alla obejkt och kan inte ändras.
        private static Random random = new Random();
        //konstant som är fält av data typ int som har namnet person
        private static int personNum;
        /// <summary>
        ///  Person det är konstruktor som skapar ny obejkt från data typen är person.
        /// </summary>
        /// <param name="name"> skapar personers nanmn occh det namn , kommer efter data typ</param>
        public Person(string name)

        {
            // vi har kallat psstion för skappa property .
            Pos = new Position();
            // användet git och sit som 1-99 i x som random.
            Pos.X = random.Next(1, 99);
            // git ,set som har random posstion som Y är 1-24 i så fall.
            Pos.Y = random.Next(1, 24);
            //  Name är  property  där vi  värdet genom  name som parameter.
            Name = name;
        }
        /// <summary>
        /// läggt fält som random direction som 0-9
        /// personerna ska gå runt  typ 5 gånger-
        /// </summary>
        int direction = random.Next(0, 9);
        //här ska vi saka sätta side effect alltså hur de rör sig.
        int directionCounter = 0;
       /// <summary>
       /// det är funktion men parameter som ritar männsikörna inom  Areasize city .
       /// </summary>
       /// <param name="city">parameter</param>
        public void DrawPerson(AreaSize city)
        {
            //Att de inte gå ut utan för väggen.
            CheckCollision(city);
            //updtearar postioner till personera i city som property
            UpdatePos(direction);

            
            //sätta X somm påbekar rilll vänster, 
            Console.CursorLeft = Pos.X;
            //sätta Y  som påpekar till övanför.
            Console.CursorTop = Pos.Y;

            Console.Write(ToString());
            //Vilkkor till fältet som kollar om fältet lika med 5
            
            if (directionCounter == 5)           // Byter håll var femte turn
            {
                //det ska få random rikting 0-9
                direction = random.Next(0, 9);
              //starta om och byter håll.
                directionCounter = 0;
            }
            directionCounter++;
        }
        /// <summary>
        /// funktion som checkar att perosner ska inte studsa utan för vägen
        /// </summary>
        /// <param name="size"></param>
        public void CheckCollision(AreaSize size)             // Kollision vid en vägg
        {
            ///switch stamtemnts med flera case som innehåller vilkor
            switch (direction)
            {
                //det brukar räkna konsulen inann så det går ett steg innan i så fall minus 1 är höger.
                case 0:                                     // Left
                    if (Pos.X - 1 == size.MinWidthX)                     // Left Wall Collision
                    {
                        direction = 2;  //Right
                    }
                    break;
                    // i detta fall ska det köras y som - och går upp.
                case 1:                                     // Up
                    if (Pos.Y - 1 == size.MinHeightY)                     // Ceiling Collision
                    {
                        direction = 3;  //Down
                    }
                    break;
                    ///detta fall ska det x gå till höger .
                case 2:                                     // Right
                    if (Pos.X + 1 == size.MaxWidthX)        // Right Wall Collision
                    {
                        direction = 0;  //Left
                    }
                    break;
                  ///detta fall Y sksa gå ner.
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
    
        /// <summary>
        /// funktion som är grundenn av systemet.
        /// </summary>
        /// <returns>det ska låta override köras</returns>
        new public virtual string ToString()
        {
            return "X";
        }
        /// <summary>
        /// vi ska ritta posttionenrna med denna acssees modefire.
        /// </summary>
        protected virtual void WritePosition()
        {
            //skappa number till varje person med posttionerna x ,y
            Console.WriteLine($"Person{personNum} med X:{Pos.X}, Y:{Pos.Y}");
            //det ska plussa ett varje gång .
            personNum++;
        }
        /// <summary>
        /// detta hjälper oss att visa postionen på x med acees modefire.
        /// </summary>
        /// <returns></returns>
        public int ShowPositionX()
        {
            // det ska skicka data .
            return Pos.X;
        }
        /// <summary>
        /// public med data typ int som visar postion på Y
        /// </summary>
        /// <returns></returns>
        public int ShowPositionY()
        {
            return Pos.Y;
        }
        public void UpdatePos(int direction)
        {///statemnst som är switch som har 7 som case .
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
    /// <summary>
    ///skapat en class som är citzen som person
    /// </summary>
    class Citizen : Person                                                          // Citizen
    {//acsse modfire som har get som proepety vilket är read only som leder till ändra färgen på citzen till grön.
        public static readonly ConsoleColor CitizenColor = ConsoleColor.Green;
        //en access mofire som har data typ boll som frågar ifall citzen är fattig pga Items slut(så om det är fattig gör false)
        public bool IsPoor = false;
       //access modfire som ligger till data typ int så vi skapar bara
        public int PoorTime = 0;
        // det bara random postion inom fattig hem för de ska inte vara samma ställe.privite är no acsess.
        private static Random poorPos = new Random();
        //här vi skapar list av random med items vad de har ,propety används här read only och sätta information.
        private List<Item> Possessions { get; set; }
        //access modifre av citzen som har virabler i sig som använder data typ string med name och base med name.
        public Citizen(string name) : base(name)
        {
            //vi kalllar vilka items en ny lista har 
            this.Possessions = new List<Item>();

            CreateList();                 // Slipper kalla på funktionen i main
        }
        /// <summary>
        /// accsess modifre som använder som innehåller random text på name och postion och  byter färgen på hur mycket de har i items.
        /// </summary>
        protected override void WritePosition()
        {
            Console.ForegroundColor = CitizenColor;
            Console.WriteLine($"Citizen {Name} at X:{Pos.X}, Y:{Pos.Y} has {Possessions.Count} items");
            Console.ForegroundColor = ConsoleColor.White;
        }
        /// <summary>
        /// skappa lista av 4 iteams med create list.
        /// </summary>
        private void CreateList()         // Skapar en lista med 4 items
        {
            //lägga till ny iteam som är mobile
            Possessions.Add(new Item("Phone"));
            //lägga till ny iteam som är klocka
            Possessions.Add(new Item("Watch"));
            //lägga till ny iteam som är penagr
            Possessions.Add(new Item("Money"));
            //lägga till ny iteam som är plånbok
            Possessions.Add(new Item("Wallet"));
        }
        /// <summary>
        /// acccess modfire som inhåller list av item somm tillbakar items.
        /// </summary>
        /// <returns></returns>
        public List<Item> GiveItem()
        {
            return this.Possessions;
        }
        // public accesss modfire som lämnar peersonerna till fatig hem om items de hade slut .
        public void GiveUp()        //Inga items kvar, hamna på fattighuset
        {//vilkor som säger om items är 0 då personen är fattig
            if (Possessions.Count <= 0 && !IsPoor)
            {// om det rätt
                IsPoor = true;
                //lägg tiden på att stanna på fattig hem ,tills round 20
                
                PoorTime = 20;
                //sätta X som 106-129 i psstioner
                Pos.X = poorPos.Next(106, 129);
                Pos.Y = poorPos.Next(16, 24);

                Logger.Poor(this);
            }
        }
        /// <summary>
        /// access modefire ska kolla om citzen fattig
        /// </summary>
        public void CheckPoor()
        {
            if (IsPoor)
            {
                if (PoorTime > 0)
                {//ligger info på sidan av poor houset
                    PrisonLogger.AddPoorHouseInfo(this);

                    PoorTime--;
                }
                else
                {
                 
                    //annars ska citzen vara inte fattig längre
                    Logger.PoorNoMore(this);
                    //posx ska röra sig 1,99 stage uppe i sidan
                    this.GivePersonRandmPosstion();
                    IsPoor = false;
                    PoorTime = 0;

                    Possessions.Add(new Item("GOLD"));
                }
            }
        }
        private void GivePersonRandmPosstion()
        {
            this.Pos.X = poorPos.Next(1, 99);
            this.Pos.Y = poorPos.Next(1, 24);
            

        }

         /// <summary>
         /// access modefire med data typen string
         /// </summary>
         /// <returns></returns>
        public override string ToString()
        {
            // kunna ändra chrartern på citzen till detta emoji
            Console.ForegroundColor = CitizenColor;

            return "👨";
        }
    }
    /// <summary>
    /// access modifre som är police i person .
    /// </summary>
    class Police : Person                                                           // Police
    {//access mofire som read only can andra police färgen emoji tilll blåå.
        public static readonly ConsoleColor PoliceColor = ConsoleColor.Blue;
        public List<Item> seizedGoods { get; set; }
/// <summary>
/// acseecc modifre som inehhåler text data typ med name.
/// </summary>
/// <param name="name">data namn på P i kunsoulen</param>
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
                if (Pos.X == person.ShowPositionX() &&
                    Pos.Y == person.ShowPositionY())
                {
                    Thief persAsThief;

                    if (person is Thief && (persAsThief = (person as Thief)!).Wanted)
                    {                        
                        SeizeItems(persAsThief!);
                    }
                }
            }
        }
        private void SeizeItems(Thief thief)                                        // Tar tjuvens items och sätter han i fängelse...
        {
            if (thief.Wanted)
            {
                seizedGoods.AddRange(thief.LoseItems());
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
        public int WantedLevel = 0;
        public int PrisonTime = 0;
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

            PrisonTime = WantedLevel * 10;

            Pos.X = prisonPos.Next(106, 129);
            Pos.Y = prisonPos.Next(1, 9);            
        }

        public void CheckJail()
        {
            if (Prisonized)
            {
                if (PrisonTime > 0)
                {
                    PrisonLogger.AddPrisonInfo(this);

                    PrisonTime--;
                }
                else
                {
                    Logger.Released(this);

                    Pos.X = prisonPos.Next(1, 99);
                    Pos.Y = prisonPos.Next(1, 24);
                    Prisonized = false;
                    Wanted = false;
                    PrisonTime = 0;
                    WantedLevel = 0;
                }
            }
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
                if(Pos.X == person.ShowPositionX() && 
                   Pos.Y == person.ShowPositionY())
                {
                    if(person is Citizen)
                    {
                        var persAsCitizen = person as Citizen;
                        Steal(persAsCitizen!);
                    }
                }
            }
        }
        private void Steal(Citizen citizen)
        {
            if (!citizen.IsPoor)
            {
                Wanted = true;

                List<Item> items = citizen.GiveItem();

                Item Stolen = items.ElementAt(Random.Shared.Next(0, items.Count));      //Takes a random item from the citizen
                Booty.Add(Stolen);

                citizen.GiveItem().Remove(Stolen);

                WantedLevel++;

                Console.CursorLeft = ShowPositionX();
                Console.CursorTop = ShowPositionY();

                Logger.Robbery(this, citizen, Stolen);
            }
        }   
    }
}