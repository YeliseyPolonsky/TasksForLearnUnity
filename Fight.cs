using System;
using System.Collections.Generic;

namespace IJunior
{
    class Fighting
    {
        private static bool _isFighterDied = false;

        static void Main(string[] args)
        {
            List<IFighter> fighters = new List<IFighter> { new Magician(), new Samurai() };

            Console.WriteLine("Выбирем первого бойца!");
            IFighter firstFighter = GetFighterByName(fighters);

            Console.WriteLine("\nВыбирем второго бойца!");
            IFighter secondFighter = GetFighterByName(fighters);

            Fight(firstFighter, secondFighter);
        }

        public static void FinishFight()
        {
            _isFighterDied = true;
        }

        private static void Fight(IFighter firstFighter, IFighter secondFighter)
        {
            bool isFighting = true;

            while (isFighting)
            {
                Console.WriteLine($"{firstFighter.Name} : здоровье {firstFighter.GetHealthInformation}\n");
                Console.WriteLine($"{secondFighter.Name} : здоровье {secondFighter.GetHealthInformation}\n");
                Console.ReadKey();
                SigleHit();

                if (_isFighterDied)
                    isFighting = false;
            }

            void SigleHit()
            {
                secondFighter.GetDamage(firstFighter.DealDamage());

                if (_isFighterDied)
                    Console.WriteLine($"Победил первый боей {firstFighter.Name}");

                firstFighter.GetDamage(secondFighter.DealDamage());

                if (_isFighterDied)
                    Console.WriteLine($"Победил второй боей {secondFighter.Name}");
            }
        }

        private static IFighter GetFighterByName(List<IFighter> fighters)
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
                    Console.WriteLine("Бойца с тааким именем не существует, попробуй еще раз.");
            }

            return resultFighter;
        }
    }

    interface IFighter : IDamagable, IDealDamage
    {
        string Name { get; }
        string GetHealthInformation { get; }
    }

    interface IDamagable
    {
        void GetDamage(int damage);
    }

    interface IDealDamage
    {
        int DealDamage();
    }

    delegate void Died();

    class Magician : IFighter
    {
        private int _health = 140;
        private Died _died;
        private int _force = 10;

        public Magician()
        {
            _died = DisplayDiedInformation;
            _died += Fighting.FinishFight;
        }

        public int Health
        {
            get => _health;

            private set
            {
                _health = value;

                if (_health <= 0)
                    _died?.Invoke();
            }
        }

        public string Name { get; } = "Маг";

        public string GetHealthInformation => _health.ToString();

        private void DisplayDiedInformation()
        {
            Console.WriteLine($"Маг погиб!");
        }

        public void GetDamage(int damage)
        {
            Health -= damage;
            this.Regeneration();
        }

        public int DealDamage()
        {
            return _force;
        }

        private void Regeneration()
        {
            const int amountOfAddedHealth = 5;
            Health += amountOfAddedHealth;
        }
    }

    class Samurai : IFighter
    {
        private int _health = 140;
        private Died _died;
        private int _force = 25;

        public Samurai()
        {
            _died = DisplayDiedInformation;
            _died += Fighting.FinishFight;
        }

        public int Health
        {
            get => _health;

            private set
            {
                _health = value;

                if (_health <= 0)
                    _died?.Invoke();
            }
        }

        public string Name { get; } = "Самурай";

        public string GetHealthInformation => _health.ToString();

        private void DisplayDiedInformation()
        {
            Console.WriteLine($"Самурай погиб смертью храбрых!");
        }

        public void GetDamage(int damage)
        {
            if (this.TryDodge() == false)
                Health -= damage;
            else
                Console.WriteLine("Самурай смог уклониться!)");
        }

        public int DealDamage()
        {
            return _force;
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
