using System;
using BlackJackGame.Cards;
using NUnit.Framework;

namespace UnitTestProject1
{
    [TestFixture]
    public class DeckOfCardsTest
    {
        [Test]
        public void testDeck()
        {
            DeckOfCards deck = new DeckOfCards();

            deck.cut();
            deck.shuffle();
            deck.cut();

            foreach (Card card in deck.deck)
            {
                Console.WriteLine(card.ToString());
                
            }

            Console.WriteLine(deck.deck.Length);
            Console.WriteLine(deck.deck[0]);

            Console.WriteLine(deck.deal().ToString());

            Console.WriteLine(deck.deck.Length);
            Console.WriteLine(deck.deck[0]);

            deck.cut();
            deck.shuffle();
            deck.cut();

            Console.WriteLine(deck.deck.Length);





        }
    }
}
