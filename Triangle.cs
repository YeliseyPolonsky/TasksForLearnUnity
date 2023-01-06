using System;
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
            const string OPTION1 = "1";
            const string OPTION2 = "2";
            Console.Write($"Введите:{OPTION1}- обычный треугольник.\n{OPTION2} - перевернутый треуголник.\n\nВаш выбор: ");
            string EulerAngel = Console.ReadLine();

            switch (EulerAngel)
            {
                case OPTION1:
                    DrawDefaultTriangle();
                    break;

                case OPTION2:
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
    }
}
