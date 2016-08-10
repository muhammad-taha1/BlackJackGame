using BlackJackGame.Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackGame
{
    public class GameRound
    {
        public Person dealer { get; private set; }
        public Person player { get; private set; }

        public GameRound(DeckOfCards deck)
        {
            dealer = new Person("dealer", false);
            player = new Person("player", true);

            deck.cut();
            deck.shuffle();
            deck.cut();

            // deal cards
            player.hand.Add(deck.dealFaceUp());
            dealer.hand.Add(deck.deal());
            player.hand.Add(deck.dealFaceUp());
            dealer.hand.Add(deck.dealFaceUp());

          






        }




    }
}
