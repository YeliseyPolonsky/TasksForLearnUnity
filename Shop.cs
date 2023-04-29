using System;
using System.Collections.Generic;

namespace Shop
{
    class Program
    {
        static void Main(string[] args)
        {
            Shop shop = new Shop();
            shop.Work();
        }
    }

    class Shop
    {
        Player player = new Player();
        Salesman salesman = new Salesman(new List<Product> { new Product("Эликсир здоровья", 30), new Product("Золотое яблоко", 150), new Product("Банан", 10) });

        bool isWorking = true;

        public void Work()
        {
            const int OptionShowProducts = 1;
            const int OptionBuyProduct = 2;
            const int OptionShowThings = 3;
            const int OptionExit = 4;

            Console.WriteLine("Приветствую!");

            while (isWorking)
            {
                Console.WriteLine("Выберите действие: \n" +
                    $"{OptionShowProducts} - посмотреть товары;\n" +
                    $"{OptionBuyProduct} - купить товар\n" +
                    $"{OptionShowThings} - посмотреть свои вещи\n" +
                    $"{OptionExit} - выйти\n");

                switch (GetNumber())
                {
                    case OptionShowProducts:
                        salesman.ShowProducts();
                        break;

                    case OptionBuyProduct:
                        Console.Write("Введите номер товара: ");
                        int NumnerOfProduct = GetNumber();
                        player.GetProduct(salesman.GiveProduct(NumnerOfProduct));
                        break;

                    case OptionShowThings:
                        player.ShowThings();
                        break;

                    case OptionExit:
                        isWorking = false;
                        break;

                    default:
                        Console.WriteLine("Такой опции нету!");
                        break;
                }

                Console.WriteLine();
                Console.Write("Нажмите любую клавишу для продолжения.");
                Console.ReadKey();
                Console.Clear();
                Console.WriteLine();
            }
        }

        private int GetNumber()
        {
            int number = 0;

            while (int.TryParse(Console.ReadLine(), out number) == false)
                Console.WriteLine("Попробуйте снова");

            return number;
        }
    }

    class Salesman
    {
        private List<Product> _products = new List<Product>();

        public Salesman(List<Product> products)
        {
            foreach (Product product in products)
            {
                _products.Add(product);
            }
        }

        public Product GiveProduct(int number)
        {
            Product product = null;

            int MinIndexInList = 0;

            if (number >= _products.Count || number < MinIndexInList)
            {
                Console.WriteLine("Товара с таким номером не существует!");
                return product;
            }

            product = _products[number];
            _products.RemoveAt(number);

            return product;
        }

        public void ShowProducts()
        {
            for (int i = 0; i < _products.Count; i++)
            {
                Console.Write($"Номер {i}: ");
                _products[i].ShowInformation();
            }
        }
    }

    class Player
    {
        private List<Product> _things = new List<Product>();

        public void GetProduct(Product product)
        {
            _things.Add(product);
        }

        public void ShowThings()
        {
            foreach (Product thing in _things)
                Console.WriteLine(thing.Name);
        }
    }

    class Product
    {
        public Product(string name, int price)
        {
            Name = name;
            Price = price;
        }

        public string Name { get; private set; }

        public int Price { get; private set; }

        public void ShowInformation()
        {
            Console.WriteLine($"{Name}  {Price} монет");
        }
    }
}