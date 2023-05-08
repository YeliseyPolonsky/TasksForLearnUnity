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
            RailwayNetworks railwayNetworks = new RailwayNetworks();
            railwayNetworks.Start();
        }
    }

    sealed class RailwayNetworks
    {
        private List<Train> _trainsOnTheRun = new List<Train>();
        private List<Station> _stations = new List<Station>() { new Station("Ярославский вокзал") };

        public void Start()
        {
            const int WorkOption = 1;
            const int ExitOption = 2;
            const int ShowFlightInformation = 3;

            const int MinimumNumberOfOptions = WorkOption;
            const int MaximumNumberOfOptions = ShowFlightInformation;

            bool isWork = true;

            while (isWork)
            {
                Console.WriteLine($"{WorkOption} - начать работать;\n" +
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

                Console.WriteLine("Нажмите любую клавишу для продолжения.");
                Console.ReadKey();
                Console.Clear();
            }
        }

        private void Work()
        {
            foreach (Station station in _stations)
                foreach (Train train in station.Work())
                    _trainsOnTheRun.Add(train);
        }

        private void ShowFlightInformation()
        {
            foreach (Train train in _trainsOnTheRun)
            {
                Console.WriteLine($"Поезд под номером {train.Number} {train.Direction.GetInformation()};");
            }
        }
    }

    sealed class Station
    {
        private Logist _logist = new Logist();
        private TrainBilder _trainBilder = new TrainBilder();

        public Station(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }

        public List<Train> Work()
        {
            List<Train> trainsOnTheRun = new List<Train>();
            Dictionary<Direction, int> newCountPassegersByDirection = _logist.Work(Name);

            if (newCountPassegersByDirection.Count != 0)
                foreach (var countPassagersByDirection in newCountPassegersByDirection)
                {
                    Train newTrain = _trainBilder.Create(countPassagersByDirection.Key);

                    if (newTrain.GetAllSeats() >= countPassagersByDirection.Value)
                        Console.WriteLine($"Поезд под номером {newTrain.Number} {newTrain.Direction.GetInformation()} успешно отправлен;");
                    else
                        Console.WriteLine($"Поезд под номером {newTrain.Number} {newTrain.Direction.GetInformation()} отправлен c ошибкой;");

                    trainsOnTheRun.Add(newTrain);
                }

            return trainsOnTheRun;
        }
    }


    sealed class Logist
    {
        private DirectionBilder _directionBilder;
        private Dictionary<Direction, int> _countPassegersByDirection;

        public Logist()
        {
            _directionBilder = new DirectionBilder();
            _countPassegersByDirection = new Dictionary<Direction, int>();
        }

        public Dictionary<Direction, int> Work(string name)
        {
            const int WorkOption = 1;
            const int ExitOption = 2;

            const int MinimumNumberOfOptions = WorkOption;
            const int MaximumNumberOfOptions = ExitOption;

            bool isWork = true;

            while (isWork)
            {
                Console.WriteLine($"{name}\n" +
                                  $"{WorkOption} - работать над новым рейсом;\n" +
                                  $"{ExitOption} - закончить работу;");

                switch (UserUtilits.GetNumberWithinLimits(MinimumNumberOfOptions, MaximumNumberOfOptions))
                {
                    case WorkOption:
                        MakeFlight();
                        break;

                    case ExitOption:
                        isWork = false;
                        break;
                }
            }

            Console.WriteLine("Нажмите любую клавишу для продолжения.");
            Console.ReadKey();
            Console.Clear();

            return _countPassegersByDirection;
        }

        private void MakeFlight()
        {
            Direction newDirection = _directionBilder.Create();
            CashRegister cashRegister = new CashRegister(newDirection);

            int countPassegers = cashRegister.CountTicketsSold;
            Console.WriteLine($"На поезд хотят сесть {countPassegers}\n");

            _countPassegersByDirection.Add(newDirection, countPassegers);
        }
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

    sealed class TrainBilder
    {
        public Train Create(Direction direction)
        {
            Console.Write($"Введите номер поезда для направления {direction.GetInformation()}: ");
            int number = UserUtilits.GetNumber();

            return new Train(number, new WagonCoupling().GetNewWagons(), direction);
        }
    }

    sealed class Train
    {
        public int Number { get; private set; }
        private List<Wagon> _wagons = new List<Wagon>();

        public Train(int number, List<Wagon> wagons, Direction direction)
        {
            Number = number;
            Direction = direction;

            foreach (Wagon wagon in wagons)
                _wagons.Add(wagon);
        }

        public Direction Direction { get; private set; }

        public int GetAllSeats()
        {
            int result = 0;

            foreach (Wagon wagon in _wagons)
                result += wagon.Compatibility;

            return result;
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