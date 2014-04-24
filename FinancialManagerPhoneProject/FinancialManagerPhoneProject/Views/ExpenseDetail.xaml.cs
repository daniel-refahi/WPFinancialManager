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
using System.Threading.Tasks;
using System.Threading;
using System.IO.IsolatedStorage;
using Microsoft.Phone.Tasks;
using System.IO;
using System.Windows.Media;
using Windows.Devices.Geolocation;
using System.Device.Location;

namespace FinancialManagerPhoneProject.Views
{
    public partial class ExpenseDetail : PhoneApplicationPage
    {
        CameraCaptureTask _CameraTask;
        public static byte[] _ImageAsByte;

        ExpenseDetailViewModel _PageModel;
        List<Category> _Categories;

        string _ID = string.Empty;
        string _Status = string.Empty;
        string _Amount = string.Empty;
        string _Description = string.Empty;
        string _Date = string.Empty;
        string _Category = string.Empty;
        string _Receipt = string.Empty;
        string _HelperPage = string.Empty;
        string _Caller = string.Empty;
        string _Latitude = string.Empty;
        string _Longtitude = string.Empty;

        public ExpenseDetail()
        {
            InitializeComponent();
            this.Loaded += ExpenseDetail_Loaded;            
        }

        void ExpenseDetail_Loaded(object sender, RoutedEventArgs e)
        {
            _CameraTask = new CameraCaptureTask();
            _CameraTask.Completed += new EventHandler<PhotoResult>(cameraTask_Completed);

            if (_Status == "update")
            {
                __liCategoryList.IsEnabled = false;
                HighlightCategory();
            }
            else if (_HelperPage == "DatePickerPage")
            {
                HighlightCategory();               
            }
        }

        void cameraTask_Completed(object sender, PhotoResult e)
        {
            if (e.TaskResult == TaskResult.OK)
            {
                _Receipt = StaticMethods.GenerateID() + ".jpg";
                _ImageAsByte = new byte[e.ChosenPhoto.Length];
                e.ChosenPhoto.Read(_ImageAsByte, 0, _ImageAsByte.Length);
                e.ChosenPhoto.Seek(0, SeekOrigin.Begin);                
            }
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);

            if (e.Uri.ToString().Contains("DatePickerPage"))
            {
                _HelperPage = "DatePickerPage";
                SaveAppSettings();
            }
            else
                NavigationService.RemoveBackEntry();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            // reseting the location button
            __btLocation.IsEnabled = true;
            __btLocationContent.Text = "Add My Location";
            __btLocationContent.Foreground = new SolidColorBrush(Colors.Black);
            __LoadingBar.Visibility = System.Windows.Visibility.Collapsed;

            _Categories = StaticValues.DB.GetAllCategories();
            _PageModel = new ExpenseDetailViewModel();
            _PageModel.ScreenWidth = XMLHandler.DEIVCE_WIDTH - 40;

            NavigationContext.QueryString.TryGetValue("caller", out _Caller);
            
            if (_HelperPage == "DatePickerPage" ||
                _HelperPage == "CameraPage"     ||
                _Caller == "recieptcamera")
            {
                LoadPageFromHelpers();
            }                      
            else if (_Caller == "mainwindow")
            {
                // navigating from main window
                NavigationContext.QueryString.TryGetValue("status", out _Status);

                switch (_Status)
                {
                    case "add":
                        LoadPageForAdd();
                        break;
                    case "update":
                        NavigationContext.QueryString.TryGetValue("ID", out _ID);
                        LoadPageForEdit();
                        break;
                }
            }
            else if (_Caller == "help")
            {
                // navigating from help page
                NavigationContext.QueryString.TryGetValue("object", out _ID);
                if (_ID != string.Empty)
                {
                    _Status = "update";
                    LoadPageFromHelpPage();
                }
                else
                {
                    _Status = "add";
                    LoadPageFromHelpPage();
                }
            }

            foreach (Category c in _Categories)
            {
                c.Icon = "../Assets/Icons/" + c.Icon + ".png";
                c.IsSelected = false;
                if (_Category == c.Name)
                    c.IsSelected = true;
            }
            
            _PageModel.Categories = _Categories;

            this.DataContext = null;
            this.DataContext = _PageModel;

            if (_HelperPage == "CameraPage"  ||
                _HelperPage == "ReceiptPage" ||
                _HelperPage == "DatePickerPage" ||
                _Caller == "help")
            {
                HighlightCategory();
            }
  
        }
       
