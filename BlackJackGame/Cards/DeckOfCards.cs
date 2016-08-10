using BlackJackGame.CardImgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackGame.Cards
{
    public class DeckOfCards
    {
        public Card[] deck { get; private set; }

        public DeckOfCards()
        {
            deck = new Card[52];
            formNewDeck();
        }

        private void formNewDeck()
        {
            int cardCnt = 0;
            int suitCount = 0;
            foreach (string suit in Enum.GetNames(typeof(CardEnums.Suit)))
            {
                int valueCount = 0;
                foreach (string value in Enum.GetNames(typeof(CardEnums.Value)))
                {
                    deck[cardCnt] = new Card(suit, value, ImageParser.getImage(valueCount, suitCount));
                    valueCount++;
                    cardCnt++;
                }
                suitCount++;
            }
        }

       public void shuffle()
        {
            List<Card> tempDeck = deck.ToList();
            Random r = new Random();

            List<Card> res = new List<Card>();
            while (tempDeck.Count > 0)
            {
                int i = r.Next(tempDeck.Count);
                res.Add(tempDeck[i]);
                tempDeck.Remove(tempDeck[i]);
            }

            deck = res.ToArray();
        }

        public void cut()
        {
            List<Card> topHalf = new List<Card>();
            List<Card> bottomHalf = new List<Card>();

            Random r = new Random();
            int cutPoint = r.Next(10, deck.Count());

            for (int i = 0; i < cutPoint; i++)
            {
                topHalf.Add(deck[i]);
            }


            for (int i = cutPoint; i < deck.Count(); i++)
            {
                bottomHalf.Add(deck[i]);
            }

            for (int i = 0; i < deck.Count(); i++)
            {
                // here bottom half and top half are swapped
                if (i < (deck.Count() - cutPoint))
                {
                    deck[i] = bottomHalf[i];
                } 
                else
                {
                    int topIdx = i - bottomHalf.Count();
                    deck[i] = topHalf[topIdx];
                }
            }
        }

        public Card deal()
        {
            Card topCard = deck[0];
            List<Card> tempDeck = deck.ToList();
            tempDeck.RemoveAt(0);
            deck = tempDeck.ToArray();
            return topCard;
        }

        public Card dealFaceUp()
        {
            Card topCard = deck[0];
            List<Card> tempDeck = deck.ToList();
            tempDeck.RemoveAt(0);
            deck = tempDeck.ToArray();
            topCard.turnCardOver();
            return topCard;
        }
    }
}
