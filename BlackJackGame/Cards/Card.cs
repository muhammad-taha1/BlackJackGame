using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackGame.Cards
{
    public class Card
    {
        public CardEnums.Suit cardSuit { get; private set; }
        public CardEnums.Value cardValue { get; private set; }
        public bool isFaceUp { get; private set; }

        public Card(CardEnums.Suit s, CardEnums.Value v)
        {
            cardSuit = s;
            cardValue = v;
            isFaceUp = false;
        }

        public Card(string s, string v)
        {
            cardSuit = (CardEnums.Suit)Enum.Parse(typeof(CardEnums.Suit), s);
            cardValue = (CardEnums.Value)Enum.Parse(typeof(CardEnums.Value), v);
        }


        public int getCardScore()
        {
            switch (cardValue.ToString())
            {
                case ("Ace"):
                    return 11;
                case ("Two"):
                    return 2;
                case ("Three"):
                    return 3;
                case ("Four"):
                    return 4;
                case ("Five"):
                    return 5;
                case ("Six"):
                    return 6;
                case ("Seven"):
                    return 7;
                case ("Eight"):
                    return 8;
                case ("Nine"):
                    return 9;

                case ("Ten"):
                case ("Jack"):
                case ("Queen"):
                case ("King"):
                    return 10;
            }
            throw new NotSupportedException("card = " + cardValue.ToString() + " is undefined");
        }

        public void turnCardOver()
        {
            isFaceUp = !isFaceUp;
        }

        public override string ToString()
        {
            return (cardValue.ToString() + " of " + cardSuit.ToString());
        }
    }
}
