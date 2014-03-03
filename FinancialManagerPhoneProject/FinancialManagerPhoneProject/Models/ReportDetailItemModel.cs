using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialManagerPhoneProject.Models
{
    public class ReportDetailItemModel
    {
        public string ImageSource { get; set; }
        public string CategoryName { get; set; }
        public double TotalExpenses { get; set; }
        public double CategoryExpenses { get; set; }
        public double GaugeCategoryExpenses { get; set; }
        public double Plan { get; set; }
        public double ScreenWidth { get; set; }
    }
}
