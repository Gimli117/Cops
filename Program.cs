using System.Globalization;
using System.Text;

namespace TjuvPolis
{
    internal class Program
    {
        static void Main()
        {
            Console.WriteLine("Welcome to Tjuv&Polis! Please maximize the console window and then press enter (and DON'T resize the window!");
            Console.ReadKey();

            Console.CursorVisible = false; ;

            Console.OutputEncoding = Encoding.Unicode;
            City stad = new City();

            stad.DrawOutput();
        }
    }
}