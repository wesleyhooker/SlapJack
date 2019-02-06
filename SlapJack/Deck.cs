using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlapJack
{
    class Deck
    {
        public List<Card> deck;

        /// <summary>
        /// Deck constructor generate list of 52 cards
        /// </summary>
        public Deck()
        {

            deck = new List<Card>();
            FillDeck();
        }

        /// <summary>
        /// Fill the deck with standard 52 cards
        /// </summary>
        private void FillDeck()
        {
            string suit = "clubs";

            for (int i = 0; i < 4; i++)
            {

                if (i == 1)
                {
                    suit = "spades";
                }
                else if (i == 2)
                {
                    suit = "hearts";
                }
                else if (i == 3)
                {
                    suit = "diamonds";
                }


                for (int j = 1; j <= 13; j++)
                {

                    if (j == 1)
                    {
                        deck.Add(new Card(suit, "ace"));
                    }
                    else if (j == 11)
                    {
                        deck.Add(new Card(suit, "jack"));
                    }
                    else if (j == 12)
                    {
                        deck.Add(new Card(suit, "queen"));
                    }
                    else if (j == 13)
                    {
                        deck.Add(new Card(suit, "king"));
                    }
                    else
                    {
                        deck.Add(new Card(suit, j.ToString()));
                    }
                }
            }
        }

        /// <summary>
        /// Shuffles the deck list.
        /// </summary>
        public void Shuffle()
        {
            Random rng = new Random();
            //number of swaps between 300-400
            int numSwaps = rng.Next(300, 400);
            int card1;
            Card temp;
            int card2;


            for (int i = 0; i < numSwaps; i++)
            {
                //pick two cards at random
                card1 = rng.Next(deck.Count);
                card2 = rng.Next(deck.Count);

                //swap them
                temp = deck[card2];
                deck[card2] = deck[card1];
                deck[card1] = temp;
            }
        }
    }
}
