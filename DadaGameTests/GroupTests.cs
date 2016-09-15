using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DadaGame;
using BlackJackGame.Cards;

namespace DadaGameTests
{
    [TestClass]
    public class GroupTests
    {       
        [TestMethod]
        public void checkIfSameValueTest()
        {
            // test valid group
            Group group = new Group();
            group.Add(new Card(CardEnums.Suit.Clubs, CardEnums.Value.Ace));
            group.Add(new Card(CardEnums.Suit.Hearts, CardEnums.Value.Ace));
            group.Add(new Card(CardEnums.Suit.Diamonds, CardEnums.Value.Ace));

            Assert.IsTrue(group.checkIfGroupValid());


            // check for invalid group size
            group = new Group();
            group.Add(new Card(CardEnums.Suit.Clubs, CardEnums.Value.Ace));
            group.Add(new Card(CardEnums.Suit.Hearts, CardEnums.Value.Ace));

            Assert.IsFalse(group.checkIfGroupValid());

            // check for invalid group - diff suit
            group = new Group();
            group.Add(new Card(CardEnums.Suit.Clubs, CardEnums.Value.Ace));
            group.Add(new Card(CardEnums.Suit.Hearts, CardEnums.Value.Ace));
            group.Add(new Card(CardEnums.Suit.Diamonds, CardEnums.Value.King));

            Assert.IsFalse(group.checkIfGroupValid());

            // check for invalid group - same suit
            group = new Group();
            group.Add(new Card(CardEnums.Suit.Clubs, CardEnums.Value.Ace));
            group.Add(new Card(CardEnums.Suit.Clubs, CardEnums.Value.Five));
            group.Add(new Card(CardEnums.Suit.Clubs, CardEnums.Value.King));

            Assert.IsFalse(group.checkIfGroupValid());
        }

        [TestMethod]
        public void checkIfSameSuitAndNumericalOrderTest()
        {
            // valid group and sorted
            Group group = new Group();
            group.Add(new Card(CardEnums.Suit.Clubs, CardEnums.Value.Ace));
            group.Add(new Card(CardEnums.Suit.Clubs, CardEnums.Value.Two));
            group.Add(new Card(CardEnums.Suit.Clubs, CardEnums.Value.Three));

            Assert.IsTrue(group.checkIfGroupValid());

            // valid group unsorted
            group = new Group();
            group.Add(new Card(CardEnums.Suit.Clubs, CardEnums.Value.King));
            group.Add(new Card(CardEnums.Suit.Clubs, CardEnums.Value.Jack));
            group.Add(new Card(CardEnums.Suit.Clubs, CardEnums.Value.Queen));

            Assert.IsTrue(group.checkIfGroupValid());

            // valid group unsorted
            group = new Group();
            group.Add(new Card(CardEnums.Suit.Hearts, CardEnums.Value.Ace));
            group.Add(new Card(CardEnums.Suit.Hearts, CardEnums.Value.Four));
            group.Add(new Card(CardEnums.Suit.Hearts, CardEnums.Value.Three));
            group.Add(new Card(CardEnums.Suit.Hearts, CardEnums.Value.Two));

            Assert.IsTrue(group.checkIfGroupValid());

            // invalid group unsorted
            group = new Group();
            group.Add(new Card(CardEnums.Suit.Hearts, CardEnums.Value.Ace));
            group.Add(new Card(CardEnums.Suit.Hearts, CardEnums.Value.Five));
            group.Add(new Card(CardEnums.Suit.Hearts, CardEnums.Value.Three));
            group.Add(new Card(CardEnums.Suit.Hearts, CardEnums.Value.Two));

            Assert.IsFalse(group.checkIfGroupValid());

            // invalid group
            group = new Group();
            group.Add(new Card(CardEnums.Suit.Hearts, CardEnums.Value.Ace));
            group.Add(new Card(CardEnums.Suit.Hearts, CardEnums.Value.Two));
            group.Add(new Card(CardEnums.Suit.Hearts, CardEnums.Value.Three));
            group.Add(new Card(CardEnums.Suit.Hearts, CardEnums.Value.King));

            Assert.IsFalse(group.checkIfGroupValid());

            // invalid group
            group = new Group();
            group.Add(new Card(CardEnums.Suit.Hearts, CardEnums.Value.Ace));
            group.Add(new Card(CardEnums.Suit.Clubs, CardEnums.Value.Ace));
            group.Add(new Card(CardEnums.Suit.Hearts, CardEnums.Value.Three));
            group.Add(new Card(CardEnums.Suit.Hearts, CardEnums.Value.King));

            Assert.IsFalse(group.checkIfGroupValid());
        }
    }
}
