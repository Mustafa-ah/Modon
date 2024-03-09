using Microsoft.AppCenter.Analytics;
using ObjCRuntime;
using System;
using System.Collections.Generic;
using UserNotifications;

namespace Tasky.iOS
{
    public class UserNotificationCenterDelegate : UNUserNotificationCenterDelegate
    {
        #region Constructors
        public UserNotificationCenterDelegate()
        {
        }
        #endregion

        #region Override Methods
        public override void WillPresentNotification(UNUserNotificationCenter center, UNNotification notification, Action<UNNotificationPresentationOptions> completionHandler)
        {
            
          
           
            

            // Tell system to display the notification anyway or use
            // `None` to say we have handled the display locally.
            completionHandler(UNNotificationPresentationOptions.None);
        }
        #endregion

        public override void DidReceiveNotificationResponse(UNUserNotificationCenter center, UNNotificationResponse response, Action completionHandler)
        {
            var properties2 = new Dictionary<string, string>
                       {
                             { "UserNotificationCenterDelegate:  ","DidReceiveNotificationResponse" },
                       };
            Analytics.TrackEvent("DidReceiveNotificationResponse_New", properties2);
            base.DidReceiveNotificationResponse(center, response, completionHandler);
        }
    }
}