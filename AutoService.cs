using System;
using System.Collections.Generic;

namespace AutoService
{
    static class UserUtilities
    {
        static private Random _random = new Random();

        static public int GetRandomNumber(int maxValue) => _random.Next(++maxValue);

        static public int GetRandomNumber(int minValue, int maxValue) => _random.Next(minValue, ++maxValue);

        static public bool GetRandomTrueOrFalse() => Convert.ToBoolean(GetRandomNumber(1));

        static public int GetCorrectNumber(int minValue, int maxValue)
        {
            int result = 0;

            while (!int.TryParse(Console.ReadLine(), out result) || result > maxValue || result < minValue)
            {
                Console.Write("Вы ввели некоректное значение,\n" +
                              "попробуй еще раз: ");
            }

            return result;
        }
    }

    static class Fine
    {
        static public int CostFine { get; } = 50;
    }

    internal class Program
    {
        static void Main() => new World().Work();
    }

    class World
    {
        private AutoService _autoService;
        private CarBilder _carBilder;
        private bool _isWorking;

        public World()
        {
            _autoService = new AutoService();
            _carBilder = new CarBilder();
            _isWorking = true;
        }

        public void Work()
        {
            const int OptionServiceCar = 1;
            const int OptionExit = 2;

            List<Action> _actionList = new List<Action>() { ServiceСar, Exit };

            while (_isWorking)
            {
                Console.WriteLine($"{OptionServiceCar} - обслужить машину;\n" +
                                  $"{OptionExit} - выйти из программы;");

                _actionList[UserUtilities.GetCorrectNumber(OptionServiceCar, OptionExit) - 1].Invoke();

                Console.ReadKey();
                Console.Clear();
            }
        }

        private void Exit() => _isWorking = false;

        private void ServiceСar() => _autoService.FixTheCar(_carBilder.Create());
    }

    class AutoService
    {
        private int _money;
        private PartsWarehouse _partsWarehouse;
        private float _profitPercentage;
        private Car _carForFix;
        private Check _check;

        public AutoService()
        {
            _money = 100;
            _partsWarehouse = new PartsWarehouseBilder().Create();
            _profitPercentage = 1.20f;
        }

        public void FixTheCar(Car car)
        {
            _check = new Check(car.Name, _money);
            _carForFix = car;
            List<Action> actionList = new List<Action>() { SendFine, ReplaceСomponents };

            if (_carForFix.GetCostsOfBreackedComponentsByNames().Count != 0)
            {
                const int OptionFine = 1;
                const int OptionFix = 2;

                ShowBreackedComponents();
                _partsWarehouse.ShowComponents();

                Console.WriteLine("Если у вас нету хотябы одной детали,\n" +
                                 $"то вы должны отказать клиенту и выплатить штраф {Fine.CostFine}:\n" +
                                 $"{OptionFine} - отказать и заплатить штраф;\n" +
                                 $"{OptionFix} - начать чинить");

                actionList[UserUtilities.GetCorrectNumber(OptionFine, OptionFix) - 1].Invoke();
                PrintResult();
            }
            else
            {
                Console.WriteLine("У вашей машины поломок нету!");
            }
        }

        private void PrintResult()
        {
            if (_carForFix.GetCostsOfBreackedComponentsByNames().Count == 0)
            {
                Console.WriteLine("Кросавчик, ты все починил!");
            }                
            else
            {
                Console.WriteLine("Ты не починил машину!");
                SendFine();
            }

            _check.PrintCheck();
        }

        private void ShowBreackedComponents()
        {
            Console.WriteLine("-----------------\n" +
                              "В машине сломано: ");

            foreach (string nameOfBreackedComponent in _carForFix.GetCostsOfBreackedComponentsByNames().Keys)
                Console.WriteLine($"{nameOfBreackedComponent} ({_carForFix.GetCostsOfBreackedComponentsByNames()[nameOfBreackedComponent] * _profitPercentage}рублей прибыли) ");

            Console.WriteLine("-----------------\n");
        }

