﻿#pragma checksum "C:\Users\abtin refahi\Documents\GitHub\WPFinancialManager\FinancialManagerPhoneProject\FinancialManagerPhoneProject\Views\MainWindow.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "A9A025F6D7F0BF986ACB1139D74AEC9D"
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
    
    
    public partial class MainWindow : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.ProgressBar @__LoadingBar;
        
        internal Microsoft.Phone.Controls.Pivot @__MainPivot;
        
        internal System.Windows.Controls.Grid @__GrExpenses;
        
        internal System.Windows.Controls.TextBlock @__tbIncome;
        
        internal System.Windows.Controls.ListBox @__liExpensesList;
        
        internal System.Windows.Controls.TextBlock @__tbBalance;
        
        internal System.Windows.Controls.ListBox @__liCategoriesList;
        
        internal System.Windows.Controls.Grid @__GrReport;
        
        internal System.Windows.Controls.Button @__btReportDetail;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/FinancialManagerPhoneProject;component/Views/MainWindow.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.@__LoadingBar = ((System.Windows.Controls.ProgressBar)(this.FindName("__LoadingBar")));
            this.@__MainPivot = ((Microsoft.Phone.Controls.Pivot)(this.FindName("__MainPivot")));
            this.@__GrExpenses = ((System.Windows.Controls.Grid)(this.FindName("__GrExpenses")));
            this.@__tbIncome = ((System.Windows.Controls.TextBlock)(this.FindName("__tbIncome")));
            this.@__liExpensesList = ((System.Windows.Controls.ListBox)(this.FindName("__liExpensesList")));
            this.@__tbBalance = ((System.Windows.Controls.TextBlock)(this.FindName("__tbBalance")));
            this.@__liCategoriesList = ((System.Windows.Controls.ListBox)(this.FindName("__liCategoriesList")));
            this.@__GrReport = ((System.Windows.Controls.Grid)(this.FindName("__GrReport")));
            this.@__btReportDetail = ((System.Windows.Controls.Button)(this.FindName("__btReportDetail")));
        }
    }
}

