using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace MarsRoverApp.Converters
{
    public class InputBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool input)
                return input ? "Input" : "Output";
            else
                return "Input";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string input)
                return input == "Input" ? true : false;
            else
                return true;
        }
    }
}