        private void LoadPageForEdit()
        {
            __tbTitle.Text = "Edit Expense";
            
            Expense expense = StaticValues.DB.GetExpense(_ID);
            _Amount = expense.Value.ToString();
            _Description = expense.Description;
            _Receipt = expense.RecieptName;
            _Latitude = expense.Latitude;
            _Longtitude = expense.Longtitude;
            _Date = expense.Date.ToShortDateString();
            _Category = StaticValues.DB.GetCategoryName(_ID);
            

            _PageModel.Amount = Convert.ToDouble(_Amount);
            _PageModel.Date = Convert.ToDateTime(_Date);
            _PageModel.Description = _Description;
            _PageModel.ID = _ID;

            if (!string.IsNullOrEmpty(_Receipt))
                __btReceiptPic.Content = "Receipt";
            else
            {
                __btReceiptPic.Content = "No Receipt";
                __btReceiptPic.IsEnabled = false;                      
            }

            if (string.IsNullOrEmpty(_Latitude))
                HandleLocationBt(false);
            else
                HandleLocationBt(true);

            UpdateApplicationBar();
        }
        private void LoadPageForAdd()
        {
            __tbTitle.Text = "Add Expense";

            _Amount = string.Empty;
            _Description = string.Empty;
            _Date = DateTime.Today.ToShortDateString();
            _Category = string.Empty;
            _ID = string.Empty;
            _Latitude = string.Empty;
            _Longtitude = string.Empty;

            _PageModel.Date = DateTime.Today;
            _PageModel.Amount = 0;
            _PageModel.Description = _Description;
            _PageModel.ID = _ID;
        }
        private void LoadPageFromHelpers()
        {
            IsolatedStorageSettings.ApplicationSettings.TryGetValue("amount", out _Amount);
            IsolatedStorageSettings.ApplicationSettings.TryGetValue("description", out _Description);
            IsolatedStorageSettings.ApplicationSettings.TryGetValue("date", out _Date);
            IsolatedStorageSettings.ApplicationSettings.TryGetValue("status", out _Status);
            IsolatedStorageSettings.ApplicationSettings.TryGetValue("category", out _Category);
            IsolatedStorageSettings.ApplicationSettings.TryGetValue("id", out _ID);
            if(string.IsNullOrEmpty(_Receipt))
                IsolatedStorageSettings.ApplicationSettings.TryGetValue("receipt", out _Receipt);
            IsolatedStorageSettings.ApplicationSettings.TryGetValue("helperpage", out _HelperPage);
            IsolatedStorageSettings.ApplicationSettings.TryGetValue("longtitude", out _Longtitude);
            IsolatedStorageSettings.ApplicationSettings.TryGetValue("latitude", out _Latitude);

            _PageModel.Amount = Convert.ToDouble(_Amount);
            _PageModel.Date = Convert.ToDateTime(_Date);
            _PageModel.Description = _Description;
            _PageModel.ID = _ID;

            if (_Status == "update")
            {
                __tbTitle.Text = "Edit Expense";
                if (string.IsNullOrEmpty(_Receipt))
                {
                    __btReceiptPic.Content = "No Receipt";
                    __btReceiptPic.IsEnabled = false;
                }
                else
                {
                    __btReceiptPic.Content = "Receipt";
                    __btReceiptPic.IsEnabled = true;
                }
            }
            else if (_Status == "add")
            {
                __tbTitle.Text = "Add Expense";
                if (string.IsNullOrEmpty(_Receipt))
                {
                    __btReceiptPic.Content = "Attach Receipt";
                    __btReceiptPic.IsEnabled = true;
                }
                else
                {
                    __btReceiptPic.Content = "Receipt Attached";
                    __btReceiptPic.IsEnabled = false;
                }
            }

            if (string.IsNullOrEmpty(_Latitude))
                HandleLocationBt(false);
            else
                HandleLocationBt(true);
         
            UpdateApplicationBar();

        }
        private void LoadPageFromHelpPage() 
        {
            IsolatedStorageSettings.ApplicationSettings.TryGetValue("amount", out _Amount);
            IsolatedStorageSettings.ApplicationSettings.TryGetValue("description", out _Description);
            IsolatedStorageSettings.ApplicationSettings.TryGetValue("date", out _Date);
            IsolatedStorageSettings.ApplicationSettings.TryGetValue("status", out _Status);
            IsolatedStorageSettings.ApplicationSettings.TryGetValue("category", out _Category);
            IsolatedStorageSettings.ApplicationSettings.TryGetValue("id", out _ID);            
            IsolatedStorageSettings.ApplicationSettings.TryGetValue("receipt", out _Receipt);
            IsolatedStorageSettings.ApplicationSettings.TryGetValue("latitude", out _Latitude);
            IsolatedStorageSettings.ApplicationSettings.TryGetValue("longtitude", out _Longtitude);

            _PageModel.Amount = Convert.ToDouble(_Amount);
            _PageModel.Date = Convert.ToDateTime(_Date);
            _PageModel.Description = _Description;
            _PageModel.ID = _ID;

            if (_Status == "update")
            {
                __tbTitle.Text = "Edit Expense";
                if (string.IsNullOrEmpty(_Receipt))
                {
                    __btReceiptPic.Content = "No Receipt";
                    __btReceiptPic.IsEnabled = false;
                }
                else
                {
                    __btReceiptPic.Content = "Receipt";
                    __btReceiptPic.IsEnabled = true;
                }
            }
            else if (_Status == "add")
            {
                __tbTitle.Text = "Add Expense";
                if (string.IsNullOrEmpty(_Receipt))
                {
                    __btReceiptPic.Content = "Attach Receipt";
                    __btReceiptPic.IsEnabled = true;
                }
                else
                {
                    __btReceiptPic.Content = "Receipt Attached";
                    __btReceiptPic.IsEnabled = false;
                }
            }

            if (string.IsNullOrEmpty(_Latitude))
                HandleLocationBt(false);
            else
                HandleLocationBt(true);

            UpdateApplicationBar();
        }
        private void HandleLocationBt(bool IsLocaionAvailable) 
        {
            if (IsLocaionAvailable)
            {
                if (_Status == "add")
                {
                    __btLocation.IsEnabled = false;
                    __btLocationContent.Text = "Location Added";
                    __btLocationContent.Foreground = new SolidColorBrush(Color.FromArgb(255, 102, 99, 98));
                }
                else
                {
                    __btLocationContent.Text = "See Location";
                    __btLocation.IsEnabled = true;
                }
            }
            else
            {
                if (_Status == "add")
                {
                    // you don't need to do anything about this
                }
                else
                {
                    __btLocationContent.Foreground = new SolidColorBrush(Color.FromArgb(255, 102, 99, 98));
                    __btLocationContent.Text = "No Location";
                    __btLocation.IsEnabled = false;
                }
            }
        }

