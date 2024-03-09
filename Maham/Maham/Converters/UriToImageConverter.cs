using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace Maham.Converters
{
  public  class UriToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string image = "usercircle";

            if (value is string)
            {
                var imageValue = (string)value;
                if (!string.IsNullOrEmpty(imageValue))
                {
                    image = imageValue;
                }
            }

            return image;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}