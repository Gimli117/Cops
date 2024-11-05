using System.Text;

namespace TjuvPolis
{
    internal class Program
    {
        static void Main()
        {
            Console.OutputEncoding = Encoding.Unicode;
            City stad = new City();
            stad.DrawOutput();
        }
    }
}