﻿#pragma checksum "E:\projects\Windows Phone\CrazyParty\CrazyParty\BinSrc\Pages\RegisterPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "CA351F218FC797C694F1E62CD1C8C855"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.269
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace CrazyParty.BinSrc.Pages {
    
    
    public partial class RegisterPage : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.StackPanel TitlePanel;
        
        internal System.Windows.Controls.TextBlock ApplicationTitle;
        
        internal System.Windows.Controls.TextBlock PageTitle;
        
        internal System.Windows.Controls.Grid ContentPanel;
        
        internal System.Windows.Controls.Grid registerGrid;
        
        internal System.Windows.Controls.TextBox registerName;
        
        internal System.Windows.Controls.TextBlock registerNameError;
        
        internal System.Windows.Controls.PasswordBox registerPassword;
        
        internal System.Windows.Controls.TextBlock registerPasswordError;
        
        internal System.Windows.Controls.PasswordBox registerPassword1;
        
        internal System.Windows.Controls.TextBlock registerPassword1Error;
        
        internal Microsoft.Phone.Shell.ApplicationBarIconButton registerSubmit;
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Windows.Application.LoadComponent(this, new System.Uri("/CrazyParty;component/BinSrc/Pages/RegisterPage.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.TitlePanel = ((System.Windows.Controls.StackPanel)(this.FindName("TitlePanel")));
            this.ApplicationTitle = ((System.Windows.Controls.TextBlock)(this.FindName("ApplicationTitle")));
            this.PageTitle = ((System.Windows.Controls.TextBlock)(this.FindName("PageTitle")));
            this.ContentPanel = ((System.Windows.Controls.Grid)(this.FindName("ContentPanel")));
            this.registerGrid = ((System.Windows.Controls.Grid)(this.FindName("registerGrid")));
            this.registerName = ((System.Windows.Controls.TextBox)(this.FindName("registerName")));
            this.registerNameError = ((System.Windows.Controls.TextBlock)(this.FindName("registerNameError")));
            this.registerPassword = ((System.Windows.Controls.PasswordBox)(this.FindName("registerPassword")));
            this.registerPasswordError = ((System.Windows.Controls.TextBlock)(this.FindName("registerPasswordError")));
            this.registerPassword1 = ((System.Windows.Controls.PasswordBox)(this.FindName("registerPassword1")));
            this.registerPassword1Error = ((System.Windows.Controls.TextBlock)(this.FindName("registerPassword1Error")));
            this.registerSubmit = ((Microsoft.Phone.Shell.ApplicationBarIconButton)(this.FindName("registerSubmit")));
        }
    }
}

