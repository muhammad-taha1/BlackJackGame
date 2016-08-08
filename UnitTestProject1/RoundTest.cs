using BlackJackGame;
using BlackJackGame.Cards;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestProject1
{
    [TestFixture]
    public class RoundTest
    {
        private GameRound round;
        private DeckOfCards deck;

        [Test]
        public void roundTest()
        {
            deck = new DeckOfCards();
            round = new GameRound(deck);

            Console.WriteLine(round.dealer.getHand());
            Console.WriteLine(round.dealer.getHandValue());

            Console.WriteLine(round.player.getHand());
            Console.WriteLine(round.player.getHandValue());

        }
    }
}
