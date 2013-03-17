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
using System.Windows.Threading;

namespace CrazyParty
{
    public enum ClockwiseDire
    {
        Clockwise, AntiClockwise
    }

    public partial class SpinBottlePage : PhoneApplicationPage
    {
        public SpinBottlePage()
        {
            InitializeComponent();
            myInit();
        }

        private int numberofPeople = 6;
        public int NumberofPeople
        {
            get { return numberofPeople; }
            set { numberofPeople =(int)value; }
        }

        private void myInit()
        {
            clockwiseDire = ClockwiseDire.Clockwise;
            a = 0.003;
            isRotating = false;
            timer = new DispatcherTimer();
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = TimeSpan.FromMilliseconds(33); ;
        }

        public const string resourceURI = @"Resources\";
        public static int result = 0;

        DispatcherTimer timer;

        bool isRotating;  //是否在旋转
        double a;  //加速度
        double V0;  //初始速度
        ClockwiseDire clockwiseDire;    //顺逆时针判断

        //计算出V0的值
        private void getV0()
        {
            //if (last == (first + 1) % queueLen)
            //{
            //    V0 = 0;
            //    return;
            //}
            completeAngle = angles[(last + queueLen - 1) % queueLen];
            preAngle = angles[first];
            double angleDiff = (completeAngle - preAngle + 720) % 360;
            clockwiseDire = ClockwiseDire.Clockwise;
            if (angleDiff > 180)
            {
                angleDiff = 360 - angleDiff;
                clockwiseDire = ClockwiseDire.AntiClockwise;
            }
            completeTime = times[(last + queueLen - 1) % queueLen]; ;
            previousTime = times[first];
            double dur = (completeTime - previousTime).TotalMilliseconds;
            if (dur < 1)
                dur = 100;
            V0 = angleDiff / dur * 10;
            if (V0 == 0)
                return;
            while (V0 > 10)
                V0 /= 2;
            while (V0 < 2)
                V0 *= 2;
        }

        private double getAngle(double x, double y)
        {
            if (x > 0)
                return 90 + Math.Atan(y / x) * 180 / 3.14159;
            else if (x < 0)
                return 270 + Math.Atan(y / x) * 180 / 3.14159;
            else if (y >= 0)
                return 0;
            else
                return 180;
        }

        private void showResult()
        {
            double finalAngle;
            while (this.rotale.Angle < 0)
                this.rotale.Angle += 360;
            finalAngle = (this.rotale.Angle + 270) - ((int)((this.rotale.Angle + 270) / 360)) * 360;
            double aveAngle = 360.0 / numberofPeople;
            result = (int)(finalAngle / aveAngle) + 1;

            this.resultNumber.Text = result.ToString();
        }

        private void startRotate()
        {
            isRotating = true;
            isCaptured = false;
            timer.Start();
        }

        private void stopRotate()
        {
            //musicBackground.Stop();
            showResult();
            timer.Stop();
            isRotating = false;
            //NavigationService.Navigate(new Uri("/PunishmentPage.xaml", UriKind.Relative));
        }


        //轮询操作
        private void timer_Tick(object sender, EventArgs e)
        {
            double dur = timer.Interval.TotalMilliseconds;
            if (V0 > a * dur)
            {
                double changeValue = V0 * dur - 0.5 * a * dur * dur;
                if (clockwiseDire == ClockwiseDire.Clockwise)
                    this.rotale.Angle = this.rotale.Angle + changeValue;
                else
                    this.rotale.Angle = this.rotale.Angle - changeValue;
                showResult();
                V0 = V0 - a * dur;
            }
            else
            {
                stopRotate();
            }
        }

        #region 手滑动事件
        private Point previousPos;
        private DateTime previousTime, completeTime;
        private double preAngle, completeAngle;
        private bool isCaptured = false;
        int first, last;
        const int queueLen = 4;
        double[] angles = new double[queueLen];
        DateTime[] times = new DateTime[queueLen];


