using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.IO.IsolatedStorage;
using System.IO;
using Microsoft.Phone;
using System.Windows.Media.Imaging;

namespace FinancialManagerPhoneProject
{
    public class ConverterClass : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            using (var store = IsolatedStorageFile.GetUserStoreForApplication())
            {
                if (value != null)
                {
                    WriteableBitmap pict = null;
                    var imageStream = store.OpenFile((string)value, FileMode.Open, FileAccess.Read);
                    pict = PictureDecoder.DecodeJpeg(imageStream);
                    return pict;
                }
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
