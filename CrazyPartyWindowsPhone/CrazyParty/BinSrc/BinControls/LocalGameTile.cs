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
using System.Windows.Media.Imaging;

namespace CrazyParty.BinSrc
{
    public class LocalGameTile : Button
    {
        #region Title property

        public static readonly DependencyProperty TitleProperty = 
            DependencyProperty.Register("Title", typeof(string), typeof(LocalGameTile), null);

        public string Title
        {
            get{ return (string)GetValue(TitleProperty); }
            set{ SetValue(TitleProperty, value); }
        }

        #endregion

        #region Abstract property

        public static readonly DependencyProperty AbstractProperty = 
            DependencyProperty.Register("Abstract", typeof(string), typeof(LocalGameTile), null);

        public string Abstract
        {
            get { return (string)GetValue(AbstractProperty); }
            set { SetValue(AbstractProperty, value); }
        }

        #endregion

        #region GameImage property

        public static readonly DependencyProperty GameImageProperty =
            DependencyProperty.Register("GameImage", typeof(ImageSource), typeof(LocalGameTile), null);

        public ImageSource GameImage
        {
            get { return (ImageSource)GetValue(GameImageProperty); }
            set { SetValue(GameImageProperty, value); }
        }

        #endregion

        #region Size property

        public static readonly DependencyProperty SizeProperty = 
            DependencyProperty.Register("Size", typeof(int), typeof(LocalGameTile), new PropertyMetadata(200));

        public int Size
        {
            get { return (int)GetValue(SizeProperty); }
            set{ SetValue(SizeProperty, value); }
        }

        #endregion

        #region BackgroundColor property

        public static readonly DependencyProperty BackgroundColorProperty = 
            DependencyProperty.Register("BackgroundColor", typeof(Color), typeof(LocalGameTile), new PropertyMetadata(Colors.Purple));

        public Color BackgroundColor
        {
            get { return (Color)GetValue(BackgroundColorProperty); }
            set { SetValue(BackgroundColorProperty, value); }
        }

        #endregion

        public LocalGameTile()
        {
            DefaultStyleKey = typeof(LocalGameTile);
        }

        #region Selected Property

        public static readonly DependencyProperty SelectedProperty =
         DependencyProperty.Register("Selected", typeof(bool), typeof(LocalGameTile), new PropertyMetadata(false));

        public bool Selected
        {
            get { return (bool)GetValue(SelectedProperty); }
            set { SetValue(SelectedProperty, value); }
        }

        #endregion

    }
}
