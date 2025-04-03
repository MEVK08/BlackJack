using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack_AT
{
    public class Player
    {
        protected List<Cards> Hand = new List<Cards>();

        public string Name { get; set; }
        public int CurrentPoints { get; set; }

        public virtual void AddToHand(Cards card)
        {
            Hand.Add(card);

            if (card.Type == CardType.Ace)
            {
                if (CurrentPoints + 11 > 21)
                    CurrentPoints += 1;
                else if (CurrentPoints + 11 <= 21)
                    CurrentPoints += 11;
            }
        }

        public virtual void PrintCards()
        {
            foreach (var card in Hand)
            {
                Console.Write(card.ToString());
            }
        }

        public virtual void ClearHand()
        {
            Hand.Clear();
        }
    }

    public class Dealer : Player { }
}
