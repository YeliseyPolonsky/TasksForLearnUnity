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
            Print print = new Print(player, '@');
            print.PrintPlayer();
        }
    }

    class Player
    {
        public int PositionX { private set; get; }
        public int PositionY { private set; get; }
        
        public Player(int positionX, int positionY)
        {
            PositionX = positionX;
            PositionY = positionY;
        }
    }

    class Print
    {
        public Player player;
        private char _symbolOfPlayer;
        public Print(Player player, char symbolOfPlayer)
        {
            this.player = player;
            this._symbolOfPlayer = symbolOfPlayer;
        }
        
        public void PrintPlayer()
        {
            Console.SetCursorPosition(player.PositionX, player.PositionY);
            Console.WriteLine(_symbolOfPlayer);
        }
    }
}
