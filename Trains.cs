using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IJunior
{
    static class UserUtilits
    {
        private static Random _random = new Random();

        public static int GetRandomNumber(int maxValue) => _random.Next(maxValue);

        public static int GetNumber()
        {
            int result = 0;

            while (int.TryParse(Console.ReadLine(), out result) == false)
                Console.Write("Неверный формат ввода! Попробуй еще: ");

            return result;
        }

        public static int GetNumber(int minValue, int maxValue)
        {
            int result = 0;

            while (int.TryParse(Console.ReadLine(), out result) == false || result < minValue || result > maxValue)
                Console.Write("Неверный формат ввода! Попробуй еще: ");

            return result;
        }
    }

    class Program
    {
        static void Main()
        {
            RailwayNetworks railwayNetworks = new RailwayNetworks();
            railwayNetworks.Work();
        }
    }

    sealed class RailwayNetworks
    {
        private List<Train> _trainsEnRoute = new List<Train>();
        private List<Station> _stations = new List<Station>() { new Station("Белорусский вокзал", "Москва") };

        public void Work()
        {
            const int WorkOption = 1;
            const int ExitOption = 2;
            const int ShowFlightInformationOption = 3;

            const int MinimumNumberOfOptions = WorkOption;
            const int MaximumNumberOfOptions = ShowFlightInformationOption;

            bool isWorking = true;

            while (isWorking)
            {
                Console.WriteLine($"{WorkOption} - начать работать;\n" +
                                  $"{ExitOption} - закончить работу;\n" +
                                  $"{ShowFlightInformationOption} - показать информацию о рейсах;");

                switch (UserUtilits.GetNumber(MinimumNumberOfOptions, MaximumNumberOfOptions))
                {
                    case WorkOption:
                        Start();
                        break;

                    case ExitOption:
                        isWorking = false;
                        break;

                    case ShowFlightInformationOption:
                        ShowFlightInformation();
                        break;
                }

                Console.WriteLine("Нажмите любую клавишу для продолжения.");
                Console.ReadKey();
                Console.Clear();
            }
        }

        private void Start()
        {
            foreach (Station station in _stations)
                _trainsEnRoute.Add(station.Work());
        }

        private void ShowFlightInformation()
        {
            foreach (Train train in _trainsEnRoute)
                Console.WriteLine($"Поезд под номером {train.Number} {train.Direction.GetInformation()};");
        }
    }

    sealed class Station
    {
        private StationInformator _stationInformator = new StationInformator(null);
        private Hangar _hangar = new Hangar();
        private Logist _logist = new Logist();

        public Station(string name, string cityName)
        {
            Name = name;
            CityName = cityName;
            _stationInformator = new StationInformator(cityName);
        }

        public string Name { get; }

        public string CityName { get; }

        public Train Work()
        {
            Direction direction;
            Train train = _hangar.CreateTrain(_logist.Work(_stationInformator.GetInformation(out direction)));
            train.Direction = direction;

            return train;
        }
    }

    sealed class StationInformator
    {
        private string _nameOfCity;
        private CashRegister _cashRegister;

        public StationInformator(string nameOfCity)
        {
            _nameOfCity = nameOfCity;
        }

        public int GetInformation(out Direction direction)
        {
            direction = new Direction(_nameOfCity, Direction.GetRandomNameOfCityExcept(_nameOfCity));
            _cashRegister = new CashRegister(direction);

            int countPassagers = _cashRegister.CountTicketsSold;

            Console.WriteLine($"На поезд {direction.PlaceOfDeparture} -- {direction.PlaceOfArrival} хотят сесть {countPassagers}");

            return countPassagers;
        }
    }

    sealed class CashRegister
    {
        private int _maxCountPassegers = 200;

        public CashRegister(Direction direction)
        {
            CountTicketsSold = UserUtilits.GetRandomNumber(_maxCountPassegers);
            Direction = direction;
        }

        public Direction Direction { get; private set; }

        public int CountTicketsSold { get; private set; }
    }

    sealed class Hangar
    {
        TrainBilder _trainBilder = new TrainBilder();

        public Train CreateTrain(WagonsInformation wagonsInformation)
        {
            return _trainBilder.Create(wagonsInformation);
        }
    }

    sealed class Logist
    {
        public WagonsInformation Work(int countPassagers)
        {
            const int AddOption = 1;
            const int FinishOption = 2;

            const int MinimumNumberOfOptions = AddOption;
            const int MaximumNumberOfOptions = FinishOption;

            int countMiniWagons = 0;
            int countAverageWagons = 0;
            int countBigWagons = 0;

            int countOfSeats = 0;

            while (countPassagers > countOfSeats)
            {
                bool isWork = true;

                while (isWork)
                {
                    Console.WriteLine($"{AddOption} - спроектировать пасадочные места еще одного вагона;\n" +
                                      $"{FinishOption} - закончить добавление вагонов;\n");

                    switch (UserUtilits.GetNumber(MinimumNumberOfOptions, MaximumNumberOfOptions))
                    {
                        case AddOption:
                            AddInformationAboutWagons(ref countMiniWagons, ref countAverageWagons, ref countBigWagons);
                            break;

                        case FinishOption:
                            isWork = false;
                            break;
                    }

                    Console.WriteLine("Нажмите любую клавишу для продолжения.");
                    Console.ReadKey();
                }

                countOfSeats = countMiniWagons * MiniWagon.GetMaximumNumberOfPlaces + countAverageWagons * AverageWagon.GetMaximumNumberOfPlaces + countBigWagons * BigWagon.GetMaximumNumberOfPlaces;

                if (countPassagers > countOfSeats)
                {
                    Console.WriteLine("Очень плохо, так пассажирам не хватит мест, попробуй еще разок заново)");
                    ResetToZero(ref countMiniWagons, ref countAverageWagons, ref countBigWagons);
                }
                else
                {
                    Console.WriteLine("Кроссавчик!! для всех хватило мест.");
                    break;
                }
            }

            return new WagonsInformation(countMiniWagons, countAverageWagons, countBigWagons);
        }

        private void ResetToZero(ref int countMiniWagons, ref int countAverageWagons, ref int countBigWagons)
        {
            countMiniWagons = 0;
            countAverageWagons = 0;
            countBigWagons = 0;
        }

        private void AddInformationAboutWagons(ref int countMiniWagons, ref int countAverageWagons, ref int countBigWagons)
        {
            const int miniValueOption = 1;
            const int averageValueOption = 2;
            const int maxValueOption = 3;

            const int MinimumNumberOfOptions = miniValueOption;
            const int MaximumNumberOfOptions = maxValueOption;

            Console.WriteLine($"{miniValueOption} - добавить маленький вагон;\n" +
                              $"{averageValueOption}- добавить средний вагон;\n" +
                              $"{maxValueOption}- добавить большой вагон;\n");

            switch (UserUtilits.GetNumber(MinimumNumberOfOptions, MaximumNumberOfOptions))
            {
                case miniValueOption:
                    countMiniWagons++;
                    break;

                case averageValueOption:
                    countAverageWagons++;
                    break;

                case maxValueOption:
                    countBigWagons++;
                    break;
            }
        }
    }

    struct WagonsInformation
    {
        public WagonsInformation(int countMiniWagons, int countAverageWagons, int countBigWagons)
        {
            CountMiniWagons = countMiniWagons;
            CountAverageWagons = countAverageWagons;
            CountBigWagons = countBigWagons;
        }

        public int CountMiniWagons { get; }

        public int CountAverageWagons { get; }

        public int CountBigWagons { get; }
    }

    sealed class FlightInformation
    {
        public FlightInformation(Train train)
        {
            NumberOfTrain = train.Number;
            NameOfDirection = train.Direction.GetInformation();
        }

        public int NumberOfTrain { get; }

        public string NameOfDirection { get; }
    }

    sealed class Direction
    {
        static private List<string> _cities = new List<string>() { "Нижний Новгород", "Санкт-Петербург", "Новосибирск", "Минск" };

        static public string GetRandomNameOfCityExcept(string nameOfCity)
        {
            int lastIndexInList = _cities.Count - 1;

            string newNameOfCity = _cities[UserUtilits.GetRandomNumber(lastIndexInList)];

            while (nameOfCity == newNameOfCity)
                newNameOfCity = _cities[UserUtilits.GetRandomNumber(lastIndexInList)];

            return newNameOfCity;
        }

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

    sealed class TrainBilder
    {
        public Train Create(WagonsInformation wagonsInformation)
        {
            Console.Write("Введите номер нового поезда: ");
            int number = UserUtilits.GetNumber();

            return new Train(number, new WagonCoupling().GetWagons(wagonsInformation));
        }
    }

    sealed class WagonCoupling
    {
        public List<Wagon> GetWagons(WagonsInformation wagonsInformation)
        {
            List<Wagon> wagons = new List<Wagon>();

            for (int i = 0; i < wagonsInformation.CountMiniWagons; i++)
                wagons.Add(new MiniWagon());


            for (int i = 0; i < wagonsInformation.CountAverageWagons; i++)
                wagons.Add(new AverageWagon());


            for (int i = 0; i < wagonsInformation.CountBigWagons; i++)
                wagons.Add(new BigWagon());

            return wagons;
        }
    }

    sealed class Train
    {
        private List<Wagon> _wagons = new List<Wagon>();

        public Train(int number, List<Wagon> wagons)
        {
            Number = number;

            foreach (Wagon wagon in wagons)
                _wagons.Add(wagon);
        }

        public int Number { get; private set; }

        public Direction Direction { get; set; }

        public int GetAllSeats()
        {
            int result = 0;

            foreach (Wagon wagon in _wagons)
                result += wagon.Compatibility;

            return result;
        }
    }

    abstract class Wagon
    {
        public int Compatibility { get; protected set; }
    }

    sealed class MiniWagon : Wagon
    {
        static private int _maximumNumberOfPlaces = 10;

        static public int GetMaximumNumberOfPlaces => _maximumNumberOfPlaces;

        public MiniWagon()
        {
            Compatibility = _maximumNumberOfPlaces;
        }
    }

    sealed class AverageWagon : Wagon
    {
        static private int _maximumNumberOfPlaces = 30;

        static public int GetMaximumNumberOfPlaces => _maximumNumberOfPlaces;

        public AverageWagon()
        {
            Compatibility = _maximumNumberOfPlaces;
        }       
    }

    sealed class BigWagon : Wagon
    {
        static private int _maximumNumberOfPlaces = 50;

        static public int GetMaximumNumberOfPlaces => _maximumNumberOfPlaces;

        public BigWagon()
        {
            Compatibility = _maximumNumberOfPlaces;
        }        
    }
}