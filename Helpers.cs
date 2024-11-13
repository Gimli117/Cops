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
        public static void CheckSize()
        {
            if (Height == 0 && Width == 0)
            {
                Height = Console.WindowHeight;
                Width = Console.WindowWidth;
            }
            if (Height != Console.WindowHeight || Width != Console.WindowWidth)
            {
                Console.Clear();
            }
            Height = Console.WindowHeight;
            Width = Console.WindowWidth;
        }
        public static void Clear(IEnumerable<Person> thingsToClear)
        {
            CheckSize();
            Foreach(thingsToClear, Clear);
        }

        public static void Clear(Person person)
        {
            int person3 = person.ShowPositionX();
            int person4 = person.ShowPositionY();
            Console.CursorLeft = person3;
            Console.CursorTop = person4;
            Console.Write(" ");
        }

        public static void Foreach<T>(IEnumerable<T> iList, Action<T> callback)
        {
            foreach (var guy in iList)
            {
                callback(guy);
            }
        }
    }
}
