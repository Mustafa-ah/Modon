﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xamarin.Forms;

namespace Maham.Converters
{
   public class ByteArrayToImage : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            ImageSource retSource = null;
            if (value != null)
            {
                byte[] imageAsBytes = (byte[])value;
                var stream = new MemoryStream(imageAsBytes);
                retSource = ImageSource.FromStream(() => stream);
            }

            if (retSource == null)
            {
                retSource = "camera";
            }

            return retSource;
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
