using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using Microsoft.AppCenter.Crashes;
using Maham.Helpers;
using Maham.iOS.Helper;
using UIKit;
using UserNotifications;

[assembly: Xamarin.Forms.Dependency(typeof(NotificationHelper))]
namespace Maham.iOS.Helper
{
    class NotificationHelper : INotificationHelper
    {
        public void Notify(string title, string body)
        {
            try
            {
                if (UIDevice.CurrentDevice.CheckSystemVersion(10, 0))
                {
                    var content = new UNMutableNotificationContent();
                    content.Title = title;
                    // content.Subtitle = "Notification Subtitle";
                    content.Body = body;
                    content.Badge = 1;

                    // New trigger time
                    var trigger = UNTimeIntervalNotificationTrigger.CreateTrigger(5, false);

                    // ID of Notification to be updated
                    var requestID = "RequestID";
                    var request = UNNotificationRequest.FromIdentifier(requestID, content, trigger);

                    // Add to system to modify existing Notification
                    UNUserNotificationCenter.Current.AddNotificationRequest(request, (err) => {
                        if (err != null)
                        {
                            // Do something with error...
                        }
                    });
                }

            }
            catch (Exception exception)
            {

                var properties = new Dictionary<string, string>
                       {
                             { "NotificationHelper", "Notify" },
                       };
                Crashes.TrackError(exception, properties);
            }
        }
    }
}