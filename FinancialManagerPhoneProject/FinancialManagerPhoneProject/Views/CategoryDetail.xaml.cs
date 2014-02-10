﻿using System;
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
        string _IconSource;
        string _Plan;
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

            string caller = string.Empty;
            NavigationContext.QueryString.TryGetValue("caller", out caller);
            if (caller == "mainwindow")
            {
                // navigating from main window
                NavigationContext.QueryString.TryGetValue("status", out _Status);
                
                switch (_Status)
                {
                    case "add":
                        _Plan = string.Empty;
                        _Name = string.Empty;
                        _IconSource = "../Assets/Icons/clothing.png";
                        break;
                    case "update":
                        __tbTitle.Text = "Edit Category";
                        NavigationContext.QueryString.TryGetValue("name", out _Name);
                        Category category = StaticValues.DB.GetCategoryObject(_Name);
                        _Plan = category.Plan.ToString();
                        _IconSource = "../Assets/Icons/" + category.Icon + ".png";

                        // adding delete icon
                        if (ApplicationBar.Buttons.Count == 2)
                        {
                            Uri uri = new Uri("//Image/delete.png", UriKind.Relative);
                            ApplicationBarIconButton deleteIcon = new ApplicationBarIconButton() { Text = "Delete", IconUri = uri };
                            deleteIcon.Click += deleteIcon_Click;
                            ApplicationBar.Buttons.Insert(1, deleteIcon);
                        }
                        break;
                }
            }
            else 
            {
                // navigating from icon selection page
                NavigationService.RemoveBackEntry();
                IsolatedStorageSettings.ApplicationSettings.TryGetValue("name", out _Name);
                IsolatedStorageSettings.ApplicationSettings.TryGetValue("plan", out _Plan);
                IsolatedStorageSettings.ApplicationSettings.TryGetValue("status", out _Status);
                NavigationContext.QueryString.TryGetValue("source", out _IconSource);
                IsolatedStorageSettings.ApplicationSettings.Clear();

            }

            _PageModel = new CategoryDetailViewModel();
            _PageModel.Name = _Name;
            _PageModel.Income = StaticValues.DB.GetCurrencySymbol() + " " + StaticValues.DB.GetIncome().ToString();
            _PageModel.Icon = _IconSource;
            _PageModel.Plan = (_Plan == string.Empty) ? 0 : Convert.ToDouble(_Plan);
            _PageModel.TotalPlanned = StaticValues.DB.GetCurrencySymbol() +" "+ StaticValues.DB.GetTotalPlan().ToString();

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
                IsolatedStorageSettings.ApplicationSettings["status"] = _Status;
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
                icon = icon.Substring(icon.LastIndexOf('/') + 1, icon.LastIndexOf('.') - icon.LastIndexOf('/') - 1);
                if (StaticValues.DB.AddCategory(new Category()
                {
                    Icon = icon,
                    Name = __tbName.Text.ToString(),
                    Plan = Convert.ToDouble(__tbPlan.Text.ToString()),
                    TotalExpenses = 0
                }))
                {
                    SaveState = false;                    
                    NavigationService.Navigate(new Uri("/Views/MainWindow.xaml?caller=categorydetail", UriKind.Relative));
                }
                else
                    MessageBox.Show("The Category Name Already Exists!");

            }
            
        }

        void deleteIcon_Click(object sender, EventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Would you like to delete the category? Be aware that all expenses in this category will be removed as well!",
                                                            "Delete", MessageBoxButton.OKCancel);

            if (result == MessageBoxResult.OK)
            {
                StaticValues.DB.DeleteCategory(_Name);
                NavigationService.Navigate(new Uri("/Views/MainWindow.xaml?caller=categorydetail", UriKind.Relative));
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
            NavigationService.Navigate(new Uri("/Views/CategoryIconSelection.xaml?caller=categorydetail&source=" + 
                                                ((BitmapImage)((Image)sender).Source).UriSource.ToString(), UriKind.Relative));
        }
    }
}