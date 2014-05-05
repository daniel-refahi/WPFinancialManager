using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialManagerPhoneProject.DataHandlers
{
    public class StaticValues
    {
        public static string CurrentVersion = "2";
        public static string NewFeatures = "New features:\n1. Your expenses are now monthly basis. \n2. You can change the month from \nsettings."+
                                           "\n3. Some bugs have been fixed. \n4. New items in settings. \n5. \"feedback\" and \"rate me\" links. ";

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
