using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FinancialManagerPhoneProject.Models
{
    public class MainPageModel
    {
        public MainPageModel() 
        {
            ExpenseListModel = new List<ExpenseItemModel>();
            CategoryListModel = new List<CategoryItemModel>();            
        }

        public List<ExpenseItemModel> ExpenseListModel { get; set; }
        public List<CategoryItemModel> CategoryListModel { get; set; }

        public double ScreenWidth { get; set; }
        public string TotalExpenses { get; set; }
        public string Saving { get; set; }
        public string Income { get; set; }
        public string MonthYear { get; set; }
        
        public string Balance { get; set; }

        public string CategoryName1 { get; set; }
        public double TotalCategory1 { get; set; }
        public string CategoryName2 { get; set; }
        public double TotalCategory2 { get; set; }
        public string CategoryName3 { get; set; }
        public double TotalCategory3 { get; set; }
        public string CategoryName4 { get; set; }
        public double TotalCategory4 { get; set; }
        public string CategoryName5 { get; set; }
        public double TotalCategory5 { get; set; }

    }
}
