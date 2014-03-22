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
        string _Caller = string.Empty;
        string _Object = string.Empty;
        string _status = string.Empty;
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
                case "reportdetail":
                    NavigationService.Navigate(new Uri("/Views/ReportDetail.xaml?caller=help", UriKind.Relative));
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
            NavigationContext.QueryString.TryGetValue("status", out _status);

            switch (_Caller)
            {
                case "mainwindow":
                    switch (_Object)
                    { 
                        case "expense":
                            __title.Text = "Expenses List Help";
                            __MainExpense.Visibility = System.Windows.Visibility.Visible;
                            break;
                        case "category":
                            __title.Text = "Categories List Help";
                            __MainCategory.Visibility = System.Windows.Visibility.Visible;
                            break;
                        case "report":
                            __title.Text = "Report Page Help";
                            __MainReport.Visibility = System.Windows.Visibility.Visible;
                            break;
                    }
                    break;
                case "expensedetail":
                    __title.Text = "Expense Detail Help";
                    if (_status == "add")
                    {
                        __title.Text = "Add Expense Help";
                        __AddExpense.Visibility = System.Windows.Visibility.Visible;
                    }
                    else
                    { }
                    break;
                case "categorydetail":
                    __title.Text = "Category Edit Help";
                    break;
                case "categorychart":
                    __title.Text = "Category Detail Help";
                    break;
                case "reportdetail":
                    __title.Text = "Report Detail Help";
                    break;
            }
        }
    }
}