namespace TjuvPolis
{
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

            Pos.X = prisonPos.Next(106, 127);
            Pos.Y = prisonPos.Next(1, 9);
        }

        public void CheckJail()
        {
            if (Prisonized)
            {
                if (PrisonTime > 0)
                {
                    PrisonTime--;
                }
                else
                {
                    Logger.Released(this);

                    Helpers.Clear(this);

                    Pos.X = prisonPos.Next(1, 97);
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
            if (citizen.GiveItem().Count <= 0)
            {
                citizen.GiveUp();
                Logger.Poor(citizen);
            }
        }   
    }
}