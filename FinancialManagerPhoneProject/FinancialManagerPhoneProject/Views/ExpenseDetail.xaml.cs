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
        string _Amount = string.Empty;
        string _Description = string.Empty;
        string _Date = string.Empty;
        string _Category = string.Empty;
        bool SaveState = true;

        // there is a bug on datepicker that it doesn't update the Uri. this is to overcome that issue. 
        public static bool IsFromDatePicker = false;

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
                HighlightCategory();
            }
            else if (ExpenseDetail.IsFromDatePicker)
            {
                HighlightCategory();
                ExpenseDetail.IsFromDatePicker = false;
            }
        }

        private void HighlightCategory()
        {
            foreach (Category category in __liCategoryList.Items)
            {
                if (category.IsSelected)
                {
                    __liCategoryList.SelectedItem = category;
                }
            }
            __liCategoryList.ScrollIntoView(__liCategoryList.SelectedItem);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);

            if (e.Uri.ToString().Contains("DatePickerPage"))
                ExpenseDetail.IsFromDatePicker = true;
            else
                NavigationService.RemoveBackEntry();

            if (SaveState)
            {
                IsolatedStorageSettings.ApplicationSettings.Clear();
                IsolatedStorageSettings.ApplicationSettings["amount"] = __tbAmount.Text.ToString();
                IsolatedStorageSettings.ApplicationSettings["description"] = __tbDescription.Text.ToString();
                IsolatedStorageSettings.ApplicationSettings["date"] = __dpDatepicker.Value.ToString();
                if(__liCategoryList.SelectedItem != null)
                    IsolatedStorageSettings.ApplicationSettings["category"] = ((Category)__liCategoryList.SelectedItem).Name;
                IsolatedStorageSettings.ApplicationSettings.Save();
            }
            else
                IsolatedStorageSettings.ApplicationSettings.Clear();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            ExpenseDetailViewModel pageModel = new ExpenseDetailViewModel();
            List<Category> categories = StaticValues.DB.GetAllCategories();

            string caller = string.Empty;
            NavigationContext.QueryString.TryGetValue("caller", out caller);

            if (ExpenseDetail.IsFromDatePicker)
            {
                // navigating from date picker                
                IsolatedStorageSettings.ApplicationSettings.TryGetValue("amount", out _Amount);
                IsolatedStorageSettings.ApplicationSettings.TryGetValue("description", out _Description);
                IsolatedStorageSettings.ApplicationSettings.TryGetValue("date", out _Date);
                IsolatedStorageSettings.ApplicationSettings.TryGetValue("category", out _Category);
                
            }
            else if (caller == "mainwindow")
            {
                // navigating from main window
                NavigationContext.QueryString.TryGetValue("status", out _Status);

                switch (_Status)
                {
                    case "add":
                        _Amount = string.Empty;
                        _Description = string.Empty;
                        _Date = DateTime.Today.ToShortDateString();
                        _Category = string.Empty;
                        break;
                    case "update":
                        NavigationContext.QueryString.TryGetValue("ID", out _ID);
                        LoadPageForEditStatus();
                        break;
                }
            }
            else if (caller == "help")
            {
                // navigating from help page
                NavigationContext.QueryString.TryGetValue("object", out _ID);
                if (_ID != string.Empty)
                {
                    _Status = "update";
                    LoadPageForEditStatus();
                }
                else
                    _Status = "add";
                
            }

            foreach (Category c in categories)
            {
                c.Icon = "../Assets/Icons/" + c.Icon + ".png";
                c.IsSelected = false;
                if (_Category == c.Name)
                    c.IsSelected = true;
            }

            pageModel.Categories = categories;
            pageModel.ScreenWidth = XMLHandler.DEIVCE_WIDTH - 40;

            try
            {
                pageModel.Amount = Convert.ToDouble(_Amount);
            }
            catch 
            {
                pageModel.Amount = 0;
            }
            try
            {
                pageModel.Date = Convert.ToDateTime(_Date);
            }
            catch 
            {
                pageModel.Date = DateTime.Today;
            }
            pageModel.Description = _Description;
            pageModel.ID = _ID;

            this.DataContext = pageModel;
        }

        private void LoadPageForEditStatus()
        {
            __tbTitle.Text = "Edit Expense";
            

            Expense expense = StaticValues.DB.GetExpense(_ID);
            _Amount = expense.Value.ToString();
            _Description = expense.Description;
            _Date = expense.Date.ToShortDateString();

            _Category = StaticValues.DB.GetCategoryName(_ID);

            // adding delete icon
            if (ApplicationBar.Buttons.Count == 2)
            {
                Uri uri = new Uri("//Image/delete.png", UriKind.Relative);
                ApplicationBarIconButton deleteIcon = new ApplicationBarIconButton() { Text = "Delete", IconUri = uri };
                deleteIcon.Click += deleteIcon_Click;
                ApplicationBar.Buttons.Insert(1, deleteIcon);
            }
        }

        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            //base.OnBackKeyPress(e);
            ExpenseDetail.IsFromDatePicker = false;
            SaveState = false;
            NavigationService.Navigate(new Uri("/Views/MainWindow.xaml?caller=expensedetail", UriKind.Relative));
        }

        void deleteIcon_Click(object sender, EventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Would you like to delete the expense?",
                                                            "Delete", MessageBoxButton.OKCancel);

            if (result == MessageBoxResult.OK)
            {
                StaticValues.DB.DeleteExpense(_ID);
                SaveState = false;
                ExpenseDetail.IsFromDatePicker = false;
                NavigationService.Navigate(new Uri("/Views/MainWindow.xaml?caller=expensedetail", UriKind.Relative));
            }
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
                ExpenseDetail.IsFromDatePicker = false;
                NavigationService.Navigate(new Uri("/Views/MainWindow.xaml?caller=expensedetail", UriKind.Relative));
            }
        }

        private void ApplicationBarHelpIcon_Click(object sender, EventArgs e)
        {
            SaveState = false;
            ExpenseDetail.IsFromDatePicker = false;
            NavigationService.Navigate(new Uri("/Views/help.xaml?caller=expensedetail&object="+_ID, UriKind.Relative));
        }

        private void __liCategoryList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

    }
}