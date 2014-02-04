using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialManagerPhoneProject.Models
{
    public class CategoryDetailViewModel : INotifyPropertyChanged
    {
        public string Icon { get; set; }
        public string Income { get; set; }        
        public double Plan { get; set; }
        public string Name { get; set; }

        private string _TotalPlanned { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        public string TotalPlanned
        {
            get { return _TotalPlanned; }
            set
            {
                if (_TotalPlanned != value)
                {
                    _TotalPlanned = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new
                                PropertyChangedEventArgs("TotalPlanned"));
                }
            }
        }


    }
}
