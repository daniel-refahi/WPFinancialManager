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
        public string Income { get; set; }
        
        public string Balance { get; set; }

    }
}
