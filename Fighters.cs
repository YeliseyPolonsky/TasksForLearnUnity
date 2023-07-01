using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        private List<IFighter> _fighters = new List<IFighter> { FighterBilder.CreateFighter("Маг"), FighterBilder.CreateFighter("Самурай"),
                                                                FighterBilder.CreateFighter("Гоблин"), FighterBilder.CreateFighter("Фея"), FighterBilder.CreateFighter("Варвар")};
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
            IFighter firstFighter = GetFighterByName();

            Console.WriteLine("\nВыбирем второго бойца!");
            IFighter secondFighter = GetFighterByName();

            Fight(firstFighter, secondFighter);
            DisplayNameOfWinner(firstFighter, secondFighter);

            Console.WriteLine("Бой окончен!");
            Console.ReadKey();
        }

        private IFighter GetFighterByName()
        {
            bool isWorkig = true;
            IFighter resultFighter = null;

            Console.Write("Доступные бойцы :");

            foreach (IFighter fighter in _fighters)
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

        private void Fight(IFighter firstFighter, IFighter secondFighter)
        {
            Console.WriteLine("\nБой начался!\n");

            bool isFighting = true;

            while (isFighting)
            {
                Console.WriteLine($"{firstFighter.Name} : здоровье {firstFighter.GetHealthInformation}\n" +
                                  $"{secondFighter.Name} : здоровье {secondFighter.GetHealthInformation}\n");
                Console.ReadKey();
                Hit(firstFighter, secondFighter, ref isFighting);
            }
        }

        private void Hit(IFighter firstFighter, IFighter secondFighter, ref bool isFighting)
        {
            firstFighter.Attack(secondFighter);

            if (secondFighter.GetHealthInformation <= 0)
            {
                isFighting = false;
                Console.WriteLine("Бой окончен.");
                return;
            }

            secondFighter.Attack(firstFighter);

            if (firstFighter.GetHealthInformation <= 0)
            {
                isFighting = false;
                Console.WriteLine("Бой окончен.");
                return;
            }
        }

        private void DisplayNameOfWinner(IFighter firstFighter, IFighter secondFighter)
        {
            if (secondFighter.GetHealthInformation <= 0)
            {
                Console.WriteLine($"Победил первый боей {firstFighter.Name}");
            }

            if (firstFighter.GetHealthInformation <= 0)
            {
                Console.WriteLine($"Победил второй боей {secondFighter.Name}");
            }
        }
    }

    interface IFighter : IDamagable, IDealDamage
    {
        string Name { get; }

        int GetHealthInformation { get; }

        Weapon Weapon { get; }
    }

    interface IDamagable
    {
        void GetDamage(int damage);
    }

    interface IDealDamage
    {
        int DealDamage { get; }

        void Attack(IDamagable something);
    }

    class FighterBilder
    {
        public static IFighter CreateFighter(string name)
        {
            const string NameOfFirstFighter = "Маг";
            const string NameOfSecondFighter = "Самурай";
            const string NameOfThirdFighter = "Варвар";
            const string NameOfFourthFighter = "Фея";
            const string NameOfFifthFighter = "Гоблин";

            switch (name)
            {
                case NameOfFirstFighter:
                    return new Magician(WeaponBilder.CreateWeapon("Магия"));

                case NameOfSecondFighter:
                    return new Samurai(WeaponBilder.CreateWeapon("Меч"));

                case NameOfThirdFighter:
                    return new Barbarian(WeaponBilder.CreateWeapon("Меч"));

                case NameOfFourthFighter:
                    return new Fairy(WeaponBilder.CreateWeapon("Магия"));

                case NameOfFifthFighter:
                    return new Goblin(WeaponBilder.CreateWeapon("Меч"));

                default:
                    throw new Exception("Передано несуществующее имя бойца!");
            }
        }
    }

    class Magician : IFighter
    {
        private int _health = 90;

        public Magician(Weapon weapon)
        {
            Weapon = weapon;
        }

        public int Health
        {
            get => _health;

            private set
            {
                _health = value;

                if (_health <= 0)
                    Console.WriteLine($"{Name} погиб!");
            }
        }

        public Weapon Weapon { get; }

        public string Name { get; } = "Маг";

        public int GetHealthInformation => _health;

        public int DealDamage => Weapon.Damage;

        public void GetDamage(int damage)
        {
            Health -= damage;
            this.Regeneration();
        }

        public void Attack(IDamagable something)
        {
            something.GetDamage(DealDamage);
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

        public Samurai(Weapon weapon)
        {
            Weapon = weapon;
        }

        public int Health
        {
            get => _health;

            private set
            {
                _health = value;

                if (_health <= 0)
                    Console.WriteLine($"{Name} погиб смертью храбрых!");
            }
        }

        public Weapon Weapon { get; }

        public string Name { get; } = "Самурай";

        public int GetHealthInformation => _health;

        public int DealDamage => Weapon.Damage;

        public void GetDamage(int damage)
        {
            if (this.TryDodge() == false)
                Health -= damage;
            else
                Console.WriteLine("Самурай смог уклониться!)");
        }

        public void Attack(IDamagable something)
        {
            something.GetDamage(DealDamage);
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

    class Barbarian : IFighter
    {
        private int _health = 120;

        public Barbarian(Weapon weapon)
        {
            Weapon = weapon;
        }

        public int Health
        {
            get => _health;

            private set
            {
                _health = value;

                if (_health <= 0)
                    Console.WriteLine($"{Name} погиб!");
            }
        }

        public Weapon Weapon { get; }

        public string Name { get; } = "Варвар";

        public int GetHealthInformation => _health;

        public int DealDamage => Weapon.Damage;

        public void GetDamage(int damage)
        {
            Health -= damage;
        }

        public void Attack(IDamagable something)
        {
            int rate = (int)Math.Round(TakePowerFactor());
            Console.WriteLine($"У {Name} коэфицент силы: {rate}");
            something.GetDamage(DealDamage * rate);
        }

        private float TakePowerFactor()
        {
            const int maxRate = 200;
            const int fullRate = 100;

            return (float)UserUtilits.GetRandomNumber(maxRate) / fullRate;
        }
    }

    class Fairy : IFighter
    {
        private int _health = 60;

        public Fairy(Weapon weapon)
        {
            Weapon = weapon;
        }

        public int Health
        {
            get => _health;

            private set
            {
                _health = value;

                if (_health <= 0)
                    Console.WriteLine($"{Name} погиб!");
            }
        }

        public Weapon Weapon { get; }

        public string Name { get; } = "Фея";

        public int GetHealthInformation => _health;

        public int DealDamage => Weapon.Damage;

        public void GetDamage(int damage)
        {
            Health -= damage - ExtinguishDamage(damage);
        }

        public void Attack(IDamagable something)
        {
            something.GetDamage(DealDamage);
        }

        private int ExtinguishDamage(int damage)
        {
            int maxValue = damage;

            return UserUtilits.GetRandomNumber(maxValue);
        }
    }

    class Goblin : IFighter
    {
        private int _health = 40;

        public Goblin(Weapon weapon)
        {
            Weapon = weapon;
        }

        public int Health
        {
            get => _health;

            private set
            {
                _health = value;

                if (_health <= 0)
                    Console.WriteLine($"{Name} погиб!");
            }
        }

        public Weapon Weapon { get; }

        public string Name { get; } = "Гоблин";

        public int GetHealthInformation => _health;

        public int DealDamage => Weapon.Damage;

        public void GetDamage(int damage)
        {
            Health -= damage;
        }

        public void Attack(IDamagable something)
        {
            something.GetDamage(DealDamage);
        }
    }

    class WeaponBilder
    {
        public static Weapon CreateWeapon(string name)
        {
            const string nameOfFirstWeapon = "Меч";
            const int damageOfFirstWeapon = 20;

            const string nameOfSecondWeapon = "Магия";
            const int damageOfSecondWeapon = 30;

            switch (name)
            {
                case nameOfFirstWeapon:
                    return new Weapon(damageOfFirstWeapon, name);

                case nameOfSecondWeapon:
                    return new Weapon(damageOfSecondWeapon, name);

                default:
                    throw new Exception("Передано несуществующее имя бойца!");
            }
        }
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
}