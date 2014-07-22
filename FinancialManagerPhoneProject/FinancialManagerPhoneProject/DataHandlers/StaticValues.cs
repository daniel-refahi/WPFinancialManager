using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialManagerPhoneProject.DataHandlers
{
    public class StaticValues
    {
        public static string CurrentVersion = "4.3";
        public static string NewFeatures = "New features:\n1. Map location accuracy has been improved. \n2. Fixing some bugs, caused by the different language settings.  .";

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
