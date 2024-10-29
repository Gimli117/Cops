namespace TjuvPolis
{
    internal class Program
    {
        static void Main()
        {
            List<Person> person = new List<Person>();
            CitySize size = new CitySize()
            {
                Width = 5,
                Height = 5
            };
            Console.WriteLine("CitySize Is Width:: {0}", size.Width);
            Console.WriteLine("CitySize Is Height:: {0}", size.Height);
            City stad = new City(size, person);
            City stad2 = new City(10, 10, []);
            City stad3 = new City();

            // Item<int> item = new Item<int>();

            // item.S

        }

        /*class Item<T>
        {
            public T S { get; set; }
        }*/
    }
}
