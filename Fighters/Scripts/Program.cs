using System;

namespace IJunior
{
    static class UserUtilits
    {
        public static int GetNumber(int minValue, int maxValue)
        {
            int result = 0;
            bool isWork = true;

            while (isWork)
            {
                if (int.TryParse(Console.ReadLine(), out result))
                {
                    if (result >= minValue && result <= maxValue)
                        isWork = false;
                    else
                        Console.WriteLine($"Данное число не входит в диапозон {minValue} -- {maxValue}");
                }
                else
                {
                    Console.WriteLine("Вы ввели некоректное значение! Попробуйте еще раз.");
                }
            }

            return result;
        }

        public static int GetRandomNumber(int maxValue)
        {
            Random random = new Random();

            return random.Next(++maxValue);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            new Arena().Work();
        }
    }           
}