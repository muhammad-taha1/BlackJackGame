using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DadaGame;
using BlackJackGame.Cards;

namespace DadaGameTests
{
    [TestClass]
    public class AITests
    {
        private AI dadaAI;
        [TestInitialize]
        public void Setup()
        {
            dadaAI = new AI();
        }

        [TestMethod]
        public void DissectHandTest()
        {

            dadaAI.dealersHand.Add(new Card(CardEnums.Suit.Clubs, CardEnums.Value.Ace));
            dadaAI.dealersHand.Add(new Card(CardEnums.Suit.Hearts, CardEnums.Value.Ace));
            dadaAI.dealersHand.Add(new Card(CardEnums.Suit.Spades, CardEnums.Value.King));
            dadaAI.dealersHand.Add(new Card(CardEnums.Suit.Clubs, CardEnums.Value.Two));
            dadaAI.dealersHand.Add(new Card(CardEnums.Suit.Clubs, CardEnums.Value.Nine));
            dadaAI.dealersHand.Add(new Card(CardEnums.Suit.Diamonds, CardEnums.Value.King));
            dadaAI.dealersHand.Add(new Card(CardEnums.Suit.Diamonds, CardEnums.Value.Five));
            dadaAI.dealersHand.Add(new Card(CardEnums.Suit.Clubs, CardEnums.Value.Seven));
            dadaAI.dealersHand.Add(new Card(CardEnums.Suit.Spades, CardEnums.Value.Three));
            dadaAI.dealersHand.Add(new Card(CardEnums.Suit.Hearts, CardEnums.Value.Two));

            dadaAI.DissectHand();
            Assert.AreEqual(dadaAI.sameSuitCards[0].Count, 4);
            Assert.AreEqual(dadaAI.sameSuitCards[1].Count, 2);
            Assert.AreEqual(dadaAI.sameSuitCards[2].Count, 2);
            Assert.AreEqual(dadaAI.sameSuitCards[3].Count, 2);

            Assert.AreEqual(dadaAI.sameValueCards[0].Count, 2);
            Assert.AreEqual(dadaAI.sameValueCards[1].Count, 2);
            Assert.AreEqual(dadaAI.sameValueCards[2].Count, 2);
            Assert.AreEqual(dadaAI.sameValueCards[3].Count, 1);
            Assert.AreEqual(dadaAI.sameValueCards[4].Count, 1);
            Assert.AreEqual(dadaAI.sameValueCards[5].Count, 1);
            Assert.AreEqual(dadaAI.sameValueCards[6].Count, 1);
        }

        [TestMethod]
        public void AssignPrioritiesToCardsTest()
        {
            dadaAI.dealersHand.Add(new Card(CardEnums.Suit.Clubs, CardEnums.Value.Ace));
            dadaAI.dealersHand.Add(new Card(CardEnums.Suit.Hearts, CardEnums.Value.Ace));
            dadaAI.dealersHand.Add(new Card(CardEnums.Suit.Spades, CardEnums.Value.King));
            dadaAI.dealersHand.Add(new Card(CardEnums.Suit.Clubs, CardEnums.Value.Two));
            dadaAI.dealersHand.Add(new Card(CardEnums.Suit.Clubs, CardEnums.Value.Nine));
            dadaAI.dealersHand.Add(new Card(CardEnums.Suit.Diamonds, CardEnums.Value.King));
            dadaAI.dealersHand.Add(new Card(CardEnums.Suit.Diamonds, CardEnums.Value.Five));
            dadaAI.dealersHand.Add(new Card(CardEnums.Suit.Clubs, CardEnums.Value.Seven));
            dadaAI.dealersHand.Add(new Card(CardEnums.Suit.Spades, CardEnums.Value.Three));
            dadaAI.dealersHand.Add(new Card(CardEnums.Suit.Hearts, CardEnums.Value.Two));

            dadaAI.DissectHand();
            dadaAI.AssignPrioritiesToCards();

            // random cards priority test
            DeckOfCards testDeck = new DeckOfCards();
            testDeck.cut();
            testDeck.shuffle();
            testDeck.cut();

            dadaAI = new AI();

            for (int i = 0; i < 10; i++)
            {
                dadaAI.dealersHand.Add(testDeck.dealFaceUp());
            }

            dadaAI.topTableCard = testDeck.dealFaceUp();
          //  dadaAI.MakeDecision();
        }
    }
}
