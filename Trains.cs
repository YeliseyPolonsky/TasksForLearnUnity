using System;
using System.Collections.Generic;

namespace IJunior
{
    static class UserUtilits
    {
        static Random random = new Random();

        public static int GetRandomNumberWithinLimits(int minValue, int maxValue) => random.Next(minValue, maxValue);

        public static int GetRandomNumberWithinLimit(int maxValue) => random.Next(0, maxValue);

        public static int GetNumber()
        {
            int result = 0;

            while (int.TryParse(Console.ReadLine(), out result) == false)
                Console.Write("Неверный формат ввода! Попробуй еще: ");

            return result;
        }

        public static int GetNumberWithinLimits(int minValue, int maxValue)
        {
            int result = 0;

            while (int.TryParse(Console.ReadLine(), out result) == false || result < minValue || result > maxValue)
                Console.Write("Неверный формат ввода! Попробуй еще: ");

            return result;
        }
    }

    sealed class Program
    {
        static void Main()
        {
            StationDispatcher stationDispatcher = new StationDispatcher();
            stationDispatcher.StartWork();
        }
    }

    sealed class StationDispatcher
    {
        private List<Train> _trains = new List<Train>();
        private TrainBilder _trainBilder = new TrainBilder();

        public void StartWork()
        {
            const int WorkOption = 1;
            const int ExitOption = 2;

            bool isWork = true;

            while (isWork)
            {
                Console.WriteLine($"{WorkOption} - работать;\n" +
                                  $"{ExitOption} - закончить работу;");

                switch (UserUtilits.GetNumberWithinLimits(1, 2))
                {
                    case WorkOption:
                        Work();
                        break;

                    case ExitOption:
                        isWork = false;
                        break;
                }
            }

            Console.WriteLine("Нажмите любую клавишу для продолжения.");
            Console.Clear();
        }

        private void Work()
        {
            IndividualBoxOfficeTrain individualBoxOfficeTrain = new IndividualBoxOfficeTrain();

            int countPassegers = individualBoxOfficeTrain.CountTicketsSold;

            Console.WriteLine($"На поезд хотят сесть {countPassegers}");
            Train train = _trainBilder.BuildTrain();
            _trains.Add(train);

            if (train.GetAllNummerOfPlaces() >= countPassegers)
            {
                Console.WriteLine($"Успешно");
                train.Direction.ShowInformation();
            }
            else
                Console.WriteLine("Вы ошиблись.");

        }
    }

    sealed class IndividualBoxOfficeTrain
    {
        private int _maxCountPassegers = 200;

        public IndividualBoxOfficeTrain()
        {
            CountTicketsSold = UserUtilits.GetRandomNumberWithinLimit(_maxCountPassegers);
        }

        public int CountTicketsSold { get; private set; }
    }

    sealed class DirectionBilder
    {
        public Direction BuildDirection()
        {
            Console.Write("Введите точку отправления: ");
            string placeOfDeparture = Console.ReadLine();

            Console.Write("Введите точку прибытия: ");
            string plaseOfArrival = Console.ReadLine();

            return new Direction(placeOfDeparture, plaseOfArrival);
        }
    }

    sealed class TrainBilder
    {
        private WagonBilder _wagonBilder = new WagonBilder();
        private DirectionBilder _directionBilder = new DirectionBilder();

        public Train BuildTrain()
        {
            Console.Write("Введите имя поезда: ");
            string name = Console.ReadLine();

            return new Train(name, GetNewWagons(), _directionBilder.BuildDirection());
        }

        private List<Wagon> GetNewWagons()
        {
            List<Wagon> newWagons = new List<Wagon>();

            const int AddOption = 1;
            const int FinishOption = 2;

            bool isWork = true;

            while (isWork)
            {
                Console.WriteLine($"{AddOption} - добавить вагон;\n" +
                                  $"{FinishOption} - закончить добавление вагонов;\n");

                switch (UserUtilits.GetNumberWithinLimits(1, 2))
                {
                    case AddOption:
                        newWagons.Add(_wagonBilder.BuildWagon());
                        break;

                    case FinishOption:
                        isWork = false;
                        break;
                }

                Console.WriteLine("Нажмите любую клавишу для продолжения.");
                Console.ReadKey();
            }

            return newWagons;
        }
    }

    sealed class WagonBilder
    {
        public Wagon BuildWagon()
        {
            const int miniValueOption = 1;
            const int averageValueOption = 2;
            const int maxValueOption = 3;

            Console.WriteLine($"{miniValueOption} - создать маленький вагон;\n" +
                              $"{averageValueOption}- создать средний вагон;\n" +
                              $"{maxValueOption}- создать большой вагон;\n");

            switch (UserUtilits.GetNumberWithinLimits(1, 3))
            {
                case miniValueOption:
                    return new MiniWagon();

                case averageValueOption:
                    return new AverageWagon();

                case maxValueOption:
                    return new MaxWagon();

                default:
                    Console.WriteLine("Ошибка!");
                    return null;
            }
        }
    }

    sealed class Train
    {
        private string Name;
        private List<Wagon> _wagons = new List<Wagon>();

        public Train(string name, List<Wagon> wagons, Direction direction)
        {
            Name = name;
            Direction = direction;

            foreach (Wagon wagon in wagons)
                _wagons.Add(wagon);
        }

        public Direction Direction { get; private set; }

        public int GetAllNummerOfPlaces()
        {
            int result = 0;

            foreach (Wagon wagon in _wagons)
                result += wagon.Compatibility;

            return result;
        }
    }

    sealed class Direction
    {
        public Direction(string placeOfDeparture, string placeOfArrival)
        {
            PlaceOfArrival = placeOfArrival;
            PlaceOfDeparture = placeOfDeparture;
        }

        public string PlaceOfDeparture { get; private set; }

        public string PlaceOfArrival { get; private set; }

        public void ShowInformation()
        {
            Console.WriteLine($"Точка отправки - {PlaceOfDeparture}, точка прибытия  - {PlaceOfArrival}");
        }
    }

    abstract class Wagon
    {
        public int Compatibility { get; protected set; }
    }

    sealed class MiniWagon : Wagon
    {
        private int _maximumNumberOfPlaces = 10;

        public MiniWagon()
        {
            Compatibility = _maximumNumberOfPlaces;
        }
    }

    sealed class AverageWagon : Wagon
    {
        private int _maximumNumberOfPlaces = 30;

        public AverageWagon()
        {
            Compatibility = _maximumNumberOfPlaces;
        }
    }

    sealed class MaxWagon : Wagon
    {
        private int _maximumNumberOfPlaces = 50;

        public MaxWagon()
        {
            Compatibility = _maximumNumberOfPlaces;
        }
    }
}