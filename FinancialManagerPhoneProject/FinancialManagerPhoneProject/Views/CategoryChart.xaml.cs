using System;
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
using Microsoft.Phone.Tasks;

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

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
        }

        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            //base.OnBackKeyPress(e);
            NavigationService.Navigate(new Uri("/Views/MainWindow.xaml?caller=categorychart&categoryname=" + _CategoryName, UriKind.Relative));
        }

        private void ApplicationBarEditIcon_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Views/CategoryDetail.xaml?caller=categorychart&categoryname="+ _CategoryName, UriKind.Relative));
        }
        private void ApplicationBarHelpIcon_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Views/Help.xaml?caller=categorychart&object="+_CategoryName, UriKind.Relative));
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


    }
}