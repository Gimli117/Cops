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
        }      //Lista med alla personer i staden

        public City(List<Person> population)
        {
            _population = population;
        }

        public City()
        {
            _population = new List<Person>();

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

                char wall = '█';

                for (int x = 0; x <= CitySize.Width; x++)
                {
                    for (int y = 0; y <= CitySize.Height; y++) 
                    {

                        Console.CursorTop = y;
                        Console.CursorLeft = x;
                        if (x == 0 || x == CitySize.Width)
                        {
                            Console.Write(wall);
                    
                        }
                        else if(y == 0  || y == CitySize.Height)
                        {
                            Console.Write(wall);
                        }                   
                    }
                }
               
                foreach (Person person in _population)
                {
                    person.DrawPerson();    
                }
                CheckEncounters();
                Thread.Sleep(500);
                //Console.ReadLine();
                Console.Clear();
            }
        }
        public void CheckEncounters()
        {
            foreach (Person person in _population)
            {
                if (person is Thief ) ((Thief)person).Scan(_population);
                if (person is Police) ((Police)person).Scan(_population);
            }
        }

        public void CreatePopulation()
        {
            for (int p = 0; p < 30; p++)        //Skapar 30 poliser och ger dem namn
            {
                _population.Add(new Police($"P{p + 1}"));
            }
            
            for (int t = 0; t < 10; t++)        //Skapar 10 tjuvar
            {
                _population.Add(new Thief($"T{t + 1}"));
            }

            for (int c = 0; c < 40; c++)        //Skapar 40 medborgare
            {
                _population.Add(new Citizen($"C{c + 1}"));
            }
        }
    }

    class Prison : City
    {
        public Prison(int[,]citySize, List<Person> population) : base()
        {
            // List<Person> prisoners = new List<Person>;
        }
    }
}