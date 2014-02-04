using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialManagerPhoneProject.DataHandlers
{
    public class Category
    {
        public string Name { get; set; }
        public string NewName { get; set; }
        public double Plan { get; set; }
        public string Icon { get; set; }
        public double TotalExpenses { get; set; }
        public bool IsSelected { get; set; }
    }
}