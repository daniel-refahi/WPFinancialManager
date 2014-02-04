using Microsoft.Phone.Shell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialManagerPhoneProject.DataHandlers
{
    public static class StaticMethods
    {
        public static string GenerateID() 
        {
            return Guid.NewGuid().ToString();
        }
    }
}