        private void ReplaceСomponents()
        {
            bool isWorking = true;
            List<Action> actionList = new List<Action>()
            {
                () => isWorking = false,
                () => Console.Write("Продолжаем замену!")
            };

            while (isWorking)
            {
                const int OptionFinish = 1;
                const int OptionContinue = 2;

                Console.Clear();
                ShowBreackedComponents();
                _partsWarehouse.ShowComponents();

                Console.Write("Введите название компонента для замены: ");
                string nameOfComponent = Console.ReadLine();

                if (_carForFix.GetCostsOfBreackedComponentsByNames().ContainsKey(nameOfComponent) && _partsWarehouse.GetListOfNamesComponent().Contains(nameOfComponent))
                {
                    _check.AddMoney(_carForFix.GetCostsOfBreackedComponentsByNames()[nameOfComponent]);
                    _carForFix.ReplaceComponent(_partsWarehouse.GetComponentByName(nameOfComponent));
                }
                else
                {
                    SendFine();
                }

                Console.WriteLine($"{OptionFinish} - Закончить замену компонентов\n" +
                                  $"{OptionContinue} - продолжить замену компонентов;");

                actionList[UserUtilities.GetCorrectNumber(OptionFinish, OptionContinue) - 1].Invoke();
            }
        }

        private void SendFine()
        {
            _money -= Fine.CostFine;
            Console.WriteLine("Ты где-то оплошал, держи штраф!");
            _check.AddFineMoney(Fine.CostFine);
        }
    }

    class PartsWarehouseBilder
    {
        public PartsWarehouse Create() => new PartsWarehouse(new MachineComponentsBilder().CreateRandomCountPerfectComponents());
    }

    class PartsWarehouse
    {
        private List<MachineComponent> _machineComponents;

        public PartsWarehouse(List<MachineComponent> machineComponents) => _machineComponents = machineComponents;

        public void ShowComponents()
        {
            List<string> buferListOfNames = new List<string>();

            Console.WriteLine("------------------\n" +
                              "На складе есть:");

            for (int i = 0; i < _machineComponents.Count; i++)
            {
                if (!buferListOfNames.Contains(_machineComponents[i].Name))
                {
                    string nameOfComponent = _machineComponents[i].Name;
                    int countOfComponent = 0;

                    foreach (MachineComponent component in _machineComponents)
                    {
                        if (component.Name == nameOfComponent)
                            countOfComponent++;
                    }

                    buferListOfNames.Add(nameOfComponent);
                    Console.WriteLine($"{nameOfComponent} -- {countOfComponent}");
                }
            }

            Console.WriteLine("------------------");
        }

        public List<string> GetListOfNamesComponent()
        {
            List<string> _namesOfComponents = new List<string>();

            foreach (MachineComponent component in _machineComponents)
                _namesOfComponents.Add(component.Name);

            return _namesOfComponents;
        }

        public MachineComponent GetComponentByName(string name)
        {
            MachineComponent machineComponent = null;

            foreach (MachineComponent component in _machineComponents)
            {
                if (component.Name == name)
                {
                    machineComponent = component;
                    _machineComponents.Remove(component);
                    break;
                }
            }

            return machineComponent;
        }
    }

    class Car
    {
        private List<MachineComponent> _machineComponents;

        public Car(List<MachineComponent> machineComponents, string name)
        {
            Name = name;
            _machineComponents = machineComponents;
        }

        public string Name { get; }

        public Dictionary<string, int> GetCostsOfBreackedComponentsByNames()
        {
            Dictionary<string, int> costsOfBreackedComponentsByNames = new Dictionary<string, int>();

            foreach (MachineComponent machineComponent in _machineComponents)
            {
                if (!machineComponent.IsWork)
                    costsOfBreackedComponentsByNames.Add(machineComponent.Name, machineComponent.Cost);
            }

            return costsOfBreackedComponentsByNames;
        }

