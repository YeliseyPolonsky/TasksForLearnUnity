using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConvertValuta
{
    class Program
    {
        static void Main(string[] args)
        {
            float dollars = 500;
            float euros = 500;
            float rubles = 500;

            float dollarsToRubles = 72.5f;
            float dollarsToEuro = 0.94f;
            float eurosToDallars = 1.064f;
            float eurosToRubles = 76.92f;
            float rublesToDollars = 0.0138f;
            float rublesToEuro = 0.013f;

            const int CONVERT_dollarsToRubles = 1;
            const int CONVERT_dollarsToEuro = 2;
            const int CONVERT_eurosToDallars = 3;
            const int CONVERT_eurosToRubles = 4;
            const int CONVERT_rublesToDollars = 5;
            const int CONVERT_rublesToEuro = 6;
            const int EXIT = 7;

            bool isWorking = true;

            while (isWorking)
            {
                ShowInfo(ref rubles, ref dollars, ref euros);

                switch (GetNumber("Что вы хотите сделать : \n" +
            $"1 - конвертировать доллары в рубли.\n" +
            $"2 - конвертировать доллары в евро.\n" +
            $"3 - конвертировать евро в доллары.\n" +
            $"4 - конвертировать евро в рубли.\n" +
            $"5 - конвертировать рубли в доллары.\n" +
            $"6 - конвертировать рубли в евро.\n" +
            $"7 - выйти из программы.",
            new List<int>() { CONVERT_dollarsToRubles, CONVERT_dollarsToEuro, CONVERT_eurosToDallars, CONVERT_eurosToRubles, CONVERT_rublesToDollars, CONVERT_rublesToEuro, EXIT }))
                {
                    case CONVERT_dollarsToRubles:
                        Convert(ref dollars, ref rubles, dollarsToRubles);
                        break;

                    case CONVERT_dollarsToEuro:
                        Convert(ref dollars, ref euros, dollarsToEuro);
                        break;

                    case CONVERT_eurosToDallars:
                        Convert(ref euros, ref dollars, eurosToDallars);
                        break;

                    case CONVERT_eurosToRubles:
                        Convert(ref euros, ref rubles, eurosToRubles);
                        break;

                    case CONVERT_rublesToDollars:
                        Convert(ref rubles, ref dollars, rublesToDollars);
                        break;

                    case CONVERT_rublesToEuro:
                        Convert(ref rubles, ref euros, rublesToEuro);
                        break;

                    case EXIT:
                        isWorking = false;
                        break;

                }
                Console.Clear();
            }
            Console.WriteLine("Конец");
        }

        static void Convert(ref float currencyOfConvert, ref float currencyToConvert, float rate)
        {
            float countCurrency = GetCountCurrensy();

            if (countCurrency <= currencyOfConvert)
            {
                currencyToConvert += countCurrency * rate;
                currencyOfConvert -= countCurrency;
            }
            else
            {
                Console.WriteLine("У вас столько нету!!");
                Console.ReadKey();
            }
        }

        static int GetNumber(string text, List<int> options)
        {
            int number = 0;
            bool isWorking = true;
            Console.WriteLine(text);

            while (isWorking)
                if (int.TryParse(Console.ReadLine(), out number))
                {
                    foreach (int option in options)
                    {
                        if (number == option)
                            isWorking = false;
                    }
                }
                else
                    Console.WriteLine("Ошибка!");
            return number;
        }

        static float GetCountCurrensy()
        {
            Console.WriteLine("Cколько валюты вы хотите конвертировать?");
            float countCurrency = float.Parse(Console.ReadLine());
            return countCurrency;
        }

        static void ShowInfo(ref float rublus, ref float dollars, ref float euros)
        {
            Console.WriteLine($"Рубли = {rublus}");
            Console.WriteLine($"Доллары = {dollars}");
            Console.WriteLine($"Евро = {euros}");
            Console.WriteLine();
        }
    }
}