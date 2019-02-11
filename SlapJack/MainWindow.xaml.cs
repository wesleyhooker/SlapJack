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
        private void ImgPlayerCard_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (slapJack.lastPlayed == 0) //if it is the players turn
            {
                //Place Player card on top of pile
                PlaceCardInPile(slapJack.players[0]);

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
        private void CanImgPile_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            //reset Timer
            while (true) //Not the players turn
            {
                SetTimer();
            }

            //Checks for a good or bad slap
            if ("Jack" == "Jack")
            {
                //Add the pile to the player/NPC Hand
                //Check for out Players/win
            }
            else //Not a jack
            {
                //Place card from player/NPC Hand to Pile
                //check for out players/win
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
            string sCardURI = @"\Images\Cards\" + slapJack.pile[0] + ".png";
            imgPile.Source = new BitmapImage(new Uri(sCardURI));
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