        public void ReplaceComponent(MachineComponent component)
        {
            if (component != null)
            {
                for (int i = 0; i < _machineComponents.Count; i++)
                {
                    if (_machineComponents[i].Name == component.Name)
                    {
                        _machineComponents[i] = component;
                        Console.WriteLine("Деталь успешна заменена!");
                    }
                }
            }
        }
    }

    class CarBilder
    {
        private MachineComponentsBilder _machineComponentBilder;
        private List<string> _names;

        public CarBilder()
        {
            _machineComponentBilder = new MachineComponentsBilder();
            _names = new List<string>()
            {
                "Toyota Camry",
                "Honda Civic",
                "Ford Mustang",
                "Chevrolet Silverado",
                "Volkswagen Golf",
                "BMW 3 Series",
                "Mercedes-Benz E-Class",
                "Audi A4",
                "Nissan Altima",
                "Hyundai Sonata",
                "Kia Sorento",
                "Jeep Wrangler",
                "Subaru Outback",
                "Mazda CX-5",
                "Lexus RX"
            };
        }

        public Car Create() => new Car(_machineComponentBilder.CreateRandomFailedComponentsForCar(), _names[UserUtilities.GetRandomNumber(_names.Count - 1)]);
    }

    class MachineComponentsBilder
    {
        private List<Func<bool, MachineComponent>> _actionsCreatingMachineComponents;

        public MachineComponentsBilder()
        {
            _actionsCreatingMachineComponents = new List<Func<bool, MachineComponent>>()
            {
                (bool isWork) => new MachineComponent("колесо", isWork, 20),
                (bool isWork) => new MachineComponent("коробка передач", isWork, 70),
                (bool isWork) => new MachineComponent("руль", isWork, 30),
                (bool isWork) => new MachineComponent("двигатель", isWork, 100),
                (bool isWork) => new MachineComponent("тормозная жидкость", isWork, 5),
                (bool isWork) => new MachineComponent("стекло", isWork, 10)
            };
        }

        public List<MachineComponent> CreateRandomFailedComponentsForCar()
        {
            List<MachineComponent> machineComponents = new List<MachineComponent>();

            foreach (Func<bool, MachineComponent> createComponent in _actionsCreatingMachineComponents)
                machineComponents.Add(createComponent.Invoke(UserUtilities.GetRandomTrueOrFalse()));

            return machineComponents;
        }

        public List<MachineComponent> CreateRandomCountPerfectComponents()
        {
            List<MachineComponent> machineComponents = new List<MachineComponent>();
            int maxCountOfComponent = 10;

            foreach (Func<bool, MachineComponent> createComponent in _actionsCreatingMachineComponents)
            {
                int count = UserUtilities.GetRandomNumber(maxCountOfComponent);

                for (int i = 0; i < count; i++)
                    machineComponents.Add(createComponent.Invoke(true));
            }

            return machineComponents;
        }
    }

    class MachineComponent
    {
        public MachineComponent(string name, bool isWork, int cost)
        {
            Name = name;
            IsWork = isWork;
            Cost = cost;
        }

        public bool IsWork { get; private set; }
        public string Name { get; }
        public int Cost { get; }

        public void FixComponent() => IsWork = true;
    }

    class Check
    {
        private string _nameOfCar;
        private int _revonue;
        private int _startMoney;
        private int _fineMoney;

        public Check(string name, int startMoney)
        {
            _startMoney = startMoney;
            _nameOfCar = name;
        }

        public void PrintCheck()
        {
            Console.WriteLine("----------------------\n" +
                             $"{_nameOfCar}\n" +
                             $"выручка: {_revonue}\n" +
                             $"штрафы {_fineMoney}\n" +
                             $"ваш капитал {_startMoney - _fineMoney + _revonue}\n" +
                             $"----------------------");
        }

        public void AddMoney(int money)
        {
            if (money > 0)
                _revonue += money;
        }

        public void AddFineMoney(int money) => _fineMoney += money;
    }
}
