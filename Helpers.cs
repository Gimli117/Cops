using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TjuvPolis
{
    internal static class Helpers
    {
        public static int Height { get; set; }
        public static int Width { get; set; }

        public static void Clear(List<Person> people)
        {
            foreach (Person person in people)
            {
                int person3 = person.ShowPositionX();
                int person4 = person.ShowPositionY();
                Console.CursorLeft = person3;
                Console.CursorTop = person4;
                Console.Write(" ");
            }
        }

        public static void Clear(Person person)
        {
            int person3 = person.ShowPositionX();
            int person4 = person.ShowPositionY();
            Console.CursorLeft = person3;
            Console.CursorTop = person4;
            Console.Write(" ");
        }
    }
}