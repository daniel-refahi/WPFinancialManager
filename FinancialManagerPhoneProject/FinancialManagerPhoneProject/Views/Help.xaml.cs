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
    public partial class Help : PhoneApplicationPage
    {
        string _Caller;
        string _Object;
        public Help()
        {
            InitializeComponent();
        }

        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            //base.OnBackKeyPress(e);
            switch (_Caller)
            {
                case "mainwindow":
                    NavigationService.Navigate(new Uri("/Views/MainWindow.xaml?caller=help&object="+_Object, UriKind.Relative));
                    break;
                case "expensedetail":
                    NavigationService.Navigate(new Uri("/Views/ExpenseDetail.xaml?caller=help&object=" + _Object, UriKind.Relative));
                    break;
                case "categorydetail":
                    NavigationService.Navigate(new Uri("/Views/CategoryDetail.xaml?caller=help&object=" + _Object, UriKind.Relative));
                    break;
                case "categorychart":
                    NavigationService.Navigate(new Uri("/Views/CategoryChart.xaml?caller=help&categoryname=" + _Object, UriKind.Relative));
                    break;
            }
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
            NavigationContext.QueryString.TryGetValue("object", out _Object);
            _title.Text = _Caller;
            _object.Text = _Object;
        }
    }
}