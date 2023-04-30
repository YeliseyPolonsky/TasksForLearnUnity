using System;
using System.Collections.Generic;

namespace Train
{
    class UserUtilits
    {
        private static Random random = new Random();

        public static int GetRandomNumber(int minValue, int maxValue) => random.Next(minValue, maxValue);

        public static int GetNumber()
        {
            int result = 0;

            while (int.TryParse(Console.ReadLine(), out result) == false)
                Console.Write("Неправильный ввод!, попробуйте еще: ");

            Console.WriteLine();

            return result;
        }
    }

    class Program
    {
        static void Main()
        {
            PlanHelper planHelper = new PlanHelper();
            planHelper.StartWork();
        }
    }

    class PlanHelper
    {
        Train train = new Train();
        string placeOfDeparture;
        string placeOfArrival;

        private int _minCountPassengers = 0;
        private int _maxCountPassengers = 50000;

        private const int MakePlanOption = 1;
        private const int ExitOption = 2;

        private bool _isWorking = true;

        public void StartWork()
        {
            while (_isWorking)
            {
                Console.WriteLine($"{MakePlanOption} - составить план поезда;\n" +
                                  $"{ExitOption} - выйти из программы;");

                Console.Write("Ваш выбор: ");

                switch (UserUtilits.GetNumber())
                {
                    case MakePlanOption:
                        MakePlan();
                        break;

                    case ExitOption:
                        _isWorking = false;
                        break;

                    default:
                        Console.WriteLine("Такой опции не существует!");
                        break;
                }

                Console.WriteLine("Нажмите любую клавишу для продолжения.");
                Console.ReadKey();
                Console.Clear();
            }
        }

        private void MakePlan()
        {
            Console.Write("Место отправления: ");
            placeOfDeparture = Console.ReadLine();

            Console.Write("Место прибывания: ");
            placeOfArrival = Console.ReadLine();

            int countPassegers = UserUtilits.GetRandomNumber(_minCountPassengers, _maxCountPassengers);

            Console.WriteLine($"\n{countPassegers} пассажиров купили билеты на это направление");

            CreateTrain();

            if (countPassegers <= train.GetCountPlaces())
                Console.WriteLine($"Кроссавчик на всех хватило мест, поезд отправляется, точка отправления - {placeOfDeparture}, точка прибытия  -{placeOfArrival}");
            else
                Console.WriteLine("Некоторым пассажирам не хватило мест. Вам строгий выговор!");

            train = new Train();
        }

        private void CreateTrain()
        {
            const int AddVan = 1;
            const int FinishCreating = 2;

            Console.WriteLine("\nТеперь создадим поезд:");

            bool isCreating = true;

            while (isCreating)
            {
                Console.WriteLine($"{AddVan} - добавить вагон\n" +
                              $"{FinishCreating} - завершить создание поезда");
                Console.Write("Ваш выбор: ");

                switch (UserUtilits.GetNumber())
                {
                    case AddVan:
                        this.AddVan();
                        break;

                    case FinishCreating:
                        isCreating = false;
                        break;

                    default:
                        Console.WriteLine("Такой опции не существует!");
                        break;
                }

                Console.WriteLine("Успешно).\n");
            }
        }

        private void AddVan()
        {
            Console.Write("Колличество мест: ");
            train.AddVan(new Van(UserUtilits.GetNumber()));
        }
    }

    class Train
    {
        private List<Van> _vans = new List<Van>();

        public void AddVan(Van van) => _vans.Add(van);

        public int GetCountPlaces()
        {
            int count = 0;

            foreach (Van van in _vans)
                count += van.NumberOfSeats;

            return count;
        }
    }

    class Van
    {
        public Van(int numberOsSeats)
        {
            NumberOfSeats = numberOsSeats;
        }

        public int NumberOfSeats { get; private set; }
    }
}