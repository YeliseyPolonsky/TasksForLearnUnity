using System;
using System.Collections.Generic;

namespace Shop
{
    sealed class Program
    {
        static void Main(string[] args)
        {
            Shop shop = new Shop();
            shop.Work();
        }
    }

    sealed class Shop
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
                        BuyProduct();
                        break;

                    case OptionShowThings:
                        player.ShowProducts();
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

        private void BuyProduct()
        {
            Console.Write("Введите номер товара: ");
            int NumnerOfProduct = GetNumber();
            player.GetProduct(salesman.GiveProduct(NumnerOfProduct));
        }
    }

    abstract class Person
    {
        protected List<Product> _products = new List<Product>();

        public virtual void ShowProducts()
        {
            foreach (Product product in _products)
                product.ShowInformation();
        }
    }

    sealed class Salesman : Person
    {
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

        public override void ShowProducts()
        {
            for (int i = 0; i < _products.Count; i++)
            {
                Console.Write($"Номер {i}: ");
                _products[i].ShowInformation();
            }
        }
    }

    sealed class Player : Person
    {
        public void GetProduct(Product product)
        {
            _products.Add(product);
        }
    }

    sealed class Product
    {
        public Product(string name, int price)
        {
            Name = name;
        }

        public string Name { get; private set; }

        public void ShowInformation()
        {
            Console.WriteLine($"{Name} ");
        }
    }
}