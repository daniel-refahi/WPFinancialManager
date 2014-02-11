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
using System.Threading.Tasks;
using FinancialManagerPhoneProject.Models;
using FinancialManagerPhoneProject.Views.UserControls;
using System.Threading;
using Windows.Storage;

namespace FinancialManagerPhoneProject.Views
{
    public partial class MainWindow : PhoneApplicationPage
    {
        
        private double _DeviceWidth;
        public MainWindow()
        {
            InitializeComponent();
        }

        void __MainPivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (((PivotItem)e.AddedItems[0]).Header.ToString())
            {
                case "Expenses":
                    StaticValues.AppStatus = StaticValues.AppStatusOptions.Expenses;
                    break;
                case "Categories":
                    StaticValues.AppStatus = StaticValues.AppStatusOptions.Categories;
                    break;
                case "Report":
                    StaticValues.AppStatus = StaticValues.AppStatusOptions.Report;
                    break;
            }
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            NavigationService.RemoveBackEntry();
            
            _DeviceWidth = Application.Current.Host.Content.ActualWidth;
            if (XMLHandler.FINANCIALMANAGER_XML == null)
            {
                await StaticValues.DB.LoadXmlFromFileAsync();
            }

            __LoadingBar.Opacity = 1;
            this.DataContext = await LoadPageModelAsync();
            __LoadingBar.Opacity = 0;

            string caller = string.Empty;
            NavigationContext.QueryString.TryGetValue("caller", out caller);
            if(caller == "categorydetail")
            {
                __MainPivot.SelectedIndex = 1;
                __liCategoriesList.ScrollIntoView(__liCategoriesList.Items[__liCategoriesList.Items.Count - 1]);
            }

        }

        private async Task<MainPageModel> LoadPageModelAsync()
        {
            Task<MainPageModel> loadTask =
                 Task.Factory.StartNew(() =>
                 {
                     MainPageModel mainPageModel = new MainPageModel();
                     string symbol = StaticValues.DB.GetCurrencySymbol();
                     foreach (Expense expense in StaticValues.DB.GetAllExpenses())
                     {
                         mainPageModel.ExpenseListModel.Add(new ExpenseItemModel()
                                                         {
                                                             Amount = symbol +" "+ expense.Value,
                                                             ID = expense.ID,
                                                             Category = expense.Category,
                                                             Date = expense.Date.ToString("dd/MMM"),
                                                             Description = expense.Description,
                                                             ImageSource = "../../Assets/Icons/" + expense.Icon+".png",
                                                             ScreenWidth = XMLHandler.DEIVCE_WIDTH - 40
                                                         });
                         
                     }

                     foreach (Category category in StaticValues.DB.GetAllCategories())
                     {
                         mainPageModel.CategoryListModel.Add(new CategoryItemModel()
                         {
                             Name = category.Name,
                             Plan = symbol + " " + category.Plan,
                             Remaining = symbol + " " + (category.Plan - category.TotalExpenses),
                             Total = symbol + " " + category.TotalExpenses,
                             ImageSource = "../../Assets/Icons/" + category.Icon + ".png",
                             ScreenWidth = XMLHandler.DEIVCE_WIDTH - 40
                         });

                     }

                     double income = StaticValues.DB.GetIncome();
                     double totalExpense = StaticValues.DB.GetTotalExpenses();
                     double balance = income - totalExpense;
                     mainPageModel.Income = symbol + " " + income;
                     mainPageModel.Balance = symbol + " " + balance;
                     return mainPageModel;
                 });
            return await loadTask;
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void ApplicationBarAddIcon_Click(object sender, EventArgs e)
        {
            if (StaticValues.AppStatus == StaticValues.AppStatusOptions.Expenses)
            {
                NavigationService.Navigate(new Uri("/Views/ExpenseDetail.xaml?status=add&caller=mainwindow", UriKind.Relative));
            }
            if (StaticValues.AppStatus == StaticValues.AppStatusOptions.Categories)
            {
                NavigationService.Navigate(new Uri("/Views/CategoryDetail.xaml?status=add&caller=mainwindow", UriKind.Relative));
            }
        }

        private void ApplicationBarHelpIcon_Click(object sender, EventArgs e)
        {

        }

        private void __liExpensesList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(((ListBox)sender).SelectedItem != null)
                NavigationService.Navigate(
                    new Uri("/Views/ExpenseDetail.xaml?status=update&caller=mainwindow&ID=" + 
                                ((ExpenseItemModel)((ListBox)sender).SelectedItem).ID,
                                                                        UriKind.Relative));
        }

        private void __liCategoriesList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (((ListBox)sender).SelectedItem != null)
                NavigationService.Navigate(
                    new Uri("/Views/CategoryChart.xaml?categoryname=" +
                                ((CategoryItemModel)((ListBox)sender).SelectedItem).Name,
                                                                        UriKind.Relative));
        }
    }
}