using System.Text;

namespace TjuvPolis
{
    internal class Program
    {
        static void Main()
        {
            Console.OutputEncoding = Encoding.Latin1;
            City stad = new City();

            stad.DrawOutput();
        }
    }
}