using System;
using System.Collections.Generic;

namespace Aquarium
{
    static class UserUtilits
    {
        public static int GetCorrectNumber(int maxNumber)
        {
            int result = 0;

            while (!int.TryParse(Console.ReadLine(), out result) || result > maxNumber || result < 0)
                Console.Write("Вы ввели не корректное число, попробуйте снова: ");

            return result;
        }
    }

    internal class Program
    {
        static void Main() => new Aquarium().Start();
    }

    class Aquarium
    {
        private FishShower _fishShower;
        private FishBilder _fishBilder;
        private List<Fish> _fishes;
        private List<Action> _listOptionMethods;
        private bool _isWorking;
        private int _age;

        public Aquarium()
        {
            _fishes = new List<Fish>();
            _fishShower = new FishShower();
            _fishBilder = new FishBilder();
            _listOptionMethods = new List<Action>() { AddFish, TakeOutFish, Exit };
            _isWorking = true;
            _age = 0;
        }

        public void Start()
        {
            const int OptionAddFish = 0;
            const int OptionTakeOutFish = 1;
            const int OptionExit = 2;
            const int OptionSkip = 3;

            while (_isWorking)
            {
                ShowInformation();

                Console.WriteLine($"Врзраст аквариума {_age}" +
                                   "\nВведите:" +
                                  $"\n{OptionAddFish} - добавить рыбку в аквариум;" +
                                  $"\n{OptionTakeOutFish} - убрать рыбку в аквариум;" +
                                  $"\n{OptionExit} - выйти;" +
                                  $"\n{OptionSkip} - ничего не делать в этом году;");
                int userInput = UserUtilits.GetCorrectNumber(OptionSkip);

                if (userInput != OptionSkip)
                    _listOptionMethods[userInput].Invoke();

                LiveForOneYear();
                RemoveDiedFishes();

                Console.ReadKey();
                Console.Clear();
            }
        }

        private void TakeOutFish()
        {
            Console.Write("Введите номер рыбки: ");
            int fishIndex = UserUtilits.GetCorrectNumber(_fishes.Count - 1);

            _fishes.RemoveAt(fishIndex);
        }

        private void AddFish() => _fishes.Add(_fishBilder.Create());

        private void LiveForOneYear()
        {
            _age++;

            for (int i = 0; i < _fishes.Count; i++)
                _fishes[i].GettingOld();
        }

        private void ShowInformation()
        {
            for (int i = 0; i < _fishes.Count; i++)
                _fishShower.ShowInformatiobAboutFish(i, _fishes[i].Name, _fishes[i].Age);
        }

        private void RemoveDiedFishes()
        {
            for (int i = 0; i < _fishes.Count; i++)
            {
                if (_fishes[i].IsALive == false)
                    _fishes.RemoveAt(i);
            }
        }

        private void Exit() => _isWorking = false;
    }

    class FishShower
    {
        public void ShowInformatiobAboutFish(int index, string name, int age) 
        {
            Console.WriteLine($"номер: {index},имя рыбки: {name} , возраст {age}");
        } 
    }

    class FishBilder
    {
        private int _maxAgeOfFish = 15;

        public Fish Create()
        {
            Console.Write("Введите имя рыбки: ");
            string name = Console.ReadLine();

            Console.Write($"Введите максимальный возраст рыбки (<= {_maxAgeOfFish}): ");
            int maxAge = UserUtilits.GetCorrectNumber(_maxAgeOfFish);

            return new Fish(name, maxAge);
        }
    }

    class Fish
    {
        private int _maxAge;
        private int _age;

        public Fish(string name, int maxAge)
        {
            Name = name;
            _maxAge = maxAge;
        }

        public string Name { get; }

        public bool IsALive => Age <= _maxAge;

        public int Age
        {
            get => _age;

            private set
            {
                if (value > _maxAge)
                    Console.WriteLine($"\'{Name}\' умер.");

                _age++;
            }
        }

        public void GettingOld() => ++Age;
    }
}
