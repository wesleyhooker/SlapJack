using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlapJack
{
    class Player
    {

        public List<Card> hand = new List<Card>();
        public bool isEliminated { get; set; }
        public Random rng;
        public int id;

        public Player(int id)
        {
            this.id = id;
            isEliminated = false;
            rng = new Random();
        }

        //Returns a card to be put in the pile
        public Card PlayCard()
        {
            if (hand.Count != 0)
            {
            Card playedCard = hand[0];
            hand.RemoveAt(0);
            return playedCard;
            }
            return null;
        }

        public Card Flip()
        {
            if (hand.Count> 0)
            {
                Card card = hand[0];
                hand.Remove(card);
                return card;
            } // else player loses 

            return null;
        }

        //Takes card and adds it to players hand
        public void receiveCard(Card card)
        {
            hand.Add(card);            
        }

        //Takes card(s) from the passed in pile and adds it to players hand
        public void GetCards(List<Card> pile)
        {            
           for (int i = 0; i < pile.Count; i++)
            {
                Card gotCard = pile[i];
                hand.Add(gotCard);                
            }
            pile.Clear(); 
            ShuffleHand();
        }
        
        //Shuffles the players hand
        public void ShuffleHand()
        {
            Random rnd = new Random();
            int n = hand.Count;
            while (n > 1)
            {
                n--;
                int y = rnd.Next(n + 1);
                Card value = hand[y];
                hand[y] = hand[n];
                hand[n] = value;
            }
        }

        //Gives a double between the min and max values to determine how long until a player slaps
        public Double getTimeToHit()
        {
            Double min = .5;
            Double max = 1.5;
            return rng.NextDouble() * (max - min) + min;
        }

        //Determines if the player will slap on a non-Jack card
        public bool RandomlySlapped()
        {
            int slappedNum = rng.Next(0, 99);
            int slapChance = 4; //if below number slap will happen

            if (slappedNum > slapChance)
            {
                return false;
            }
            else
            {
                return true;
            }


        }

        //Returns the count for the players hand
        public int getHandCount()
        {
            return hand.Count;
        }


        public bool Elimanate()
        {
            if (isEliminated)
            {
                return isEliminated;
            }
            else
            {
                if (hand.Count == 0)
                {
                    isEliminated = true;
                    return isEliminated;
                }
                else
                {
                    return isEliminated;
                }
            }
        }


    }
}
