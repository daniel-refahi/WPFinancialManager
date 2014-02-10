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
    public partial class CategoryChart : PhoneApplicationPage
    {
        public CategoryChart()
        {
            InitializeComponent();
        }

        private void ApplicationBarSaveIcon_Click(object sender, EventArgs e)
        {
            //double plan = 0;
            //bool isNum = double.TryParse(__tbPlan.Text.ToString(), out plan);
            //if (!isNum)
            //    MessageBox.Show("Please Enter a Valid Plan!");
            //else
            //{
            //    string icon = ((BitmapImage)(__Icon.Source)).UriSource.ToString();
            //    icon = icon.Substring(icon.LastIndexOf('/') + 1, icon.LastIndexOf('.') - icon.LastIndexOf('/') - 1);
            //    if (StaticValues.DB.AddCategory(new Category()
            //    {
            //        Icon = icon,
            //        Name = __tbName.Text.ToString(),
            //        Plan = Convert.ToDouble(__tbPlan.Text.ToString()),
            //        TotalExpenses = 0
            //    }))
            //    {
            //        SaveState = false;
            //        NavigationService.Navigate(new Uri("/Views/MainWindow.xaml?caller=categorydetail", UriKind.Relative));
            //    }
            //    else
            //        MessageBox.Show("The Category Name Already Exists!");

            //}

        }

        void deleteIcon_Click(object sender, EventArgs e)
        {
            //MessageBoxResult result = MessageBox.Show("Would you like to delete the category? Be aware that all expenses in this category will be removed as well!",
            //                                                "Delete", MessageBoxButton.OKCancel);

            //if (result == MessageBoxResult.OK)
            //{
            //    StaticValues.DB.DeleteCategory(_Name);
            //    NavigationService.Navigate(new Uri("/Views/MainWindow.xaml?caller=categorydetail", UriKind.Relative));
            //}
        }

        private void ApplicationBarHelpIcon_Click(object sender, EventArgs e)
        {
            //SaveState = false;
        }


    }
}