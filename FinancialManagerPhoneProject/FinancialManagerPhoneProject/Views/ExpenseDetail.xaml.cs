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
using System.Threading.Tasks;
using System.Threading;

namespace FinancialManagerPhoneProject.Views
{
    public partial class ExpenseDetail : PhoneApplicationPage
    {
        string _ID;
        string _Status;
        public ExpenseDetail()
        {
            InitializeComponent();
            this.Loaded += ExpenseDetail_Loaded;
        }

        void ExpenseDetail_Loaded(object sender, RoutedEventArgs e)
        {
            if (_Status == "update")
            {
                __liCategoryList.IsEnabled = false;
                foreach (Category category in __liCategoryList.Items)
                {
                    if (category.IsSelected)
                    {
                        __liCategoryList.SelectedItem = category;
                    }
                }
                __liCategoryList.ScrollIntoView(__liCategoryList.SelectedItem);
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            NavigationContext.QueryString.TryGetValue("status", out _Status);
            NavigationContext.QueryString.TryGetValue("ID", out _ID);

            ExpenseDetailViewModel pageModel = new ExpenseDetailViewModel();
            List<Category> categories = StaticValues.DB.GetAllCategories();
            string selectedCategory = string.Empty;

            if (_Status == "update")
            {
                if (ApplicationBar.Buttons.Count == 2)
                {
                    Uri uri = new Uri("//Image/delete.png", UriKind.Relative);
                    ApplicationBarIconButton deleteIcon = new ApplicationBarIconButton() { Text = "Delete", IconUri = uri };
                    deleteIcon.Click += deleteIcon_Click;
                    ApplicationBar.Buttons.Insert(1, deleteIcon);
                }
            
                selectedCategory = StaticValues.DB.GetCategoryName(_ID);
            }

            foreach (Category category in categories)
            {
                category.Icon = "../Assets/Icons/" + category.Icon + ".png";
                category.IsSelected = false;
                if (_Status == "update")
                {
                    if (selectedCategory == category.Name)
                        category.IsSelected = true;
                }
            }

            pageModel.Categories = categories;
            pageModel.ScreenWidth = XMLHandler.DEIVCE_WIDTH - 40;
            if (_Status == "update")
            {
                Expense expense = StaticValues.DB.GetExpense(_ID);
                pageModel.Amount = expense.Value;
                pageModel.Date = expense.Date;
                pageModel.Description = expense.Description;
                pageModel.ID = _ID;
                __tbTitle.Text = "Edit Expense";
            }
            else
            {
                pageModel.Date = DateTime.Today;
                pageModel.Description = string.Empty;
            }
            this.DataContext = pageModel;
        }

        void deleteIcon_Click(object sender, EventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Would you like to delete the expense?",
                                                            "Delete", MessageBoxButton.OKCancel);

            if (result == MessageBoxResult.OK)
            {
                StaticValues.DB.DeleteExpense(_ID);
                NavigationService.GoBack();
            }
        }

        private void DatePicker_ValueChanged(object sender, DateTimeValueChangedEventArgs e)
        {

        }

        private void TimePicker_ValueChanged(object sender, DateTimeValueChangedEventArgs e)
        {

        }

        private void ApplicationBarSaveIcon_Click(object sender, EventArgs e)
        {
            double amount = 0;
            bool isNum = double.TryParse(__tbAmount.Text.ToString(), out amount);
            if (!isNum)
                MessageBox.Show("Please Enter a Valid Amount!");
            else if (__dpDatepicker.Value == null)
                MessageBox.Show("Please Select Date!");
            else if (__tbAmount.Text.ToString() == null || amount <= 0 )
                MessageBox.Show("Please Enter a Valid Amount!");
            else if (__liCategoryList.SelectedItem == null)
                MessageBox.Show("Please Select a Category!");
            else
            {
                if (_Status == "update")
                {
                    StaticValues.DB.UpdateExpense(new Expense()
                    {
                        ID = _ID,
                        Category = ((Category)__liCategoryList.SelectedItem).Name,
                        Date = (DateTime)__dpDatepicker.Value,
                        Description = __tbDescription.Text.ToString(),
                        Value = amount
                    });
                }
                else
                {
                    StaticValues.DB.AddExpense(new Expense()
                    {
                        Category = ((Category)__liCategoryList.SelectedItem).Name,
                        Date = (DateTime)__dpDatepicker.Value,
                        Description = __tbDescription.Text.ToString(),
                        Value = amount
                    });
                }
                NavigationService.GoBack();
            }
        }

        private void ApplicationBarHelpIcon_Click(object sender, EventArgs e)
        {

        }

        private void __liCategoryList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int a = 4;
        }
    }
}