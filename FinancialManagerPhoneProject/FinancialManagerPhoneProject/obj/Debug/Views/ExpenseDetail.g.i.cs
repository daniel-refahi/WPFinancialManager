﻿#pragma checksum "C:\Users\Abtin Refahi\Desktop\WPFinancialManager\FinancialManagerPhoneProject\FinancialManagerPhoneProject\Views\ExpenseDetail.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "9BB2A9EEEF8304E361C7E7A3C582CD0E"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.33440
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.Phone.Controls;
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


namespace FinancialManagerPhoneProject.Views {
    
    
    public partial class ExpenseDetail : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.ProgressBar @__LoadingBar;
        
        internal System.Windows.Controls.TextBlock @__tbTitle;
        
        internal System.Windows.Controls.TextBox @__tbAmount;
        
        internal System.Windows.Controls.TextBox @__tbDescription;
        
        internal Microsoft.Phone.Controls.DatePicker @__dpDatepicker;
        
        internal System.Windows.Controls.Button @__btReceiptPic;
        
        internal System.Windows.Controls.Button @__btLocation;
        
        internal System.Windows.Controls.TextBlock @__LocationTx;
        
        internal Progress @__LocationLoadingBar;
        
        internal System.Windows.Controls.ListBox @__liCategoryList;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/FinancialManagerPhoneProject;component/Views/ExpenseDetail.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.@__LoadingBar = ((System.Windows.Controls.ProgressBar)(this.FindName("__LoadingBar")));
            this.@__tbTitle = ((System.Windows.Controls.TextBlock)(this.FindName("__tbTitle")));
            this.@__tbAmount = ((System.Windows.Controls.TextBox)(this.FindName("__tbAmount")));
            this.@__tbDescription = ((System.Windows.Controls.TextBox)(this.FindName("__tbDescription")));
            this.@__dpDatepicker = ((Microsoft.Phone.Controls.DatePicker)(this.FindName("__dpDatepicker")));
            this.@__btReceiptPic = ((System.Windows.Controls.Button)(this.FindName("__btReceiptPic")));
            this.@__btLocation = ((System.Windows.Controls.Button)(this.FindName("__btLocation")));
            this.@__LocationTx = ((System.Windows.Controls.TextBlock)(this.FindName("__LocationTx")));
            this.@__LocationLoadingBar = ((Progress)(this.FindName("__LocationLoadingBar")));
            this.@__liCategoryList = ((System.Windows.Controls.ListBox)(this.FindName("__liCategoryList")));
        }
    }
}

