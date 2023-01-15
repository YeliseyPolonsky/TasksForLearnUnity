using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp38
{
    class Program
    {
        static void Main(string[] args)
        {
            Player player = new Player(5, 10);
            Print print = new Print(player,'@');
            print.PrintPlayer();
        }
    }

    class Player
    {
        public int _positionX { private set; get; }
        public int _positionY { private set; get; }

        public Player(int positionX, int positionY)
        {
            _positionX = positionX;
            _positionY = positionY;
        }
    }

    class Print 
    {
        public Player player;
        char symbolOfPlayer;
        public Print(Player player,char symbolOfPlayer)
        {
            this.player = player;
            this.symbolOfPlayer = symbolOfPlayer;

        }
        public void PrintPlayer()
        {
            Console.SetCursorPosition(player._positionX, player._positionY);
            Console.WriteLine(symbolOfPlayer);
        }
    }


}
