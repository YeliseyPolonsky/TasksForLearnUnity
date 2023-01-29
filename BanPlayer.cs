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
        public string Name { get; private set; }
        private bool _isBaned;
        private int _level;

        public Player(int number, string name, int level)
        {
            Number = number;
            Name = name;
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
        private List<Player> _players = new List<Player>();

        public void StartWork()
        {
            const int OPTION_ADD_PLAYER = 1;
            const int OPTION_DELETE_PLAYER = 2;
            const int OPTION_BAN_PLAYER = 3;
            const int OPTION_UNBAN_PLAYER = 4;

            bool isWorking = true;

            while (isWorking)
            {
                Console.WriteLine("Что вы хотите сделать:\n" +
                $"{OPTION_ADD_PLAYER} - добавить игрока;\n" +
                $"{OPTION_DELETE_PLAYER} - удалить игрока;\n" +
                $"{OPTION_BAN_PLAYER} - забанить игрока;\n" +
                $"{OPTION_UNBAN_PLAYER} - разбанить игрока;\n");

                ShowPlayers();

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
            int number = SetNumber();
            Console.Write("Имя игрока: ");
            string name = Console.ReadLine();
            Console.WriteLine();
            Console.Write("Уровень игрока: ");
            int level = GetNumber();
            Console.WriteLine("Уникальный номер данного игрока : " + number);
            _players.Add(new Player(number, name, level));
        }

        private void DeletePlayer()
        {
            int number = GetPlayerNumber();
            bool wasDone = false;

            for (int i = 0; i < _players.Count; i++)
            {
                if (_players[i].Number == number)
                {
                    _players.Remove(_players[i]);
                    wasDone = true;
                }
            }
                
            if (wasDone == false)
                Console.WriteLine("Игрока с таким номером не существует!");
        }

        private void UnBan()
        {
            int number = GetPlayerNumber();
            bool wasDone = false;

            foreach (Player player in _players)
            {
                if (player.Number == number)
                {
                    player.UnBanPlayer();
                    wasDone = true;
                }
            }

            if (wasDone == false)
                Console.WriteLine("Игрока с таким номером не существует!");
        }

        private void Ban()
        {
            int number = GetPlayerNumber();
            bool wasDone = false;

            foreach (Player player in _players)
            {
                if (player.Number == number)
                {
                    player.BanPlayer();
                    wasDone = true;
                }
            }

            if (wasDone == false)
                Console.WriteLine("Игрока с таким номером не существует!");
        }

        private int GetNumber()
        {
            bool isWorking = true;
            int result = 0;

            while (isWorking)
                if (int.TryParse(Console.ReadLine(), out result))
                    isWorking = false;
                else
                    Console.WriteLine("Ошибка!");

            Console.WriteLine();
            return result;
        }

        private int GetPlayerNumber()
        {
            Console.Write("Введите номер игрока : ");
            int number = GetNumber();

            return number;
        }

        private int SetNumber()
        {
            Random rand = new Random();
            bool isWorking = true;
            int countTheSameNumbers = 0;
            int number = 0;

            while (isWorking)
            {
                number = rand.Next(101);
                foreach (Player player in _players)
                {
                    if (player.Number == number)
                    {
                        countTheSameNumbers++;
                    }
                }
                if (countTheSameNumbers == 0)
                    isWorking = false;
            }

            return number;
        }

        private void ShowPlayers()
        {
            Console.SetCursorPosition(40, 0);
            Console.WriteLine("Игроки:");
            int line = 0;

            foreach (Player player in _players)
            {
                Console.SetCursorPosition(40, ++line);
                Console.Write(player.Name);
            }

            Console.SetCursorPosition(0, 7);
        }
    }
}
