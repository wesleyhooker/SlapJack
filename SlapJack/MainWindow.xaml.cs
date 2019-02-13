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
        static Timer roundTimer;
        /// <summary>
        /// Game Logic
        /// </summary>
        Board slapJack;
        #endregion


        #region MAIN METHODS
        public MainWindow()
        {
            InitializeComponent();
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
                roundTimer.Dispose();
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

                //Start timer for players to place card
                while (false)//Not the players turn
                {
                    SetTimer();
                }
            }
        }

        /// <summary>
        /// Slaps the top card on the pile.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Slap(object sender, MouseButtonEventArgs e)
        {
            //reset Timer
            while (false) //Not the players turn
            {
                SetTimer();
            }

            //Checks for a good or bad slap
            if (slapJack.pile[0].Face == "Jack")
            {
                //Add the pile to the player/NPC Hand
                slapJack.players[slapJack.pile.Count-1].GetCards(slapJack.pile);
                imgPile = null;
                //Check for Player win
            }
            else //Not a jack
            {
                //Place card from player Hand to Pile
                PlaceCardInPile(slapJack.players[0]);
                //check for player loss
                if (slapJack.players[0].getHandCount() == 0)
                {
                    GameResult("LOSE");
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
            string sCardURI = @"Images/Cards/" + slapJack.pile[0];
            imgPile.Source = new BitmapImage(new Uri(sCardURI, UriKind.RelativeOrAbsolute));
        }

        private void GameResult(string WinOrLose)
        {
            imgPlayerCard.IsEnabled = false;
            canImgPile.IsEnabled = false;
            slapJack = null;
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
        private static void SetTimer()
        {
            // Create a timer with a two second interval.
            roundTimer = new Timer(2000);
            // Hook up the Elapsed event for the timer. 
            roundTimer.AutoReset = false;
            roundTimer.Enabled = true;
            roundTimer.Elapsed += OnTimedEvent;
        }

        /// <summary>
        /// Play the card
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        private static void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            //Place NPC Hand card to top of pile.
        }
        #endregion
    }
}
