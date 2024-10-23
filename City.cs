using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TjuvPolis
{
    internal class City
    {
        public int[,] CitySize { get; set; }
        public List<Person> Population { get; set; }      //Lista med alla personer i staden

        public City(int[,] citySize, List<Person> population)
        {
            CitySize = citySize;
            Population = population;
        }

        public void DrawOutput()
        {
            // Skriv ut allt i konsolen
        }

        public void ChangeDirection()
        {
            // Kollision vid en vägg
        }
    }

    class Prison : City
    {
        public Prison(int[,]citySize, List<Person> population) : base(citySize, population)
        {
            // List<Person> prisoners = new List<Person>;
        }
    }
}
