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

            //foreach (Person person in _population)
            //{
            //    string result = ((person is Police) ? (Police)person : (person is Thief) ? (Thief)person : (person is Citizen) ? (Citizen)person : person).Name;

            //    Console.ForegroundColor = (person.GetType().FullName == "TjuvPolis.Police") ? (Police.PoliceColor) : 
            //        (person.GetType() == typeof(Thief)) ? (Thief.ThiefColor) : 
            //        (person.GetType() == typeof(Citizen)) ? (Citizen.CitizenColor) : 
            //        ConsoleColor.White;

            //    Console.WriteLine(result);
            //}
            //Console.ForegroundColor = ConsoleColor.White;

            //Console.WriteLine("Ska vara vit...");
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

                Logger.Print();

                Thread.Sleep(500);
                //Console.ReadLine();
                Console.Clear();
            }
        }

        public void CreatePopulation()
        {
            for (int p = 0; p < 20; p++)        //Skapar 20 poliser och ger dem namn
            {
                _population.Add(new Police($"P{p + 1}"));
            }
            
            for (int t = 0; t < 20; t++)        //Skapar 20 tjuvar
            {
                _population.Add(new Thief($"T{t + 1}"));
            }

            for (int c = 0; c < 10; c++)        //Skapar 10 medborgare
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
