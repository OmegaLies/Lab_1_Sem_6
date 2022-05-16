using System;
using System.Windows;
using System.Windows.Data;
using System.Globalization;

namespace WpfApp
{
    public class ChangeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                bool val = (bool)value;
                if (val)
                {
                    return "Collection are changed";
                }
                else
                {
                    return "";
                }
            }
            catch (Exception error)
            {
                MessageBox.Show($"Error: {error.Message}.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return "Error";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return false;
        }
    }
}
