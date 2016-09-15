using BlackJackGame.Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DadaGame
{
    public class AI
    {
        public List<Card> dealersHand;
        public List<List<Card>> sameValueCards { get; private set; } // each list within is collection cards with same value
        public List<List<Card>> sameSuitCards { get; private set; } // each list within is collection cards with same suit
        public Card topTableCard { get; set; }
        private List<Card> CardsDealt { get; set; }
        private int cardCount { get; set; }

        public AI()
        {
            dealersHand = new List<Card>();
            // dealersHand has to be set in xaml.cs file before any decision is made
            sameSuitCards = new List<List<Card>>();
            sameValueCards = new List<List<Card>>();
            CardsDealt = new List<Card>(); // add to this list from xaml.cs, whenever a card is dealt on table
            // update topTableCard as well
            // update card count as well
        }

        // Program operates on the fact that the current turn is performed by AI.

        // Sort of Fuzzy Logic implementation -> all functions should go in MakeDecision Function.

        // disect all cards in dealersHand in groups of same value and same suit
        // check if disections are valid groups
        // 
        // priority structure for each card in hand:
        // highest: common cards in valid groups
        //          common cards in invalid groups
        //          cards in valid groups
        //          cards in invalid groups
        // lowest: no groups / single group of cards in invalid groups

        // perform the above steps with top tabled card as well

        // AI steps:
        //  try & form group of 4 first from above priority structure
        //  try & form groups of 3
        //  keep track of cards dealt on table so far, to invalidate any possible group
        //  see if top card on table forms a group
        //  see if top card gives a 'good' high priority; discard card with lowest priority
        //  deal card from deck and discard card with lowest priority


        public void MakeDecision()
        {

        }

        public void DissectHand()
        {
            foreach (Card card in dealersHand)
            {
                // add the very first card in both groups.
                if (sameValueCards.Count == 0 && sameSuitCards.Count == 0)
                {
                    List<Card> initialGroup = new List<Card> { card };
                    sameSuitCards.Add(initialGroup.ToList());
                    sameValueCards.Add(initialGroup.ToList());
                }

                else
                {
                    // further iterations of the loop
                    bool sameValListExists = false;
                    foreach (List<Card> sameValueGroup in sameValueCards.ToArray())
                    {
                        // add to sameValueCards
                        if (sameValueGroup.Exists(c => c.cardValue.Equals(card.cardValue)))
                        {
                            sameValueGroup.Add(card);
                            sameValListExists = true;
                        }
                    }

                    if (!sameValListExists)
                    {
                        // if group with same value doesn't exist, create new group
                        List<Card> newGroup = new List<Card> { card };
                        sameValueCards.Add(newGroup);
                    }

                    bool sameSuitListExists = false;
                    foreach (List<Card> sameSuitGroup in sameSuitCards.ToArray())
                    {
                        // add to sameSuitCards
                        if (sameSuitGroup.Exists(c => c.cardSuit.Equals(card.cardSuit)))
                        {
                            sameSuitGroup.Add(card);
                            sameSuitListExists = true;
                        }
                    }

                    if (!sameSuitListExists)
                    {
                        // if group with same suit doesn't exist, create new group
                        List<Card> newGroup = new List<Card> { card };
                        sameSuitCards.Add(newGroup);
                    }
                }
            }
        }

        // assign priorities to groups
        public void AssignPrioritiesToCards()
        {
            // assign priorities for same value cards 
            foreach (List<Card> sameValueGroup in sameValueCards)
            {
                // same value groups with size > 3 or 4 should have high priority
                if (sameValueGroup.Count >= 3)
                {
                    foreach (Card c in sameValueGroup)
                    {
                        c.priority += 50;
                    }
                }

                // same value, group size = 2; lower priority
                if (sameValueGroup.Count == 2)
                {
                    foreach (Card c in sameValueGroup)
                    {
                        int numberOfCardsDealtForSuit = NumCardsDealtForSuit(c.cardSuit);
                        c.priority += numberOfCardsDealtForSuit == 0 ? 13 : numberOfCardsDealtForSuit;
                    }
                }
            }

            // TODO: add logic -> if tabled card forms group of size 3 or 4, make prio = 10


            // logic for sameSuitCards -> priority = 50, for groups which are 3/4 and in numeric order (TODO: including top tabled card)
            foreach (List<Card> sameSuitGroup in sameSuitCards)
            {
                if (sameSuitGroup.Count >= 3)
                {
                    if (GroupInNumericOrder(sameSuitGroup))
                    {
                        // increase priority for groups that are in numeric sequence and hence valid
                        foreach (Card c in sameSuitGroup)
                        {
                            c.priority += 50;
                        }
                    }

                    else
                    {
                        // distance logic
                        List<Card> sortedSameSuitGroup = sameSuitGroup.OrderBy(card => card.cardValue).ToList();
                        for (int i = 0; i < sortedSameSuitGroup.Count - 1; i++)
                        {
                            int start = (int)sortedSameSuitGroup[i].cardValue;
                            int end = (int)sortedSameSuitGroup[i + 1].cardValue;

                            int dist = Math.Abs(end - start);

                            // dist can never be 0!
                            // higher distance, lower priority added
                            sortedSameSuitGroup[i].priority += 25 / dist;
                            sortedSameSuitGroup[i + 1].priority += 25 / dist;

                        }
                    }
                }

                else if (sameSuitGroup.Count == 2)
                {

                    // if group is in sequence
                    if (GroupInNumericOrder(sameSuitGroup))
                    {
                        // check if the card to complete the group has been dealt
                        if (CheckCardRequiredAgainstDealtCards(sameSuitGroup))
                        {
                            // decrement group's priority by 30 -> cards may have priority from other groups
                            foreach (Card c in sameSuitGroup)
                            {
                                c.priority -= 30;
                            }
                        }
                    }

                    else
                    {
                        // distance logic
                        List<Card> sortedSameSuitGroup = sameSuitGroup.OrderBy(card => card.cardValue).ToList();
                        for (int i = 0; i < sortedSameSuitGroup.Count - 1; i++)
                        {
                            int start = (int)sortedSameSuitGroup[i].cardValue;
                            int end = (int)sortedSameSuitGroup[i + 1].cardValue;

                            int dist = Math.Abs(end - start);

                            // dist can never be 0!
                            // higher distance, lower priority added
                            sortedSameSuitGroup[i].priority += 25 / dist;
                            sortedSameSuitGroup[i + 1].priority += 25 / dist;

                        }
                    }
                }
            }

            // prioritize distance between cards; groups of 2 having smaller distance between cards value - higher priority than
            // groups of 3/4 with large distances. greater distance, lower priority between cards

            // group needs a particular card; if required card has been dealt before, forget the group; if not, determine its probability via 'card counting' + # of cards dealt for the suit

            // single groups - priority  = 0



            // check for overlapping cards between sameSuitCards and sameValueCards; *2 priority for each common card
        }

        private bool GroupInNumericOrder(List<Card> sameSuitGroup)
        {
            // ascending sort
            List<Card> sortedSameSuitGroup = sameSuitGroup.OrderBy(card => card.cardValue).ToList();

            for (int i = 0; i < sortedSameSuitGroup.Count - 1; i++)
            {
                if ((sortedSameSuitGroup[i].cardValue + 1) != sortedSameSuitGroup[i + 1].cardValue)
                {
                    return false;
                }
            }
            return true;
        }

        private bool CheckCardRequiredAgainstDealtCards(List<Card> sameSuitGroup)
        {
            // ascending sort
            List<Card> sortedSameSuitGroup = sameSuitGroup.OrderBy(card => card.cardValue).ToList();

            Card lowerCard = new Card(sortedSameSuitGroup[0].cardSuit, sortedSameSuitGroup[0].cardValue - 1);
            if (CardsDealt.Contains(lowerCard)) return true;

            Card upperCard = new Card(sortedSameSuitGroup[0].cardSuit, sortedSameSuitGroup[0].cardValue + 1);
            if (CardsDealt.Contains(upperCard)) return true;

            return false;
        }


        private int NumCardsDealtForSuit(CardEnums.Suit suit)
        {
            int cardsDealt = 0;
            foreach (Card c in CardsDealt)
            {
                if (c.cardSuit.Equals(suit))
                {
                    cardsDealt++;
                }
            }

            return cardsDealt;
        }

    }
}
