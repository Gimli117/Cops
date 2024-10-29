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
        private CitySize Size { get; set; }
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


        public City(CitySize citySize, List<Person> population)
        {
            Size = citySize;
            _population = population;
        }

        public City(int widht, int height, List<Person> population)
        {
            Size = new CitySize()
            {
                Width = widht,
                Height = height
            };
            _population = population;
        }

        public City()
        {
            Size = new CitySize()
            {
                Width = 100,
                Height = 25
            };
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
            for (int x = 0; x <= Size.Width; x++)
            {
                for (int y = 0; y <= Size.Height; y++) 
                {
                
                    Console.CursorTop = y;
                    Console.CursorLeft = x;
                    if (x == 0 || x == Size.Width) 
                    {
                        Console.Write("|");
                    
                    }
                    else if(y==0  || y == Size.Height)
                    {
                        Console.Write("-");
                    }
                   
                }
            }
            foreach (Person person in _population) {
                person.DrawPerson();
           };
            Console.ReadLine();
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

        public void ChangeDirection()
        {
            // Kollision vid en vägg
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
