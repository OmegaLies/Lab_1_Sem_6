using System;
using System.Windows;
using System.Windows.Data;
using System.Globalization;
using ClassLibrary;

namespace WpfApp
{
    public class AccurasyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                if (value != null)
                {
                    VMAccuracy val = (VMAccuracy)value;
                    string res = "Max Abs " + val.Abs.ToString() + " on " + val.x.ToString("F5");
                    return res;
                }
                return "";
            }
            catch (Exception error)
            {
                MessageBox.Show($"Error: {error.Message}.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return "Error";
            }
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return new VMTime();
        }
    }
}
