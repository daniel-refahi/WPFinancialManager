﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialManagerPhoneProject.DataHandlers
{
    public class StaticValues
    {
        public static string CurrentVersion = "3.1";
        public static string NewFeatures = "New features:\n1. Fixing the decimal point issue in expense, income value and category plan value.";

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
