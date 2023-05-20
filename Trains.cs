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
        }
    }

    sealed class Station
    {
        private List<FlightInformation> _flightsInformation = new List<FlightInformation>();

        public Station(string name, string cityName)
        {
            Name = name;
            CityName = cityName;
        }

        public String Name { get; }

        public string CityName { get; }

        public void SendTrain(Train train)
        {

        }

        public List<FlightInformation> GetFlightsInformation()
        {
            List<FlightInformation> flightInformation = new List<FlightInformation>();

            foreach (FlightInformation singleFlightInformation in _flightsInformation)
                flightInformation.Add(singleFlightInformation);

            return flightInformation;
        }
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

    sealed class TrainBilder
    {
        public Train Create()
        {
            Console.Write("Введите номер нового поезда: ");
            int number = UserUtilits.GetNumber();

            return new Train(number, new WagonCoupling().GetWagons());
        }
    }

    sealed class WagonCoupling
    {
        public List<Wagon> GetWagons()
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

                switch (UserUtilits.GetNumber(MinimumNumberOfOptions, MaximumNumberOfOptions))
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

            switch (UserUtilits.GetNumber(MinimumNumberOfOptions, MaximumNumberOfOptions))
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

        public Train(int number, List<Wagon> wagons)
        {
            Number = number;

            foreach (Wagon wagon in wagons)
                _wagons.Add(wagon);
        }

        public int Number { get; private set; }

        public Direction Direction { get; private set; }

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
