﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Triangles
{
    class Program
    {
        static void Main(string[] args)
        {
            const int DefaultTriangleCommand = 1;
            const int FlippedOverTtiangleCommand = 2;
            Console.Write($"Введите:{DefaultTriangleCommand}- обычный треугольник.\n{FlippedOverTtiangleCommand} - перевернутый треуголник.\n\nВаш выбор: ");
            string EulerAngel = Console.ReadLine();

            switch (EulerAngel)
            {
                case DefaultTriangleCommand:
                    DrawDefaultTriangle();
                    break;

                case FlippedOverTtiangleCommand:
                    DrawFlippedOverTtiangle();
                    break;

                default:
                    Console.Write("Вы ввели некоректное значение");
                    break;
            }

            Console.WriteLine("\nГотово!");
            Console.ReadKey(true);
        }

        static int AskQuationOfHeight()
        {
            Console.Write("Какой высоты треугольник? : ");
            int CountLine = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("\n");
            return CountLine;
        }

        static void DrawDefaultTriangle()
        {
            int CountLine = AskQuationOfHeight();
            int CountOfStarsAtLine = 1;

            for (int i = 0; i < CountLine; i++)
            {

                for (int j = 0; j < CountOfStarsAtLine; j++)
                {
                    Console.Write("*");
                }

                ++CountOfStarsAtLine;
                Console.WriteLine();
            }
        }

        static void DrawFlippedOverTtiangle()
        {
            int CountLine = AskQuationOfHeight();
            int CountOfStarsAtLine = CountLine;

            for (int i = 0; i < CountLine; i++)
            {

                for (int j = 0; j < CountOfStarsAtLine; j++)
                {
                    Console.Write("*");
                }

                CountOfStarsAtLine--;
                Console.WriteLine();
            }
        }

        static int GetString(int command)
        { 
                return Convert.ToInt32(command);     
        }
    }
}

