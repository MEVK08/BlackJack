using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack_AT
{
    public enum CardType
    {
        Two,
        Three,
        Four,
        Five,
        Six,
        Seven,
        Eight,
        Nine,
        Ten,
        Jack,
        Queen,
        King,
        Ace
    }

    public enum CardSymbol
    {
        Spade,
        Heart,
        Diamond,
        Club,
    }

    public class Cards
    {
        public CardType Type;
        public CardSymbol Symbol;
        public Cards(CardType type, CardSymbol symbol)
        {
            Type = type;
            Symbol = symbol;
        }

        public string ToString()
        {
            switch (Type)
            {
                case CardType.Ace:
                    return $"[♣A] ";
                case CardType.King:
                    return $"[♦K] ";
                case CardType.Jack:
                    return $"[♠J] ";
                case CardType.Queen:
                    return $"[♥Q] ";
            }

            switch (Symbol)
            {
                case CardSymbol.Heart:
                    return $"[♥{Type.GetNum()}] ";
                case CardSymbol.Diamond:
                    return $"[♠{Type.GetNum()}] ";
                case CardSymbol.Club:
                    return $"[♣{Type.GetNum()}] ";
                case CardSymbol.Spade:
                    return $"[♦{Type.GetNum()}] ";
                default:
                    return "FEHLER.....";
            }
        }
    }

    public class Deck<T>
    {
        private Queue<Cards> deckOfCards = new Queue<Cards>();

        public void InitializeDeck()
        {
            foreach (CardType type in Enum.GetValues<CardType>())
            {
                foreach (CardSymbol symbol in Enum.GetValues<CardSymbol>())
                {
                    deckOfCards.Enqueue(new Cards(type, symbol));
                }
            }
        }


        public void Shuffle()
        {
            var shuffled = new List<Cards>(deckOfCards);
            var rng = new Random();

            for (int i = shuffled.Count - 1; i > 0; i--)
            {
                int j = rng.Next(i + 1);
                (shuffled[i], shuffled[j]) = (shuffled[j], shuffled[i]);
            }

            deckOfCards = new Queue<Cards>(shuffled);
        }

        public Cards GetTopCard()
        {
            return deckOfCards.Dequeue();
        }
    }
}
