using System;
using System.Windows;
using System.Windows.Data;
using System.Globalization;
using ClassLibrary;

namespace WpfApp
{
    public class TimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                if (value != null)
                {
                    VMTime val = (VMTime)value;
                    string res = "VML_EP to VML_HA: " + val.EP_to_HA.ToString() + "\nVML_LA to VML_HA: " + val.LA_to_HA.ToString();
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
