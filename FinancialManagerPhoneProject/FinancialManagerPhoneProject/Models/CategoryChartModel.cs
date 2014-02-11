using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialManagerPhoneProject.Models
{
    public class CategoryChartModel
    {
        public CategoryChartModel()
        {
            Expenses = new List<CategoryChartExpenseModel>();
        }

        public double ScreenWidth { get; set; }
        public string Icon { get; set; }
        public string CategoryName { get; set; }
        public string TotalCategoryText { get; set; }
        public double TotalAll { get; set; }
        public double TotalCategory { get; set; }
        public List<CategoryChartExpenseModel> Expenses { get; set; }
    }

    public class CategoryChartExpenseModel
    {
        public string Date { get; set; }
        public string Amount { get; set; }
        public string Description { get; set; }
    }
}
