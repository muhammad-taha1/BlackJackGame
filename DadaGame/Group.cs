using BlackJackGame.Cards;
using System.Collections.Generic;
using System;
using System.Windows.Controls;

namespace DadaGame
{
    public class Group
    {
        public List<Card> cardsInGroup { get; set; }
        public List<Image> cardImagesOfGroup { get; private set; }

        // create group
        public Group()
        {
            cardsInGroup = new List<Card>();
            cardImagesOfGroup = new List<Image>();
        }

        // add cards to group - for testing purpose only
        public void Add(Card c)
        {
            // max group size is 4
            if (cardsInGroup.Count <= 4) cardsInGroup.Add(c);
        }

        // add cards to group
        public void Add(Card c, Image cardImg)
        {
            // max group size is 4
            if (cardsInGroup.Count <= 4)
            {
                cardsInGroup.Add(c);
                cardImagesOfGroup.Add(cardImg);
            }
        }

        public bool checkIfGroupValid()
        {
            bool validGroup = false;
            if (cardsInGroup.Count == 3 || cardsInGroup.Count == 4)
            {
                validGroup = checkIfSameValue();
                validGroup |= checkIfSameSuitAndNumericalOrder();
            }

            return validGroup;
        }

        private bool checkIfSameSuitAndNumericalOrder()
        {
            // default suit; all suits should be the same
            CardEnums.Suit suit = cardsInGroup[0].cardSuit;
            foreach (Card c in cardsInGroup)
            {
                bool validCardFound = false;
                if (c.cardSuit == suit)
                {
                    // check if numeric sequence is satisfied 
                    foreach (Card c2 in cardsInGroup)
                    {
                        // continue comparison if numeric sequence exists
                        if ((c.cardValue + 1) == c2.cardValue || (c.cardValue - 1) == c2.cardValue)
                        {                         
                            validCardFound = true;
                            break;
                        }

                    }

                    // if valid card is not found, return false, else continue with the loop
                    if (!validCardFound) return false;
                }
                else
                {
                    return false;
                }
            }
            // should hit here when both loops are complete
            return true;
        }

        private bool checkIfSameValue()
        {
            CardEnums.Value val = cardsInGroup[0].cardValue;
            foreach (Card c in cardsInGroup)
            {
                if (val != c.cardValue)
                {
                    // values do not match with initial
                    return false;
                }
            }

            // loop is successful
            return true;
        }
    }
}
