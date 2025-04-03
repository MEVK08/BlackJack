using Microsoft.VisualBasic;
using System.Numerics;
using System.Text;
using BlackJack_AT;

namespace BlackJack
{
    internal class Program
    {
        static Deck<Cards> deck = new Deck<Cards>();
        static Player player1 = new Player();
        static Dealer dealer = new Dealer();
        static CardType? cardType;
        static int currentMoney = 100;
        static int setMoney;
        static string? currentPlayer;
        static string? winner;


        static void Main(string[] args)
        {
            Console.InputEncoding = Encoding.UTF8;
            Console.OutputEncoding = Encoding.UTF8;

            player1.Name = AskPlayerName();
            do
            {
                ResetGame();
                deck.InitializeDeck();
                deck.Shuffle();

                currentMoney -= SetMoney();

                PlayRound();
            }
            while (PlayNextRound());
        }


        public static string AskPlayerName()
        {
            while (true)
            {
                Console.Write("Spielername: ");
                player1.Name = Console.ReadLine();

                if (!string.IsNullOrEmpty(player1.Name))
                    return player1.Name;
                Console.Clear();
            }
        }


        public static int SetMoney()
        {
            Console.Clear();
            while (true)
            {
                Console.Write($"Wähle deinen Einsatz (10-{currentMoney}): ");
                if (int.TryParse(Console.ReadLine(), out setMoney))
                {
                    if (setMoney < 10 || setMoney > currentMoney)
                    {
                        Console.Clear();
                        continue;
                    }
                    else
                    {
                        Console.Clear();
                        return setMoney;
                    }
                }
                Console.Clear();
            }
        }


        public static void PlayRound()
        {
            currentPlayer = player1.Name;

            HandOutFirstCards();

            while (true)
            {
                PrintMessage($"x${currentMoney}\n", ConsoleColor.Green);
                Console.Write($"Dealer ({dealer.CurrentPoints}) | ");
                dealer.PrintCards();

                Console.Write($"\n\n{player1.Name} ({player1.CurrentPoints}) | ");
                player1.PrintCards();

                if (CheckCurrentPoints())
                    break;

                if (currentPlayer == player1.Name)
                    PlayerMove();
                else if (dealer.CurrentPoints < 17)
                    DealerMove();
                else
                    break;

                Console.Clear();
            }
            currentMoney += CheckWin();
            Thread.Sleep(1200);
        }

        public static void HandOutFirstCards()
        {
            var getTopCard1 = deck.GetTopCard();
            player1.AddToHand(getTopCard1);
            player1.CurrentPoints += getTopCard1.Type.GetNum();

            var getTopCard2 = deck.GetTopCard();
            dealer.AddToHand(getTopCard2);
            dealer.CurrentPoints += getTopCard2.Type.GetNum();
        }

        public static ConsoleKey ChooseOption()
        {
            Console.CursorVisible = false;
            Console.WriteLine("\n\nOptionen:\n[Enter] Weitere Karte ziehen\n[ESC] Nicht weiter ziehen");

            ConsoleKey optionAnswer = Console.ReadKey(true).Key;
            return optionAnswer;
        }


        public static void PlayerMove()
        {
            ConsoleKey optionAnswer = ChooseOption();
            switch (optionAnswer)
            {
                case ConsoleKey.Enter:
                    var topCard = deck.GetTopCard();
                    player1.AddToHand(topCard);
                    player1.CurrentPoints += topCard.Type.GetNum();
                    return;
                case ConsoleKey.Escape:
                    currentPlayer = dealer.Name;
                    break;
                default:
                    Console.Clear();
                    break;
            };
        }

        public static void DealerMove()
        {
            var topCard = deck.GetTopCard();
            dealer.AddToHand(topCard);
            dealer.CurrentPoints += topCard.Type.GetNum();
            Thread.Sleep(1000);
        }


        public static bool CheckCurrentPoints()
        {
            if (player1.CurrentPoints > 21 || dealer.CurrentPoints > 21)
                return true;
            else if (player1.CurrentPoints == 21)
            {
                currentPlayer = dealer.Name;
                return false;
            }
            return false;
        }

        public static int CheckWin()
        {
            if (player1.CurrentPoints > 21 || dealer.CurrentPoints == 21 || player1.CurrentPoints < dealer.CurrentPoints && dealer.CurrentPoints < 21)
            {
                PrintMessage("\n\nDealer hat gewonnen!", ConsoleColor.Red);
                return 0;
            }
            else if (player1.CurrentPoints == 21 || dealer.CurrentPoints > 21 || player1.CurrentPoints > dealer.CurrentPoints && player1.CurrentPoints < 21)
            {
                PrintMessage($"\n\n{player1.Name} hat gewonnen!  +{setMoney * 2}$$", ConsoleColor.Green);
                return setMoney * 2;
            }
            else if (player1.CurrentPoints == dealer.CurrentPoints)
            {
                PrintMessage("\n\nKein Gewinner!", ConsoleColor.Yellow);
                return 0;
            }
            else
                return 0;
        }


        public static bool PlayNextRound()
        {
            if (currentMoney >= 10)
            {
                Console.WriteLine("\nNoch eine Runde? (Enter/ESC)");
                while (true)
                {
                    ConsoleKeyInfo optionAnswer = Console.ReadKey(true);
                    if (optionAnswer.Key == ConsoleKey.Escape || optionAnswer.Key == ConsoleKey.Enter)
                    {
                        switch (optionAnswer.Key)
                        {
                            case ConsoleKey.Enter:
                                return true;
                            case ConsoleKey.Escape:
                                return false;
                            default:
                                break;
                        }
                    }
                }
            }
            else
            {
                PrintMessage("\n\nDu hast keine Münzen mehr zum spielen....", ConsoleColor.Red);
                return false;
            }
        }


        public static void ResetGame()
        {
            player1.CurrentPoints = 0;
            player1.ClearHand();
            dealer.CurrentPoints = 0;
            dealer.ClearHand();
        }

        public static void PrintMessage(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();
        }

    }
}
