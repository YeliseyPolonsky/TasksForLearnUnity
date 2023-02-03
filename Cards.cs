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
    public Card(string suit, string meaning)
    {
        Suit = suit;
        Meaning = meaning;
    }

    public string Suit { get; private set; }

    public string Meaning { get; private set; }
}

class Deck
{
    private string[] _suits = { "черви", "пики", "кресте", "бубы" };
    private string[] _meanings = { "6", "7", "8", "9", "10", "король", "дама", "валет" };
    private List<Card> _cards;

    public Deck()
    {
        _cards = new List<Card>();
        CreateCards();
    }

    public void CreateCards()
    {
        for (int i = 0; i < _suits.Length; i++)
        {
            for (int j = 0; j < _meanings.Length; j++)
            {
                _cards.Add(new Card(_suits[i], _meanings[j]));
            }
        }
    }

    public List<Card> GetListOfCards()
    {
        return _cards;
    }
}

class Player
{
    private List<Card> _cards;

    public Player()
    {
        _cards = new List<Card>();
    }

    public List<Card> GetCards()
    {
        return _cards;
    }

    public void ShowCards()
    {
        foreach (Card card in _cards)
        {
            Console.WriteLine(card.Suit + " " + card.Meaning);
        }
    }
}

class Croupier
{
    private Deck _deck;

    public Croupier()
    {
        _deck = new Deck();
    }

    public void GiveCard(Player player)  
    {
        int countCards = _deck.GetListOfCards().Count; 

        if (countCards > 0)
        {
                player.GetCards().Add(_deck.GetListOfCards()[0]);
                _deck.GetListOfCards().RemoveAt(0);
        }
        else
        {
            Console.WriteLine("Больше карт нету!");
        }
    }

    public void ShowCards()
    {
        foreach (Card card in _deck.GetListOfCards())
        {
            Console.Write(card.Suit + " ");
            Console.WriteLine(card.Meaning);
        }
    }

    public Deck GiveDeck() 
    {
        return _deck;
    }
}

class MixingMachine
{
    public List<Card> MixCards(Deck deck)
    {
        Random random = new Random();
        Card buferCard;
        List<Card> cards = deck.GetListOfCards();
        int countCards = cards.Count;

        for (int i = 0; i < countCards; i++)
        {
            int newIndex = random.Next(countCards);
            buferCard = cards[newIndex];
            cards[newIndex] = cards[i];
            cards[i] = buferCard;
        }

        return cards;
    }
}

class Casino
{
    public void StartWork()
    {
        const int OPTION_GET_CARD = 1;
        const int OPTION_SHOW_CARDS = 2;
        const int OPTION_EXIT = 3;
        const int OPTION_SHOW_REMAINING_CARDS = 4;
        const int OPTION_MIX_CARDS = 5;


        bool isWorking = true;
        Player player = new Player();
        Croupier croupier = new Croupier();
        MixingMachine mixingMachine = new MixingMachine();

        Console.WriteLine("Приветствую!");

        while (isWorking)
        {
            Console.WriteLine("Выберите действие: \n" +
                $"{OPTION_GET_CARD} - взять карту;\n" +
                $"{OPTION_SHOW_CARDS} - вскрыть карты(показать)\n" +
                $"{OPTION_EXIT} - выйти;\n" +
                $"{OPTION_SHOW_REMAINING_CARDS} - показать оставшиеся карты;\n" +
                $"{OPTION_MIX_CARDS} - перемешать колоду;");

            switch (GetNumber())
            {
                case OPTION_GET_CARD:
                    croupier.GiveCard(player);
                    break;

                case OPTION_SHOW_CARDS:
                    player.ShowCards();
                    break;

                case OPTION_EXIT:
                    isWorking = false;
                    break;

                case OPTION_SHOW_REMAINING_CARDS:
                    croupier.ShowCards();
                    break;

                case OPTION_MIX_CARDS:
                    mixingMachine.MixCards(croupier.GiveDeck());
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
