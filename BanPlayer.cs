using System;
using System.Collections.Generic;

namespace DataBase
{
    class Program
    {
        static void Main(string[] args)
        {
            Database database = new Database();
            database.StartWork();
        }
    }

    class Player
    {
        public int Number { get; private set; }
        private string _name;
        private bool _isBaned;
        private int _level;

        public Player(int number, string name, int level)
        {
            Number = number;
            _name = name;
            _isBaned = false;
            _level = level;
        }

        public void BanPlayer()
        {
            _isBaned = true;
        }

        public void UnBanPlayer()
        {
            _isBaned = false;
        }
    }

    class Database
    {
        private List<Player> _dataBase = new List<Player>();

        public void StartWork()
        {
            const int OPTION_ADD_PLAYER = 1;
            const int OPTION_DELETE_PLAYER = 2;
            const int OPTION_BAN_PLAYER = 3;
            const int OPTION_UNBAN_PLAYER = 4;

            bool isWorking = true;
            while(isWorking)
            {
                Console.WriteLine("Что вы хотите сделать:\n" +
                $"{OPTION_ADD_PLAYER} - добавить игрока;\n" +
                $"{OPTION_DELETE_PLAYER} - удалить игрока;\n" +
                $"{OPTION_BAN_PLAYER} - забанить игрока;\n" +
                $"{OPTION_UNBAN_PLAYER} - разбанить игрока;\n");

                switch (GetNumber())
                {
                    case OPTION_ADD_PLAYER:
                        AddPlayer();
                        break;

                    case OPTION_DELETE_PLAYER:
                        DeletePlayer();
                        break;

                    case OPTION_BAN_PLAYER:
                        Ban();
                        break;

                    case OPTION_UNBAN_PLAYER:
                        UnBan();
                        break;

                    default:
                        Console.WriteLine("Вы ввели некоректное значение!");
                        break;
                }

                Console.ReadLine();
                Console.Clear();
            }    
        }

        private void AddPlayer()
        {
            int number = GetNumberOfPlayer();
            Console.Write("Имя игрока: ");
            string name = Console.ReadLine();
            Console.WriteLine();
            Console.Write("Уровень игрока: ");
            int level = GetNumber();
            _dataBase.Add(new Player(number, name, level));
        }

        private void DeletePlayer()
        {
            int number = GetNumberOfPlayer();
            bool isMake = false;

            foreach (Player player in _dataBase)
            {
                if (player.Number == number)
                {
                    _dataBase.Remove(player);
                    isMake = true;
                }
            }

            if(isMake == false)
                Console.WriteLine("Игрока с таким номером не существует!");
        }

        private void UnBan()
        {
            int number = GetNumberOfPlayer();
            bool isMake = false;

            foreach (Player player in _dataBase)
            {
                if (player.Number == number)
                {
                    player.UnBanPlayer();
                    isMake = true;
                }
            }

            if (isMake == false)
                Console.WriteLine("Игрока с таким номером не существует!");
        }

        private void Ban()
        {
            int number = GetNumberOfPlayer();
            bool isMake = false;

            foreach (Player player in _dataBase)
            {
                if (player.Number == number)
                {
                    player.BanPlayer();
                    isMake = true;
                }
            }

            if (isMake == false)
                Console.WriteLine("Игрока с таким номером не существует!");
        }

        private int GetNumber()
        {
            bool isWork = true;
            int result = 0;

            while (isWork)
                if (int.TryParse(Console.ReadLine(), out result))
                    isWork = false;
                else
                    Console.WriteLine("Ошибка!");

            Console.WriteLine();
            return result;
        }

        private int GetNumberOfPlayer()
        {
            Console.Write("Введите номер игрока : ");
            int number = GetNumber();
            
            return number;
        }
    }
}
