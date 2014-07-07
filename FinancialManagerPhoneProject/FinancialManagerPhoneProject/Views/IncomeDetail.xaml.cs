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
    public partial class IncomeDetail : PhoneApplicationPage
    {
        IncomeDetailViewModel _PageModel;

        string _ID = string.Empty;
        string _Status = string.Empty;
        string _Value = string.Empty;
        string _Description = string.Empty;
        string _Date = string.Empty;
        string _HelperPage = string.Empty;
        string _Caller = string.Empty;

        public IncomeDetail()
        {
            InitializeComponent();
            this.Loaded += incomedetail_Loaded;            
        }

        void incomedetail_Loaded(object sender, RoutedEventArgs e)
        {        
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
            _PageModel = new IncomeDetailViewModel();
            _PageModel.ScreenWidth = XMLHandler.DEIVCE_WIDTH - 40;

            NavigationContext.QueryString.TryGetValue("caller", out _Caller);
            
            if (_HelperPage == "DatePickerPage")
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

            this.DataContext = null;
            this.DataContext = _PageModel;
  
        }
       
        private void LoadPageForEdit()
        {
            __tbTitle.Text = "Edit Income";
            
            Income Income = StaticValues.DB.GetIncome(_ID);
            _Value = Income.Value.ToString();
            _Description = Income.Description;
            _Date = Income.Date.ToShortDateString();
            _PageModel.Value = Convert.ToDouble(_Value);
            _PageModel.Date = Convert.ToDateTime(_Date);
            _PageModel.Description = _Description;
            _PageModel.ID = _ID;

            UpdateApplicationBar();
        }

        private void LoadPageForAdd()
        {
            __tbTitle.Text = "Add Income";

            _Value = string.Empty;
            _Description = string.Empty;
            _Date = DateTime.Today.ToShortDateString();
            _ID = string.Empty;
            _PageModel.Date = DateTime.Today;
            _PageModel.Value = 0;
            _PageModel.Description = _Description;
            _PageModel.ID = _ID;
        }

        private void LoadPageFromHelpers()
        {
            IsolatedStorageSettings.ApplicationSettings.TryGetValue("amount", out _Value);
            IsolatedStorageSettings.ApplicationSettings.TryGetValue("description", out _Description);
            IsolatedStorageSettings.ApplicationSettings.TryGetValue("date", out _Date);
            IsolatedStorageSettings.ApplicationSettings.TryGetValue("status", out _Status);
            IsolatedStorageSettings.ApplicationSettings.TryGetValue("id", out _ID);        
            IsolatedStorageSettings.ApplicationSettings.TryGetValue("helperpage", out _HelperPage);

            _PageModel.Value = Convert.ToDouble(_Value);
            _PageModel.Date = Convert.ToDateTime(_Date);
            _PageModel.Description = _Description;
            _PageModel.ID = _ID;

            if (_Status == "update")
                __tbTitle.Text = "Edit Income";                
            else if (_Status == "add")
                __tbTitle.Text = "Add Income";                
         
            UpdateApplicationBar();

        }
        private void LoadPageFromHelpPage() 
        {
            IsolatedStorageSettings.ApplicationSettings.TryGetValue("amount", out _Value);
            IsolatedStorageSettings.ApplicationSettings.TryGetValue("description", out _Description);
            IsolatedStorageSettings.ApplicationSettings.TryGetValue("date", out _Date);
            IsolatedStorageSettings.ApplicationSettings.TryGetValue("status", out _Status);
            IsolatedStorageSettings.ApplicationSettings.TryGetValue("id", out _ID);            

            _PageModel.Value = Convert.ToDouble(_Value);
            _PageModel.Date = Convert.ToDateTime(_Date);
            _PageModel.Description = _Description;
            _PageModel.ID = _ID;

            if (_Status == "update")
                __tbTitle.Text = "Edit Income";               
            else if (_Status == "add")
                __tbTitle.Text = "Add Income";
            
            UpdateApplicationBar();
        }
        
        private void SaveAppSettings()
        {
            IsolatedStorageSettings.ApplicationSettings.Clear();
            IsolatedStorageSettings.ApplicationSettings["firsttime"] = "0";
            IsolatedStorageSettings.ApplicationSettings["helperpage"] = _HelperPage;
            IsolatedStorageSettings.ApplicationSettings["amount"] = __tbAmount.Text.ToString();
            IsolatedStorageSettings.ApplicationSettings["description"] = __tbDescription.Text.ToString();
            IsolatedStorageSettings.ApplicationSettings["date"] = __dpDatepicker.Value.ToString();
            IsolatedStorageSettings.ApplicationSettings["status"] = _Status;
            IsolatedStorageSettings.ApplicationSettings["id"] = _ID;
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

        #region Buttons Events

        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            //base.OnBackKeyPress(e);
            NavigationService.Navigate(new Uri("/Views/MainWindow.xaml?caller=incomedetail", UriKind.Relative));
        }        

        void deleteIcon_Click(object sender, EventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Would you like to delete the Income?",
                                                            "Delete", MessageBoxButton.OKCancel);

            if (result == MessageBoxResult.OK)
            {
                StaticValues.DB.DeleteIncome(_ID);
                NavigationService.Navigate(new Uri("/Views/MainWindow.xaml?caller=incomedetail", UriKind.Relative));
            }
        }
        private void ApplicationBarSaveIcon_Click(object sender, EventArgs e)
        {

            double amount = 0;
            amount = StaticMethods.CleanNumber(__tbAmount.Text.ToString());
            if (__dpDatepicker.Value == null)
                MessageBox.Show("Please Select Date!");
            else if (__tbAmount.Text.ToString() == null || amount <= 0)
                MessageBox.Show("Please Enter a Valid Amount!");
            else
            {
                if (_Status == "update")
                {
                    StaticValues.DB.UpdateIncome(new Income()
                    {
                        ID = _ID,
                        Date = (DateTime)__dpDatepicker.Value,
                        Description = __tbDescription.Text.ToString(),
                        Value = amount
                    });
                }
                else
                {
                    StaticValues.DB.AddIncome(new Income()
                    {
                        Date = (DateTime)__dpDatepicker.Value,
                        Description = __tbDescription.Text.ToString(),
                        Value = amount
                    });
                }

                NavigationService.Navigate(new Uri("/Views/MainWindow.xaml?caller=incomedetail", UriKind.Relative));
            }
        }
        private void ApplicationBarHelpIcon_Click(object sender, EventArgs e)
        {            
            SaveAppSettings();
            NavigationService.Navigate(new Uri("/Views/help.xaml?caller=incomedetail&status="+_Status+"&object=" + _ID, UriKind.Relative));
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