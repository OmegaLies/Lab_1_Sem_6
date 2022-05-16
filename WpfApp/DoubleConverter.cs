using System;
using System.Windows.Data;
using System.Globalization;
using System.Windows;

namespace WpfApp
{
    class DoubleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                double val = (double)value;
                return val.ToString();
            }
            catch (Exception error)
            {
                MessageBox.Show($"Error: {error.Message}.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return "Null";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return 0;
        }
    }
}
