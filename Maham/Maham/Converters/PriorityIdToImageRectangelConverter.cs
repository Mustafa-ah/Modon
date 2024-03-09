using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Maham.Models;
using Xamarin.Forms;

namespace Maham.Converters
{
    public class PriorityIdToImageRectangelConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var priorityId = (int)value;
            switch (priorityId)
            {
                case 1:
                    return  (Color)App.Current.Resources["LowColor"];
                case 2:
                    return (Color)App.Current.Resources["NormalColor"]; 
                case 3:
                    return (Color)App.Current.Resources["HighColor"];
                case 4:
                    return (Color)App.Current.Resources["CriticalColor"];

                default:
                    return (Color)App.Current.Resources["LowColor"];
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
