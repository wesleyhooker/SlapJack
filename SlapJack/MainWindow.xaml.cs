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
        Timer timer;
        #endregion



        #region METHODS
        public MainWindow()
        {
            InitializeComponent();

            // Create a timer with a two second interval.
            timer = new System.Timers.Timer(2000);
            // Hook up the Elapsed event for the timer. 
            //timer.Elapsed += OnTimedEvent; The function to be called when 2 seconds finishes
            timer.AutoReset = true;
            timer.Enabled = true;
        }

        private void CanImgPile_MouseEnter(object sender, MouseEventArgs e)
        {
            //Highlight the Card
            borderImgPile.BorderBrush = Brushes.Yellow;
            txtImgPile.Visibility = Visibility.Visible;
            imgPile.Opacity = 0.9;
        }

        private void CanImgPile_MouseLeave(object sender, MouseEventArgs e)
        {
            //UnHighlight the Card
            borderImgPile.BorderBrush = null;
            txtImgPile.Visibility = Visibility.Hidden;
            imgPile.Opacity = 1;
        }
        #endregion
    }
}
