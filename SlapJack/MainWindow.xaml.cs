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


        #region TIMER METHODS
        private static void SetTimer()
        {
            // Create a timer with a two second interval.
            roundTimer = new System.Timers.Timer(2000);
            // Hook up the Elapsed event for the timer. 
            roundTimer.Elapsed += OnTimedEvent;
            roundTimer.AutoReset = true;
            roundTimer.Enabled = true;
        }

        /// <summary>
        /// Play the card
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        private static void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
        }
        #endregion
    }
}
