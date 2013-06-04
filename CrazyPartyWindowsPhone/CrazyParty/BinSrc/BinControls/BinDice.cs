using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Media.Imaging;

namespace CrazyParty.BinSrc
{
    public class BinDice : ContentControl
    {
        public BinDice()
        {
            DefaultStyleKey = typeof(BinDice);
        }

        #region Point property

        public static readonly DependencyProperty PointProperty =
            DependencyProperty.Register("Point", typeof(int), typeof(BinDice), new PropertyMetadata(6));

        public int Point
        {
            get { return (int)GetValue(PointProperty); }
            set 
            {
                if (!PointProperty.Equals(value))
                {
                    SetValue(PointProperty, value);
                    if (value >= 1 && value <= 6)
                    {
                        Background = DiceImages[value - 1];
                    }
                }
            }
        }
        #endregion

        #region DiceImagesDir property

        private ImageBrush[] DiceImages = new ImageBrush[6];

        public static readonly DependencyProperty DiceImagesDirProperty =
            DependencyProperty.Register("DiceImagesDir", typeof(string), typeof(BinDice), null);

        public string DiceImagesDir
        {
            get { return (string)GetValue(DiceImagesDirProperty); }
            set 
            { 
                SetValue(DiceImagesDirProperty, value);
                for (int i = 1; i <= 6; i++)
                {
                    DiceImages[i - 1].ImageSource = new BitmapImage(new Uri(string.Format("{0}{1}.png", value, i)));
                }
            }
        }

        #endregion

        #region Size property

        public static readonly DependencyProperty SizeProperty = 
            DependencyProperty.Register("Size", typeof(int), typeof(BinDice), new PropertyMetadata(200));

        public int Size
        {
            get { return (int)GetValue(SizeProperty); }
            set
            { 
                SetValue(SizeProperty, value);
                Width = value;
                Height = value;
            }
        }

        #endregion

        #region BackgroundColor property

        public static readonly DependencyProperty BackgroundColorProperty = 
            DependencyProperty.Register("BackgroundColor", typeof(Color), typeof(BinDice), new PropertyMetadata(Colors.Purple));

        public Color BackgroundColor
        {
            get { return (Color)GetValue(BackgroundColorProperty); }
            set { SetValue(BackgroundColorProperty, value); }
        }

        #endregion
    }
}
