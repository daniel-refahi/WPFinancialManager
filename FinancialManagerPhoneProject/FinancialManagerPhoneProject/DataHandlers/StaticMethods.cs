using Microsoft.Phone.Shell;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialManagerPhoneProject.DataHandlers
{
    public static class StaticMethods
    {
        public static Random rd = new Random();
        public static string GenerateID() 
        {
            return Guid.NewGuid().ToString();
        }

        public static string GetMonthSymbol(int month)
        {
            switch (month)
            {
                case 1:
                    return "Jan";
                case 2:
                    return "Feb";
                case 3:
                    return "Mar";
                case 4:
                    return "Apr";
                case 5:
                    return "May";
                case 6:
                    return "Jun";
                case 7:
                    return "Jul";
                case 8:
                    return "Aug";
                case 9:
                    return "Sep";
                case 10:
                    return "Oct";
                case 11:
                    return "Nov";
                default:
                    return "Dec";
            }
        }

        public static string GetMonthNumber(string month)
        {
            switch (month)
            {
                case "Jan":
                    return "1";
                case "Feb":
                    return "2";
                case "Mar":
                    return "3";
                case "Apr":
                    return "4";
                case "May":
                    return "5";
                case "Jun":
                    return "6";
                case "Jul":
                    return "7";
                case "Aug":
                    return "8";
                case "Sep":
                    return "9";
                case "Oct":
                    return "10";
                case "Nov":
                    return "11";
                default:
                    return "12";
            }
        }

        public static string DefaultRandomDate()
        {
            return rd.Next(1, 29) + "/" + DateTime.Today.Month + "/" + DateTime.Today.Year;
        }

        public static double CleanNumber(string number)
        {
            string cleanNumber = number;
            cleanNumber = cleanNumber.Replace(',', '&');
            cleanNumber = cleanNumber.Replace('\'', '&');
            cleanNumber = cleanNumber.Replace('.', '&');
            cleanNumber = cleanNumber.Replace('\\', '&');
            cleanNumber = cleanNumber.Replace('/', '&');

            string temp = cleanNumber.Replace('&','.');
            try
            {
                double value = Convert.ToDouble(temp, new CultureInfo("en-us"));

                return value;
            }
            catch 
            {
                return 0;
            }
        }
    }
}