        private void SaveAppSettings()
        {
            IsolatedStorageSettings.ApplicationSettings.Clear();
            IsolatedStorageSettings.ApplicationSettings["firsttime"] = "0";
            IsolatedStorageSettings.ApplicationSettings["helperpage"] = _HelperPage;
            IsolatedStorageSettings.ApplicationSettings["amount"] = __tbAmount.Text.ToString();
            IsolatedStorageSettings.ApplicationSettings["description"] = __tbDescription.Text.ToString();
            IsolatedStorageSettings.ApplicationSettings["date"] = __dpDatepicker.Value.ToString();
            IsolatedStorageSettings.ApplicationSettings["receipt"] = _Receipt;
            IsolatedStorageSettings.ApplicationSettings["category"] = _Category;
            IsolatedStorageSettings.ApplicationSettings["status"] = _Status;
            IsolatedStorageSettings.ApplicationSettings["id"] = _ID;
            IsolatedStorageSettings.ApplicationSettings["latitude"] = _Latitude;
            IsolatedStorageSettings.ApplicationSettings["longtitude"] = _Longtitude;
            IsolatedStorageSettings.ApplicationSettings.Save();
        }

        private void UpdateApplicationBar()
        {
            // adding delete icon
            if (ApplicationBar.Buttons.Count == 2 && _Status == "update")
            {
                Uri uri = new Uri("//Image/delete.png", UriKind.Relative);
                ApplicationBarIconButton deleteIcon = new ApplicationBarIconButton() { Text = "Delete", IconUri = uri };
                deleteIcon.Click += deleteIcon_Click;
                ApplicationBar.Buttons.Insert(1, deleteIcon);
            }
            else if( ApplicationBar.Buttons.Count == 3 && _Status == "add")
            {
                ApplicationBar.Buttons.RemoveAt(1);
            }
        }

        private void HighlightCategory()
        {
            foreach (Category category in __liCategoryList.Items)
            {
                if (category.IsSelected)
                {
                    __liCategoryList.SelectedItem = category;
                }
            }
            __liCategoryList.ScrollIntoView(__liCategoryList.SelectedItem);
        }

