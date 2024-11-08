using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
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

        public static int roundCount = 1;

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
            // siffrona beskrier olika storleker på väggenrna omkring city.
            citySize = new AreaSize(0, 0, 100, 25);
            //det är en fält som är typ av area size och själva sifrorna är stolrleken omkring fängelsen ,( där tjuverna ska hämna
            // i efter de stålet säkerna från citzen).
            prisonSize = new AreaSize(105, 0, 130, 10);
            //det är också fält som en typ av area size, siffrorna är storleken på fattig hem( där citzen hämnar i om de fick inte kavr item 
            //på grund av tjuverna har stolet).
            poorPlaceSize = new AreaSize(105, 15, 130, 25);
         
            //Void Private Function with side effect of populating _population list with people.
         CreatePopulation();
        }

         
            //Void Private Function with side effect of populating _population list with people.
            
         
        
       //
       
        /// <summary>
        /// 
        /// </summary>
        public void DrawOutput()
        {
            // vi laggt till enn loop som kan ändra färger så som hela backgrunden och texten till vit .
            // om det rätt ändra texten till vit och bakrgundet till svart .
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.Black;
                /// det annars om loppet stämmer inte ska visa den som skriver ska vissas i skärmen.
                
                Console.CursorVisible = false;
               ///  även ska storleken på skärmen ska kunna se längden och höjden
               Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
                // sakerna nere ska ritas som vi har skapat innan .
                DrawCity();
                DrawPrison();
                DrawPoorHouse();
               // beskriver ensamt exakt hur ska texten står typ under fängelsen och fattighem och  vad som händer i city
               /// innnehåller färgen och hur ska rittas ska man  exempelvis att anvnäda 0 till ritta fattig hem och stolrekrna.
                DrawOther();
                ///vi  började sakpa loop till fälten population varje person i city.
                foreach (Person person in _population)
                {
                    // beskriver citzen om som if  den blir fattig eller har inte items kvar på grund av tjuven har stolet 
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

                InteractionsLogic();

                Thread.Sleep(100);
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

        public void DrawOther()
        {
            Console.CursorLeft = 48;
            Console.CursorTop = 0;
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("<City>");

            Console.CursorTop = 26;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\n\n Reports");
            Console.CursorLeft = 0;
            Console.WriteLine("-----------------------------------------------------------------------------------------------------");

            Console.CursorLeft = 114;
            Console.CursorTop = 0;
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("<Prison>");

            Console.CursorLeft = 112;
            Console.CursorTop = 15;
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("<Poor House>");

            Console.CursorTop = 28;
            Console.CursorLeft = 92;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"Round {roundCount}");
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

        public void InteractionsLogic()
        {
            foreach (Person person in _population)
            {
                if (person is Thief)
                {
                    ((Thief)person).Scan(_population);
                    ((Thief)person).CheckJail();
                }
                else if (person is Police) ((Police)person).Scan(_population);
                else if (person is Citizen)
                {
                    ((Citizen)person).GiveUp();
                    ((Citizen)person).CheckPoor();
                }
            }
            Logger.PrintQueue();
            roundCount++;
        }
        /// <summary>
        /// 
        /// </summary>
        public void CreatePopulation()

        {
            //for loppen ska  skapa och ge tjuverna och policer och medborgare namn och visa de från 0 som sifror med texter.


            for (int p = 0; p < 20; p++)        //Skapar 10 poliser och ger dem namn
            {
                _population.Add(new Police($"P{p + 1}"));
                if (p < 15)

                    //  för att kunna skapa lista med medlemmar ligger vi  bokastaven T som referar till tjuv och t är 0 i först a loppen.
                    _population.Add(new Thief($"T{p + 1}"));
                else if (p < 15)
                    _population.Add(new Citizen($"C{p + 1}"));
            }

            for (int t = 0; t < 30; t++)        //Skapar 30 tjuvar
            {
                _population.Add(new Thief($"T{t + 1}"));
            }

            for (int c = 0; c < 40; c++)        //Skapar 40 medborgare
            {
                _population.Add(new Citizen($"C{c + 1}"));

                //for loppen ska  skapa och ge tjuverna och policer och medborgare namn och visa de från 0 som sifror med texter.


                for (int p = 0; p < 20; p++)        //Skapar 10 poliser och ger dem namn
                {
                    _population.Add(new Police($"P{p + 1}"));
                    if (p < 15)

                        //  för att kunna skapa lista med medlemmar ligger vi  bokastaven T som referar till tjuv och t är 0 i först a loppen.
                        _population.Add(new Thief($"T{p + 1}"));
                    else if (p < 15)
                        _population.Add(new Citizen($"C{p + 1}"));

                }
                //Skapar 30 tjuvar



            }

        }