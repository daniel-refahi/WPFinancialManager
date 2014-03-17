using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Device.Location;

namespace FinancialManagerPhoneProject.Views
{
    public partial class MapView : PhoneApplicationPage
    {
        string _Caller;
        string _Latitude = string.Empty;
        string _Longitude = string.Empty;
        public MapView()
        {
            InitializeComponent();
            __Map.Center = new GeoCoordinate(80, 45);
        }

        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            //NavigationService.Navigate(new Uri("/Views/ExpenseDetail.xaml?caller=help&object=" + _Object, UriKind.Relative));
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            NavigationService.RemoveBackEntry();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            NavigationContext.QueryString.TryGetValue("caller", out _Caller);
            NavigationContext.QueryString.TryGetValue("latitude", out _Latitude);
            NavigationContext.QueryString.TryGetValue("longitude", out _Longitude);

            //__Map.Center = new GeoCoordinate(Convert.ToDouble(_Latitude), Convert.ToDouble(_Longitude));
        }
    }
}