        #region Buttons Events

        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            //base.OnBackKeyPress(e);
            NavigationService.Navigate(new Uri("/Views/MainWindow.xaml?caller=expensedetail", UriKind.Relative));
        }        

        private void __liCategoryList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (__liCategoryList.SelectedItem != null)
                _Category = ((Category)__liCategoryList.SelectedItem).Name;
        }

        private void __btReceiptPic_Click(object sender, RoutedEventArgs e)
        {          
            if (string.IsNullOrEmpty(_Receipt))
            {                
                _HelperPage = "CameraPage";
                _CameraTask.Show();
            }
            else
            {
                _HelperPage = "ReceiptPage";
                NavigationService.Navigate(new Uri("/Views/RecieptCamera.xaml?caller=expensedetail&object=" + _ID +
                    "&image=" + _Receipt,
                    UriKind.Relative));
            }
            SaveAppSettings();
        }
        private async void __btLocation_Click(object sender, RoutedEventArgs e)
        {
            if (_Status == "add")
            {
                __LoadingBar.Visibility = System.Windows.Visibility.Visible;
                __btLocationContent.Text = "Loading";
                __btLocation.IsEnabled = false;

                try
                {
                    Geolocator geolocator = new Geolocator();
                    geolocator.DesiredAccuracyInMeters = 50;

                    Geoposition geoposition = await geolocator.GetGeopositionAsync(
                        maximumAge: TimeSpan.FromMinutes(5),
                        timeout: TimeSpan.FromSeconds(10)
                        );
                    _Latitude = geoposition.Coordinate.Latitude.ToString("0.00");
                    _Longtitude = geoposition.Coordinate.Longitude.ToString("0.00");
                    __btLocationContent.Text = "Location Added";
                    __btLocationContent.Foreground = new SolidColorBrush(Color.FromArgb(255, 102, 99, 98));

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Couldn't get you location info!");
                    __btLocation.IsEnabled = true;
                    __btLocationContent.Text = "Add My Location";
                }
                finally
                {
                    __LoadingBar.Visibility = System.Windows.Visibility.Collapsed;
                }
            }
            else
            {
                SaveAppSettings();
                NavigationService.Navigate(new Uri("/Views/MapView.xaml?caller=expensedetail&longtitude=" + _Longtitude +
                    "&latitude=" + _Latitude + "", UriKind.Relative));
            }

            
        }

        void deleteIcon_Click(object sender, EventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Would you like to delete the expense?",
                                                            "Delete", MessageBoxButton.OKCancel);

            if (result == MessageBoxResult.OK)
            {
                StaticValues.DB.DeleteExpense(_ID);
                NavigationService.Navigate(new Uri("/Views/MainWindow.xaml?caller=expensedetail", UriKind.Relative));
            }
        }
        private void ApplicationBarSaveIcon_Click(object sender, EventArgs e)
        {

            double amount = 0;
            bool isNum = double.TryParse(__tbAmount.Text.ToString(), out amount);
            if (!isNum)
                MessageBox.Show("Please Enter a Valid Amount!");
            else if (__dpDatepicker.Value == null)
                MessageBox.Show("Please Select Date!");
            else if (__tbAmount.Text.ToString() == null || amount <= 0)
                MessageBox.Show("Please Enter a Valid Amount!");
            else if (__liCategoryList.SelectedItem == null)
                MessageBox.Show("Please Select a Category!");
            else
            {
                if (_Status == "update")
                {
                    StaticValues.DB.UpdateExpense(new Expense()
                    {
                        ID = _ID,
                        Category = ((Category)__liCategoryList.SelectedItem).Name,
                        RecieptName = _Receipt,
                        Latitude = (string.IsNullOrEmpty(_Latitude))? string.Empty : _Latitude,
                        Longtitude = (string.IsNullOrEmpty(_Longtitude)) ? string.Empty : _Longtitude,
                        Date = (DateTime)__dpDatepicker.Value,
                        Description = __tbDescription.Text.ToString(),
                        Value = amount
                    });
                }
                else
                {
                    StaticValues.DB.AddExpense(new Expense()
                    {
                        Category = ((Category)__liCategoryList.SelectedItem).Name,
                        Date = (DateTime)__dpDatepicker.Value,
                        RecieptName = _Receipt,
                        Latitude = (string.IsNullOrEmpty(_Latitude)) ? string.Empty : _Latitude,
                        Longtitude = (string.IsNullOrEmpty(_Longtitude)) ? string.Empty : _Longtitude,
                        Description = __tbDescription.Text.ToString(),
                        Value = amount
                    });
                }
                if(!string.IsNullOrEmpty(_Receipt))
                    StaticValues.DB.SaveImageAsync(_ImageAsByte, _Receipt);

                NavigationService.Navigate(new Uri("/Views/MainWindow.xaml?caller=expensedetail", UriKind.Relative));
            }
        }
        private void ApplicationBarHelpIcon_Click(object sender, EventArgs e)
        {            
            SaveAppSettings();
            NavigationService.Navigate(new Uri("/Views/help.xaml?caller=expensedetail&status="+_Status+"&object=" + _ID, UriKind.Relative));
        }
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
        #endregion

    }
}