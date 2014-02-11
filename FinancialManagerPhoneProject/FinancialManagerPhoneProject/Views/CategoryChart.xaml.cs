﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using FinancialManagerPhoneProject.DataHandlers;
using FinancialManagerPhoneProject.Models;

namespace FinancialManagerPhoneProject.Views
{
    public partial class CategoryChart : PhoneApplicationPage
    {
        string _CategoryName;

        public CategoryChart()
        {
            InitializeComponent();
            this.Loaded += CategoryChart_Loaded;
        }

        void CategoryChart_Loaded(object sender, RoutedEventArgs e)
        {
            Category category = StaticValues.DB.GetCategoryObject(_CategoryName);
            CategoryChartModel model = new CategoryChartModel();
            model.CategoryName = _CategoryName;
            model.Icon = "../Assets/Icons/" + category.Icon + ".png";
            model.TotalCategoryText = StaticValues.DB.GetCurrencySymbol() + category.TotalExpenses;
            model.TotalCategory = category.TotalExpenses;
            model.TotalAll = StaticValues.DB.GetTotalExpenses() - category.TotalExpenses;
            model.ScreenWidth = XMLHandler.DEIVCE_WIDTH - 40;
            List<Expense> expenses = StaticValues.DB.GetAllExpenses(_CategoryName);
            
            foreach (Expense expense in expenses)
            {
                CategoryChartExpenseModel expenseModel = new CategoryChartExpenseModel();
                expenseModel.Amount = StaticValues.DB.GetCurrencySymbol() + expense.Value.ToString();
                expenseModel.Description = expense.Description;
                expenseModel.Date = expense.Date.ToString("MMM") + "/" + expense.Date.Day;
                model.Expenses.Add(expenseModel);
            }

            this.DataContext = model;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            NavigationContext.QueryString.TryGetValue("categoryname", out _CategoryName);
        }

        private void ApplicationBarEditIcon_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Views/CategoryDetail.xaml?caller=categorychart&category="+ _CategoryName, UriKind.Relative));
        }

        void deleteIcon_Click(object sender, EventArgs e)
        {
            //MessageBoxResult result = MessageBox.Show("Would you like to delete the category? Be aware that all expenses in this category will be removed as well!",
            //                                                "Delete", MessageBoxButton.OKCancel);

            //if (result == MessageBoxResult.OK)
            //{
            //    StaticValues.DB.DeleteCategory(_Name);
            //    NavigationService.Navigate(new Uri("/Views/MainWindow.xaml?caller=categorydetail", UriKind.Relative));
            //}
        }

        private void ApplicationBarHelpIcon_Click(object sender, EventArgs e)
        {
            //SaveState = false;
        }


    }
}