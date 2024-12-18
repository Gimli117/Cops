﻿using System;
using System.Collections.Generic;
using System.Data;
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
            //
            citySize = new AreaSize(0, 0, 100, 25);
            prisonSize = new AreaSize(105, 0, 130, 10);
            poorPlaceSize = new AreaSize(105, 15, 130, 25);
            //Void Private Function with side effect of populating _population list with people. 
            CreatePopulation();
        }
        

        public int DrawOutput()
        {
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.Black;

                Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);

                DrawCity();
                DrawPrison();
                DrawPoorHouse();
                DrawOther();

                PrintRoundCount();

                Helpers.Clear(_population);

                foreach (Person person in _population)
                {
                    if (person is Citizen && ((Citizen)person).IsPoor)
                    {
                        person.DrawPerson(poorPlaceSize);

                        PrisonLogger.AddPoorHouseInfo((Citizen)person);
                    }
                    else if (person is Thief && ((Thief)person).Prisonized)
                    {
                        person.DrawPerson(prisonSize);

                        PrisonLogger.AddPrisonInfo((Thief)person);
                    }
                    else
                    {
                        person.DrawPerson(citySize);
                    }
                }

                InteractionsLogic();

                Logger.PrintQueue();

                Thread.Sleep(500);

                roundCount++;

                if (Logger.newEncounter)
                {
                    //Console.ReadLine();
                    Thread.Sleep(2000);
                    Logger.newEncounter = false;
                }
            }
        }

        public void DrawWalls(int MinWidthX, int MinHeightY, int MaxWidthX, int MaxHeightY, char? otherWall)
        {
            Console.CursorTop = MinHeightY;
            Console.CursorLeft = MinWidthX;

            int PosX = 0;

            for (int x = 0; x <= (MaxWidthX - MinWidthX); x++)          //Taket och Botten
            {
                Console.Write((otherWall is null) ? wall : otherWall);
                Console.CursorTop = MaxHeightY;
                Console.Write((otherWall is null) ? wall : otherWall);
                Console.CursorTop = MinHeightY;

                Console.CursorLeft = MinWidthX + PosX;

                PosX++;
            }

            Console.CursorTop = MinHeightY;
            Console.CursorLeft = MinWidthX;

            for (int x = 0; x <= (MaxHeightY - MinHeightY); x++)        //Vänster o Höger vägg
            {
                Console.Write((otherWall is null) ? wall : otherWall);
                Console.CursorLeft = MaxWidthX;
                Console.Write((otherWall is null) ? wall : otherWall);
                Console.CursorLeft = MinWidthX;
                Console.CursorTop++;
            }
        }

        public void DrawOther()
        {
            Console.CursorLeft = 48;
            Console.CursorTop = 26;
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("<City>");

            Console.CursorTop = 26;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\n\n Reports");
            Console.CursorLeft = 0;
            Console.WriteLine("-----------------------------------------------------------------------------------------------------");

            Console.CursorLeft = 114;
            Console.CursorTop = 11;
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("<Prison>");

            Console.CursorLeft = 112;
            Console.CursorTop = 26;
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("<Poor House>");
        }

        public void PrintRoundCount()
        {
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
                    ((Citizen)person).CheckPoor();
                }
            }
        }

        public void CreatePopulation()
        {
            for (int i = 0; i < 30; i++)
            {
                if (i < 10)                                     
                {
                    _population.Add(new Police($"P{i + 1}"));       //Skapar 10 poliser
                }

                if (i < 20)                                    
                {
                    _population.Add(new Thief($"T{i + 1}"));        //Skapar 20 tjuvar
                }
                
                _population.Add(new Citizen($"C{i + 1}"));          //Skapar 30 medborgare
            }
        }
    }
}