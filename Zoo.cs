using System;
using System.Collections.Generic;

namespace Zoo
{
    static class UserUtilities
    {
        private static Random _random = new Random();

        public static int GetRandomNumber(int minNumber, int maxNumber) => _random.Next(minNumber, ++maxNumber);

        public static int GetCorrectNumber(int minNumber, int maxNumber)
        {
            int result = 0;
            Console.WriteLine($"Введите число ({minNumber} - {maxNumber}): ");

            while (!int.TryParse(Console.ReadLine(), out result) || result < minNumber || result > maxNumber)
                Console.Write("Вы ввели некоректное значение, попробуйте еще раз: ");

            return result;
        }

        public static int GetNumber()
        {
            int result = 0;

            while (!int.TryParse(Console.ReadLine(), out result))
                Console.Write("Вы ввели некоректное значение, попробуйте еще раз: ");

            return result;
        }
    }

    internal class Program
    {
        static void Main() => new Zoo().Work();
    }

    class Zoo
    {
        private ValliereBilder _valliereBilder = new ValliereBilder();
        private List<Valliere> _vallieres = new List<Valliere>();
        private bool _isWorking = true;

        public Zoo()
        {
            int countVallieres = 4;

            for (int i = 0; i < countVallieres; i++)
                _vallieres.Add(_valliereBilder.Create());
        }

        public void Work()
        {
            List<Action> OptionsList = new List<Action>() { ShowValliere, Exit };

            bool isWorking = true;

            while (isWorking)
            {
                const int OptionShow = 1;
                const int OptionExit = 2;

                while (_isWorking)
                {
                    Console.WriteLine($"{OptionShow} - просмотр вальеров;\n" +
                                      $"{OptionExit} - выйти;");

                    OptionsList[UserUtilities.GetCorrectNumber(OptionShow, OptionExit) - 1].Invoke();
                    Console.ReadKey();
                    Console.Clear();
                }               
            }
        }

        private void ShowValliere()
        {
            Console.WriteLine("Намера вальеров:");

            foreach (Valliere valliere in _vallieres)
                Console.WriteLine(valliere.Number);

            int number = GetValliereNumber();

            foreach (Valliere valliere in _vallieres)
            {
                if (number == valliere.Number)
                {
                    valliere.ShowInformation();
                    return;
                }
            }
        }

        private int GetValliereNumber()
        {
            Console.Write("Укажи номер вальера: ");
            int result = 0;
            bool isWorking = true;

            while (isWorking)
            {
                result = UserUtilities.GetNumber();

                if (IsValliereWithNumber(result))
                    isWorking = false;
                else
                    Console.Write("Вальера с таким номером не сущнствует попробуй еще раз: ");
            }

            return result;
        }

        private bool IsValliereWithNumber(int number)
        {
            foreach (Valliere valliere in _vallieres)
            {
                if (number == valliere.Number)
                    return true;
            }

            return false;
        }

        private void Exit() => _isWorking = false;
    }

    class ValliereBilder
    {
        private NumberCreator _numberCreator = new NumberCreator();
        private AnimalBilder _animalBilder = new AnimalBilder();

        public Valliere Create()
        {
            List<Animal> animals = new List<Animal>();

            int minCountAnimals = 1;
            int maxCountAnimals = 5;

            for (int i = 0; i < UserUtilities.GetRandomNumber(minCountAnimals, maxCountAnimals); i++)
                animals.Add(_animalBilder.Create());

            return new Valliere(_numberCreator.Create(), animals);
        }
    }    

    class Valliere
    {
        private List<Animal> _animals = new List<Animal>();

        public Valliere(int number, List<Animal> animals)
        {
            Number = number;
            _animals = animals;
        }

        public int Number { get; }
        public int CountAnimals => _animals.Count;

        public void ShowInformation()
        {
            Console.WriteLine($"Вальер номер {Number},\n" +
                              $"Колличество животных {CountAnimals},\n" +
                              $"Список животных и информация об них:");

            foreach (Animal animal in _animals)
                Console.WriteLine($"Имя: {animal.Name}, гендер {animal.Gender}, звук {animal.Sound};");
        }
    }

    class NumberCreator
    {
        private static List<int> _usedNumbers = new List<int>();

        public int Create()
        {
            int minValue = 10;
            int maxValue = 200;

            int newNumber = 0;
            bool isWorking = true;

            while (isWorking)
            {
                newNumber = UserUtilities.GetRandomNumber(minValue, maxValue);
                bool wasNumberUsed = false;

                foreach (int number in _usedNumbers)
                {
                    if (number == newNumber)
                    {
                        wasNumberUsed = true;
                    }
                }

                if (!wasNumberUsed)
                {
                    isWorking = false;
                    _usedNumbers.Add(newNumber);
                }
            }

            return newNumber;
        }
    }

    class AnimalBilder
    {
        private List<InformationAboutSound> _informationAboutSounds = new List<InformationAboutSound>
        {
            new InformationAboutSound("Коза", "бееееееееее"),
            new InformationAboutSound("Лев", "РРРрррЫыы"),
            new InformationAboutSound("Кот", "мяу"),
            new InformationAboutSound("Собака", "ГААаВв"),
            new InformationAboutSound("Корова", "МууууУ"),
            new InformationAboutSound("Лошадь", "Игого"),
            new InformationAboutSound("Осел", "ИИиААааа"),
            new InformationAboutSound("Баран", "БЭЭэээ"),
            new InformationAboutSound("Медведь", "ЭЭэээээ"),
            new InformationAboutSound("Заяц", "Зязязязязя"),
        };

        public Animal Create()
        {
            Gender gender;

            if (UserUtilities.GetRandomNumber(0, 1) == 0)
                gender = Gender.Male;
            else
                gender = Gender.Female;

            InformationAboutSound informationAboutSound = _informationAboutSounds[UserUtilities.GetRandomNumber(0, _informationAboutSounds.Count - 1)];

            return new Animal(informationAboutSound.NameSomeoneMakingSound, gender, informationAboutSound.Sound);
        }
    }

    class Animal
    {
        public Animal(string name, Gender gender, string sound)
        {
            Gender = gender;
            Sound = sound;
            Name = name;
        }

        public Gender Gender { get; }
        public string Name { get; }
        public string Sound { get; }
    }

    class InformationAboutSound
    {
        public InformationAboutSound(string nameSomeoneMakingSound, string sound)
        {
            NameSomeoneMakingSound = nameSomeoneMakingSound;
            Sound = sound;
        }

        public string NameSomeoneMakingSound { get; }
        public string Sound { get; }
    }

    enum Gender
    {
        Male,
        Female
    }
}
