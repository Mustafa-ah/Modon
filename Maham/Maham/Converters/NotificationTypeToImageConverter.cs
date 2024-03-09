using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Maham.Models;
using Xamarin.Forms;

namespace Maham.Converters
{
    public class NotificationTypeToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var notificationType = (NotificationType)value;
            switch (notificationType)
            {
                case NotificationType.AddTask:
                    return "plus_ic.png";
                case NotificationType.UpdateTask:
                    return "pin_ic.png";
                case NotificationType.Completed:
                    return "pin_ic.png";
                case NotificationType.UrgentSupport:
                    return "notfiy.png";
                case NotificationType.TaskClosed:
                    return "close.png";
                case NotificationType.TaskReturnd:
                    return "refresh.png";
                case NotificationType.AddComment:
                    return "pin_ic.png";
                case NotificationType.notUrgent:
                    return "notfiy.png";
                default:
                    return "notfiy.png";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
