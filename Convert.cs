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

            Dictionary<string, float> rateCurrency = new Dictionary<string, float>();
            rateCurrency.Add("dollarsToRubles", dollarsToRubles);
            rateCurrency.Add("dollarsToEuro", dollarsToEuro);
            rateCurrency.Add("eurosToDallars", eurosToDallars);
            rateCurrency.Add("eurosToRubles", eurosToRubles);
            rateCurrency.Add("rublesToDollars", rublesToDollars);
            rateCurrency.Add("rublesToEuro", rublesToEuro);

            const int CONVERT = 1;
            const int EXIT = 2;

            bool isWorking = true;

            while (isWorking)
            {
                ShowInfo(ref rubles, ref dollars, ref euros);

                if (GetNumber($"Что бы вы хотели сделать: {CONVERT} - конвертировать.\n" +
                    $"{EXIT} - выйти из рограммы.\n" +
                    "Ваш выбор: ", new List<int> { EXIT, CONVERT }) == EXIT)
                    isWorking = false;
                else
                    switch (GetNumber("Какую валюту вы хотите конвертировать : \n" +
                $"1 - рубли.\n2 - доллары.\n3 - евро.", new List<int>() { 1, 2, 3 }))
                    {
                        case 1:
                            ConvertRubles(ref rubles, ref dollars, ref euros, rateCurrency);
                            break;

                        case 2:
                            ConvertDollars(ref rubles, ref dollars, ref euros, rateCurrency);
                            break;

                        case 3:
                            ConvertEuros(ref rubles, ref dollars, ref euros, rateCurrency);
                            break;
                    }
                Console.Clear();
            }
            Console.WriteLine("Конец");
        }


        static void ConvertRubles(ref float rublus, ref float dollars, ref float euros, Dictionary<string, float> rateCurrency)
        {
            Console.WriteLine("Сколько рублей вы хотите конвертировать?");
            float countCurrency = Convert.ToSingle(Console.ReadLine());

            if (countCurrency <= rublus)
            {
                switch (GetNumber("В какую валюту вы хотите конвертировать? : \n"
                        + $"1 - доллары.\n2 - евро.", new List<int>() { 1, 2 }))
                {
                    case 1:
                        dollars += countCurrency * rateCurrency["rublesToDollars"];
                        break;

                    case 2:
                        euros += countCurrency * rateCurrency["rublesToEuros"];
                        break;
                }
                rublus -= countCurrency;
            }
            else
            {
                Console.WriteLine("У вас столько нету!!");
                Console.ReadKey();
            }
        }

        static void ConvertDollars(ref float rublus, ref float dollars, ref float euros, Dictionary<string, float> rateCurrency)
        {
            Console.WriteLine("Сколько долларов вы хотите конвертировать?");
            float countCurrency = Convert.ToSingle(Console.ReadLine());

            if (countCurrency <= dollars)
            {
                switch (GetNumber("В какую валюту вы хотите конвертировать? : \n"
                        + $"1 - рубли.\n2 - евро.", new List<int>() { 1, 2 }))
                {
                    case 1:
                        rublus += countCurrency * rateCurrency["dollarsToRubles"];
                        break;

                    case 2:
                        euros += countCurrency * rateCurrency["dollarsToEuros"];
                        break;
                }
                dollars -= countCurrency;
            }
            else
            {
                Console.WriteLine("У вас столько нету!!");
                Console.ReadKey();
            }
        }

        static void ConvertEuros(ref float rublus, ref float dollars, ref float euros, Dictionary<string, float> rateCurrency)
        {
            Console.WriteLine("Сколько евро вы хотите конвертировать?");
            float countCurrency = Convert.ToSingle(Console.ReadLine());

            if (countCurrency <= euros)
            {
                switch (GetNumber("В какую валюту вы хотите конвертировать? : \n"
                        + $"1 - рубли.\n2 - доллары.", new List<int>() { 1, 2 }))
                {
                    case 1:
                        rublus += countCurrency * rateCurrency["eurosToRubles"];
                        break;

                    case 2:
                        dollars += countCurrency * rateCurrency["eurosToDollars"];
                        break;
                }
                euros -= countCurrency;
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

        static void ShowInfo(ref float rublus, ref float dollars, ref float euros)
        {
            Console.WriteLine($"Рубли = {rublus}");
            Console.WriteLine($"Доллары = {dollars}");
            Console.WriteLine($"Евро = {euros}");
            Console.WriteLine();
        }
    }
}