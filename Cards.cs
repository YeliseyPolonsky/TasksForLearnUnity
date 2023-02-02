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

class Deck 
{
    private string[] _suits = { "черви", "пики", "кресте", "бубы" };
    private string[] _meanings = { "1", "2", "3", "4", "5", "6", "король", "дама", "валет" };
    private List<Card> _cards = new List<Card>();

    public Deck() 
    {
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

    public List<Card> GetCardDesk()
    {
        return _cards;
    }
}

class Player
{
    private List<Card> _cards = new List<Card>();  

    public List<Card> GetCards()
    {
        return _cards;
    }

    public void ShowCards() 
    {
        foreach (Card card in _cards) 
        {
            Console.Write(card.Suit + " ");
            Console.WriteLine(card.Meaning);
        }
    }
}

class Croupier  
{
    private Player _player;
    private Deck _deck = new Deck(); 
    private Random random = new Random();

    public Croupier(Player player)
    {
        _player = player;
    }

    public void GiveCard() 
    {
        int countCards = _deck.GetCardDesk().Count;
        int indexCard;

        if (countCards > 0)
        {
            indexCard = random.Next(countCards); 
            _player.GetCards().Add(_deck.GetCardDesk()[indexCard]);
            _deck.GetCardDesk().RemoveAt(indexCard);
        }
        else
        {
            Console.WriteLine("Больше карт нету!");
        }
    }

    public void ShowCards()
    {
        foreach (Card card in _deck.GetCardDesk())
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
    private Croupier _croupier;

    public MixingMachine(Croupier croupier)
    {
        _croupier = croupier;
    }

    public List<Card> MixCards()
    {
        Random random = new Random();
        Card buferCard;
        List<Card> deck = _croupier.GiveDeck().GetCardDesk();
        int countCards = deck.Count;

        for (int i = 0; i < countCards; i++)
        {
            int newIndex = random.Next(countCards);
            buferCard = deck[newIndex];
            deck[newIndex] = deck[i];
            deck[i] = buferCard;
        }

        return deck;
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
        Croupier croupier = new Croupier(player); 
        MixingMachine mixingMachine = new MixingMachine(croupier); 
        
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
                    croupier.GiveCard();
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
                    mixingMachine.MixCards();
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
