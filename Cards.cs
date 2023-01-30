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
            ChangeBackgroundColor(ConsoleColor.DarkCyan);
            ChangeForegroundColor(ConsoleColor.Yellow);
            Console.Write("Введите:\n1 - обычный треугольник.\n2 - перевернутый треуголник.\n\nВаш выбор: ");
            int EulerAngel = Convert.ToInt32(Console.ReadLine());
            

            switch (EulerAngel)
            {
                case 1:
                    int CountLine = AskQuationOfHeight();
                    ChangeForegroundColor(ConsoleColor.Green);
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
                    break;
                case 2:
                    CountLine = AskQuationOfHeight();
                    ChangeForegroundColor(ConsoleColor.Green);
                    CountOfStarsAtLine = CountLine;
                    for (int i = 0; i < CountLine; i++)
                    {
                        for (int j = 0; j < CountOfStarsAtLine; j++)
                        {
                            Console.Write("*");
                        }
                        CountOfStarsAtLine--;
                        Console.WriteLine();
                    }
                    break;
                default:
                    Console.Write("Вы ввели некоректное число");
                    break;
            }
            ChangeForegroundColor(ConsoleColor.Yellow);
            Console.WriteLine("\nГотово!");
            Console.ReadKey(true);
        }

        static void ChangeBackgroundColor(ConsoleColor backgroungColor)
        {
            Console.BackgroundColor = backgroungColor;
            Console.Clear();
        }

        static void ChangeForegroundColor(ConsoleColor foregroundColor)
        {
            Console.ForegroundColor = foregroundColor;
        }

        static int AskQuationOfHeight()
        {
            Console.Write("Какой высоты треугольник? : ");
            int CountLine = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("\n");
            return CountLine;
        }
    }

}
