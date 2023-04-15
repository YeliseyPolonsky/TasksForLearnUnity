using System;
using System.Collections.Generic;
using System.Linq;

namespace Library
{
    class Program
    {
        static void Main(string[] args)
        {
            WorkbookStorage workbookStorage = new WorkbookStorage();
            workbookStorage.Work();
        }
    }

    class Book
    {
        public Book(string name, string author, int year)
        {
            Name = name;
            Author = author;
            Year = year;
        }

        public string Name { get; private set; }
        public string Author { get; private set; }
        public int Year { get; private set; }

        public void ShowInformation()
        {
            Console.WriteLine($"Имя: {Name}, автор: {Author}, год выпуска: {Year}");
        }
    }

    class Library
    {
        private List<Book> _books = new List<Book>();

        public void AddBook(Book book)
        {
            _books.Add(book);
        }

        public void DelateBookAt(int index)
        {
            _books.RemoveAt(index);
        }

        public void ShowAllBooks()
        {
            foreach (Book book in _books)
                book.ShowInformation();
        }

        public int GetBooksCount()
        {
            return _books.Count;
        }

        public Book GetBookAt(int index)
        {
            return _books.ElementAt(index);
        }
    }

    class WorkbookStorage
    {
        private Library _library = new Library();
        const int YearNow = 2023;

        public void Work()
        {
            Console.WriteLine("Здравствуйте!");

            const int OptionAddBook = 1;
            const int OptionDelateBook = 2;
            const int OptionShowByAuthor = 3;
            const int OptinShowByYear = 4;
            const int OptionShowByName = 5;
            const int OptionExit = 6;
            bool IsWork = true;

            while (IsWork)
            {
                Console.WriteLine("Выберите действие: \n" +
                $"{OptionAddBook} - добавить книгу;\n" +
                $"{OptionDelateBook} - удалить книгу по номеру;\n" +
                $"{OptionShowByAuthor} - показ по автору;\n" +
                $"{OptinShowByYear} - показ по году выпуска;\n" +
                $"{OptionShowByName} - показ по имени;\n" +
                $"{OptionExit} - выйти");

                switch (GetNumber())
                {
                    case OptionAddBook:
                        AddBook();
                        break;

                    case OptionDelateBook:
                        DelateBook();
                        break;

                    case OptionShowByAuthor:
                        ShowBooksByAuthor();
                        break;

                    case OptinShowByYear:
                        ShowBooksByYear();
                        break;

                    case OptionShowByName:
                        ShowBooksByName();
                        break;

                    case OptionExit:
                        IsWork = false;
                        break;

                    default:
                        Console.WriteLine("Вы ввели некоректное значение!");
                        break;
                }

                Console.ReadKey();
                Console.Clear();
            }
        }

        private void AddBook()
        {
            Console.Write("Название : ");
            string newName = Console.ReadLine();

            Console.Write("Автор : ");
            string newAuthor = Console.ReadLine();

            Console.Write("Год выпуска : ");
            int newYear = GetNumberUpTo(YearNow);

            _library.AddBook(new Book(newName, newAuthor, newYear));
        }

        private void DelateBook()
        {
            int countBooks = _library.GetBooksCount();

            Console.Write("Номер : ");
            _library.DelateBookAt(GetNumberUpTo(countBooks - 1));
        }

        private void ShowBooksByAuthor()
        {
            int CountSuitableBooks = 0;
            Console.Write(" Автор : ");
            string Author = Console.ReadLine();

            for (int i = 0; i < _library.GetBooksCount(); i++)
            {
                Book BookNaw = _library.GetBookAt(i);
                if (BookNaw.Author == Author)
                {
                    BookNaw.ShowInformation();
                    CountSuitableBooks++;
                }
            }

            if (CountSuitableBooks == 0)
                Console.WriteLine("Не найденено!");
        }

        private void ShowBooksByName()
        {
            int CountSuitableBooks = 0;
            Console.Write(" Название : ");
            string Name = Console.ReadLine();

            for (int i = 0; i < _library.GetBooksCount(); i++)
            {
                Book BookNaw = _library.GetBookAt(i);
                if (BookNaw.Name == Name)
                {
                    BookNaw.ShowInformation();
                    CountSuitableBooks++;
                }
            }

            if (CountSuitableBooks == 0)
                Console.WriteLine("Не найденено!");
        }

        private void ShowBooksByYear()
        {
            int CountSuitableBooks = 0;
            Console.Write(" Год издания : ");
            int Year = GetNumber();

            for (int i = 0; i < _library.GetBooksCount(); i++)
            {
                Book BookNaw = _library.GetBookAt(i);
                if (BookNaw.Year == Year)
                {
                    BookNaw.ShowInformation();
                    CountSuitableBooks++;
                }
            }

            if (CountSuitableBooks == 0)
                Console.WriteLine("Не найденено!");
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

        private int GetNumberUpTo(int maxValue)
        {
            int result = 0;
            bool isWorking = true;

            while (isWorking)
            {
                if (int.TryParse(Console.ReadLine(), out result) && result <= maxValue)
                    isWorking = false;
                else
                    Console.WriteLine("Вы ввели некоректное чтсло! Попробуйте еще раз.");
            }

            return result;
        }
    }
}
