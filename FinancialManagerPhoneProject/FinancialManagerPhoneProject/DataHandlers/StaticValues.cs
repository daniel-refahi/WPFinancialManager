using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialManagerPhoneProject.DataHandlers
{
    public class StaticValues
    {
        public static XMLHandler DB;
        public static AppStatusOptions AppStatus;
        public enum AppStatusOptions
        {
            Expenses,
            Categories,
            Report
        };

    }
}