        private void StackPanel_MouseEnter(object sender, MouseEventArgs e)
        {
            if (!isRotating && !isCaptured)
            {
                //musicBackground.Play();
                previousPos = e.GetPosition(contentstack);
                previousTime = DateTime.Now;
                this.rotale.Angle = getAngle(previousPos.X - 200, previousPos.Y - 200);
                preAngle = this.rotale.Angle;
                first = 0;
                angles[0] = preAngle;
                times[0] = previousTime;
                last = 1;
                isCaptured = true;
            }
        }

        private void StackPanel_MouseLeave(object sender, MouseEventArgs e)
        {
            if (!isRotating && isCaptured)
            {
                //completePos = e.GetPosition(contentstack);
                //this.rotale.Angle = getAngle(completePos.X - 200, completePos.Y - 200);
                //completeAngle = this.rotale.Angle;
                //completeTime = DateTime.Now;
                getV0();
                startRotate();
            }
        }

        private void StackPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (!isRotating && isCaptured)
            {
                previousPos = e.GetPosition(contentstack);
                previousTime = completeTime;
                completeTime = DateTime.Now;
                preAngle = completeAngle;
                this.rotale.Angle = getAngle(previousPos.X - 200, previousPos.Y - 200);
                showResult();
                completeAngle = this.rotale.Angle;
                if (last == first)
                {
                    first = (first + 1) % queueLen;
                    angles[last] = completeAngle;
                    times[last] = completeTime;
                    last = (last + 1) % queueLen;
                }
                else
                {
                    angles[last] = completeAngle;
                    times[last] = completeTime;
                    last = (last + 1) % queueLen;
                }
            }
        }

        #endregion

        #region 工具栏和菜单栏处理
        private void startButton_Click(object sender, EventArgs e)
        {
            if (!isRotating)
            {
                Random ro = new Random();
                long tick = DateTime.Now.Ticks;
                Random ran = new Random((int)(tick & 0xffffffffL) | (int)(tick >> 32));
                int iResult;
                iResult = ro.Next(2800, 10000);
                V0 = iResult / 1000.0;
                isRotating = true;

            }
        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            if (isRotating)
            {
                stopRotate();
            }
        }

        private void settingItem_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/SettingPivotPage.xaml", UriKind.Relative));
        }

        private void PunishmentItem_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/PunishmentPage.xaml", UriKind.Relative));
        }

        private void helpItem_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/HelpPage.xaml", UriKind.Relative));
        }

        private void aboutItem_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/AboutMePage.xaml", UriKind.Relative));
        }
        #endregion

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            partition(NumberofPeople);
            showResult();
        }


        #region 分块
        Line[] line = new Line[20];
        TextBlock[] textblock = new TextBlock[20];
        public void partition(int area)
        {
            if (area <= 0)
            {
                MessageBox.Show("人数有问题，请输入人数.");
                NavigationService.Navigate(new Uri("/SettingPivotPage.xaml", UriKind.Relative));
            }
            double aveAngle = 360.0 / area;
            this.partitionLines.Children.Clear();
            for (int count = 0; count < area; count++)
            {
                line[count] = new Line();
                line[count].X1 = 40;
                line[count].Y1 = 0;
                line[count].X2 = 140;
                line[count].Y2 = 0;
                line[count].Width = 140;
                line[count].Height = 140;
                line[count].StrokeThickness = 2;
                line[count].StrokeDashArray.Add(2);
                line[count].Stroke = new SolidColorBrush(Colors.Black);
                line[count].RenderTransformOrigin = new Point(0, 0);
                RotateTransform rot = new RotateTransform();
                rot.Angle = aveAngle * count;
                line[count].RenderTransform = rot;

                textblock[count] = new TextBlock();
                textblock[count].Text = (count + 1).ToString();
                textblock[count].Foreground = new SolidColorBrush(Colors.Black);
                textblock[count].FontSize = 40;
                textblock[count].Width = 120;
                textblock[count].Height = 120;
                textblock[count].TextAlignment = TextAlignment.Right;
                textblock[count].FontFamily = new FontFamily("微软雅黑");
                RotateTransform rot1 = new RotateTransform();
                rot1.Angle = aveAngle * count + aveAngle / 2 - 8;
                textblock[count].RenderTransform = rot1;

                this.partitionLines.Children.Add(line[count]);
                this.partitionLines.Children.Add(textblock[count]);
            }
        }
        #endregion
    }
}