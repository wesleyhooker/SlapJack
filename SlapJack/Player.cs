using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlapJack
{
    class Player
    {

        public List<Card> hand;
        public bool isEliminated { get; set; }
        public Random rng;

        public Player()
        {
            isEliminated = false;
            rng = new Random();
        }

        //Returns a card to be put in the pile
        public Card PlayCard()
        {
            Card playedCard = hand[0];
            hand.RemoveAt(0);
            return playedCard;
        }

        //Takes card(s) from the passed in pile and adds it to players hand
        public void GetCards(List<Card> pile)
        {            
           for (int i = 0; i < pile.Count; i++)
            {
                Card gotCard = pile[i];
                hand.Add(gotCard);
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


    }
}
