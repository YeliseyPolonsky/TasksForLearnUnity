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
                if (int.TryParse(Console.ReadLine(), out result))
                {
                    if (result >= minValue && result <= maxValue)
                        isWork = false;
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

        public static int GetRandomNumber(int maxValue)
        {
            Random random = new Random();

            return random.Next(++maxValue);
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
        Fighter firstFighter;
        Fighter secondFighter;

        public void Work()
        {
            Console.WriteLine("Добро пожаловать на Арену!!!");

            bool isWorking = true;

            while (isWorking)
            {
                const int PlayMode = 1;
                const int ExitOption = 2;

                Console.WriteLine($"{PlayMode} - начать игру;\n" +
                                  $"{ExitOption} - выйти;\n");

                switch (UserUtilits.GetNumber(PlayMode, ExitOption))
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
            FighterBilder fighterBilder = new FighterBilder();

            Console.WriteLine("\nВыбирем первого бойца!");
            firstFighter = fighterBilder.CreateFighter();

            Console.WriteLine("\nВыбирем второго бойца!");
            secondFighter = fighterBilder.CreateFighter();

            Fight();
            ShowWinner();

            Console.WriteLine("Бой окончен!");
            Console.ReadKey();
        }

        private void Fight()
        {
            Console.WriteLine("\nБой начался!\n");

            bool isFighting = true;

            while (isFighting)
            {
                firstFighter.ShowInformation();
                secondFighter.ShowInformation();

                Console.ReadKey();
                Hit(ref isFighting);
                Console.WriteLine();
            }
        }

        private void Hit(ref bool isFighting)
        {
            secondFighter.GetHit(firstFighter.DealDamage);

            if (!secondFighter.IsAlive)
            {
                isFighting = false;
                Console.WriteLine("Бой окончен.");
                return;
            }

            firstFighter.GetHit(secondFighter.DealDamage);

            if (!firstFighter.IsAlive)
            {
                isFighting = false;
                Console.WriteLine("Бой окончен.");
                return;
            }
        }

        private void ShowWinner()
        {
            if (firstFighter.IsAlive)
            {
                Console.WriteLine($"Победил первый боей {firstFighter.Name}");
            }

            if (secondFighter.IsAlive)
            {
                Console.WriteLine($"Победил второй боей {secondFighter.Name}");
            }
        }
    }

    class FighterBilder
    {
        private List<Fighter> _allFighters;

        public FighterBilder()
        {
            UpdateList();
        }

        private void UpdateList()
        {
            _allFighters = new List<Fighter>(5)
            {
                new Barbarian(),
                new Samurai(),
                new Fairy(),
                new Goblin(),
                new Magician()
            };
        }

        public Fighter CreateFighter()
        {
            ShowFighters();
            Console.Write("Введите номер бойца: ");
            int numberOfFighter = UserUtilits.GetNumber(1, _allFighters.Count);
            UpdateList();
            return _allFighters[--numberOfFighter];
        }

        private void ShowFighters()
        {
            for (int i = 0; i < _allFighters.Count; i++)
            {
                Console.WriteLine($"{_allFighters[i].Name} номер: {i + 1}");
            }
        }
    }

    abstract class Fighter
    {
        protected int _health;

        public Fighter(string name, int health, Weapon weapon)
        {
            Name = name;
            _health = health;
            Weapon = weapon;
        }

        public int Health
        {
            get => _health;

            protected set
            {
                _health = value;

                if (_health <= 0)
                    Console.WriteLine($"{Name} погиб!");
            }
        }

        public string Name { get; protected set; }

        public bool IsAlive => _health > 0;

        public Weapon Weapon { get; }

        public virtual int DealDamage => Weapon.Damage;

        public void ShowInformation() => Console.WriteLine($"{Name} : здоровье {_health}");

        protected void GetDamage(int damage)
        {
            Health -= damage;
        }

        abstract public void GetHit(int damage);
    }

    class Magician : Fighter
    {
        public Magician() : base("Маг", 90, new Magic()) { }

        public override void GetHit(int damage)
        {
            GetDamage(damage);

            if (IsAlive)
                Regeneration();
        }

        private void Regeneration()
        {
            const int amountOfAddedHealth = 10;

            if (_health > 0)
                Health += amountOfAddedHealth;
        }
    }

    class Samurai : Fighter
    {
        public Samurai() : base("Самурай", 120, new Sword()) { }

        public override void GetHit(int damage)
        {
            if (TryDodge())
                Console.WriteLine($"{Name}: я смог увернуться!");
            else
                GetDamage(damage);
        }

        private bool TryDodge()
        {
            const int successRate = 30;
            const int maxRate = 100;

            if (UserUtilits.GetRandomNumber(maxRate) <= successRate)
                return true;
            else
                return false;
        }
    }

    class Barbarian : Fighter
    {
        public Barbarian() : base("Варвар", 100, new Sword()) { }

        public override int DealDamage
        {
            get
            {
                int rate = (int)Math.Round(TakePowerFactor());
                Console.WriteLine($"У {Name} коэфицент силы: {rate}");

                return Weapon.Damage * rate;
            }
        }

        public override void GetHit(int damage)
        {
            GetDamage(damage);
        }

        private float TakePowerFactor()
        {
            const int maxRate = 200;
            const int fullRate = 100;

            return (float)UserUtilits.GetRandomNumber(maxRate) / fullRate;
        }
    }

    class Fairy : Fighter
    {
        public Fairy() : base("Фея", 60, new Sword()) { }

        public override void GetHit(int damage)
        {
            GetDamage(damage - ExtinguishDamage(damage));
        }

        private int ExtinguishDamage(int damage)
        {
            int maxValue = damage;

            return UserUtilits.GetRandomNumber(maxValue);
        }
    }

    class Goblin : Fighter
    {
        public Goblin() : base("Гоблин", 40, new Sword()) { }

        public override void GetHit(int damage)
        {
            GetDamage(damage);
        }
    }

    abstract class Weapon
    {
        public Weapon(int damage, string name)
        {
            Damage = damage;
            Name = name;
        }

        public int Damage { get; }
        public string Name { get; }
    }

    class Sword : Weapon
    {
        public Sword() : base(30, "Меч") { }
    }

    class Magic : Weapon
    {
        public Magic() : base(40, "Магия") { }
    }
}