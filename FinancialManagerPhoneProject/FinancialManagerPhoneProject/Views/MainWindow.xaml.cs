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
using System.Threading.Tasks;
using FinancialManagerPhoneProject.Models;
using FinancialManagerPhoneProject.Views.UserControls;
using System.Threading;
using Windows.Storage;
using System.Windows.Media;
using Visifire.Charts;

namespace FinancialManagerPhoneProject.Views
{
    public partial class MainWindow : PhoneApplicationPage
    {
        
        private double _DeviceWidth;
        private MainPageModel _MainPageModel;
        private string _Caller = string.Empty;
        private string _PassedCategoryName = string.Empty;        
        private bool IsChartAdded = false;

        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;

        }

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            _DeviceWidth = Application.Current.Host.Content.ActualWidth;

            __LoadingBar.Opacity = 1;
            Task t_LoadPageModel = Task.Factory.StartNew(() =>
            {
                _MainPageModel = LoadPageModel();
            });

            Task UpdateUI = t_LoadPageModel.ContinueWith((t) =>
            {
                if (_MainPageModel == null)
                {
                    MessageBox.Show("The Database connection failed! \nPlease restart the app.");                    
                }
                this.DataContext = _MainPageModel;
                __LoadingBar.Opacity = 0;

                if (_Caller == "categorydetail" || _Caller == "categorychart")
                {
                    __MainPivot.SelectedIndex = 1;
                }
                if (_Caller == "report")
                {
                    __MainPivot.SelectedIndex = 2;
                }
                else if (_Caller == "help")
                {
                    string objectString = string.Empty;
                    NavigationContext.QueryString.TryGetValue("object", out objectString);
                    switch (objectString)
                    {
                        case "expense":
                            __MainPivot.SelectedIndex = 0;
                            break;
                        case "category":
                            __MainPivot.SelectedIndex = 1;
                            break;
                        case "report":
                            __MainPivot.SelectedIndex = 2;
                            break;
                    }
                }
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        void __MainPivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (((PivotItem)e.AddedItems[0]).Header.ToString())
            {
                case "Expenses":
                    StaticValues.AppStatus = StaticValues.AppStatusOptions.Expenses;
                    
                    if (ApplicationBar.Buttons.Count == 1)
                    {
                        Uri uri = new Uri("//Image/add.png", UriKind.Relative);
                        ApplicationBarIconButton addIcon = new ApplicationBarIconButton() { Text = "add", IconUri = uri };
                        addIcon.Click += ApplicationBarAddIcon_Click;
                        ApplicationBar.Buttons.Insert(0, addIcon);
                    }
                    break;
                case "Categories":
                    StaticValues.AppStatus = StaticValues.AppStatusOptions.Categories;                    
                    if (ApplicationBar.Buttons.Count == 1)
                    {
                        Uri uri = new Uri("//Image/add.png", UriKind.Relative);
                        ApplicationBarIconButton addIcon = new ApplicationBarIconButton() { Text = "add", IconUri = uri };
                        addIcon.Click += ApplicationBarAddIcon_Click;
                        ApplicationBar.Buttons.Insert(0, addIcon);
                    }
                    break;
                case "Report":
                    StaticValues.AppStatus = StaticValues.AppStatusOptions.Report;
                    ApplicationBar.Buttons.RemoveAt(0);

                    if (! IsChartAdded)
                    {

                        Chart topCategoryChart = new Chart();
                        topCategoryChart.Height = 250;
                        topCategoryChart.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
                        topCategoryChart.Background = new SolidColorBrush(Colors.Transparent);
                        topCategoryChart.LightingEnabled = false;
                        topCategoryChart.View3D = true;                        
                        topCategoryChart.BorderThickness = new Thickness(0);

                        Axis yaxis = new Axis();
                        yaxis.Enabled = false;
                        ChartGrid grids = new ChartGrid();
                        grids.Enabled = false;
                        yaxis.Grids.Add(grids);
                        topCategoryChart.AxesY.Add(yaxis);

                        Axis xaxis = new Axis();
                        xaxis.Enabled = true;
                        grids.Enabled = false;
                        xaxis.Grids.Add(grids);
                        topCategoryChart.AxesX.Add(xaxis);

                        DataSeries ds = new DataSeries();
                        ds.LabelEnabled = true;
                        ds.ShowInLegend = false;
                        ds.IncludeDataPointsInLegend = false;
                        ds.RenderAs = RenderAs.Column;
                        ds.DataPoints.Add(new DataPoint()
                        {
                            AxisXLabel = _MainPageModel.CategoryName1,
                            YValue = _MainPageModel.TotalCategory1
                        });
                        ds.DataPoints.Add(new DataPoint()
                        {
                            AxisXLabel = _MainPageModel.CategoryName2,
                            YValue = _MainPageModel.TotalCategory2
                        });
                        ds.DataPoints.Add(new DataPoint()
                        {
                            AxisXLabel = _MainPageModel.CategoryName3,
                            YValue = _MainPageModel.TotalCategory3
                        });
                        ds.DataPoints.Add(new DataPoint()
                        {
                            AxisXLabel = _MainPageModel.CategoryName4,
                            YValue = _MainPageModel.TotalCategory4
                        });
                        ds.DataPoints.Add(new DataPoint()
                        {
                            AxisXLabel = _MainPageModel.CategoryName5,
                            YValue = _MainPageModel.TotalCategory5
                        });
                        topCategoryChart.Series.Add(ds);                        
                        Grid.SetColumn(topCategoryChart, 0);
                        Grid.SetColumnSpan(topCategoryChart, 2);
                        Grid.SetRow(topCategoryChart, 1);
                        __GrReport.Children.Add(topCategoryChart);

                        IsChartAdded = true;
                    }
                    break;
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            while(NavigationService.BackStack.Count() >0)
                NavigationService.RemoveBackEntry();
            NavigationContext.QueryString.TryGetValue("caller", out _Caller);
            NavigationContext.QueryString.TryGetValue("categoryname", out _PassedCategoryName);
            
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            NavigationService.RemoveBackEntry();
        }

        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            
            base.OnBackKeyPress(e);            
        }

        private MainPageModel LoadPageModel()
        {            
            MainPageModel mainPageModel = new MainPageModel();
            string symbol = StaticValues.DB.GetCurrencySymbol();            
            foreach (Expense expense in StaticValues.DB.GetAllExpenses())
            {
                mainPageModel.ExpenseListModel.Add(new ExpenseItemModel()
                {
                    Amount = symbol + " " + expense.Value,
                    ID = expense.ID,
                    Category = expense.Category,
                    Date = expense.Date.ToString("dd/MMM"),
                    Description = expense.Description,
                    ReceiptVisibility = string.IsNullOrEmpty(expense.RecieptName) ? "Collapsed" : "Visible",
                    ImageSource = "../../Assets/Icons/" + expense.Icon + ".png",
                    ScreenWidth = XMLHandler.DEIVCE_WIDTH - 40
                });
            }

            foreach (Category category in StaticValues.DB.GetAllCategories())
            {
                mainPageModel.CategoryListModel.Add(new CategoryItemModel()
                {
                    Name = category.Name,
                    Plan = symbol + " " + category.Plan,
                    Remaining = symbol + " " + (category.Plan - category.TotalExpenses),
                    Total = symbol + " " + category.TotalExpenses,
                    ImageSource = "../../Assets/Icons/" + category.Icon + ".png",
                    ScreenWidth = XMLHandler.DEIVCE_WIDTH - 40
                });
            }

            int topCategoriesCounter = 1;            
            foreach (Category category in StaticValues.DB.GetTopCategories())
            {
                switch (topCategoriesCounter)
                {
                    case 1:
                        mainPageModel.CategoryName1 = category.Name;
                        mainPageModel.TotalCategory1 = category.TotalExpenses;
                        break;
                    case 2:
                        mainPageModel.CategoryName2 = category.Name;
                        mainPageModel.TotalCategory2 = category.TotalExpenses;
                        break;
                    case 3:
                        mainPageModel.CategoryName3 = category.Name;
                        mainPageModel.TotalCategory3 = category.TotalExpenses;
                        break;
                    case 4:
                        mainPageModel.CategoryName4 = category.Name;
                        mainPageModel.TotalCategory4 = category.TotalExpenses;
                        break;
                    case 5:
                        mainPageModel.CategoryName5 = category.Name;
                        mainPageModel.TotalCategory5 = category.TotalExpenses;
                        break;
                }
                topCategoriesCounter++;
            }

            double income = StaticValues.DB.GetIncome();
            double totalExpense = StaticValues.DB.GetTotalExpenses();
            double balance = income - totalExpense;
            mainPageModel.Income = symbol + " " + income;
            mainPageModel.Balance = symbol + " " + balance;
            mainPageModel.TotalExpenses = StaticValues.DB.GetCurrencySymbol()+ " " + totalExpense;
            mainPageModel.Saving = StaticValues.DB.GetCurrencySymbol() + " " + balance;

            return mainPageModel;
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void ApplicationBarAddIcon_Click(object sender, EventArgs e)
        {
            if (StaticValues.AppStatus == StaticValues.AppStatusOptions.Expenses)
            {
                NavigationService.Navigate(new Uri("/Views/ExpenseDetail.xaml?status=add&caller=mainwindow", UriKind.Relative));
            }
            else if (StaticValues.AppStatus == StaticValues.AppStatusOptions.Categories)
            {
                NavigationService.Navigate(new Uri("/Views/CategoryDetail.xaml?status=add&caller=mainwindow", UriKind.Relative));
            }
        }

        private void ApplicationBarHelpIcon_Click(object sender, EventArgs e)
        {
            switch (StaticValues.AppStatus)
            {
                case StaticValues.AppStatusOptions.Expenses:
                    NavigationService.Navigate(new Uri("/Views/help.xaml?caller=mainwindow&object=expense", UriKind.Relative));
                    break;
                case StaticValues.AppStatusOptions.Categories:
                    NavigationService.Navigate(new Uri("/Views/help.xaml?caller=mainwindow&object=category", UriKind.Relative));
                    break;
                case StaticValues.AppStatusOptions.Report:
                    NavigationService.Navigate(new Uri("/Views/help.xaml?caller=mainwindow&object=report", UriKind.Relative));
                    break;
            }            
        }

        private void __liExpensesList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(((ListBox)sender).SelectedItem != null)
                NavigationService.Navigate(
                    new Uri("/Views/ExpenseDetail.xaml?status=update&caller=mainwindow&ID=" + 
                                ((ExpenseItemModel)((ListBox)sender).SelectedItem).ID,
                                                                        UriKind.Relative));
        }

        private void __liCategoriesList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (((ListBox)sender).SelectedItem != null)
                NavigationService.Navigate(
                    new Uri("/Views/CategoryChart.xaml?categoryname=" +
                                ((CategoryItemModel)((ListBox)sender).SelectedItem).Name,
                                                                        UriKind.Relative));
        }

        private void __btReportDetail_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Views/ReportDetail.xaml?",UriKind.Relative));
        }
    }
}