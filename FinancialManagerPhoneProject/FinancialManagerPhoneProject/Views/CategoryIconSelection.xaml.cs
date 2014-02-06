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
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace FinancialManagerPhoneProject.Views
{
    public partial class CategoryIconSelection : PhoneApplicationPage
    {
        private Image _CurrentIcon;
        private string _SelectedIconSource;
        private string _OldIconSource;

        public CategoryIconSelection()
        {
            InitializeComponent();
            this.Loaded += CategoryIconSelection_Loaded;
            
        }

        void CategoryIconSelection_Loaded(object sender, RoutedEventArgs e)
        {
            List<string> icons = StaticValues.DB.GetIcons("../Assets/Icons/");

            foreach (var icon in icons)
            {
                Image image = new Image();
                Uri uri = new Uri(icon, UriKind.RelativeOrAbsolute);
                image.Source = new BitmapImage(uri);
                image.Width = 125;
                image.Height = 125;
                image.Margin = new Thickness(0, 5, 5, 0);

                if (_OldIconSource == icon)
                {
                    image.Opacity = 1;
                    _SelectedIconSource = icon;
                    _CurrentIcon = image;
                }
                else
                {
                    image.Opacity = 0.2;
                }

                image.MouseLeftButtonUp += image_MouseLeftButtonUp;

                __wpIconList.Children.Add(image);
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            NavigationContext.QueryString.TryGetValue("source", out _OldIconSource);

        }

        void image_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            _CurrentIcon.Opacity = 0.2;            
            ((Image)sender).Opacity = 1;
            _SelectedIconSource = ((BitmapImage)((Image)sender).Source).UriSource.ToString();
            _CurrentIcon = (Image)sender;
        }

        private void ApplicationBarSelectButton_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Views/CategoryDetail.xaml?source=" +
                                                ((BitmapImage)_CurrentIcon.Source).UriSource.ToString(), UriKind.Relative));
        }

        private void ApplicationBarCancelButton_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Views/CategoryDetail.xaml?source=" + _OldIconSource, UriKind.Relative));
        }        
    }
}