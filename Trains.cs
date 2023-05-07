using System;
using System.Collections.Generic;

namespace IJunior
{
    static class UserUtilits
    {
        private static Random _random = new Random();

        public static int GetRandomNumberWithinLimit(int maxValue) => _random.Next(maxValue);

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
            Logist stationDispatcher = new Logist();
            stationDispatcher.StartWork();
        }
    }

    sealed class Logist
    {
        private TrainBilder _trainBilder = new TrainBilder();
        private DirectionBilder _directionBilder = new DirectionBilder();
        private List<string> _flightInformation = new List<string>();
        private Depot _depot = new Depot();

        public void StartWork()
        {
            const int WorkOption = 1;
            const int ExitOption = 2;
            const int ShowFlightInformation = 3;

            const int MinimumNumberOfOptions = WorkOption;
            const int MaximumNumberOfOptions = ShowFlightInformation;

            bool isWork = true;

            while (isWork)
            {
                Console.WriteLine($"{WorkOption} - работать над новым рейсом;\n" +
                                  $"{ExitOption} - закончить работу;\n" +
                                  $"{ShowFlightInformation} - показать информацию о рейсах;");

                switch (UserUtilits.GetNumberWithinLimits(MinimumNumberOfOptions, MaximumNumberOfOptions))
                {
                    case WorkOption:
                        Work();
                        break;

                    case ExitOption:
                        isWork = false;
                        break;

                    case ShowFlightInformation:
                        this.ShowFlightInformation();
                        break;
                }
            }

            Console.WriteLine("Нажмите любую клавишу для продолжения.");
            Console.Clear();
        }

        private void Work()
        {
            Direction newDirection = _directionBilder.Create();
            CashRegister cashRegister = new CashRegister(newDirection);

            int countPassegers = cashRegister.CountTicketsSold;
            Console.WriteLine($"На поезд хотят сесть {countPassegers}");

            Train train = _trainBilder.Create(newDirection);
            _depot.AddTrain(train);
            _flightInformation = _depot.GetInfornationAboutAllTrais();

            if (train.GetAllSeats() >= countPassegers)
                Console.WriteLine($"Успешно {train.Number} {train.Direction.GetInformation()}");
            else
                Console.WriteLine("Вы ошиблись.");
        }

        private void ShowFlightInformation()
        {
            foreach (string singleFlightInformation in _flightInformation)
                Console.WriteLine(singleFlightInformation);
        }
    }

    sealed class Depot
    {
        private List<Train> _trains = new List<Train>();

        public void AddTrain(Train train)
        {
            _trains.Add(train);
        }

        public List<string> GetInfornationAboutAllTrais()
        {
            List<string> listInformation = new List<string>();

            foreach (Train train in _trains)
            {
                listInformation.Add($"{train.Number} {train.Direction.GetInformation()}");
            }

            return listInformation;
        }
    }

    sealed class CashRegister
    {
        private int _maxCountPassegers = 200;

        public CashRegister(Direction direction)
        {
            CountTicketsSold = UserUtilits.GetRandomNumberWithinLimit(_maxCountPassegers);
            Direction = direction;
        }

        public Direction Direction { get; private set; }

        public int CountTicketsSold { get; private set; }
    }

    sealed class DirectionBilder
    {
        public Direction Create()
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
        public Train Create(Direction direction)
        {
            Console.Write("Введите номер поезда: ");
            int number = UserUtilits.GetNumber();

            return new Train(number, new WagonCoupling().GetNewWagons(), direction);
        }
    }

    sealed class WagonCoupling
    {
        public List<Wagon> GetNewWagons()
        {
            const int AddOption = 1;
            const int FinishOption = 2;

            const int MinimumNumberOfOptions = AddOption;
            const int MaximumNumberOfOptions = FinishOption;

            WagonBilder _wagonBilder = new WagonBilder();
            List<Wagon> wagons = new List<Wagon>();

            bool isWork = true;

            while (isWork)
            {
                Console.WriteLine($"{AddOption} - добавить вагон;\n" +
                                  $"{FinishOption} - закончить добавление вагонов;\n");

                switch (UserUtilits.GetNumberWithinLimits(MinimumNumberOfOptions, MaximumNumberOfOptions))
                {
                    case AddOption:
                        wagons.Add(_wagonBilder.Create());
                        break;

                    case FinishOption:
                        isWork = false;
                        break;
                }

                Console.WriteLine("Нажмите любую клавишу для продолжения.");
                Console.ReadKey();
            }

            return wagons;
        }
    }

    sealed class WagonBilder
    {
        public Wagon Create()
        {
            const int miniValueOption = 1;
            const int averageValueOption = 2;
            const int maxValueOption = 3;

            const int MinimumNumberOfOptions = miniValueOption;
            const int MaximumNumberOfOptions = maxValueOption;

            Console.WriteLine($"{miniValueOption} - создать маленький вагон;\n" +
                              $"{averageValueOption}- создать средний вагон;\n" +
                              $"{maxValueOption}- создать большой вагон;\n");

            switch (UserUtilits.GetNumberWithinLimits(MinimumNumberOfOptions, MaximumNumberOfOptions))
            {
                case miniValueOption:
                    return new MiniWagon();

                case averageValueOption:
                    return new AverageWagon();

                case maxValueOption:
                    return new BigWagon();
            }

            return null;
        }
    }

    sealed class Train
    {
        private List<Wagon> _wagons = new List<Wagon>();

        public Train(int number, List<Wagon> wagons, Direction direction)
        {
            Number = number;
            Direction = direction;

            foreach (Wagon wagon in wagons)
                _wagons.Add(wagon);
        }

        public Direction Direction { get; private set; }
        public int Number { get; private set; }

        public int GetAllSeats()
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

        public string GetInformation()
        {
            return $"Точка отправки - {PlaceOfDeparture}, точка прибытия  - {PlaceOfArrival}";
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

    sealed class BigWagon : Wagon
    {
        private int _maximumNumberOfPlaces = 50;

        public BigWagon()
        {
            Compatibility = _maximumNumberOfPlaces;
        }
    }
}