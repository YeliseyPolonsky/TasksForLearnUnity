using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp31
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.Clear();

            Console.WriteLine("Введите:\n1- треугольник обычный.\n2 - треуголник перевернутый.");
            int EulerAngel = Convert.ToInt32(Console.ReadLine());
            Console.Write("Какой высоты треугольник? : ");
            int CountLine = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Green;

            if (EulerAngel == 1)
            {
                int K = 1;
                for (int i = 0; i < CountLine; i++)
                {

                    for (int j = 0; j < K; j++)
                    {
                        Console.Write("*");
                    }
                    ++K;
                    Console.WriteLine();
                }

            }

            else
            {
                int K = CountLine;
                for (int i = 0; i < CountLine; i++)
                {
                    for (int j = 0; j < K; j++)
                    {
                        Console.Write("*");
                    }
                    K--;
                    Console.WriteLine();
                }
            }
            Console.ReadKey(true);




        }
    }
}
