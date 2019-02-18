using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Timers;
using System.Media;
using System.Windows.Threading;
using System.Threading;

namespace SlapJack
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region VARIABLES
        /// <summary>
        /// Keeps Track of inGame Time
        /// </summary>
        DispatcherTimer roundTimer = null;
        /// <summary>
        /// Npc's time to hit
        /// </summary>
        DispatcherTimer npcTimeToHit = null;
        /// <summary>
        /// Game Logic
        /// </summary>
        Board slapJack;
        #endregion


        #region MAIN METHODS
        public MainWindow()
        {
            InitializeComponent();
            roundTimer  = SetTimer(roundTimer, 2500);
            roundTimer.Tick += new EventHandler(OnNpcTurn);
        }
        #endregion


        #region UI METHODS
        /// <summary>
        /// Starts a new SlapJack game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewGame_Click(object sender, RoutedEventArgs e)
        {
            //Allow Buttons to be pressed
            imgPlayerCard.IsEnabled = true;
            canImgPile.IsEnabled = true;
            slapJack = new Board();

            //Reset timer
            if (roundTimer != null)
            {
                roundTimer.Stop();
            }
        }

        /// <summary>
        /// Players Hand is clicked. Places top card on pile.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PlayCard(object sender, MouseButtonEventArgs e)
        {
            if (slapJack.lastPlayed == 0) //if it is the players turn
            {
                //Place Player card on top of pile
                if (slapJack.players[0].getHandCount() != 0)
                {
                    PlaceCardInPile(slapJack.players[0]);
                }
                else
                {     
                    return;
                }

                //Start timer for NPCs to place cards
                roundTimer.Start();
            }
        }

        /// <summary>
        /// Slaps the top card on the pile.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PlayerSlap(object sender, MouseButtonEventArgs e)
        {
            //Checks for a good or bad slap
            if (slapJack.pile.Count != 0)
            {
                if (slapJack.pile[0].Face == "jack")
                {
                    //Add the pile to the player/NPC Hand
                    slapJack.players[0].GetCards(slapJack.pile);
                    txtCount.Text = "0";
                    imgPile.Visibility = Visibility.Hidden;
                    //Check for Player win
                    if (slapJack.checkWinner() != -1)
                    {
                        if (slapJack.checkWinner() != 0)
                        {
                            GameResult("LOSS");
                        }
                        else
                        {
                            GameResult("WIN");
                        }
                    }
                }
                else //Not a jack
                {
                    //Place card from player Hand to Pile
                    PlaceCardInPile(slapJack.players[0]);
                    txtCount.Text = slapJack.pile.Count.ToString();
                    //check for player loss
                    if (slapJack.players[0].getHandCount() == 0)
                    {
                        GameResult("LOSE");
                    }
                }
            }
        }

        /// <summary>
        /// Highlights the pile for slapping
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CanImgPile_MouseEnter(object sender, MouseEventArgs e)
        {
            //Highlight the Card
            borderImgPile.BorderBrush = Brushes.Yellow;
            txtImgPile.Visibility = Visibility.Visible;
            imgPile.Opacity = 0.9;
        }

        /// <summary>
        /// Unhighlights the pile for slapping
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CanImgPile_MouseLeave(object sender, MouseEventArgs e)
        {
            //UnHighlight the Card
            borderImgPile.BorderBrush = null;
            txtImgPile.Visibility = Visibility.Hidden;
            imgPile.Opacity = 1;
        }
        #endregion


        #region HELPER METHODS
        /// <summary>
        /// Places The card on the top of the hand into the pile.
        /// </summary>
        /// <param name="player"></param>
        private void PlaceCardInPile(Player player)
        {
            slapJack.addCard(player.PlayCard());

            //Pile Card Image
            if (player.getHandCount() == 0)
            {
                switch (player.id)
                {
                    case 0:
                        imgPlayerCard.Source = null;
                        break;
                    case 1:
                        imgNpc1.Source = null;
                        break;
                    case 2:
                        imgNpc2.Source = null;
                        break;
                    case 3:
                        imgNpc3.Source = null;
                        break;
                    default:
                        break;
                }
            }
            imgPile.Visibility = Visibility.Visible;
            string sCardURI = @"Images/Cards/" + slapJack.getTopCard();
            imgPile.Source = new BitmapImage(new Uri(sCardURI, UriKind.RelativeOrAbsolute));
            txtCount.Text = slapJack.pile.Count.ToString();

            SoundPlayer simpleSound = new SoundPlayer("../../Sounds/deal.wav");
            simpleSound.Play();
        }


        private void GameResult(string WinOrLose)
        {
            imgPlayerCard.IsEnabled = false;
            canImgPile.IsEnabled = false;
            slapJack = null;
            roundTimer.Stop();
            npcTimeToHit.Stop();
            if (WinOrLose == "WIN")
            {
                txtWinOrLose.Text = "YOU WIN";
            }
            else
            {
                txtWinOrLose.Text = "YOU LOSE";
            }
        }
        #endregion


        #region TIMER METHODS
        private DispatcherTimer SetTimer(DispatcherTimer timer, int miliseconds)
        {
            // Create a timer with a two second interval.
            timer = new DispatcherTimer();
            // Hook up the Elapsed event for the timer. 
            timer.Interval = new TimeSpan(0, 0, 0, 0, miliseconds);
            return timer;
        }

        /// <summary>
        /// Play the card
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        private void OnNpcTurn(object sender, EventArgs e)
        {
            if (slapJack.lastPlayed != 0 || slapJack.players[0].hand.Count() == 0)
            {
                //Place NPC Hand card to top of pile. 
                PlaceCardInPile(slapJack.players[(slapJack.lastPlayed)]);
                //hit after a certain time if jack
                npcTimeToHit = SetTimer(npcTimeToHit, slapJack.getTimeToHit());
                npcTimeToHit.Tick += new EventHandler(OnNpcHit);
                if (slapJack.getTopCard().Face == "jack")
                {
                    npcTimeToHit.Start();
                }
            }
            else
            {
                //pause timer
                roundTimer.Stop();
            }
        }

        /// <summary>
        /// When the NPC hits the jack
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnNpcHit(object sender, EventArgs e)
        {
            //Checks for a good or bad slap
            if (slapJack.pile.Count != 0)
            {
                if (slapJack.pile[0].Face == "jack")
                {
                    //Add the pile to the player/NPC Hand
                    slapJack.players[slapJack.lastPlayed].GetCards(slapJack.pile);
                    txtCount.Text = "0";
                    imgPile.Visibility = Visibility.Hidden;
                    //Check for Player win
                    if (slapJack.checkWinner() != -1)
                    {
                        if (slapJack.checkWinner() == 0)
                        {
                            GameResult("WIN");
                        }
                        else
                        {
                            GameResult("LOSS");
                        }
                    }
                }
            }
            npcTimeToHit.Stop();
        }
        #endregion
    }
}
