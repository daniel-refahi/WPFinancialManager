using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialManagerPhoneProject.DataHandlers
{
    public class StaticValues
    {
        public static string CurrentVersion = "4.2";
        public static string NewFeatures = "New features:\n1. Fixing Edit Category Bugs. \n2. Fixing Losing Data Bug.";

        public static XMLHandler DB;
        public static AppStatusOptions AppStatus;
        public enum AppStatusOptions
        {
            Expenses,
            Categories,
            Report,
            Incomes
        };

    }
}
