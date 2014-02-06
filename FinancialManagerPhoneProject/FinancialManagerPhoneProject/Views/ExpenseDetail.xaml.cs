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
using System.IO.IsolatedStorage;

namespace FinancialManagerPhoneProject.Views
{
    public partial class ExpenseDetail : PhoneApplicationPage
    {
        string _ID;
        string _Status;
        bool SaveState = true;
        public ExpenseDetail()
        {
            InitializeComponent();
            this.Loaded += ExpenseDetail_Loaded;
            this.BackKeyPress += ExpenseDetail_BackKeyPress;
        }

        void ExpenseDetail_BackKeyPress(object sender, System.ComponentModel.CancelEventArgs e)
        {
            SaveState = false;
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

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);

            if (SaveState)
            {
                IsolatedStorageSettings.ApplicationSettings.Clear();
                IsolatedStorageSettings.ApplicationSettings["amount"] = __tbAmount.Text.ToString();
                IsolatedStorageSettings.ApplicationSettings["description"] = __tbDescription.Text.ToString();
                IsolatedStorageSettings.ApplicationSettings["date"] = __dpDatepicker.Value.ToString();
                IsolatedStorageSettings.ApplicationSettings["category"] = ((Category)__liCategoryList.SelectedItem).Name;
                IsolatedStorageSettings.ApplicationSettings.Save();
            }
            else
                IsolatedStorageSettings.ApplicationSettings.Clear();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            #region getting application status

            bool IsSettingExist = false;
            string s_amount = string.Empty;
            string s_description = string.Empty;
            string s_date = string.Empty;
            string s_category = string.Empty;

            if (IsolatedStorageSettings.ApplicationSettings.Contains("amount"))
            {
                IsSettingExist = true;
                IsolatedStorageSettings.ApplicationSettings.TryGetValue("amount", out s_amount);
                IsolatedStorageSettings.ApplicationSettings.TryGetValue("description", out s_description);
                IsolatedStorageSettings.ApplicationSettings.TryGetValue("date", out s_date);
                IsolatedStorageSettings.ApplicationSettings.TryGetValue("category", out s_category);
            }

            #endregion

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

                if (IsSettingExist)
                    selectedCategory = s_category;
                else 
                    selectedCategory = StaticValues.DB.GetCategoryName(_ID);
            }

            foreach (Category c in categories)
            {
                c.Icon = "../Assets/Icons/" + c.Icon + ".png";
                c.IsSelected = false;
                if (s_category == c.Name)
                    c.IsSelected = true;
                else if (_Status == "update")
                {
                    if (selectedCategory == c.Name)
                        c.IsSelected = true;
                }
            }

            pageModel.Categories = categories;
            pageModel.ScreenWidth = XMLHandler.DEIVCE_WIDTH - 40;
            if (_Status == "update")
            {
                Expense expense = StaticValues.DB.GetExpense(_ID);
                if (IsSettingExist)
                {
                    pageModel.Amount = Convert.ToDouble(s_amount);
                    pageModel.Date = Convert.ToDateTime(s_date);
                    pageModel.Description = s_description;
                }
                else 
                {
                    pageModel.Amount = expense.Value;
                    pageModel.Date = expense.Date;
                    pageModel.Description = expense.Description;
                }
                pageModel.ID = _ID;
                __tbTitle.Text = "Edit Expense";
            }
            else
            {
                if (IsSettingExist)
                {
                    pageModel.Amount = Convert.ToDouble(s_amount);
                    pageModel.Date = Convert.ToDateTime(s_date);
                    pageModel.Description = s_description;
                }
                else {
                pageModel.Date = DateTime.Today;
                pageModel.Description = string.Empty;
                    }
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
                SaveState = false;
                NavigationService.Navigate(new Uri("/Views/MainWindow.xaml", UriKind.Relative));
            }
        }

        private void ApplicationBarHelpIcon_Click(object sender, EventArgs e)
        {
            SaveState = false;
        }

        private void __liCategoryList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

    }
}