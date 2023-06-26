using System;
using System.Collections.Generic;

namespace IJunior
{
    static class UserUtilits
    {
        public static int GetNumber(int minValue, int maxValue)
        {
            int result = 0;
            bool isWork = true;

            while (isWork)
            {
                Console.Write("Введите ваше число: ");

                if (int.TryParse(Console.ReadLine(), out result))
                {
                    if (result >= minValue && result <= maxValue)
                    {
                        Console.WriteLine($"Успешно принято {result}");
                        isWork = false;
                    }
                    else
                        Console.WriteLine($"Данное число не входит в диапозон {minValue} -- {maxValue}");
                }
                else
                {
                    Console.WriteLine("Вы ввели некоректное значение! Попробуйте еще раз.");
                }
            }

            return result;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            new Arena().Work();
        }
    }

    class Arena
    {
        private List<IFighter> _fighters = new List<IFighter> { new Magician(), new Samurai() };

        public void Work()
        {
            Console.WriteLine("Добро пожаловать на Арену!!!");

            bool isWorking = true;

            while (isWorking)
            {
                const int PlayMode = 1;
                const int ExitOption = 2;

                const int minValueOfOptions = PlayMode;
                const int maxValueOfOptions = ExitOption;

                Console.WriteLine($"{PlayMode} - начать игру;\n" +
                                  $"{ExitOption} - выйти;\n");

                switch (UserUtilits.GetNumber(minValueOfOptions, maxValueOfOptions))
                {
                    case PlayMode:
                        this.StartFight();
                        break;

                    case ExitOption:
                        isWorking = false;
                        break;
                }
            }
        }

        private void StartFight()
        {
            Console.WriteLine("\nВыбирем первого бойца!");
            IFighter firstFighter = GetFighterByName(_fighters);

            Console.WriteLine("\nВыбирем второго бойца!");
            IFighter secondFighter = GetFighterByName(_fighters);

            Fight(firstFighter, secondFighter);

            Console.WriteLine("Бой окончен!");
            Console.ReadKey();
        }

        private void Fight(IFighter firstFighter, IFighter secondFighter)
        {
            Console.WriteLine("\nБой начался!\n");

            bool isFighting = true;

            while (isFighting)
            {
                Console.WriteLine($"{firstFighter.Name} : здоровье {firstFighter.GetHealthInformation}\n" +
                              $"{secondFighter.Name} : здоровье {secondFighter.GetHealthInformation}\n");
                Console.ReadKey();
                SigleHit();
            }

            void SigleHit()
            {
                secondFighter.GetDamage(firstFighter.DealDamage);

                if (secondFighter.GetHealthInformation <= 0)
                {
                    Console.WriteLine($"Победил первый боей {firstFighter.Name}");
                    isFighting = false;
                }

                firstFighter.GetDamage(secondFighter.DealDamage);

                if (firstFighter.GetHealthInformation <= 0)
                {
                    Console.WriteLine($"Победил второй боей {secondFighter.Name}");
                    isFighting = false;
                }
            }
        }

        private IFighter GetFighterByName(List<IFighter> fighters)
        {
            bool isWorkig = true;
            IFighter resultFighter = null;

            Console.Write("Доступные бойцы :");

            foreach (IFighter fighter in fighters)
                Console.Write(fighter.Name + " ");

            while (isWorkig)
            {
                Console.Write("\nВведите имя бойца: ");
                string name = Console.ReadLine();

                for (int i = 0; i < fighters.Count; i++)
                {
                    if (fighters[i].Name == name)
                    {
                        Console.WriteLine($"Успешно! Вы выбрали \"{name}\"");
                        resultFighter = fighters[i];
                        fighters.RemoveAt(i);
                        isWorkig = false;
                    }
                }

                if (resultFighter == null)
                    Console.WriteLine("Бойца с таким именем не существует, попробуй еще раз.");
            }

            return resultFighter;
        }
    }

    interface IFighter : IDamagable, IDealDamage
    {
        string Name { get; }
        int GetHealthInformation { get; }
    }

    interface IDamagable
    {
        void GetDamage(int damage);
    }

    interface IDealDamage
    {
        int DealDamage { get; }
    }

    class Weapon
    {
        public Weapon(int damage, string name)
        {
            Damage = damage;
            Name = name;
        }

        public int Damage { get; }
        public string Name { get; }
    }

    class Magician : IFighter
    {
        private int _health = 90;
        private Weapon _weapon = new Weapon(15, "волшебный шар");

        public int Health
        {
            get => _health;

            private set
            {
                _health = value;

                if (_health <= 0)
                    this.DisplayDiedInformation();
            }
        }

        public string Name { get; } = "Маг";

        public int GetHealthInformation => _health;

        public int DealDamage => _weapon.Damage;

        private void DisplayDiedInformation() => Console.WriteLine($"Маг погиб!");

        public void GetDamage(int damage)
        {
            Health -= damage;
            this.Regeneration();
        }

        private void Regeneration()
        {
            const int amountOfAddedHealth = 10;

            if (_health > 0)
                Health += amountOfAddedHealth;
        }
    }

    class Samurai : IFighter
    {
        private int _health = 140;
        private Weapon _weapon = new Weapon(20, "меч");

        public int Health
        {
            get => _health;

            private set
            {
                _health = value;

                if (_health <= 0)
                    this.DisplayDiedInformation();
            }
        }

        public string Name { get; } = "Самурай";

        public int GetHealthInformation => _health;

        public int DealDamage => _weapon.Damage;

        private void DisplayDiedInformation() => Console.WriteLine($"Самурай погиб смертью храбрых!");

        public void GetDamage(int damage)
        {
            if (this.TryDodge() == false)
                Health -= damage;
            else
                Console.WriteLine("Самурай смог уклониться!)");
        }

        private bool TryDodge()
        {
            const int successRate = 30;
            const int maxRate = 100;
            const int nextNumberUftermaxRate = maxRate + 1;

            if (new Random().Next(nextNumberUftermaxRate) <= successRate)
                return true;
            else
                return false;
        }
    }
}