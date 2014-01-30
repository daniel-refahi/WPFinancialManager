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
        ExpenseDetailModel _ExpenseDetailModel;
        string _ID;
        string _Status;
        public ExpenseDetail()
        {
            InitializeComponent();
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            NavigationContext.QueryString.TryGetValue("status", out _Status);
            NavigationContext.QueryString.TryGetValue("ID", out _ID);
            if (_Status == "update")
            {
                Uri uri = new Uri("//Image/delete.png",UriKind.Relative);
                ApplicationBarIconButton deleteIcon = new ApplicationBarIconButton() { Text = "Delete", IconUri = uri };
                deleteIcon.Click += deleteIcon_Click;
                ApplicationBar.Buttons.Insert(1,deleteIcon);
            }
            __LoadingBar.Opacity = 1;
            _ExpenseDetailModel = await LoadPageModelAsync();
            this.DataContext = _ExpenseDetailModel;
            __LoadingBar.Opacity = 0;

        }

        void deleteIcon_Click(object sender, EventArgs e)
        {
            
        }

        private async Task<ExpenseDetailModel> LoadPageModelAsync()
        {
            Task<ExpenseDetailModel> loadTask =
                 Task.Factory.StartNew(() =>
                 {
                     ExpenseDetailModel pageModel = new ExpenseDetailModel();
                     List<Category> tempCategories = StaticValues.DB.GetAllCategories();
                     foreach (Category category in tempCategories)
                     {
                         category.Icon = "../Assets/Icons/"+ category.Icon + ".png";
                     }
                     pageModel.Categories = tempCategories; 
                     pageModel.ScreenWidth = XMLHandler.DEIVCE_WIDTH - 40;
                     if (_Status == "Update")
                     {
                         Expense expense = StaticValues.DB.GetExpense(_ID);
                         pageModel.Amount = expense.Value;
                         pageModel.Date = expense.Date;
                         pageModel.Description = expense.Description;
                         pageModel.ID = _ID;
                         //pageModel.Categories = expense.Category;
                     }
                     return pageModel;
                 });
            return await loadTask;
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
                MessageBox.Show("Please Enter an Acceptable Amount!");
            else if (__dpDatepicker.Value == null)
                MessageBox.Show("Please Select Date!");
            else if (__tbAmount.Text.ToString() == null || amount <= 0 )
                MessageBox.Show("Please Enter an Acceptable Amount!");
            else if (__liCategoryList.SelectedItem == null)
                MessageBox.Show("Please Select a Category!");
            else
            {
                StaticValues.DB.AddExpense(new Expense()
                {
                    Category = ((Category)__liCategoryList.SelectedItem).Name,
                    Date = (DateTime)__dpDatepicker.Value,
                    Description = __tbDescription.Text.ToString(),
                    Value = amount
                });
                NavigationService.GoBack();
            }
        }

        private void ApplicationBarHelpIcon_Click(object sender, EventArgs e)
        {

        }

        private void __liCategoryList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }
    }
}