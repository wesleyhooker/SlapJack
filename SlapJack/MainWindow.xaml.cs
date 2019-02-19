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
        /// The timer for NPC's to place their card in the pile
        /// </summary>
        DispatcherTimer timerNpcTimeToPlace = null;
        /// <summary>
        /// The timer for Npc's to hit the Jack
        /// </summary>
        DispatcherTimer timerNpcTimeToHit = null;
        /// <summary>
        /// Game Logic
        /// </summary>
        Board slapJack;
        #endregion


        #region MAIN METHODS
        public MainWindow()
        {
            InitializeComponent();
            timerNpcTimeToPlace  = SetTimer(timerNpcTimeToPlace, 2500);
            timerNpcTimeToPlace.Tick += new EventHandler(OnNpcTurn);
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
            if (timerNpcTimeToPlace != null)
            {
                timerNpcTimeToPlace.Stop();
            }
        }

        /// <summary>
        /// Players Hand is clicked. Places top card on pile.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PlayerPlayCard(object sender, MouseButtonEventArgs e)
        {
            if (slapJack.currentPlayer == 0) //if it is the players turn
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
                timerNpcTimeToPlace.Start();
            }
        }

        /// <summary>
        /// Slaps the top card on the pile.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PlayerSlap(object sender, MouseButtonEventArgs e)
        {
            Hit(slapJack.players[0]);
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
            //Player/NPC Hand
            slapJack.addCard(player.PlayCard());
            DisplayHand(player);

            //Pile
            imgPile.Visibility = Visibility.Visible;
            string sCardURI = @"Images/Cards/" + slapJack.getTopCard();
            imgPile.Source = new BitmapImage(new Uri(sCardURI, UriKind.RelativeOrAbsolute));
            txtCount.Text = slapJack.pile.Count.ToString();

            //Sound for Placing Card in Pile
            SoundPlayer simpleSound = new SoundPlayer("../../Sounds/deal.wav");
            simpleSound.Play();
        }

        /// <summary>
        /// Carries out the event of a Win or Loss
        /// </summary>
        /// <param name="WinOrLose"></param>
        private void GameResult(string WinOrLose)
        {
            imgPlayerCard.IsEnabled = false;
            canImgPile.IsEnabled = false;
            slapJack = null;
            timerNpcTimeToPlace.Stop();
            timerNpcTimeToHit.Stop();
            if (WinOrLose == "WIN")
            {
                txtWinOrLose.Text = "YOU WIN";
            }
            else
            {
                txtWinOrLose.Text = "YOU LOSE";
            }
        }

        /// <summary>
        /// Displays or Hides the players UI Hand based on their hand count
        /// </summary>
        /// <param name="player"></param>
        private void DisplayHand(Player player)
        {
            //Pile Card Image
            if (player.getHandCount() != 0)
            {
                switch (player.id)
                {
                    case 0:
                        imgPlayerCard.Visibility = Visibility.Visible;
                        break;
                    case 1:
                        imgNpc1.Visibility = Visibility.Visible;
                        break;
                    case 2:
                        imgNpc2.Visibility = Visibility.Visible;
                        break;
                    case 3:
                        imgNpc3.Visibility = Visibility.Visible;
                        break;
                    default:
                        break;
                }
            }
            else
            {
                switch (player.id)
                {
                    case 0:
                        imgPlayerCard.Visibility = Visibility.Hidden;
                        break;
                    case 1:
                        imgNpc1.Visibility = Visibility.Hidden;
                        break;
                    case 2:
                        imgNpc2.Visibility = Visibility.Hidden;
                        break;
                    case 3:
                        imgNpc3.Visibility = Visibility.Hidden;
                        break;
                    default:
                        break;
                }
            }
        }

        private void Hit(Player player)
        {
            if (slapJack.pile.Count != 0) //You cant hit a pile with 0 cards
            {
                if (slapJack.pile[0].Face == "jack")
                {
                    //Hand
                    slapJack.players[player.id].GetCards(slapJack.pile);
                    DisplayHand(slapJack.players[player.id]);

                    //Pile
                    txtCount.Text = "0";
                    imgPile.Visibility = Visibility.Hidden;

                    //Check for Player Win/Loss
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
                else //Hits a non-jack
                {
                    //Place a hand-card in the pile
                    PlaceCardInPile(slapJack.players[player.id]);
                    txtCount.Text = slapJack.pile.Count.ToString();

                    //check for loss
                    if (slapJack.players[player.id].getHandCount() == 0)
                    {
                        GameResult("LOSE");
                    }
                }
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
            if (slapJack.currentPlayer != 0 || slapJack.players[0].hand.Count() == 0)
            {
                //Place NPC Hand card to top of pile. 
                if (slapJack.players[(slapJack.currentPlayer)].hand.Count != 0)
                {
                    PlaceCardInPile(slapJack.players[(slapJack.currentPlayer)]);
                }

                //hit after a certain time if jack
                timerNpcTimeToHit = SetTimer(timerNpcTimeToHit, slapJack.getTimeToHit());
                timerNpcTimeToHit.Tick += new EventHandler(OnNpcHit);
                if (slapJack.pile.Count != 0)
                {
                    if (slapJack.getTopCard().Face == "jack")
                    {
                        timerNpcTimeToHit.Start();
                    }
                }
            }
            else
            {
                //pause timer
                timerNpcTimeToPlace.Stop();
            }
        }

        /// <summary>
        /// When the NPC hits the jack
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnNpcHit(object sender, EventArgs e)
        {
            //NPC Hit
            Hit(slapJack.players[slapJack.currentPlayer]);

            //Stop the timer for NPC to Hit 
            //(Timers are on loops and if you dont stop the timer then the NPC will hit the pile over and over)
            timerNpcTimeToHit.Stop();
        }
        #endregion
    }
}
