using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp37
{
    class Program
    {
        static void Main(string[] args)
        {
            Player knight = new Player("Рыцарь", 100, 50, 45);
            knight.ShowInfo();
        }
    }

    class Player
    {
        private string _name;
        private int _health;
        private int _armor;
        private int _damage;

        public Player(string name, int health, int armor, int damage)
        {
            _name = name;
            _health = health;
            _armor = armor;
            _damage = damage;
        }

        public void ShowInfo()
        {
            Console.WriteLine($"Имя - {_name}.\nЗдоровье - {_health}.\nБроня - {_armor}.\nУрон - {_damage}");
        }
    }
}
