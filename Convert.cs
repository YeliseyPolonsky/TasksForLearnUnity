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
            float dollars = 30;
            float euros = 20;
            float rubles = 100;
            bool isWorking = true;
            float dollarsToRubles = 72.5f;
            float dollarsToEuro = 0.94f;
            float rublesToEuro = 0.013f; 
            Dictionary<int, float> numberForCountOfCurrensy = new Dictionary<int, float>();
            numberForCountOfCurrensy.Add(1, dollars);
            numberForCountOfCurrensy.Add(2, euros);
            numberForCountOfCurrensy.Add(3, rubles);
            Dictionary<int, float> numberForExchangeRateOfCurrensy = new Dictionary<int, float>();
            numberForExchangeRateOfCurrensy.Add(1, dollarsToEuro);
            numberForExchangeRateOfCurrensy.Add(2, dollarsToRubles);
            numberForExchangeRateOfCurrensy.Add(3, rublesToEuro);


            while (isWorking)
            {
                const int CONVERT = 1;
                const int EXIT = 2;
                Console.WriteLine($"доллары - {numberForCountOfCurrensy[1]}");
                Console.WriteLine($"евро - {numberForCountOfCurrensy[2]}");
                Console.WriteLine($"рубли - {numberForCountOfCurrensy[3]}");
                Console.WriteLine();
                Console.WriteLine($"Что бы вы хотели сделать: {CONVERT} - конвертировать.\n" +
                    $"{EXIT} - выйти из рограммы.\n" +
                    "Ваш выбор: ");

                if (GetNumber() == CONVERT)
                {
                    PreConvert(numberForCountOfCurrensy, numberForExchangeRateOfCurrensy);
                }
                else if (GetNumber() == EXIT)
                    break;
                else
                    Console.WriteLine("Вы ввели некоректное число!");

                Console.WriteLine("Готово");
                Console.ReadLine();
                Console.Clear();
            }
        }

        static int GetNumber()
        {
            int number = 0;
            bool isWorking = true;

            while (isWorking)
                if (int.TryParse(Console.ReadLine(), out number))
                    isWorking = false;
                else
                    Console.WriteLine("Вы ввели некоректное число!");
            return number;
        }

        static float FloatCountOfCurrensy(float maxCount)
        {
            bool isWorking = true;
            float floatCountOfCurrensy = 0;

            while (isWorking)
                if (float.TryParse(Console.ReadLine(), out floatCountOfCurrensy) && floatCountOfCurrensy>=maxCount)
                    isWorking = false;
                else
                    Console.WriteLine("Вы ввели некоректное число! или превышающее ваш кошелек");

            return floatCountOfCurrensy;
        }

        static void PreConvert(Dictionary<int,float> numberForCountOfCurrensy,Dictionary<int,float> numberForExchangeRateOfCurrensy)
        {
            float maxCountOfCurrensy = 0;
            float changeRate;
            Console.WriteLine("Какую валюту вы хотите конвертировать : \n" +
                $"1 - доллары.\n2 - евро.\n3 - рубли.");
            int userInputNumberCurrensy = GetNumber();

            if (numberForCountOfCurrensy.ContainsKey(userInputNumberCurrensy))
            {
                maxCountOfCurrensy = numberForCountOfCurrensy[userInputNumberCurrensy];
                Console.Write("Cколько валюты вы хотите конвнртировать? : ");
                float countCurrensy = FloatCountOfCurrensy(maxCountOfCurrensy);

                Console.WriteLine("В какую валюту вы хотите конвертировать? : \n"
                    + $"1 - доллары.\n2 - евро.\n3 - рубли.");
                int userInputNumberOfWontCurrensy = GetNumber();

                if (numberForCountOfCurrensy.ContainsKey(userInputNumberOfWontCurrensy))
                    numberForCountOfCurrensy[userInputNumberCurrensy] -= Convert(countCurrensy, userInputNumberCurrensy, userInputNumberOfWontCurrensy, numberForExchangeRateOfCurrensy);
                else
                    Console.WriteLine("Ошибка!!");
            }  
        }

        static float Convert(float countCurrensy, int userInputNumberCurrensy, int userInputNumberOfWontCurrensy, Dictionary<int, float> numberForExchangeRateOfCurrensy)
        {
            float rate=0;

            if (userInputNumberCurrensy == 1 && userInputNumberOfWontCurrensy == 2)
                rate = numberForExchangeRateOfCurrensy[1];
            else if (userInputNumberCurrensy == 2 && userInputNumberOfWontCurrensy == 1)
                rate = 1/ numberForExchangeRateOfCurrensy[1];
            else if(userInputNumberCurrensy == 2 && userInputNumberOfWontCurrensy == 3)
                rate = 1 / numberForExchangeRateOfCurrensy[3];
            else if (userInputNumberCurrensy == 3 && userInputNumberOfWontCurrensy == 2)
                rate = numberForExchangeRateOfCurrensy[3];
            else if (userInputNumberCurrensy == 1 && userInputNumberOfWontCurrensy == 3)
                rate = numberForExchangeRateOfCurrensy[2];
            else if (userInputNumberCurrensy == 3 && userInputNumberOfWontCurrensy == 1)
                rate = 1/numberForExchangeRateOfCurrensy[2];

            return rate * countCurrensy;
        }
    }
}
