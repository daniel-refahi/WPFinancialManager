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

            ShellTile TileToFind = ShellTile.ActiveTiles.FirstOrDefault(x => x.NavigationUri.ToString().Contains("DefaultTitle=FinancialManagerFromTile"));
            __cbLiveTile.IsChecked = (TileToFind != null);
            __txIncome.Text = StaticValues.DB.GetIncome().ToString("n0");
        }

        private void __btSave_Click(object sender, RoutedEventArgs e)
        {
            if ((bool)__cbLiveTile.IsChecked)
            {
                ShellTile TileToFind = ShellTile.ActiveTiles.FirstOrDefault(x => x.NavigationUri.ToString().Contains("DefaultTitle=FinancialManagerFromTile"));
                
                if (TileToFind == null)
                {
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

                    ShellTile.Create(new Uri("/Views/MainWindow.xaml?DefaultTitle=FinancialManagerFromTile", UriKind.Relative), NewTileData);
                }
            }
            else 
            {
                ShellTile TileToFind = ShellTile.ActiveTiles.FirstOrDefault(x => x.NavigationUri.ToString().Contains("DefaultTitle=FinancialManagerFromTile"));

                if (TileToFind != null)
                {
                    TileToFind.Delete();
                }
            }

            MessageBox.Show("Setting has been saved.");
        }

        private void __btDeleteAll_Click(object sender, RoutedEventArgs e)
        {

        }        
    }
}