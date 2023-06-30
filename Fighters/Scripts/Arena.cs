using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IJunior
{
    class Arena
    {
        private List<IFighter> _fighters = new List<IFighter> { FighterBilder.CreateFighter("Маг"), FighterBilder.CreateFighter("Самурай") };

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
                        resultFighter = GetNewFighterClone(_fighters[i]);
                        isWorkig = false;
                    }
                }

                if (resultFighter == null)
                    Console.WriteLine("Бойца с таким именем не существует, попробуй еще раз.");
            }

            return resultFighter;
        }

        private IFighter GetNewFighterClone(IFighter fighterCloned)
        {
            IFighter newfighter = null;

            const string FirstFighterName = "Маг";
            const string SecondFighterName = "Самурай";

            foreach (IFighter fighter in _fighters)
                switch (fighter.Name)
                {
                    case FirstFighterName:
                        newfighter = new Magician(fighterCloned.Weapon);
                        break;

                    case SecondFighterName:
                        newfighter = new Samurai(fighterCloned.Weapon);
                        break;
                }

            return newfighter;
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
                SigleHit(firstFighter, secondFighter, ref isFighting);
            }
        }

        private void SigleHit(IFighter firstFighter, IFighter secondFighter, ref bool isFighting)
        {
            firstFighter.Attack(secondFighter);

            if (secondFighter.GetHealthInformation <= 0)
            {
                isFighting = false;
                Console.WriteLine("Бой окончен.");
            }

            secondFighter.Attack(firstFighter);

            if (firstFighter.GetHealthInformation <= 0)
            {
                isFighting = false;
                Console.WriteLine("Бой окончен.");
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
}
