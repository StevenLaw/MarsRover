using System;
using System.Globalization;
using Xamarin.Forms;

namespace MarsRoverApp.ViewModel
{
    //class CharEnumConverter : IValueConverter
    //{
    //    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    //    {
    //        if (value is Direction)
    //        {
    //            try
    //            {
    //                return (char)value;
    //            }
    //            catch (Exception)
    //            {
    //                return 'N';
    //            }
    //        }
    //        return 'N';
    //    }

    //    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    //    {
    //        if (value is char)
    //        {
    //            Enum.TryParse(value.ToString(), out Direction tmp);
    //            return tmp;
    //        } 
    //        else if (value is string)
    //        {
    //            Enum.TryParse(value.ToString(), out Direction tmp);
    //            return tmp;
    //        }
    //        return Direction.North;
    //    }
    //}
}
