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

            switch (name)
            {
                case NameOfFirstFighter:
                    return new Magician(WeaponBilder.CreateWeapon("Магия"));

                case NameOfSecondFighter:
                    return new Samurai(WeaponBilder.CreateWeapon("Меч"));

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
                    Console.WriteLine($"Маг погиб!");
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
                    Console.WriteLine($"Самурай погиб смертью храбрых!");
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
}
