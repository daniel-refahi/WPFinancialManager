using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialManagerPhoneProject.DataHandlers
{
    public class StaticValues
    {
        public static string CurrentVersion = "3";
        public static string NewFeatures = "New features:\n1. Your incomes are now monthly basis.\n2. You can save your receipts in your image gallery\n3. Fixing bugs related to update live tile\n4. Fixing bug related to receipt image\n5. Adding Shillings to currency list.\n1. You can now delete Expenses, Incomes separately.";

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
