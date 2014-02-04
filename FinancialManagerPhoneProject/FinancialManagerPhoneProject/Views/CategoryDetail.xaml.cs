using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using FinancialManagerPhoneProject.Models;
using FinancialManagerPhoneProject.DataHandlers;

namespace FinancialManagerPhoneProject.Views
{
    public partial class CategoryDetail : PhoneApplicationPage
    {

        string _Name;
        string _Status;
        CategoryDetailViewModel _PageModel;

        public CategoryDetail()
        {
            InitializeComponent();
            this.Loaded += CategoryDetail_Loaded;
        }

        void CategoryDetail_Loaded(object sender, RoutedEventArgs e)
        {
            
            if (_Status == "update")
            {
                
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            NavigationContext.QueryString.TryGetValue("status", out _Status);
            NavigationContext.QueryString.TryGetValue("name", out _Name);

            _PageModel = new CategoryDetailViewModel();
            _PageModel.Income = StaticValues.DB.GetCurrencySymbol() + " " + StaticValues.DB.GetIncome().ToString();
            _PageModel.Name = string.Empty;
            _PageModel.Icon = "../Assets/Icons/saving.png";
            _PageModel.TotalPlanned = StaticValues.DB.GetCurrencySymbol() +" "+ StaticValues.DB.GetTotalPlan().ToString();

            if (_Status == "update")
            {
                //if (ApplicationBar.Buttons.Count == 2)
                //{
                //    Uri uri = new Uri("//Image/delete.png", UriKind.Relative);
                //    ApplicationBarIconButton deleteIcon = new ApplicationBarIconButton() { Text = "Delete", IconUri = uri };
                //    deleteIcon.Click += deleteIcon_Click;
                //    ApplicationBar.Buttons.Insert(1, deleteIcon);
                //}

                //selectedCategory = StaticValues.DB.GetCategoryName(_ID);
            }

            this.DataContext = _PageModel;
        }

        private void ApplicationBarSaveIcon_Click(object sender, EventArgs e)
        {
            
        }

        private void ApplicationBarHelpIcon_Click(object sender, EventArgs e)
        {

        }

        private void __tbPlan_TextChanged(object sender, TextChangedEventArgs e)
        {
            double plan = 0;
            bool isNum = double.TryParse(__tbPlan.Text.ToString(), out plan);
            if (__tbPlan.Text == "")
            {
                isNum = true;
                plan = 0;
            }
            if (!isNum)
            {
                MessageBox.Show("Please Enter a Valid Amount!");
            }
            else
            {
                _PageModel.TotalPlanned = StaticValues.DB.GetCurrencySymbol() + " " + (plan + StaticValues.DB.GetTotalPlan()).ToString();
            }
        }
    }
}