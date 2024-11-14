using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace TjuvPolis
{
    public class CitySize
    {
        public static int Width = 100;
        public static int Height = 25;
    }
    internal class Person
    {
        public string Name { get; set; }

        protected Position Pos { get; set; }
        private static Random random = new Random();
        private static int personNum;

        public Person(string name)

        {
            Pos = new Position();

            Pos.X = random.Next(1, 97);
            Pos.Y = random.Next(1, 24);

            Name = name;
        }

        int direction = random.Next(0, 9);
        int directionCounter = 0;
        public void DrawPerson(AreaSize city)
        {
            CheckCollision(city);

            UpdatePos(direction);

            Console.CursorLeft = Pos.X;
            Console.CursorTop = Pos.Y;

            Console.Write(ToString());

            if (directionCounter == 5)           // Byter håll var femte turn
            {
                direction = random.Next(0, 9);
                directionCounter = 0;
            }
            directionCounter++;
        }

        public void CheckCollision(AreaSize size)             // Kollision vid en vägg
        {
            switch (direction)
            {
                case 0:                                     // Left
                    if (Pos.X - 1 == size.MinWidthX)                     // Left Wall Collision
                    {
                        direction = 2;  //Right
                    }
                    break;

                case 1:                                     // Up
                    if (Pos.Y - 1 == size.MinHeightY)                     // Ceiling Collision
                    {
                        direction = 3;  //Down
                    }
                    break;

                case 2:                                     // Right
                    if (Pos.X + 1 == size.MaxWidthX-2)        // Right Wall Collision
                    {
                        direction = 0;  //Left
                    }
                    break;

                case 3:                                     // Down
                    if (Pos.Y + 1 == size.MaxHeightY)       // Bottom Collision
                    {
                        direction = 1;  //Up
                    }
                    break;

                case 4:                                                    // Left+Down
                    if (Pos.X - 1 == size.MinWidthX)                                    // Left Wall Collision
                    {
                        direction = 7;  //Right+Down
                    }
                    if (Pos.Y + 1 == size.MaxHeightY)                      // Bottom Collision
                    {
                        direction = 5;  //Left+Up
                    }
                    if (Pos.X - 1 == size.MinWidthX && Pos.Y + 1 == size.MaxHeightY)    // CORNER collision
                    {
                        direction = 6;  //Right+Up
                        //Console.BackgroundColor = ConsoleColor.DarkYellow;
                    }
                    break;

                case 5:                                                    // Left+Up
                    if (Pos.X - 1 == size.MinWidthX)                                    // Left Wall collision
                    {
                        direction = 6;  //Right+Up
                    }
                    if (Pos.Y - 1 == size.MinHeightY)                                    // Ceiling collision
                    {
                        direction = 4;  //Left+Down
                    }
                    if (Pos.X - 1 == size.MinWidthX && Pos.Y - 1 == size.MinHeightY)                  // CORNER collision
                    {
                        direction = 7;  //Right+Down
                        //Console.BackgroundColor = ConsoleColor.DarkYellow;
                    }
                    break;

                case 6:                                                    // Right+Up
                    if (Pos.X + 1 == size.MaxWidthX-2)                       // Right Wall collision
                    {
                        direction = 5;  //Left+Up
                    }
                    if (Pos.Y - 1 == size.MinHeightY)                                    // Ceiling collision
                    {
                        direction = 7;  //Right+Down
                    }
                    if (Pos.X + 1 == size.MaxWidthX-2 && Pos.Y - 1 == size.MinHeightY)     // CORNER collision
                    {
                        direction = 4;  //Left+Down
                        //Console.BackgroundColor = ConsoleColor.DarkYellow;
                    }
                    break;

                case 7:                                                    // Right+Down
                    if (Pos.X + 1 == size.MaxWidthX-2)                       // Right Wall collision
                    {
                        direction = 4;  //Left+Down
                    }
                    if (Pos.Y + 1 == size.MaxHeightY)                      // Bottom collision
                    {
                        direction = 6;  //Right+Up
                    }
                    if (Pos.X + 1 == size.MaxWidthX-2 && Pos.Y + 1 == size.MaxHeightY)     // CORNER collision
                    {
                        direction = 5;  //Left+Up
                        //Console.BackgroundColor = ConsoleColor.DarkYellow;
                    }
                    break;

                default:                                      // Freeze...
                    break;
            }
        }
    

        new public virtual string ToString()
        {
            return "X";
        }

        protected virtual void WritePosition()
        {
            Console.WriteLine($"Person{personNum} med X:{Pos.X}, Y:{Pos.Y}");
            personNum++;
        }
        public int ShowPositionX()
        {
            return Pos.X;
        }
        public int ShowPositionY()
        {
            return Pos.Y;
        }
        public void UpdatePos(int direction)
        {
            switch (direction) 
            {
                case 0:         //personen ska röra sig till vänster
                    Pos.X--;
                    break;

                case 1:         //personen ska röra sig uppåt
                    Pos.Y--;
                    break;

                case 2:         //personen ska röra sig höger
                    Pos.X++;
                    break;

                case 3:         //personen ska röra sig neråt
                    Pos.Y++;
                    break;

                case 4:         //personen ska röra sig snett ner till vänster
                    Pos.X--;
                    Pos.Y++;
                    break;

                case 5:         //personen ska röra sig snett upp till vänster
                    Pos.X--;
                    Pos.Y--;
                    break;

                case 6:         //personen ska röra sig snett upp till höger
                    Pos.X++;
                    Pos.Y--;
                    break;

                case 7:         //personen ska röra sig snett ner höger
                    Pos.X++;
                    Pos.Y++;
                    break;

                default:         //personen står still
                    break;
            }
        }
    }
}