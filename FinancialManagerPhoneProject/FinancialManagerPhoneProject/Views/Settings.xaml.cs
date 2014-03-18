using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

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
        }

        private void __btSave_Click(object sender, RoutedEventArgs e)
        {
            if ((bool)__cbLiveTile.IsChecked)
            {
                ShellTile TileToFind = ShellTile.ActiveTiles.FirstOrDefault(x => x.NavigationUri.ToString().Contains("DefaultTitle=FinancialManagerFromTile"));
                
                if (TileToFind == null)
                {
                    StandardTileData NewTileData = new StandardTileData
                    {
                        BackgroundImage = new Uri("Assets/150_150_Logo.png", UriKind.Relative),
                        Title = "Financial Manager",                        
                        BackTitle = "Car",
                        BackContent = "Total Expenses:\n4300 $\nRemaining:\n1200$",
                        BackBackgroundImage = new Uri("TileMediumSecond.jpg", UriKind.Relative)
                    };

                    // Create the tile and pin it to Start. This will cause a navigation to Start and a deactivation of our application.
                    ShellTile.Create(new Uri("/Views/MainWindow.xaml?DefaultTitle=FinancialManagerFromTile", UriKind.Relative), NewTileData);
                }
            }
        }

        private void __btDeleteAll_Click(object sender, RoutedEventArgs e)
        {

        }        
    }
}