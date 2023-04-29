using System;
using System.Collections.Generic;
using System.Linq;

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
        Player player = new Player(1000);
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

                int Option = GetNumberWithinLimit(1, 4);

                if (Option == OptionShowProducts)
                    salesman.ShowProducts();

                if (Option == OptionBuyProduct)
                {
                    Console.WriteLine($"Ваш баланс: {player.CountCoins}")
                    Console.Write("Введите номер товара: ");
                    int NumnerOfProduct = GetNumber();
                    Product selectedProduct = salesman.GiveProduct(NumnerOfProduct);

                    if (selectedProduct.Price <= player.CountCoins)
                    {
                        player.GetProduct(selectedProduct);
                        player.GiveCoins(selectedProduct.Price);
                    }
                }

                if (Option == OptionShowThings)
                    player.ShowThings();

                if (Option == OptionExit)
                    isWorking = false;

                Console.WriteLine();
                Console.Write("Нажмите любую клавишу для продолжения.");
                Console.ReadKey();
                Console.Clear();
                Console.WriteLine();
            }
        }

        private int GetNumberWithinLimit(int minNumber, int maxNumber)
        {
            int result = 0;
            bool isWorking = true;

            while (isWorking)
            {
                if (int.TryParse(Console.ReadLine(), out result) && result <= maxNumber && result >= minNumber)
                    isWorking = false;
                else
                    Console.WriteLine("Вы ввели некоректное чтсло! Попробуйте еще раз.");
            }

            return result;
        }

        private int GetNumber()
        {
            int result = 0;
            bool isWorking = true;

            while (isWorking)
            {
                if (int.TryParse(Console.ReadLine(), out result))
                    isWorking = false;
                else
                    Console.WriteLine("Вы ввели некоректное чтсло! Попробуйте еще раз.");
            }

            return result;
        }
    }

    class Salesman
    {
        private List<Product> _products = new List<Product>();

        public Salesman(List<Product> products)
        {
            _products = products;
        }

        public Product GiveProduct(int number)
        {
            int MinIndexInList = 0;

            if (number >= _products.Count || number < MinIndexInList)
            {
                Console.WriteLine("Товара с таким номером не существует!");
                return null;
            }

            Product product = _products.ElementAt(number);
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

        public Player(int countCoins)
        {
            CountCoins = countCoins;
        }

        public int CountCoins { get; private set; }

        public void GiveCoins(int count)
        {
            CountCoins -= count;
        }

        public void GetProduct(Product product)
        {
            _things.Add(product);
        }

        public void ShowThings()
        {
            foreach (Product thing in _things)
            {
                Console.WriteLine(thing.Name);
            }
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
