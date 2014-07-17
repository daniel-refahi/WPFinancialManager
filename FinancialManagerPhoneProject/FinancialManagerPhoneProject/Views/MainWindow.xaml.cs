using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using FinancialManagerPhoneProject.DataHandlers;
using System.Threading.Tasks;
using FinancialManagerPhoneProject.Models;
using System.Windows.Media;
using Visifire.Charts;
using Microsoft.Phone.Tasks;
using System.Windows.Media.Animation;
using System.Collections.Generic;

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

        async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            _DeviceWidth = Application.Current.Host.Content.ActualWidth;

            __LoadingBar.Opacity = 1;

            if (XMLHandler.FINANCIALMANAGER_XML == null)
            {
                await StaticValues.DB.LoadXmlFromFileAsync();
                if (StaticValues.DB.IsUsePassword())
                {
                    __PasswordLayer.Visibility = System.Windows.Visibility.Visible;
                    this.ApplicationBar.IsVisible = false;
                }
            }
            
                
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

                if (StaticValues.DB.GetVersion() != StaticValues.CurrentVersion)
                {
                    MessageBox.Show(StaticValues.NewFeatures);
                }

                if (_Caller == "categorydetail" || _Caller == "categorychart")
                {
                    __MainPivot.SelectedIndex = 1;
                }
                if (_Caller == "report")
                {
                    __MainPivot.SelectedIndex = 2;
                }
                if (_Caller == "incomedetail")
                {
                    __MainPivot.SelectedIndex = 3;
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
                        case "income":
                            __MainPivot.SelectedIndex = 3;
                            break;
                    }
                }
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        private MainPageModel LoadPageModel()
        {
            MainPageModel mainPageModel = new MainPageModel();
            string symbol = StaticValues.DB.GetCurrencySymbol();
            mainPageModel.ScreenHeight = XMLHandler.DEIVCE_HEIGHT;
            mainPageModel.ScreenWidth = XMLHandler.DEIVCE_WIDTH;

            // Getting all the expenses
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
                    MapVisibility = string.IsNullOrEmpty(expense.Latitude) ? "Collapsed" : "Visible",
                    ImageSource = "../../Assets/Icons/" + expense.Icon + ".png",
                    ScreenWidth = XMLHandler.DEIVCE_WIDTH - 40
                });
            }            

            // Getting all the categories
            foreach (Category category in StaticValues.DB.GetAllCategories())
            {
                mainPageModel.CategoryListModel.Add(new CategoryItemModel()
                {
                    Name = category.Name,
                    Plan = symbol + " " + category.Plan.ToString("n2"),
                    Remaining = symbol + " " + (category.Plan - category.TotalExpenses).ToString("n2"),
                    Total = symbol + " " + category.TotalExpenses.ToString("n2"),
                    ImageSource = "../../Assets/Icons/" + category.Icon + ".png",
                    ScreenWidth = XMLHandler.DEIVCE_WIDTH - 40
                });
            }

            // data for the report page 
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

            double totalIncome = StaticValues.DB.GetIncome();
            double totalExpense = StaticValues.DB.GetTotalExpenses();
            double balance = totalIncome - totalExpense;
            mainPageModel.TotalIncome = symbol + " " + totalIncome.ToString("n0");
            mainPageModel.Balance = symbol + " " + balance.ToString("n2");
            mainPageModel.TotalExpenses = StaticValues.DB.GetCurrencySymbol() + " " + totalExpense.ToString("n2");
            mainPageModel.Saving = StaticValues.DB.GetCurrencySymbol() + " " + balance.ToString("n2");
            mainPageModel.MonthYear = StaticMethods.GetMonthSymbol(StaticValues.DB.GetCurrentMonth()) + "/" + StaticValues.DB.GetCurrentYear();

            // Getting all the incomes
            foreach (Income income in StaticValues.DB.GetAllIncomes())
            {
                mainPageModel.IncomeListModel.Add(new IncomeItemModel()
                {
                    Value = symbol + " " + income.Value,
                    ID = income.ID,
                    Date = income.Date.ToString("dd/MMM"),
                    Description = income.Description,
                    ScreenWidth = XMLHandler.DEIVCE_WIDTH - 40
                });
            }

            return mainPageModel;
        }

        #region Page Events
        void __MainPivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (((PivotItem)e.AddedItems[0]).Header.ToString())
            {
                case "Incomes":
                    StaticValues.AppStatus = StaticValues.AppStatusOptions.Incomes;

                    
                    if (ApplicationBar.Buttons.Count == 1)
                    {
                        Uri uri = new Uri("//Image/add.png", UriKind.Relative);
                        ApplicationBarIconButton addIcon = new ApplicationBarIconButton() { Text = "add income", IconUri = uri };
                        addIcon.Click += ApplicationBarAddIcon_Click;
                        ApplicationBar.Buttons.Insert(0, addIcon);
                    }
                    ((ApplicationBarIconButton)ApplicationBar.Buttons[0]).Text = "add income";
                    break;
                case "Expenses":
                    StaticValues.AppStatus = StaticValues.AppStatusOptions.Expenses;

                    
                    if (ApplicationBar.Buttons.Count == 1)
                    {
                        Uri uri = new Uri("//Image/add.png", UriKind.Relative);
                        ApplicationBarIconButton addIcon = new ApplicationBarIconButton() { Text = "add expense", IconUri = uri };
                        addIcon.Click += ApplicationBarAddIcon_Click;
                        ApplicationBar.Buttons.Insert(0, addIcon);
                    }
                    ((ApplicationBarIconButton)ApplicationBar.Buttons[0]).Text = "add expense";
                    break;
                case "Categories":
                    StaticValues.AppStatus = StaticValues.AppStatusOptions.Categories;                    
                    if (ApplicationBar.Buttons.Count == 1)
                    {
                        Uri uri = new Uri("//Image/add.png", UriKind.Relative);
                        ApplicationBarIconButton addIcon = new ApplicationBarIconButton() { Text = "add category", IconUri = uri };
                        addIcon.Click += ApplicationBarAddIcon_Click;
                        ApplicationBar.Buttons.Insert(0, addIcon);
                    }
                    ((ApplicationBarIconButton)ApplicationBar.Buttons[0]).Text = "add category";
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
        
        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            __grPassword.Width = XMLHandler.DEIVCE_WIDTH;
            __grPassword.Height = XMLHandler.DEIVCE_HEIGHT;
        }
        #endregion

        #region List selection events
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

        private void __liIncomeList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (((ListBox)sender).SelectedItem != null)
                NavigationService.Navigate(
                    new Uri("/Views/IncomeDetail.xaml?status=update&caller=mainwindow&ID=" +
                                ((IncomeItemModel)((ListBox)sender).SelectedItem).ID,
                                                                        UriKind.Relative));
        }
        #endregion       

        #region Application bar
        private void ApplicationBarMenuItem_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Views/Settings.xaml?", UriKind.Relative));
        }
        private void ApplicationBarFeedback_Click(object sender, EventArgs e)
        {
            EmailComposeTask emailComposeTask = new EmailComposeTask();

            emailComposeTask.Subject = "Report bug & Suggestion";
            emailComposeTask.Body = "";
            emailComposeTask.To = "financialmanager.pro@outlook.com";

            emailComposeTask.Show();
        }
        private void ApplicationBarRateMe_Click(object sender, EventArgs e)
        {
            MarketplaceReviewTask marketplaceReviewTask = new MarketplaceReviewTask();
            marketplaceReviewTask.Show();
        }
        private async void ApplicationBarAddIcon_Click(object sender, EventArgs e)
        {
            if (StaticValues.AppStatus == StaticValues.AppStatusOptions.Expenses)
            {
                if (await IsUserValid())
                    NavigationService.Navigate(new Uri("/Views/ExpenseDetail.xaml?status=add&caller=mainwindow", UriKind.Relative));
            }
            else if (StaticValues.AppStatus == StaticValues.AppStatusOptions.Incomes)
            {
                if (await IsUserValid())
                    NavigationService.Navigate(new Uri("/Views/IncomeDetail.xaml?status=add&caller=mainwindow", UriKind.Relative));
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
                case StaticValues.AppStatusOptions.Incomes:
                    NavigationService.Navigate(new Uri("/Views/help.xaml?caller=mainwindow&object=income", UriKind.Relative));
                    break;
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
        #endregion

        private void __btReportDetail_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Views/ReportDetail.xaml?", UriKind.Relative));
        }

        private async Task<bool> IsUserValid()
        {
            if (StaticValues.DB.IsValidToSave())
            {
                // continue
                return true;
            }
            else
            {
                // load buy from store
                MessageBoxResult result = MessageBox.Show("With your free account, you can only add 10 expense records. \n\nDo you want to get the full access?",
                                                           "Buy Full Access", MessageBoxButton.OKCancel);

                if (result == MessageBoxResult.OK)
                {
                    if (await StaticValues.DB.BuyUltimateUser())
                    {
                        return true;
                    }
                    else
                        return false;
                }
                else
                    return false;
            }
        }

        private void PasswordEnter_Click(object sender, RoutedEventArgs e)
        {

            if (StaticValues.DB.CheckPassword(__tbPassword.Password))
            {
                __PasswordLayer.Background = new SolidColorBrush(Colors.Transparent);
                Duration duration = new Duration(TimeSpan.FromSeconds(0.8));
                Storyboard passwordUp = new Storyboard();
                passwordUp.Completed += passwordUp_Completed;
                DoubleAnimation passwordUpAnimation = new DoubleAnimation();

                BackEase easing = new BackEase();
                easing.EasingMode = EasingMode.EaseIn;
                easing.Amplitude = 0.4;
                passwordUpAnimation.EasingFunction = easing;

                passwordUpAnimation.Duration = duration;
                passwordUp.Children.Add(passwordUpAnimation);
                Storyboard.SetTarget(passwordUpAnimation, __grPassword);

                Storyboard.SetTargetProperty(passwordUpAnimation, new PropertyPath("(Canvas.top)"));

                passwordUpAnimation.To = -1200;

                passwordUp.Begin();
            }
            else
            {
                
                MessageBox.Show("It is Not Your Password! Try Again.");
                __tbPassword.Password = string.Empty;
            }

        }

        void passwordUp_Completed(object sender, EventArgs e)
        {
            __PasswordLayer.Visibility = System.Windows.Visibility.Collapsed;
            this.ApplicationBar.IsVisible = true;
        }

    }
}