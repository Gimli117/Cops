namespace TjuvPolis
{
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
            seizedGoods.AddRange(thief.LoseItems());
            thief.LoseItems().Clear();

            thief.GoToJail();

            Console.CursorLeft = ShowPositionX();
            Console.CursorTop = ShowPositionY();

            Logger.Arrest(this, thief);    
        }
    }
}