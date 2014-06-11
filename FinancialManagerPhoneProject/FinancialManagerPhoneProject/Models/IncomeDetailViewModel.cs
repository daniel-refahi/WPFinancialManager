using FinancialManagerPhoneProject.DataHandlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialManagerPhoneProject.Models
{
    public class IncomeDetailViewModel
    {
        public double ScreenWidth { get; set; }
        public string ID { get; set; }
        public double Value { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
    }
}
