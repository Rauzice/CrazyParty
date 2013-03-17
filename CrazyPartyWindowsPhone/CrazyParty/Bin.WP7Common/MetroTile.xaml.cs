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
using System.ComponentModel;

namespace CrazyParty.Bin.WP7Common
{
    public partial class MetroTile : UserControl
    {
        public MetroTile()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty _TitleProperty =
                DependencyProperty.Register(
                "Title", typeof(string),
                 typeof(MetroTile), 
                 new PropertyMetadata("game")
                );

        public string Title
        {
            get { return (string)GetValue(_TitleProperty); }
            set
            {
                SetValue(_TitleProperty, value);
            }
        }

        //public event PropertyChangedEventHandler PropertyChanged;

        //public void NotifyPropertyChanged(string propertyName)
        //{
        //    if (PropertyChanged != null)
        //    {
        //        PropertyChanged(this, 
        //            new PropertyChangedEventArgs(propertyName));
        //    }
        //}

        public string About { get; set; }
    }
}
