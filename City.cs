using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace TjuvPolis
{
    internal class City
    {
        private readonly AreaSize citySize;
        private readonly AreaSize prisonSize;
        private readonly AreaSize poorPlaceSize;
        const char wall = '█';

        private List<Person> _population;
        public List<Person> Population { 
            get
            {
                return _population;
            }
            set
            {
                _population = value;
            }
        }      // Lista med alla personer i staden

        public City()
        {
            _population = new List<Person>();

            citySize = new AreaSize(0, 0, 100, 25);
            prisonSize = new AreaSize(105, 0, 130, 10);
            poorPlaceSize = new AreaSize(105, 15, 130, 25);

            CreatePopulation();
        }
        public void DrawOutput()
        {
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.Black;

                Console.CursorVisible = false;
                Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);

                DrawCity();
                DrawPrison();
                DrawPoorHouse();

                foreach (Person person in _population)
                {
                    if (person is Citizen && ((Citizen)person).isPoor)
                    {
                        person.DrawPerson(poorPlaceSize);
                    }
                    else if (person is Thief && ((Thief)person).Prisonized)
                    {
                        person.DrawPerson(prisonSize);
                    }
                    else
                    {
                        person.DrawPerson(citySize);
                    }
                }

                CheckEncounters();

                //Thread.Sleep(500);
                Console.ReadLine();
                Console.Clear();
            }
        }

        public void DrawWalls(int MinWidthX, int MinHeightY, int MaxWidthX, int MaxHeightY, char? otherWall)
        {
            Console.ForegroundColor = ConsoleColor.White;
            for (int x = MinWidthX; x <= MaxWidthX; x++)
            {
                for (int y = MinHeightY; y <= MaxHeightY; y++)
                {
                    Console.CursorTop = y;
                    Console.CursorLeft = x;
                    if (x == MinWidthX || x == MaxWidthX)
                    {
                        Console.Write((otherWall is null) ? wall : otherWall);
                    }
                    else if (y == MinHeightY || y == MaxHeightY)
                    {
                        Console.Write((otherWall is null) ? wall : otherWall);
                    }
                }
            }

            Console.CursorLeft = 48;
            Console.CursorTop = 26;
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("City");

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\n\n Reports");
            Console.CursorLeft = 0;
            Console.WriteLine("-----------------------------------------------------------------------------------------------------");

            Console.CursorLeft = 115;
            Console.CursorTop = 11;
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("Prison");

            Console.CursorLeft = 113;
            Console.CursorTop = 26;
            Console.ForegroundColor= ConsoleColor.DarkGreen;
            Console.WriteLine("Poor House");
        }

        public void DrawCity()
        {
            DrawWalls(citySize.MinWidthX, citySize.MinHeightY, citySize.MaxWidthX, citySize.MaxHeightY, null);
        }

        public void DrawPrison()
        {
            char otherWall = 'X';

            DrawWalls(prisonSize.MinWidthX,  prisonSize.MinHeightY, prisonSize.MaxWidthX, prisonSize.MaxHeightY, otherWall);
        }

        public void DrawPoorHouse()
        {
            char otherWall = '0';

            DrawWalls(poorPlaceSize.MinWidthX, poorPlaceSize.MinHeightY, poorPlaceSize.MaxWidthX, poorPlaceSize.MaxHeightY, otherWall);
        }

        public void CheckEncounters()
        {
            foreach (Person person in _population)
            {
                if (person is Thief) ((Thief)person).Scan(_population);
                else if (person is Police) ((Police)person).Scan(_population);
                else if (person is Citizen) ((Citizen)person).GiveUp();
            }
            Logger.PrintQueue();
        }

        public void CreatePopulation()
        {
            for (int p = 0; p < 15; p++)        //Skapar 10 poliser och ger dem namn
            {
                _population.Add(new Police($"P{p + 1}"));
            }
            
            for (int t = 0; t < 15; t++)        //Skapar 30 tjuvar
            {
                _population.Add(new Thief($"T{t + 1}"));
            }

            for (int c = 0; c < 20; c++)        //Skapar 40 medborgare
            {
                _population.Add(new Citizen($"C{c + 1}"));
            }
        }
    }
}