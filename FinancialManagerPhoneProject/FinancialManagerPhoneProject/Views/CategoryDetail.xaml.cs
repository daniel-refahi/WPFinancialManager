using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using FinancialManagerPhoneProject.Models;
using FinancialManagerPhoneProject.DataHandlers;
using System.Windows.Media.Imaging;
using System.IO.IsolatedStorage;
using Microsoft.Phone.Tasks;

namespace FinancialManagerPhoneProject.Views
{
    public partial class CategoryDetail : PhoneApplicationPage
    {
        string _Name = string.Empty;
        string _OldName = string.Empty;
        string _Status = string.Empty;
        string _IconSource = string.Empty;
        string _Plan = string.Empty;
        string _TotalPlanned = string.Empty;

        CategoryDetailViewModel _PageModel;

        public CategoryDetail()
        {
            InitializeComponent();            
        }
        
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            string caller = string.Empty;
            NavigationContext.QueryString.TryGetValue("caller", out caller);
            if (caller == "mainwindow")
            {
                // navigating from main window
                NavigationContext.QueryString.TryGetValue("status", out _Status);
                
                switch (_Status)
                {
                    case "add":
                        LoadPageFromAdd();
                        break;
                    case "update":
                        
                        break;
                }
            }
            else if (caller == "categorychart")
            {
                NavigationContext.QueryString.TryGetValue("categoryname", out _Name);
                NavigationContext.QueryString.TryGetValue("categoryname", out _OldName);
                LoadPageFromEdit();
            }
            else if (caller == "help")
            {
                NavigationContext.QueryString.TryGetValue("object", out _Name);
                LoadAppSettings(false);
            }
            else 
            {
                // navigating from icon selection page
                NavigationContext.QueryString.TryGetValue("source", out _IconSource);
                LoadAppSettings(true);
            }

            _PageModel = new CategoryDetailViewModel();
            _PageModel.Name = _Name;
            _PageModel.Income = StaticValues.DB.GetCurrencySymbol() + " " + StaticValues.DB.GetIncome().ToString();
            _PageModel.Icon = _IconSource;
            _PageModel.Plan = (_Plan == string.Empty) ? 0 : Convert.ToDouble(_Plan);
            if (string.IsNullOrEmpty(_TotalPlanned))
            {
                _TotalPlanned = StaticValues.DB.GetCurrencySymbol() + " " + StaticValues.DB.GetTotalPlan().ToString();                
            }
            _PageModel.TotalPlanned = _TotalPlanned;

