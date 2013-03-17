using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;

namespace CrazyParty.BinSrc
{
    public partial class BullsAndCows : PhoneApplicationPage
    {
        public BullsAndCows()
        {
            InitializeComponent();
        }

        private void showNumber_Click(object sender, RoutedEventArgs e)
        {
            showNumber.Visibility = Visibility.Collapsed;
            hideNumber.Visibility = Visibility.Visible;

            shutter.Visibility = Visibility.Collapsed;
            numbersGrid.Visibility = Visibility.Visible;
        }

        private void hideNumber_Click(object sender, RoutedEventArgs e)
        {
            showNumber.Visibility = Visibility.Visible;
            hideNumber.Visibility = Visibility.Collapsed;

            shutter.Visibility = Visibility.Visible;
            numbersGrid.Visibility = Visibility.Collapsed;
        }
    }
}