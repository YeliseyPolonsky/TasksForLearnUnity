using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp31
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WindowWidth += 10;
            Console.ForegroundColor = ConsoleColor.Yellow;


            int Lenght = 30;
            int[] Array = new int[30];
            List<int> LocalsMax=new List<int>();
            Random rand = new Random();

            Console.Write("Массив : ");
            for (int i = 1; i < Array.Length - 1; i++)
            {
                Array[i] = rand.Next(0, 101);
                Console.Write(Array[i] + " ");
            }


            for (int i = 1; i < Array.Length-1; i++)
            {
                if (Array[i] > Array[i - 1] && Array[i] > Array[i + 1])
                    LocalsMax.Add(Array[i]);
            }
            if(Array[0] > Array[1])
                LocalsMax.Add(Array[0]);
            if (Array[Lenght-1] > Array[Lenght - 2])
                LocalsMax.Add(Array[Lenght - 1]);


            Console.Write("\n\nЛокальные максимумы: ");
            foreach (var item in LocalsMax)
            {
                Console.Write(item+ " ");
            }

            Console.ReadKey(true);




        }
    }
}
