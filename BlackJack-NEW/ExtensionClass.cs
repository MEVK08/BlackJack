using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack_AT
{
    public static class EnumExtension
    {
        public static int GetNum(this CardType cardType)
        {
            switch (cardType)
            {
                case CardType.Two:
                    return 2;
                case CardType.Three:
                    return 3;
                case CardType.Four:
                    return 4;
                case CardType.Five:
                    return 5;
                case CardType.Six:
                    return 6;
                case CardType.Seven:
                    return 7;
                case CardType.Eight:
                    return 8;
                case CardType.Nine:
                    return 9;
                case CardType.Ten:
                    return 10;
                case CardType.King:
                    return 10;
                case CardType.Queen:
                    return 10;
                case CardType.Jack:
                    return 10;
                default:
                    return 0;
            }
        }

    }
}
