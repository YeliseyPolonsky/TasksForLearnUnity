using System;
using System.Collections.Generic;

namespace Cards
{
    class Program
    {
        static void Main(string[] args)
        {
            Casino casino = new Casino();
            casino.Work();
        }
    }
}

class Casino
{
    Player player = new Player();
    Croupier croupier = new Croupier();
    bool isWorking = true;

    public void Work()
    {
        const int OptionGetCards = 1;
        const int OptionShowCards = 2;
        const int OptionExit = 3;
        const int OptionShowRemainingCards = 4;
        const int OptionMixCards = 5;

        Console.WriteLine("Приветствую!");

        while (isWorking)
        {
            Console.WriteLine("Выберите действие: \n" +
                $"{OptionGetCards} - взять карту;\n" +
                $"{OptionShowCards} - вскрыть карты(показать)\n" +
                $"{OptionExit} - выйти;\n" +
                $"{OptionShowRemainingCards} - показать оставшиеся карты;\n" +
                $"{OptionMixCards} - перемешать колоду;");

            switch (GetNumber())
            {
                case OptionGetCards:
                    TransferCardToPlayer();
                    break;

                case OptionShowCards:
                    player.ShowCards();
                    break;

                case OptionExit:
                    isWorking = false;
                    break;

                case OptionShowRemainingCards:
                    croupier.ShowCards();
                    break;

                case OptionMixCards:
                    croupier.Shuffle();
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

    private void TransferCardToPlayer()
    {
        Card newCard = croupier.GiveCard();

        if (newCard != null)
            player.AddCard(newCard);
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

class Player
{
    private List<Card> _cardsInHand;

    public Player()
    {
        _cardsInHand = new List<Card>();
    }

    public void ShowCards()
    {
        foreach (Card card in _cardsInHand)
            Console.WriteLine(card.Suit + " " + card.Meaning);
    }

    public void AddCard(Card card)
    {
        _cardsInHand.Add(card);
    }
}

class Croupier
{
    private Deck _deck = new Deck();
    private List<Card> _cards;
    private ShuffleMachine _shuffleMachine = new ShuffleMachine();

    public Croupier()
    {
        _cards = _deck.CreateCards();
    }

    public void Shuffle()
    {
        _shuffleMachine.MixCards(_cards);
    }

    public Card GiveCard()
    {
        int countCards = _cards.Count;

        if (countCards > 0)
        {
            Card newCard = _cards[0];
            _cards.RemoveAt(0);
            return newCard;
        }
        else
        {
            Console.WriteLine("Больше карт нету!");
            return null;
        }
    }

    public void ShowCards()
    {
        foreach (Card card in _cards)
            Console.Write(card.Suit + " " + card.Meaning);
    }
}

class ShuffleMachine
{
    public List<Card> MixCards(List<Card> cards)
    {
        Random random = new Random();
        Card buferCard;
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

class Deck
{
    private string[] _suits = { "черви", "пики", "кресте", "бубы" };
    private string[] _meanings = { "6", "7", "8", "9", "10", "валет", "дама", "король", "туз" };

    public List<Card> CreateCards()
    {
        List<Card> Cards = new List<Card>();

        for (int i = 0; i < _suits.Length; i++)
        {
            for (int j = 0; j < _meanings.Length; j++)
            {
                Cards.Add(new Card(_suits[i], _meanings[j]));
            }
        }

        return Cards;
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