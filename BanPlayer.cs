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
            Player Stas = new Player(1, "Stas");
            Player Vano = new Player(2, "Vano");
            Player Yelisey = new Player(3, "Yelisey");
            Player Ilya = new Player(4, "Ilya");
            Server server = new Server(new List<Player>() { Stas, Vano, Yelisey, Ilya });
            server.Ban(1);
            Console.WriteLine(Stas._flag);
        }
    }

    class Player
    {
        public int _number;
        public string _name;
        public bool _flag;
        
        public Player(int number,string name)
        {
            _number = number;
            _name = name;
            _flag = false;
        }
        
        public bool GetFlag()
        {
            return _flag;
        }
    }
    class Server
    {
        private List<Player> _server;

        public Server(List<Player> server)
        {
            _server = server;
        }

        public void Ban(int number)
        {
            foreach (Player player in _server)
            {
                if (player._number == number)
                    player._flag = true;
            }
        }
    }
}
