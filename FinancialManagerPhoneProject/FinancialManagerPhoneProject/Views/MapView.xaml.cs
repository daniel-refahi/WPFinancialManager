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
using Windows.Devices.Geolocation;
using Microsoft.Phone.Maps.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using FinancialManagerPhoneProject.DataHandlers;

namespace FinancialManagerPhoneProject.Views
{
    public partial class MapView : PhoneApplicationPage
    {
        string _Caller;
        string _Latitude = string.Empty;
        string _Longtitude = string.Empty;
        public MapView()
        {
            InitializeComponent();                        
        }

        private async void LoadMap() 
        {
            
            Geolocator geolocator = new Geolocator();
            geolocator.DesiredAccuracyInMeters = 50;

            try
            {
                Geoposition geoposition = await geolocator.GetGeopositionAsync(
                    maximumAge: TimeSpan.FromMinutes(5),
                    timeout: TimeSpan.FromSeconds(10)
                    );

                GeoCoordinate myLocation = new GeoCoordinate(geoposition.Coordinate.Latitude, geoposition.Coordinate.Longitude);

                Ellipse myCircle = new Ellipse();
                myCircle.Fill = new SolidColorBrush(Colors.Red);
                myCircle.Height = 20;
                myCircle.Width = 20;
                myCircle.Opacity = 50;

                MapOverlay myLocationOverlay = new MapOverlay();
                myLocationOverlay.Content = myCircle;
                myLocationOverlay.PositionOrigin = new Point(0.5, 0.5);
                myLocationOverlay.GeoCoordinate = myLocation;

                MapLayer myLocationLayer = new MapLayer();
                myLocationLayer.Add(myLocationOverlay);

                Map map = new Map();
                map.LandmarksEnabled = true;                
                map.ZoomLevel = 16;
                map.Center = myLocation;
                map.Layers.Add(myLocationLayer);
                LayoutRoot.Children.Add(map);

            }
            catch (Exception ex)
            {
                if ((uint)ex.HResult == 0x80004004)
                {
                    MessageBox.Show("location  is disabled in phone settings.");
                }
                else
                {
                    MessageBox.Show(ex.InnerException.ToString());
                }
            }
        }

        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Views/ExpenseDetail.xaml?caller=help", UriKind.Relative));
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
            NavigationContext.QueryString.TryGetValue("longtitude", out _Longtitude);

            GeoCoordinate myLocation = new GeoCoordinate(StaticMethods.CleanNumber(_Latitude), StaticMethods.CleanNumber(_Longtitude));

            Ellipse myCircle = new Ellipse();
            myCircle.Fill = new SolidColorBrush(Colors.Red);
            myCircle.Height = 20;
            myCircle.Width = 20;
            myCircle.Opacity = 50;

            MapOverlay myLocationOverlay = new MapOverlay();
            myLocationOverlay.Content = myCircle;
            myLocationOverlay.PositionOrigin = new Point(0.5, 0.5);
            myLocationOverlay.GeoCoordinate = myLocation;

            MapLayer myLocationLayer = new MapLayer();
            myLocationLayer.Add(myLocationOverlay);

            Map map = new Map();
            map.LandmarksEnabled = true;
            map.ZoomLevel = 16;
            map.Center = myLocation;
            map.Layers.Add(myLocationLayer);
            LayoutRoot.Children.Add(map);
            
        }
    }
}