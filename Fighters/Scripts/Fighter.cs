using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IJunior
{
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
            something.GetDamage(DealDamage*rate);
        }

        private float TakePowerFactor()
        {
            const int maxRate = 200;
            const int fullRate = 100;

            return (float)UserUtilits.GetRandomNumber(maxRate)/ fullRate;
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
            Health -= damage-ExtinguishDamage(damage);
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
}
