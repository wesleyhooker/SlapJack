﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlapJack
{
    class Board
    {
        private static int MAX_PLAYERS = 4;
        public List<Card> pile;
        public List<Player> players { get; }
        private Deck deck;
        public int currentPlayer { get; set; }


        public Card getTopCard()
        {
            try
            {
                return pile[0];
            }

            catch (ArgumentOutOfRangeException)

            {
                Console.WriteLine("There are no cards on the pile.");

            }

            return null;
        }

        public int getPileCount()

        {
            return pile.Count;

        }

        public Board()

        {
            //instantiate objects needed for game

            pile = new List<Card>();

            players = new List<Player>();

            for (int playerId = 0; playerId < MAX_PLAYERS; playerId++)
            {
                players.Add(new Player(playerId));
            }

            currentPlayer = 0;

            //deal players their cards

            deck = new Deck();
            List<Card> tempDeck = deck.deck;

            Card nextCard = tempDeck[0];
            tempDeck.RemoveAt(0);

            while (nextCard != null)
            {
                for (int playerId = 0; playerId < MAX_PLAYERS; playerId++)
                {
                    players[playerId].receiveCard(nextCard);

                    if (tempDeck.Count != 0)
                    {
                    nextCard = tempDeck[0];
                    tempDeck.RemoveAt(0);
                    }
                    else
                    {
                        nextCard = null;
                    }
                }
            }
        }
        //get a player

        public Player getPlayer(int playerNum)

        {
            return players[playerNum - 1];

        }

        /// <summary>
        /// Adds the player's top card to the pile
        /// </summary>
        /// <param name="newCard"></param>
        public void addCard(Card newCard)
        {
            if (newCard != null)
            {
                pile.Insert(0, newCard);

                if (currentPlayer != 3)
                {
                    currentPlayer++;
                }
                else //cycle back to first players turn.
                {
                    currentPlayer = 0;
                }
            }
        }


    
        /// <param name="playerId">The id of the player who slapped</param>
        /// <returns> true if the players slap is valid.false it is not</returns>

        public bool playerSlapped(int playerId)

        {
            bool slapValidity = isValidSlap();

            if (slapValidity)

            {
                getPlayer(playerId).GetCards(pile);

                Console.WriteLine("Player " + playerId + "'s slap succeded! He added " + pile.Count + " cards to his hand");

                pile.Clear();
            }

            else

            {
                Console.WriteLine("Player " + playerId + "'s slap failed. He gave a card to player " + (3 - playerId));

                getPlayer((3 - playerId)).receiveCard(getPlayer(playerId).Flip());

            }

            return slapValidity;
        }

        public Card playerFlipped(int playerId)

        {
            if (playerId != currentPlayer)

            {
                Card card = getPlayer(playerId).Flip();

                if (card == null)

                    return null;

                //addCard(card);

                currentPlayer = playerId;

                return card;
            }

            return null;
        }

        /// <summary>

        /// Checks each players hand for a winner

        /// </summary>

        /// <returns>-1 if there is no winner or the id of the player that won </returns>

        public int checkWinner()

        {
            int winnerId = -1;

            for (int playerId = 0; playerId < MAX_PLAYERS; playerId++)

            {

                winnerId = players[playerId].getHandCount() >= 52 ? playerId : winnerId;

            }

            for (int playerId = 0; playerId < MAX_PLAYERS; playerId++)
            {

                winnerId = (players[playerId].getHandCount() + pile.Count) >= 52 ? playerId : winnerId;
            }

            return winnerId;
        }
        public Boolean isValidSlap()

        {
            try

            {
                return getTopCard().Face == "jack";
            }
            catch (NullReferenceException)

            {
                return false;
            }
        }

        /// <summary>
        /// Returns the Lowest Npc time to hit;
        /// </summary>
        /// <returns></returns>
        public int getTimeToHit()
        {
            double timeToHit = 2;
            foreach (var player in players)
            {
                if (player.getTimeToHit() < timeToHit)
                {
                    timeToHit = player.getTimeToHit();
                }
            }
            return (int)(timeToHit*1000);
        }
    }
}
