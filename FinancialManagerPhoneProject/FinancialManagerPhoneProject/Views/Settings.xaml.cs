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

namespace FinancialManagerPhoneProject.Views
{
    public partial class Settings : PhoneApplicationPage
    {
        public Settings()
        {
            InitializeComponent();            
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
            __txIncome.Text = StaticValues.DB.GetIncome().ToString("n0");
        }

        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Views/MainWindow.xaml?caller=settings", UriKind.Relative));
        }

        private void __btSave_Click(object sender, RoutedEventArgs e)
        {
            int income = 0;
            bool isNum = int.TryParse(__txIncome.Text.ToString().Replace(",",""), out income);
            if (!isNum || income <= 0)
            {
                MessageBox.Show("Please Enter a Valid Income!");
            }
            else
            {
                string newSymbol = string.Empty;
                switch(__liSymbols.SelectedIndex)
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
                }
                StaticValues.DB.UpdateSettings(income.ToString(), newSymbol);

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
            MessageBoxResult result = MessageBox.Show("I know that you've hit Delete All, but are you really sure?",
                                                            "Delete All!", MessageBoxButton.OKCancel);

            if (result == MessageBoxResult.OK)
            {
                StaticValues.DB.DeleteAll();
                NavigationService.Navigate(new Uri("/Views/MainWindow.xaml?caller=settings", UriKind.Relative));
            }
            
        }

        
    }
}