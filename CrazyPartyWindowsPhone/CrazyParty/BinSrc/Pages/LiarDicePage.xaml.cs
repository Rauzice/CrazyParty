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

namespace CrazyParty.BinSrc.Pages
{
    public partial class LiarDicePage : PhoneApplicationPage
    {
        public LiarDicePage()
        {
            InitializeComponent();

            GlobalVars.liarDiceGameHelper.GamePage = this;

            addDices();
        }

        private void addDices()
        {
            localListBox.Items.Add(new Dice() { Size = 80 });
            localListBox.Items.Add(new Dice() { Size = 80 });
            localListBox.Items.Add(new Dice() { Size = 80 });
            localListBox.Items.Add(new Dice() { Size = 80 });
            localListBox.Items.Add(new Dice() { Size = 80 });


            oppListBox.Items.Add(new Dice() { Size = 80 });
            oppListBox.Items.Add(new Dice() { Size = 80 });
            oppListBox.Items.Add(new Dice() { Size = 80 });
            oppListBox.Items.Add(new Dice() { Size = 80 });
            oppListBox.Items.Add(new Dice() { Size = 80 });
        }

        public int player { get; set; }

        private List<Dice> diceList;

        public List<Dice> DiceList
        {
            get
            {
                if (diceList == null)
                {
                    diceList = new List<Dice>();
                    diceList.Add(dice1);
                    diceList.Add(dice2);
                    diceList.Add(dice3);
                    diceList.Add(dice4);
                    diceList.Add(dice5);
                }
                return diceList;
            }
        }

        private void rock()
        {
            foreach (Dice dice in DiceList)
            {
                dice.Rock();
            }
        }

        private void submitStateBtn_Click(object sender, EventArgs e)
        {
            
        }

        private void prepareBtn_Click(object sender, EventArgs e)
        {
            rock();
        }

        private void exitBtn_Click(object sender, EventArgs e)
        {

        }
    }
}