using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Tasks;
using System.Windows.Media.Imaging;
using System.IO;
using System.IO.IsolatedStorage;
using Microsoft.Phone;
using FinancialManagerPhoneProject.DataHandlers;

namespace FinancialManagerPhoneProject.Views
{
    public partial class RecieptCamera : PhoneApplicationPage
    {

        public RecieptCamera()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(MainPage_Loaded);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            string imageSource = string.Empty;
            NavigationContext.QueryString.TryGetValue("image", out imageSource);


            using (IsolatedStorageFile store = IsolatedStorageFile.GetUserStoreForApplication())
            {
                string source = store.GetFileNames(string.Format("{0}\\*", imageSource)).FirstOrDefault();
                Uri uri = new Uri(source, UriKind.Absolute);
                Image image = new Image();
                __Image.Source = new BitmapImage(uri);                
            }            
        }

        void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
        }

        
    }
}