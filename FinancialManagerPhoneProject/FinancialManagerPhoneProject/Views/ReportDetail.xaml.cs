﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Threading.Tasks;
using FinancialManagerPhoneProject.Models;
using FinancialManagerPhoneProject.DataHandlers;
using FinancialManagerPhoneProject.Views.UserControls;
using Microsoft.Phone.Tasks;

namespace FinancialManagerPhoneProject.Views
{
    public partial class ReportDetail : PhoneApplicationPage
    {
        List<ReportDetailItemModel> ReportDetails;
        private double _AppWidth;
        public ReportDetail()
        {
            InitializeComponent();
            this.Loaded += ReportDetail_Loaded;
            ReportDetails = new List<ReportDetailItemModel>();
        }

        void ReportDetail_Loaded(object sender, RoutedEventArgs e)
        {
            __LoadingBar.Opacity = 1;
            _AppWidth = Application.Current.Host.Content.ActualWidth / 2 - 40;
            Task t_LoadPageModel = Task.Factory.StartNew(() =>
            {
                double totalExpenses = StaticValues.DB.GetTotalExpenses();
                foreach (Category item in StaticValues.DB.GetAllCategories())
                {
                    ReportDetailItemModel itemModel = new ReportDetailItemModel();
                    itemModel.CategoryName = item.Name;
                    itemModel.ImageSource = "../../Assets/Icons/" + item.Icon + ".png";
                    itemModel.TotalExpenses = totalExpenses;

                    double plan = item.Plan;
                    double categoryExpenses = item.TotalExpenses;
                    itemModel.Plan = plan;
                    if (plan <= categoryExpenses)
                        itemModel.GaugeCategoryExpenses = plan;
                    else
                        itemModel.GaugeCategoryExpenses = categoryExpenses;
                    
                    itemModel.CategoryExpenses= item.TotalExpenses;
                    itemModel.ScreenWidth = _AppWidth;

                    ReportDetails.Add(itemModel);
                }

            });

            Task UpdateUI = t_LoadPageModel.ContinueWith((t) =>
            {
                foreach (ReportDetailItemModel report in ReportDetails)
                {
                    ReportDetaiItem item = new ReportDetaiItem();
                    item.DataContext = report;
                    item.Margin = new Thickness(10);
                    __wpReportList.Children.Add(item);
                    
                }
                //this.DataContext = _PageModel;
                __LoadingBar.Opacity = 0;
                
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
        }

        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            //base.OnBackKeyPress(e);
            NavigationService.Navigate(new Uri("/Views/MainWindow.xaml?caller=report", UriKind.Relative));
            NavigationService.RemoveBackEntry();
        }

        private void __liReportList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ApplicationBarMenuItem_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Views/Settings.xaml?", UriKind.Relative));
        }
        private void ApplicationBarFeedback_Click(object sender, EventArgs e)
        {
            EmailComposeTask emailComposeTask = new EmailComposeTask();

            emailComposeTask.Subject = "Report bug & Suggestion";
            emailComposeTask.Body = "";
            emailComposeTask.To = "financialmanager.pro@outlook.com";

            emailComposeTask.Show();
        }
        private void ApplicationBarHelpIcon_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Views/help.xaml?caller=reportdetail&object=" + string.Empty, UriKind.Relative));
        }
    }

   
}