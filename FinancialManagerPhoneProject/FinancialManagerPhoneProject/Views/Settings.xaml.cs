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
using System.Windows.Media;
using System.Threading.Tasks;
using System.Windows.Media.Animation;

namespace FinancialManagerPhoneProject.Views
{
    public partial class Settings : PhoneApplicationPage
    {
        public Settings()
        {
            InitializeComponent();

            string currentYear = StaticValues.DB.GetCurrentYear().ToString();
            string currentMonth = StaticMethods.GetMonthSymbol(StaticValues.DB.GetCurrentMonth());

            __btYearPicker.SetValue(Microsoft.Phone.Controls.ListPicker.ItemCountThresholdProperty, 50);
            List<ListPickerItem> yearItems = new List<ListPickerItem>();
            for (int year = 2013; year <= DateTime.Today.Year; year++)
            {
                ListPickerItem item = new ListPickerItem();
                item.Background = new SolidColorBrush(Colors.White);
                Canvas.SetZIndex(item, 2);
                item.Content = year;
                yearItems.Add(item);
                __btYearPicker.Items.Add(item);    
            }
            __btYearPicker.SelectedItem = yearItems.Where(i => ((ListPickerItem)i).Content.ToString() == currentYear).FirstOrDefault();
            __liSymbols.SetValue(Microsoft.Phone.Controls.ListPicker.ItemCountThresholdProperty, 50);

            __btMonthPicker.SetValue(Microsoft.Phone.Controls.ListPicker.ItemCountThresholdProperty, 50);
            List<ListPickerItem> monthItems = new List<ListPickerItem>();
            for (int month = 1; month <= 12; month++)
            {
                ListPickerItem item = new ListPickerItem();
                item.Background = new SolidColorBrush(Colors.White);
                Canvas.SetZIndex(item, 2);
                switch (month)
                {
                    case 1:
                        item.Content = "Jan";
                        break;
                    case 2:               
                        item.Content = "Feb";
                        break;            
                    case 3:
                        item.Content = "Mar";
                        break;
                    case 4:               
                        item.Content = "Apr";
                        break;            
                    case 5:
                        item.Content = "May";
                        break;
                    case 6:               
                        item.Content = "Jun";
                        break;            
                    case 7:
                        item.Content = "Jul";
                        break;
                    case 8:               
                        item.Content = "Aug";
                        break;            
                    case 9:
                        item.Content = "Sep";
                        break;
                    case 10:
                        item.Content = "Oct";
                        break;
                    case 11:
                        item.Content = "Nov";
                        break;
                    case 12:
                        item.Content = "Dec";
                        break;
                }
                monthItems.Add(item);
                __btMonthPicker.Items.Add(item);
            }
            __btMonthPicker.SelectedItem = monthItems.Where(m => ((ListPickerItem)m).Content.ToString() == currentMonth).FirstOrDefault();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            switch (StaticValues.DB.GetCurrencySymbol())
            {
                case "$":
                    __liSymbols.SelectedIndex = 0;
                    break;
                case "€":
                    __liSymbols.SelectedIndex = 1;
                    break;
                case "&#x20b9;":
                    __liSymbols.SelectedIndex = 2;
                    break;
                case "RM":
                    __liSymbols.SelectedIndex = 3;
                    break;
                case "£":
                    __liSymbols.SelectedIndex = 4;
                    break;
            }


            ShellTile TileToFind = ShellTile.ActiveTiles.FirstOrDefault(x => x.NavigationUri.ToString().Contains("DefaultTitle=FinancialManagerFromTile"));
            __btCreateTile.Content = (TileToFind == null) ? "Create Live Tile" : "Remove Live Tile";
            //__txIncome.Text = StaticValues.DB.GetIncome().ToString("n0");
        }

        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Views/MainWindow.xaml?caller=settings", UriKind.Relative));
        }

