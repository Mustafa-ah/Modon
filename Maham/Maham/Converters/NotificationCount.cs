using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace Maham.Converters
{
    public class NotificationCount : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                var notificationCount = (int)value;
                switch (notificationCount)
                {
                    case 0:
                        return "notification_0.png";
                    case 1:
                        return "notification_1.png";
                    case 2:
                        return "notification_2.png";
                    case 3:
                        return "notification_3.png";
                    case 4:
                        return "notification_4.png";
                    case 5:
                        return "notification_5.png";
                    default:
                        return "notification_plus5.png";
                }
            }
            catch  
            {
                return "notification_0.png";
            }
            
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
