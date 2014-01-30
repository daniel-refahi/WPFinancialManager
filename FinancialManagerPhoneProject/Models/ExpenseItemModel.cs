using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialManagerPhoneProject.Models
{
    public class ExpenseItemModel
    {
        public string ImageSource { get; set; }
        public string ID { get; set; }
        public string Amount { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string Date { get; set; }
        public double ScreenWidth { get; set; }
    }
}