        private void __btSave_Click(object sender, RoutedEventArgs e)
        {
            string newSymbol = string.Empty;
            switch (__liSymbols.SelectedIndex)
            {
                case 0:
                    newSymbol = "$";
                    break;
                case 1:
                    newSymbol = "€";
                    break;
                case 2:
                    newSymbol = "&#x20b9;";
                    break;
                case 3:
                    newSymbol = "RM";
                    break;
                case 4:
                    newSymbol = "£";
                    break;
                case 5:
                    newSymbol = "UGX";
                    break;
            }

            __LoadingLayer.Visibility = System.Windows.Visibility.Visible;
            string month = StaticMethods.GetMonthNumber(((ListPickerItem)__btMonthPicker.SelectedItem).Content.ToString());
            string year = ((ListPickerItem)__btYearPicker.SelectedItem).Content.ToString();

            Task t_LoadPageModel = Task.Factory.StartNew(() =>
            {
                StaticValues.DB.UpdateSettings(newSymbol, month, year);
            });

            Task UpdateUI = t_LoadPageModel.ContinueWith((t) =>
            {
                __LoadingLayer.Visibility = System.Windows.Visibility.Collapsed;
            }, TaskScheduler.FromCurrentSynchronizationContext());


            ShellTile TileToFind = ShellTile.ActiveTiles.FirstOrDefault(x => x.NavigationUri.ToString().Contains("DefaultTitle=FinancialManagerFromTile"));

            double expenses = StaticValues.DB.GetTotalExpenses();
            double remaining = StaticValues.DB.GetIncome() - expenses;
            string symbol = StaticValues.DB.GetCurrencySymbol();

            StandardTileData NewTileData = new StandardTileData
            {
                BackgroundImage = new Uri("Assets/150_150_Logo.png", UriKind.Relative),
                Title = "Financial Manager",
                BackContent = "Expenses:\n" + expenses.ToString("n0") + " " + symbol + "\nRemaining:\n" +
                              remaining.ToString("n0") + " " + symbol,
                BackBackgroundImage = new Uri("Black.jpg", UriKind.Relative)
            };

            if (TileToFind != null)
                TileToFind.Update(NewTileData);

            MessageBox.Show("Setting has been saved.");
        }

        private void __btCreateTile_Click(object sender, RoutedEventArgs e)
        {
            ShellTile TileToFind = ShellTile.ActiveTiles.FirstOrDefault(x => x.NavigationUri.ToString().Contains("DefaultTitle=FinancialManagerFromTile"));

            double expenses = StaticValues.DB.GetTotalExpenses();
            double remaining = StaticValues.DB.GetIncome() - expenses;
            string symbol = StaticValues.DB.GetCurrencySymbol();

            StandardTileData NewTileData = new StandardTileData
            {
                BackgroundImage = new Uri("Assets/150_150_Logo.png", UriKind.Relative),
                Title = "Financial Manager",
                BackContent = "Expenses:\n" + expenses.ToString("n0") + " " + symbol + "\nRemaining:\n" +
                              remaining.ToString("n0") + " " + symbol,
                BackBackgroundImage = new Uri("Black.jpg", UriKind.Relative)
            };

            if (TileToFind == null)
                ShellTile.Create(new Uri("/Views/MainWindow.xaml?DefaultTitle=FinancialManagerFromTile", UriKind.Relative), NewTileData);
            else
            {
                TileToFind.Delete();
                MessageBox.Show("Live Tile has been removed!");
                __btCreateTile.Content = "Create Live Tile";
            }
        }        

        private void __btDeleteAll_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("I know that you've hit Delete All, but are you really sure?\n\n"+
                "There will be some default categories but all your expenses will be deleted!",
                                                            "Delete All!", MessageBoxButton.OKCancel);

