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
using System.Windows.Media.Imaging;
using System.IO.IsolatedStorage;

namespace FinancialManagerPhoneProject.Views
{
    public partial class CategoryDetail : PhoneApplicationPage
    {

        string _Name;
        string _Status;
        bool SaveState = true;
        CategoryDetailViewModel _PageModel;

        public CategoryDetail()
        {
            InitializeComponent();
            this.Loaded += CategoryDetail_Loaded;
            this.BackKeyPress += CategoryDetail_BackKeyPress;
        }

        void CategoryDetail_BackKeyPress(object sender, System.ComponentModel.CancelEventArgs e)
        {
            SaveState = false;
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

            #region getting application status

            bool IsSettingExist = false;
            string s_name = string.Empty;
            string s_plan = string.Empty;
            string iconSource = string.Empty;

            if (IsolatedStorageSettings.ApplicationSettings.Contains("name"))
            {
                IsSettingExist = true;
                IsolatedStorageSettings.ApplicationSettings.TryGetValue("name", out s_name);
                IsolatedStorageSettings.ApplicationSettings.TryGetValue("plan", out s_plan);
                NavigationContext.QueryString.TryGetValue("source", out iconSource);
            }

            #endregion

            NavigationContext.QueryString.TryGetValue("status", out _Status);
            NavigationContext.QueryString.TryGetValue("name", out _Name);

            _PageModel = new CategoryDetailViewModel();
            _PageModel.Income = StaticValues.DB.GetCurrencySymbol() + " " + StaticValues.DB.GetIncome().ToString();
            if (IsSettingExist)
            {
                _PageModel.Icon = iconSource;
                _PageModel.Name = s_name;
                __tbPlan.Text = s_plan;
            }
            else
            {
                _PageModel.Name = string.Empty;
                _PageModel.Icon = "../Assets/Icons/saving.png";
            }
            
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

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            if (SaveState)
            {
                IsolatedStorageSettings.ApplicationSettings.Clear();
                IsolatedStorageSettings.ApplicationSettings["name"] = __tbName.Text.ToString();
                IsolatedStorageSettings.ApplicationSettings["plan"] = __tbPlan.Text.ToString();
                IsolatedStorageSettings.ApplicationSettings.Save();
            }
            else
                IsolatedStorageSettings.ApplicationSettings.Clear();
        }

        private void ApplicationBarSaveIcon_Click(object sender, EventArgs e)
        {
            double plan = 0;
            bool isNum = double.TryParse(__tbPlan.Text.ToString(), out plan);
            if (!isNum)
                MessageBox.Show("Please Enter a Valid Plan!");
            else
            {
                string icon = ((BitmapImage)(__Icon.Source)).UriSource.ToString();
                icon = icon.Substring(icon.LastIndexOf('/') + 1, icon.LastIndexOf('.') - icon.LastIndexOf('/'));
                if (StaticValues.DB.AddCategory(new Category()
                {
                    Icon = icon,
                    Name = __tbName.Name,
                    Plan = Convert.ToDouble(__tbPlan.Text.ToString()),
                    TotalExpenses = 0
                }))
                {
                    SaveState = false;                    
                    NavigationService.Navigate(new Uri("/Views/MainWindow.xaml", UriKind.Relative));
                }
                else
                    MessageBox.Show("The Category Name Already Exists!");

            }
            
        }

        private void ApplicationBarHelpIcon_Click(object sender, EventArgs e)
        {
            SaveState = false;
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

        private void Icon_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Views/CategoryIconSelection.xaml?source=" + 
                                                ((BitmapImage)((Image)sender).Source).UriSource.ToString(), UriKind.Relative));
        }
    }
}