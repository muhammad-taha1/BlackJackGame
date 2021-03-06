﻿using System;
using System.Collections.Generic;
using System.Drawing;
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
        public Bitmap cardFaceImage { get; private set; }
        public string cardBackImage { get; private set; }
        public int priority; // used by AI for dada game

        public Card(CardEnums.Suit s, CardEnums.Value v, Bitmap image)
        {
            cardSuit = s;
            cardValue = v;
            isFaceUp = false;
            cardFaceImage = image;
            cardBackImage = "C:/Users/Muhammad Taha/Documents/Visual Studio 2015/Projects/BlackJackGame/BlackJackGame/CardImgs/back.jpg";
        }

        public Card(string s, string v, Bitmap image)
        {
            cardSuit = (CardEnums.Suit)Enum.Parse(typeof(CardEnums.Suit), s);
            cardValue = (CardEnums.Value)Enum.Parse(typeof(CardEnums.Value), v);
            isFaceUp = false;
            cardFaceImage = image;
            cardBackImage = "C:/Users/Muhammad Taha/Documents/Visual Studio 2015/Projects/BlackJackGame/BlackJackGame/CardImgs/back.jpg";
        }

        // for testing only!
        public Card(CardEnums.Suit s, CardEnums.Value v)
        {
            cardSuit = s;
            cardValue = v;
            isFaceUp = false;
            cardFaceImage = null;
            cardBackImage = "C:/Users/Muhammad Taha/Documents/Visual Studio 2015/Projects/BlackJackGame/BlackJackGame/CardImgs/back.jpg";
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

        public string shortString()
        {
            string cardVal = "";
            switch (cardValue.ToString())
            {
                case ("Ace"):
                    cardVal = "A";
                    break;
                case ("Two"):
                    cardVal = "2";
                    break;
                case ("Three"):
                    cardVal = "3";
                    break;
                case ("Four"):
                    cardVal = "4";
                    break;
                case ("Five"):
                    cardVal = "5";
                    break;
                case ("Six"):
                    cardVal = "6";
                    break;
                case ("Seven"):
                    cardVal = "7";
                    break;
                case ("Eight"):
                    cardVal = "8";
                    break;
                case ("Nine"):
                    cardVal = "9";
                    break;
                case ("Ten"):
                    cardVal = "10";
                    break;
                case ("Jack"):
                    cardVal = "J";
                    break;
                case ("Queen"):
                    cardVal = "Q";
                    break;
                case ("King"):
                    cardVal = "K";
                    break;
            }
            return (cardVal + cardSuit.ToString().ElementAt(0));
        }
    }
}
