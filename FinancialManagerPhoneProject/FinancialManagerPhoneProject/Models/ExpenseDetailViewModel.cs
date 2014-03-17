using FinancialManagerPhoneProject.DataHandlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialManagerPhoneProject.Models
{
    public class ExpenseDetailViewModel
    {
        public double ScreenWidth { get; set; }
        public string ID { get; set; }
        public List<Category> Categories { get; set; }
        public Category SelectedCategory { get; set; }
        public double Amount { get; set; }
        public string RecieptName { get; set; }
        public string Latitude { get; set; }
        public string Longtitude { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string IconSource { get; set; }
    }
}
