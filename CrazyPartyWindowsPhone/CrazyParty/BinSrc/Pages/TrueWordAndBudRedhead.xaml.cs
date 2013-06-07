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
using CrazyParty.BinSrc.Model;

namespace CrazyParty.BinSrc.Pages
{
    public partial class TrueWordAndBudRedhead : PhoneApplicationPage
    {
        public TrueWordAndBudRedhead()
        {
            InitializeComponent();

            this.punishment.Text = Punishment.GetPunishment();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }
    }
}