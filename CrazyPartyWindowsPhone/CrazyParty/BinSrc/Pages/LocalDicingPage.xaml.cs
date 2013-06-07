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
using System.ComponentModel;

namespace CrazyParty
{
    public partial class LocalDicingPage : PhoneApplicationPage
    {
        public LocalDicingPage()
        {
            InitializeComponent();

            myInit();
        }

        private int numberofDice = 6;
        public int NumberofDice
        {
            get { return numberofDice; }
            set
            {
                numberofDice = (int)value;
            }
        }

        private void myInit()
        {
        }

        private void RockButton_Click(object sender, EventArgs e)
        {
            Dice1.Rock();
        }
    }
}