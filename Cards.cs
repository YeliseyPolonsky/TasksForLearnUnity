using System;
using System.Collections.Generic;

namespace Cards
{
    class Program
    {
        static void Main(string[] args)
        {
            Casino casino = new Casino();
            casino.StartWork();
        }
    }   
}

class Card
{
    public string Suit { get; private set; }
    public string Meaning { get; private set; }

    public Card(string suit, string meaning)
    {
        Suit = suit;
        Meaning = meaning;
    }
}

class CardDeck
{
    private string[] _suits = { "черви", "пики", "кресте", "бубы" };
    private string[] _meanings = { "1", "2", "3", "4", "5", "6", "король", "дама", "валет" };
    private List<Card> _cards = new List<Card>();

    public CardDeck()
    {
        CreateCardDesk();
    }

    public void CreateCardDesk()
    {
        for (int i = 0; i < _suits.Length; i++)
        {
            for (int j = 0; j < _meanings.Length; j++)
            {
                _cards.Add(new Card(_suits[i], _meanings[j]));
            }
        }
    }

    public List<Card> GetCardDesk()
    {
        return _cards;
    }
}

class Player
{
    protected List<Card> _playerCards = new List<Card>();    
}

class Croupier : Player
{
    private CardDeck cardDeck = new CardDeck();
    Random rand = new Random();
    public void GiveCard()
    {
        int countCards = 36;

        if (countCards > 0)
        {
            int indexCard = rand.Next(0,countCards--);
            _playerCards.Add(cardDeck.GetCardDesk()[indexCard]);
            cardDeck.GetCardDesk().RemoveAt(indexCard);
        }
        else
            Console.WriteLine("Больше карт нету!");
    }

    public void ShowCards()
    {
        foreach (Card card in _playerCards)
        {
            Console.Write(card.Suit + " ");
            Console.WriteLine(card.Meaning);
        }
    }
}

class Casino
{
    public void StartWork()
    {
        const int OPTION_GET_CARD = 1;
        const int OPTION_SHOW_CARDS = 2;
        const int OPTION_EXIT = 3;
        bool isWorking = true;
        Croupier croupier = new Croupier();

        Console.WriteLine("Приветствую!");

        while (isWorking)
        {
            Console.WriteLine("Выберите действие: \n" +
                $"{OPTION_GET_CARD} - взять карту;\n" +
                $"{OPTION_SHOW_CARDS} - вскрыть карты(показать)\n" +
                $"{OPTION_EXIT} - выйти;");

            switch (GetNumber())
            {
                case OPTION_GET_CARD:
                    croupier.GiveCard();
                    break;

                case OPTION_SHOW_CARDS:
                    croupier.ShowCards();
                    break;

                case OPTION_EXIT:
                    isWorking = false;
                    break;

                default:
                    Console.WriteLine("Вы ввели некоректное значение!");
                    break;
            }

            Console.Write("Нажмите любую клавишу для продолжения.");
            Console.ReadKey();
            Console.Clear();
            Console.WriteLine();
        }
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
