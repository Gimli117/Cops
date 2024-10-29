using System;
using System.Collections.Generic;
using System.Linq;
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
            Population = population;

            CreatePopulation();

            foreach (Police police in _population)
            {
                Console.WriteLine(police.Name);
            }
        }
        public City(int widht, int height, List<Person> population)
        {
            Size = new CitySize()
            {
                Width = widht,
                Height = height
            };
            Population = population;
        }

        public City()
        {
            Size = new CitySize()
            {
                Width = 10,
                Height = 10
            };
            Population = new List<Person>();
        }

        public void DrawOutput()
        {
            //Skriv ut allt i konsolen
            /*Person person = new Person();
            {
                var a = (person is Thief) ? person as Thief : null;
            }*/
        }
        public void CreatePopulation()
        {
            for (int p = 0; p < 20; p++)
            {
                _population.Add(new Police($"P{p+1}"));
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
