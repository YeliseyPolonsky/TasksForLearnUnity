using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp40
{
    class Program
    {
        static void Main(string[] args)
        {
            Player Stas = new Player(1, "Stas",35);
            Player Vano = new Player(2, "Vano",56);
            Player Yelisey = new Player(3, "Yelisey",30);
            Player Ilya = new Player(4, "Ilya",30);
            Database server = new Database(new List<Player>() { Stas, Vano, Yelisey, Ilya });
            server.AddPlayer(4, "Ilya1982", 30);
            server.BanByNumberAtDataBase(1);
        }
    }

    class Player
    {
        public int Number { get; private set; }
        private string _name;
        private bool _isBaned;
        private int Level;

        public Player(int number, string name,int level)
        {
            Number = number;
            _name = name;
            _isBaned = false;
            Level = level;
        }

        public bool GetFlag()
        {
            return _isBaned;
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
        private List<Player> _dataBase;

        public Database(List<Player> dataBase)
        {
            _dataBase = dataBase;
        }

        public void BanByNumberAtDataBase(int number)
        {
            foreach (Player player in _dataBase)
            {
                if (player.Number == number)
                    player.BanPlayer();
            }
        }

        public void AddPlayer(int number, string name, int level)
        {
            _dataBase.Add(new Player(number, name, level));
        }

        public void UnBanByNumberAtDataBase(int number)
        {
            foreach (Player player in _dataBase)
            {
                if (player.Number == number)
                    player.UnBanPlayer();
            }
        }
    }
}
