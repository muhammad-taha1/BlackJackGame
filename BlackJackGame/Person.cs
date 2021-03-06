﻿using BlackJackGame.Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackGame
{
    public class Person
    {
        public String name { get; private set; }
        public List<Cards.Card> hand { get; private set; }
        public bool bustFlag { get; private set; }

        public bool isPlayer { get; private set; }

        public Person(String aName, bool aIsPlayer)
        {
            name = aName;
            hand = new List<Cards.Card>();
            bustFlag = false;
            isPlayer = aIsPlayer; //indicates if person is dealer or player
        }

        public int getHandValue()
        {
            int value = 0;
            foreach (Cards.Card c in hand)
            {
                if (c.isFaceUp)
                {
                    value += c.getCardScore();
                }
            }

            if (value > 21)
            {
                bustFlag = true;
            }

            return value;
        }

        public string getHand()
        {
            string handDescription = name + " hand: ";
            foreach (Cards.Card c in hand)
            {
                if (c.isFaceUp)
                {
                    handDescription += c.ToString();
                }
                else
                {
                    handDescription += "facedown";
                }

                handDescription += ", ";
            }

            return handDescription;
        }

        public Card hit(DeckOfCards deck)
        {
            Card c = null;
            if (isPlayer)
            {
                c = deck.dealFaceUp();
                hand.Add(c);
            }
            return c;
        }

        public void turnAllCardsFaceUp()
        {
            foreach (Card card in hand)
            {
                if (!card.isFaceUp)
                {
                    card.turnCardOver();
                }
            }    
        }

        public void disposeHand()
        {
            foreach (Card c in hand)
            {
                c.cardFaceImage.Dispose();
            }

            hand = new List<Card>();
        }




    }
}
