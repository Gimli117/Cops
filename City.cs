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
        /// <summary>
        /// citySize är en fält som är medlem i city som kan inte kommas åt i andra classer för att den är privat 
        /// även det är typ av AreaSize 
        /// ADDING FOR TESTING PURPOSES
        /// </summary>
        private readonly AreaSize citySize;

        /// <summary>
        /// perisonsize är en fält som är medlem i city som kan inte kommas åt i andra classer för att den är privat
        /// även det är typ av AreaSize
        /// </summary>
        private readonly AreaSize prisonSize;

        /// <summary>
        /// poorPlaceSize är en fält som är medlem i ciy som kan inte kommas åt i andra classer för att den är privat 
        /// även det är typ av Areasize .
        /// </summary>
        private readonly AreaSize poorPlaceSize;
        /// <summary>
        /// Wall är en char som är en fält som är medlem i city som kan inte kommas åt i an dra classer för att den är privat
        /// även det är en konstant altså man kan inte ändra det som virable
        /// </summary>
        const char wall = '█';
        
        /// <summary>
        /// _population är en medlem i city classen som är special fält som heter backingfält för propeprty Population
        ///  och där kan skickas ut genom get metoden och värdet ändras i den genom sett metoden
        /// </summary>
        private List<Person> _population;

        /// <summary>
        /// Population är en medlem i city classen som är proppety som kan använda en fält som heter backningsfält
        /// även propety kan innehålla get metoden och värdet ändras genom sett metoden . Det är ett Public proetty
        // som har accecee modefire, denna fältet kan användas i andra classer.
        /// </summary>
        public List<Person> Population { 
            get
            {
                return _population;
            }
            set
            {
                _population = value;
            }
        }      // Lista med alla personer i staden

        /// <summary>
        /// City är en Public som är kunstroktur vilken kan skapa obejkt i andra classer och den skrivs efter ordet new
        /// </summary>
        public City()
        {
            //har vi skapat en lista med hjälp av kunstrokter vilken är kan skrivas efter ord new , vi har också skapat en lista
            //av typen peron i city classen då vi lagt till en ny lista till _population.

            _population = new List<Person>();
            //har vi kallat en kunstrokter som är typ av AreaSize i city classen 
            //
            citySize = new AreaSize(0, 0, 100, 25);
            prisonSize = new AreaSize(105, 0, 130, 10);
            poorPlaceSize = new AreaSize(105, 15, 130, 25);

            CreatePopulation();
        }
        public void DrawOutput()
        {
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.Black;

                Console.CursorVisible = false;
                Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);

                DrawCity();
                DrawPrison();
                DrawPoorHouse();

                foreach (Person person in _population)
                {
                    if (person is Citizen && ((Citizen)person).isPoor)
                    {
                        person.DrawPerson(poorPlaceSize);
                    }
                    else if (person is Thief && ((Thief)person).Prisonized)
                    {
                        person.DrawPerson(prisonSize);
                    }
                    else
                    {
                        person.DrawPerson(citySize);
                    }
                }

                CheckEncounters();

                Thread.Sleep(500);
                //Console.ReadLine();
                Console.Clear();
            }
        }

        public void DrawWalls(int MinWidthX, int MinHeightY, int MaxWidthX, int MaxHeightY, char? otherWall)
        {
            for (int x = MinWidthX; x <= MaxWidthX; x++)
            {
                for (int y = MinHeightY; y <= MaxHeightY; y++)
                {
                    Console.CursorTop = y;
                    Console.CursorLeft = x;
                    if (x == MinWidthX || x == MaxWidthX)
                    {
                        Console.Write((otherWall is null) ? wall : otherWall);
                    }
                    else if (y == MinHeightY || y == MaxHeightY)
                    {
                        Console.Write((otherWall is null) ? wall : otherWall);
                    }
                }
            }
        }

        public void DrawCity()
        {
            DrawWalls(citySize.MinWidthX, citySize.MinHeightY, citySize.MaxWidthX, citySize.MaxHeightY, null);
        }

        public void DrawPrison()
        {
            char otherWall = 'X';

            DrawWalls(prisonSize.MinWidthX,  prisonSize.MinHeightY, prisonSize.MaxWidthX, prisonSize.MaxHeightY, otherWall);
        }

        public void DrawPoorHouse()
        {
            char otherWall = '0';

            DrawWalls(poorPlaceSize.MinWidthX, poorPlaceSize.MinHeightY, poorPlaceSize.MaxWidthX, poorPlaceSize.MaxHeightY, otherWall);
        }

        public void CheckEncounters()
        {
            foreach (Person person in _population)
            {
                if (person is Thief) ((Thief)person).Scan(_population);
                else if (person is Police) ((Police)person).Scan(_population);
                else if (person is Citizen) ((Citizen)person).GiveUp();
            }
        }

        public void CreatePopulation()
        {
            for (int p = 0; p < 10; p++)        //Skapar 10 poliser och ger dem namn
            {
                _population.Add(new Police($"P{p + 1}"));
            }
            
            for (int t = 0; t < 30; t++)        //Skapar 30 tjuvar
            {
                _population.Add(new Thief($"T{t + 1}"));
            }

            for (int c = 0; c < 40; c++)        //Skapar 40 medborgare
            {
                _population.Add(new Citizen($"C{c + 1}"));
            }
        }
    }
}