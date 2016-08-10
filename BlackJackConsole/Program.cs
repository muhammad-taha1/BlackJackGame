using BlackJackGame;
using BlackJackGame.Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackConsole
{
    public class Program
    {
        private static GameRound round;
        private static DeckOfCards deck;

        public static void Main(string[] args)
        {
            deck = new DeckOfCards();
            round = new GameRound(deck);

            Console.WriteLine(round.dealer.getHand());
            Console.WriteLine(round.dealer.getHandValue());

            Console.WriteLine(round.player.getHand());
            Console.WriteLine(round.player.getHandValue());


            playGame();
        }

        public static void playGame()
        {
            string inpput = "";
            while (inpput != "stay" && !round.player.bustFlag)
            {
                Console.Write("hit or stay?");

                inpput = Console.ReadLine();
                Console.Write("");


                if (inpput == "hit")
                {
                    round.player.hit(deck);
                    Console.WriteLine("players hand: " + round.player.getHand());
                    Console.WriteLine(round.player.getHandValue());


                }

                else
                {
                    // player calls stay
                    round.dealer.turnAllCardsFaceUp();
                    Console.WriteLine("dealers hand: " + round.dealer.getHand());
                    Console.WriteLine(round.dealer.getHandValue());

                }
            }

            if (round.player.bustFlag)
            {
                Console.WriteLine("player busted!!");
            }

            else
            {
                while (!round.dealer.bustFlag && (round.dealer.getHandValue() < round.player.getHandValue()))
                {
                    round.dealer.hand.Add(deck.dealFaceUp());
                    Console.WriteLine("dealers hand: " + round.dealer.getHand());
                    Console.WriteLine(round.dealer.getHandValue());
                }

                if (round.dealer.bustFlag)
                {
                    Console.WriteLine("dealer busted!");
                }
                else
                {
                    Console.WriteLine("player lost");
                }
            }


            Console.Write("Press any key to exit...");
            Console.ReadLine();
        }
    }
}