            this.DataContext = _PageModel;
        }
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);            
            NavigationService.RemoveBackEntry();
        }

        private void LoadPageFromEdit()
        {
            _Status = "edit";
            __tbTitle.Text = "Edit Category";
            Category category = StaticValues.DB.GetCategoryObject(_Name);
            _Plan = category.Plan.ToString();
            _IconSource = "../Assets/Icons/" + category.Icon + ".png";

            UpdatingAppBar();
        }        
        private void LoadPageFromAdd()
        {
            _Plan = string.Empty;
            _Name = string.Empty;
            _IconSource = "../Assets/Icons/clothing.png";
        }        

        #region Buttons Events

        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            //base.OnBackKeyPress(e);
            
            if (_Status == "add")
                NavigationService.Navigate(new Uri("/Views/MainWindow.xaml?caller=categorydetail", UriKind.Relative));
            else
                NavigationService.Navigate(new Uri("/Views/CategoryChart.xaml?caller=categorydetail&categoryname=" + _OldName, UriKind.Relative));
        }

        void deleteIcon_Click(object sender, EventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Would you like to delete the category? Be aware that all expenses in this category will be removed as well!",
                                                            "Delete", MessageBoxButton.OKCancel);

            if (result == MessageBoxResult.OK)
            {
                if (StaticValues.DB.DeleteCategory(__tbName.Text.ToString()))
                    NavigationService.Navigate(new Uri("/Views/MainWindow.xaml?caller=categorydetail", UriKind.Relative));
                else
                    MessageBox.Show("The category can't be deleted, because there is no category as " + __tbName.Text.ToString() + " !");
            }
        }
        private void ApplicationBarHelpIcon_Click(object sender, EventArgs e)
        {
            SaveAppSettings();
            NavigationService.Navigate(new Uri("/Views/help.xaml?caller=categorydetail&status="+_Status+"&object=" + _Name, UriKind.Relative));
        }
        private void ApplicationBarSaveIcon_Click(object sender, EventArgs e)
        {
            double plan = 0;
            bool isNum = double.TryParse(StaticMethods.CleanNumber(__tbPlan.Text.ToString()), out plan);
            if (!isNum || plan <= 0)
                MessageBox.Show("Please Enter a Valid Plan!");
            else if(string.IsNullOrEmpty(__tbName.Text) || string.IsNullOrWhiteSpace(__tbName.Text))
            {
                MessageBox.Show("Please Enter a Valid Category Name!");
            }
            else
            {
                string icon = ((BitmapImage)(__Icon.Source)).UriSource.ToString();
                icon = icon.Substring(icon.LastIndexOf('/') + 1, icon.LastIndexOf('.') - icon.LastIndexOf('/') - 1);

                if (_Status == "add")
                {
                    if (StaticValues.DB.AddCategory(new Category()
                    {
                        Icon = icon,
                        Name = __tbName.Text.ToString(),
                        Plan = Convert.ToDouble(__tbPlan.Text.ToString()),
                        TotalExpenses = 0
                    }))
                    {
                        NavigationService.Navigate(new Uri("/Views/MainWindow.xaml?caller=categorydetail&categoryname=" + __tbName.Text.ToString(), UriKind.Relative));
                    }
                    else
                        MessageBox.Show("The Category Name Already Exists!");
                }
                if (_Status == "edit")
                {
                    StaticValues.DB.UpdateCategory(new Category()
                    {
                        Icon = icon,
                        Name = _OldName,
                        NewName = __tbName.Text.ToString(),
                        Plan = plan,
                        TotalExpenses = 0
                    });
                    NavigationService.Navigate(new Uri("/Views/CategoryChart.xaml?caller=categorydetail&categoryname=" + __tbName.Text.ToString(), UriKind.Relative));
                }
            }

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

        private void SaveAppSettings()
        {
            IsolatedStorageSettings.ApplicationSettings.Clear();
            IsolatedStorageSettings.ApplicationSettings["firsttime"] = "0";
            IsolatedStorageSettings.ApplicationSettings["name"] = __tbName.Text.ToString();
            IsolatedStorageSettings.ApplicationSettings["oldname"] = _OldName;
            IsolatedStorageSettings.ApplicationSettings["plan"] = __tbPlan.Text.ToString();
            IsolatedStorageSettings.ApplicationSettings["status"] = _Status;
            IsolatedStorageSettings.ApplicationSettings["totalplanned"] = _TotalPlanned;
            IsolatedStorageSettings.ApplicationSettings["iconsource"] = _IconSource;
            IsolatedStorageSettings.ApplicationSettings.Save();
        }
        private void LoadAppSettings(bool IsFromIconSelection)
        {
            IsolatedStorageSettings.ApplicationSettings.TryGetValue("name", out _Name);
            IsolatedStorageSettings.ApplicationSettings.TryGetValue("oldname", out _OldName);
            IsolatedStorageSettings.ApplicationSettings.TryGetValue("plan", out _Plan);
            IsolatedStorageSettings.ApplicationSettings.TryGetValue("status", out _Status);
            IsolatedStorageSettings.ApplicationSettings.TryGetValue("totalplanned", out _TotalPlanned);
            if(!IsFromIconSelection)
                IsolatedStorageSettings.ApplicationSettings.TryGetValue("iconsource", out _IconSource);
            IsolatedStorageSettings.ApplicationSettings.Clear();
            IsolatedStorageSettings.ApplicationSettings["firsttime"] = "0";
            IsolatedStorageSettings.ApplicationSettings.Save();

            if (_Status == "edit")
            {
                __tbTitle.Text = "Edit Category";
                UpdatingAppBar();
            }
        }
        private void UpdatingAppBar()
        {
            if (ApplicationBar.Buttons.Count == 2)
            {
                Uri uri = new Uri("//Image/delete.png", UriKind.Relative);
                ApplicationBarIconButton deleteIcon = new ApplicationBarIconButton() { Text = "Delete", IconUri = uri };
                deleteIcon.Click += deleteIcon_Click;
                ApplicationBar.Buttons.Insert(1, deleteIcon);
            }
        }

        private void __tbPlan_TextChanged(object sender, TextChangedEventArgs e)
        {
            double plan = 0;
            bool isNum = double.TryParse(__tbPlan.Text.ToString(), out plan);
            if (__tbPlan.Text == "")
            {
                isNum = true;
                plan = 0;
            }
            if (!isNum)
            {
                MessageBox.Show("Please Enter a Valid Amount!");
            }
            else
            {
                _TotalPlanned = StaticValues.DB.GetCurrencySymbol() + " " + (plan + StaticValues.DB.GetTotalPlan()).ToString();
                _PageModel.TotalPlanned = _TotalPlanned;
            }
        }
        private void Icon_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            SaveAppSettings();
            NavigationService.Navigate(new Uri("/Views/CategoryIconSelection.xaml?caller=categorydetail&source=" + 
                                                ((BitmapImage)((Image)sender).Source).UriSource.ToString(), UriKind.Relative));
        }
    }
}