            if (result == MessageBoxResult.OK)
            {
                __LoadingLayer.Visibility = System.Windows.Visibility.Visible;

                Task t_LoadPageModel = Task.Factory.StartNew(() =>
                {
                    StaticValues.DB.DeleteAll();
                });

                Task UpdateUI = t_LoadPageModel.ContinueWith((t) =>
                {
                    NavigationService.Navigate(new Uri("/Views/MainWindow.xaml?caller=settings", UriKind.Relative));
                }, TaskScheduler.FromCurrentSynchronizationContext());
                
            }
            
        }

        private void __btDeleteAllIncome_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("I know that you've hit Delete All Income records, but are you really sure?\n\n" +
                "Your categories and expenses will be intact and I'll just delete your income records.",
                                                            "Delete All Income Records!", MessageBoxButton.OKCancel);

            if (result == MessageBoxResult.OK)
            {
                StaticValues.DB.DeleteAllIncomes();
                NavigationService.Navigate(new Uri("/Views/MainWindow.xaml?caller=settings", UriKind.Relative));
            }
        }

        private void __btDeleteAllExpenses_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("I know that you've hit Delete All Expenses, but are you really sure?\n\n" +
                "Your categories will be intact and I'll just delete your expenses.",
                                                            "Delete All Expenses!", MessageBoxButton.OKCancel);

            if (result == MessageBoxResult.OK)
            {
                StaticValues.DB.DeleteAllExpenses();
                NavigationService.Navigate(new Uri("/Views/MainWindow.xaml?caller=settings", UriKind.Relative));
            }
        }


        #region Password

        bool IsPasswordSaved = false; 
        private void __btCancel_Click(object sender, RoutedEventArgs e)
        {
            IsPasswordSaved = false; 
            ClosePasswordLayer();
        }

        private void __btPasswordEnter_Click(object sender, RoutedEventArgs e)
        {
            IsPasswordSaved = true;
            ClosePasswordLayer();
            StaticValues.DB.SetPassword(__tbPassword.Text.ToString());
        }

        private void ClosePasswordLayer()
        {
            __PasswordLayer.Background = new SolidColorBrush(Colors.Transparent);
            Duration duration = new Duration(TimeSpan.FromSeconds(0.8));
            Storyboard passwordUp = new Storyboard();
            passwordUp.Completed += passwordOUT_Completed;
            DoubleAnimation passwordUpAnimation = new DoubleAnimation();

            BackEase easing = new BackEase();
            easing.EasingMode = EasingMode.EaseIn;
            easing.Amplitude = 0.4;
            passwordUpAnimation.EasingFunction = easing;

            passwordUpAnimation.Duration = duration;
            passwordUp.Children.Add(passwordUpAnimation);
            Storyboard.SetTarget(passwordUpAnimation, __grPassword);

            Storyboard.SetTargetProperty(passwordUpAnimation, new PropertyPath("(Canvas.top)"));

            passwordUpAnimation.To = -1200;

            passwordUp.Begin();
        }

        private void __btCreatePassword_Click(object sender, RoutedEventArgs e)
        {
            __tbPassword.Text = string.Empty;
            Canvas.SetTop(__grPassword, 1200);
            __PasswordLayer.Visibility = System.Windows.Visibility.Visible;
            __PasswordLayer.Background = new SolidColorBrush(Colors.Transparent);
            Duration duration = new Duration(TimeSpan.FromSeconds(0.8));
            Storyboard passwordUp = new Storyboard();
            passwordUp.Completed += passwordUp_Completed;
            DoubleAnimation passwordUpAnimation = new DoubleAnimation();

            BackEase easing = new BackEase();
            easing.EasingMode = EasingMode.EaseOut;
            easing.Amplitude = 0.4;
            passwordUpAnimation.EasingFunction = easing;

            passwordUpAnimation.Duration = duration;
            passwordUp.Children.Add(passwordUpAnimation);
            Storyboard.SetTarget(passwordUpAnimation, __grPassword);

            Storyboard.SetTargetProperty(passwordUpAnimation, new PropertyPath("(Canvas.top)"));

            passwordUpAnimation.To = 0;

            passwordUp.Begin();
        }       

        void passwordUp_Completed(object sender, EventArgs e)
        {
               
        }

        void passwordOUT_Completed(object sender, EventArgs e)
        {
            if(IsPasswordSaved)
                MessageBox.Show("Your New Password Has Been Saved. Try Not To Lose It !");         
            __PasswordLayer.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void __btRemovePassword_Click(object sender, RoutedEventArgs e)
        {
            StaticValues.DB.RemovePassword();
            MessageBox.Show("No More Password! I Promise!");
        }
        #endregion

        
    }
}