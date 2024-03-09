using Foundation;
using Plugin.FirebasePushNotification;
using Naxam.Controls.Platform.iOS;
using Prism;
using Prism.Ioc;
using SuaveControls.FloatingActionButton.iOS.Renderers;
using UIKit;
using Microsoft.AppCenter.Distribute;
using UserNotifications;
using System.Collections.Generic;
using Microsoft.AppCenter.Analytics;
using Maham.Service.General;
using System;
using Microsoft.AppCenter.Crashes;
using Syncfusion.XForms.iOS.TreeView;

namespace Maham.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
           // Rg.Plugins.Popup.Popup.Init();
           
            //Syncfusion.XForms.iOS.ComboBox.SfComboBoxRenderer.Init();
            Syncfusion.XForms.iOS.ProgressBar.SfCircularProgressBarRenderer.Init();
            Rg.Plugins.Popup.Popup.Init();
            // UserDialogs.Init(this);
            Syncfusion.SfChart.XForms.iOS.Renderers.SfChartRenderer.Init();

            global::Xamarin.Forms.Forms.Init();
            FloatingActionButtonRenderer.InitRenderer();

            SfTreeViewRenderer.Init();
            Syncfusion.XForms.iOS.Border.SfBorderRenderer.Init();
            Syncfusion.XForms.iOS.Buttons.SfButtonRenderer.Init();

            TopTabbedRenderer.Init();
            Distribute.DontCheckForUpdatesInDebug();
            LoadApplication(new App(new iOSInitializer()));
            //just make sure that changes pushed

            FirebasePushNotificationManager.Initialize(options, true);

            FirebasePushNotificationManager.CurrentNotificationPresentationOption = 
                UNNotificationPresentationOptions.Alert | UNNotificationPresentationOptions.Badge;

            //Xamarin does not load the renderer rendered assemblies, by default, in iOS and UWP projects
            new Syncfusion.SfGauge.XForms.iOS.SfGaugeRenderer();


            // Request notification permissions from the user
            UNUserNotificationCenter.Current.RequestAuthorization(UNAuthorizationOptions.Alert, (approved, err) => {
                // Handle approval
                var properties2 = new Dictionary<string, string>
                       {
                             { "AppDelegate:  ","Handle approval" },
                       };
                Analytics.TrackEvent("RequestAuthorization", properties2);
            });

            // Watch for notifications while the app is active
            //UNUserNotificationCenter.Current.Delegate = new UserNotificationCenterDelegate();

            // CrossFirebasePushNotification.Current.OnNotificationReceived += Current_OnNotificationReceived;

            CrossFirebasePushNotification.Current.OnNotificationOpened += async (s, p) =>
            {
                try
                {
                    if (p.Data.Count > 0 && p.Data.ContainsKey("taskId"))
                    {
                        string taskId_ = p.Data["taskId"].ToString();
                        Setting.Settings.TaskId = taskId_;
                        await ((App)App.Current).ExtResolve<INavService>().NavigateToAsync<ViewModels.TaskDetailsPageViewModel>(taskId_);
                    }
                }
                catch (Exception ex)
                {
                    Crashes.TrackError(ex);
                }
            };
           
            return base.FinishedLaunching(app, options);
        }

        private void Current_OnNotificationReceived(object source, Plugin.FirebasePushNotification.Abstractions.FirebasePushNotificationDataEventArgs p)
        {
            string title = "Maham";
            string body = " ";
            // int notificationType = 1;
            try
            {
                if (!Maham.Setting.Settings.AllowPushNotification)
                {
                    return;
                }
                bool rtl = Maham.Setting.Settings.AppLang.Contains("ar");
                if (rtl)
                {
                    title = p.Data["titleAr"].ToString();
                    body = p.Data["messageBodyAr"].ToString();
                }
                else
                {
                    title = p.Data["title"].ToString();
                    body = p.Data["messageBody"].ToString();
                }

                Maham.Setting.Settings.TaskId = p.Data["taskId"].ToString();
                // Int32.TryParse(p.Data["NotificatonType"].ToString(), out notificationType);
            }
            catch
            {

            }
            new Helper.NotificationHelper().Notify(title, body);
        }

        public override void RegisteredForRemoteNotifications(UIApplication application, NSData deviceToken)
        {
            var properties2 = new Dictionary<string, string>
                       {
                             { "AppDelegate:  deviceToken ",deviceToken.ToString() },
                       };
            Analytics.TrackEvent("RegisteredForRemoteNotifications", properties2);
            FirebasePushNotificationManager.DidRegisterRemoteNotifications(deviceToken);
        }

        public override void FailedToRegisterForRemoteNotifications(UIApplication application, NSError error)
        {
            var properties2 = new Dictionary<string, string>
                       {
                             { "AppDelegate:  "," approval fail" },
                       };
            Analytics.TrackEvent("FailedToRegisterForRemoteNotifications", properties2);
            FirebasePushNotificationManager.RemoteNotificationRegistrationFailed(error);
        }
        // To receive notifications in foregroung on iOS 9 and below.
        // To receive notifications in background in any iOS version
        public override void DidReceiveRemoteNotification(UIApplication application, NSDictionary userInfo, System.Action<UIBackgroundFetchResult> completionHandler)
        {
            var properties2 = new Dictionary<string, string>
                       {
                             { "AppDelegate:  ","DidReceiveRemoteNotification" },
                       };
            Analytics.TrackEvent("DidReceiveRemoteNotification", properties2);
            // If you are receiving a notification message while your app is in the background,
            // this callback will not be fired 'till the user taps on the notification launching the application.

            // If you disable method swizzling, you'll need to call this method. 
            // This lets FCM track message delivery and analytics, which is performed
            // automatically with method swizzling enabled.
            FirebasePushNotificationManager.DidReceiveMessage(userInfo);
            // Do your magic to handle the notification data
            System.Console.WriteLine(userInfo);

            completionHandler(UIBackgroundFetchResult.NewData);
        }
    }

    public class iOSInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IContainerRegistry container)
        {

        }
    }
}
