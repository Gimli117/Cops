namespace TjuvPolis
{
    class Citizen : Person                                                          // Citizen
    {
        public static readonly ConsoleColor CitizenColor = ConsoleColor.Green;
        public bool IsPoor = false;
        public int PoorTime = 0;
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
            Console.WriteLine($"Citizen {Name} at X:{Pos.X}, Y:{Pos.Y} has {Possessions.Count} items");
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
            if (!IsPoor)
            {
                IsPoor = true;
                PoorTime = 20;

                Pos.X = poorPos.Next(106, 129);
                Pos.Y = poorPos.Next(16, 24);
            }
        }

        public void CheckPoor()
        {
            if (IsPoor)
            {
                if (PoorTime > 0)
                {
                    PrisonLogger.AddPoorHouseInfo(this);

                    PoorTime--;
                }
                else
                {
                    Logger.PoorNoMore(this);

                    Helpers.Clear(this);

                    Pos.X = poorPos.Next(1, 99);
                    Pos.Y = poorPos.Next(1, 24);
                    IsPoor = false;
                    PoorTime = 0;

                    Possessions.Add(new Item("GOLD"));
                }
            }
        }

        public override string ToString()
        {
            Console.ForegroundColor = CitizenColor;

            return "👨";
        }
    }
}