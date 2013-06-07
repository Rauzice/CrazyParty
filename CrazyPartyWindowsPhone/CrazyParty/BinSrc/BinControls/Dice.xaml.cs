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
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace CrazyParty.BinSrc
{
    public partial class Dice : UserControl
    {
        private const string DiceImagesDir = @"..\..\Resources\Dices\";

        public Dice()
        {
            InitializeComponent();

            timer = new DispatcherTimer();
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = TimeSpan.FromMilliseconds(33.333);
        }

        #region Point property
        public static readonly DependencyProperty PointProperty =
            DependencyProperty.Register("Point", typeof(int), typeof(Dice), new PropertyMetadata(1));

        public int Point
        {
            get { return (int)GetValue(PointProperty); }
            set
            {
                if (!PointProperty.Equals(value))
                {
                    SetValue(PointProperty, value);
                    DiceImage.Source = new BitmapImage(new Uri(string.Format("{0}{1}.png", DiceImagesDir, value), UriKind.Relative));
                }
            }
        }
        #endregion

        #region Size property

        public static readonly DependencyProperty SizeProperty =
            DependencyProperty.Register("Size", typeof(int), typeof(Dice), new PropertyMetadata(200));

        public int Size
        {
            get { return (int)GetValue(SizeProperty); }
            set
            {
                SetValue(SizeProperty, value);
                if (value != 0)
                {
                    //ScaleTransform scale = new ScaleTransform();
                    //scale.ScaleX = ((double)value / 480);
                    //scale.ScaleY = ((double)value / 480);
                    //this.RenderTransformOrigin = new Point(0, 0);
                    //this.RenderTransform = scale;
                    this.DiceImage.Width = value * 0.875;
                    this.DiceImage.Height = value * 0.875;
                    this.diceBorder.Width = value;
                    this.diceBorder.Height = value;
                    this.Width = value;
                    this.Height = value;
                }
            }
        }

        #endregion

        int count = 0;
        int[] countList = { 1, 10, 18, 25, 31, 36, 40, 43, 45, 46, 47, 48, 49, 50, 52, 55, 59, 64, 70, 77, 85, 94, 104 };

        public void Rock()
        {
            timer.Start();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if(count > 104)
            {
                timer.Stop();
                count = 0;
            }
            if(countList.Contains(count))
            {
                Random rand = new Random();
                Point = rand.Next() % 6 + 1;
            }
            count++;
        }

        DispatcherTimer timer;
    }
}
