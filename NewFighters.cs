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
            new Arena(FighterBilder.CreateFighters()).Work();
        }
    }

    class Arena
    {
        private List<Fighter> _fighters;

        public Arena(List<Fighter> fighters)
        {
            _fighters = fighters;
        }

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
            Console.WriteLine("\nВыбирем первого бойца!");
            Fighter firstFighter = GetFighterByName();

            Console.WriteLine("\nВыбирем второго бойца!");
            Fighter secondFighter = GetFighterByName();

            Fight(firstFighter, secondFighter);
            ShowWinner(firstFighter, secondFighter);

            Console.WriteLine("Бой окончен!");
            Console.ReadKey();
        }

        private Fighter GetFighterByName()
        {
            bool isWorkig = true;
            Fighter resultFighter = null;

            Console.Write("Доступные бойцы :");

            foreach (Fighter fighter in _fighters)
                Console.Write(fighter.Name + " ");

            while (isWorkig)
            {
                Console.Write("\nВведите имя бойца: ");
                string name = Console.ReadLine();

                for (int i = 0; i < _fighters.Count; i++)
                {
                    if (_fighters[i].Name == name)
                    {
                        Console.WriteLine($"Успешно! Вы выбрали \"{name}\"");
                        resultFighter = FighterBilder.CreateFighter(name);
                        isWorkig = false;
                    }
                }

                if (resultFighter == null)
                    Console.WriteLine("Бойца с таким именем не существует, попробуй еще раз.");
            }

            return resultFighter;
        }

        private void Fight(Fighter firstFighter, Fighter secondFighter)
        {
            Console.WriteLine("\nБой начался!\n");

            bool isFighting = true;

            while (isFighting)
            {
                firstFighter.ShowInformation();
                secondFighter.ShowInformation();

                Console.ReadKey();
                Hit(firstFighter, secondFighter, ref isFighting);
            }
        }

        private void Hit(Fighter firstFighter, Fighter secondFighter, ref bool isFighting)
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

        private void ShowWinner(Fighter firstFighter, Fighter secondFighter)
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
        private static List<Fighter> _allFighters = new List<Fighter> { new Magician(), new Samurai(), new Barbarian(), new Fairy(), new Goblin(), };

        public static List<Fighter> CreateFighters()
        {
            return _allFighters;
        }

        public static Fighter CreateFighter(string name)
        {
            const string NameOfFirstFighter = "Маг";
            const string NameOfSecondFighter = "Самурай";
            const string NameOfThirdFighter = "Варвар";
            const string NameOfFourthFighter = "Фея";
            const string NameOfFifthFighter = "Гоблин";

            switch (name)
            {
                case NameOfFirstFighter:
                    return new Magician();

                case NameOfSecondFighter:
                    return new Samurai();

                case NameOfThirdFighter:
                    return new Barbarian();

                case NameOfFourthFighter:
                    return new Fairy();

                case NameOfFifthFighter:
                    return new Goblin();

                default:
                    throw new Exception("Передано несуществующее имя бойца!");
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

                return DealDamage * rate;
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