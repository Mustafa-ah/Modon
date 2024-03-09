using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Media;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Plugin.FirebasePushNotification;
using Java.Lang;
using Android.Support.V4.App;
using Maham.Models;
using Notification = Android.App.Notification;
using Microsoft.AppCenter.Crashes;
using Maham.Service.General;
using System.Threading.Tasks;
using Plugin.CurrentActivity;

namespace Maham.Droid
{
#if DEBUG
    [Application(Debuggable = true)]
#else
	[Application(Debuggable = false)]
#endif
    public class MainApplication : Application
    {
        public static MainApplication instance;
        public static Activity activity;
        public static MainApplication GetInstance()
        {
            return instance;
        }
        public MainApplication(IntPtr handle, JniHandleOwnership transer) : base(handle, transer)
        {
        }

        public override void OnCreate()
        {
            base.OnCreate();

            CrossCurrentActivity.Current.Init(this);

            StrictMode.VmPolicy.Builder builder = new StrictMode.VmPolicy.Builder();
            StrictMode.SetVmPolicy(builder.Build());
            //Set the default notification channel for your app when running Android Oreo
            if (Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.O)
            {
                //Change for your default notification channel id here
                FirebasePushNotificationManager.DefaultNotificationChannelId = "FirebasePushNotificationChannel";

                //Change for your default notification channel name here
                FirebasePushNotificationManager.DefaultNotificationChannelName = "General";
            }


            //If debug you should reset the token each time.
#if DEBUG
            FirebasePushNotificationManager.Initialize(this, true);
#else
              FirebasePushNotificationManager.Initialize(this,false);
#endif
            /*
            //Handle notification when app is closed here
            CrossFirebasePushNotification.Current.OnNotificationReceived += (s, p) =>
            {
                try
                {
                    //ConfigNotification();
                    if (!Maham.Setting.Settings.AllowPushNotification)
                    {
                        return;
                    }
                    string title = "Maham";
                    string body = " ";
                   // int notificationType = 1;
                    try
                    {
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

                    MakeCustomNotification(1, title, body);
                    //}
                }
                catch (System.Exception exception)
                {
                   
                    var properties = new Dictionary<string, string>
                       {
                             { "mainapplication", "currentonnotified" },
                       };
                    Crashes.TrackError(exception, properties);
                }
                // MakeCustomNotification(0, "", "");
            };
            */

            CrossFirebasePushNotification.Current.OnNotificationOpened += async (s, p) =>
            {
                try
                {
                    if (p.Data.Count > 0 && p.Data.ContainsKey("taskId"))
                    {
                        string taskId_ = p.Data["taskId"].ToString();
                        Setting.Settings.TaskId = taskId_;
                        await Task.Delay(700);
                        await ((App)App.Current).ExtResolve<INavService>().NavigateToAsync<ViewModels.TaskDetailsPageViewModel>(taskId_);
                    }
                }
                catch (System.Exception ex)
                {
                    Crashes.TrackError(ex);
                }
            };
            // handel un handeled exceptions
            instance = this;
            AndroidEnvironment.UnhandledExceptionRaiser += AndroidEnvironmentOnUnhandledExceptionRaiser;
        }

        private void AndroidEnvironmentOnUnhandledExceptionRaiser(object sender, RaiseThrowableEventArgs e)
        {
            var properties = new Dictionary<string, string>
                {
                    { "MainApplication", "LiveAppCrashes" }
                };
            Crashes.TrackError(e.Exception, properties);
            //https://github.com/Metlina/XamarinRestartAndroidAppOnCrash/blob/master/RestartAndroidForms/RestartAndroidForms.Android/MyApplication.cs
            var intent = new Intent(activity, typeof(MainActivity));
            intent.PutExtra("crash", true);
            intent.AddFlags(ActivityFlags.ClearTop | ActivityFlags.ClearTask | ActivityFlags.NewTask);

            var pendingIntent = PendingIntent.GetActivity(MainApplication.instance, 0, intent, PendingIntentFlags.OneShot);

            var mgr = (AlarmManager)MainApplication.instance.GetSystemService(Context.AlarmService);
            mgr.Set(AlarmType.Rtc, DateTime.Now.Millisecond + 100, pendingIntent);

            activity.Finish();

            JavaSystem.Exit(2);
        }

        protected override void Dispose(bool disposing)
        {
            AndroidEnvironment.UnhandledExceptionRaiser -= AndroidEnvironmentOnUnhandledExceptionRaiser;
            base.Dispose(disposing);
        }

        void ConfigNotification()
        {
            FirebasePushNotificationManager.SoundUri = RingtoneManager.GetDefaultUri(RingtoneType.Notification);
            FirebasePushNotificationManager.IconResource = Resource.Drawable.AppIcon;//SetIconSource(notificationType);
        }

        int SetIconSource(NotificationType notificationType)
        {
            switch (notificationType)
            {
                case NotificationType.AddTask:
                    return Resource.Drawable.plus_ic;
                case NotificationType.UpdateTask:
                    return Resource.Drawable.pin_ic;
                case NotificationType.Completed:
                    return Resource.Drawable.pin_ic;
                case NotificationType.UrgentSupport:
                    return Resource.Drawable.notfiy;
                case NotificationType.TaskClosed:
                    return Resource.Drawable.close;
                case NotificationType.TaskReturnd:
                    return Resource.Drawable.refresh;
                case NotificationType.AddComment:
                    return Resource.Drawable.pin_ic;
                case NotificationType.notUrgent:
                    return Resource.Drawable.notfiy;
                default:
                    return Resource.Drawable.notfiy;
            }
        }

        public void MakeCustomNotification(int notificationId, string title, string message)
        {
            try
            {
               // iconType = SetIconSource((NotificationType)iconType);

                Context context = Application.Context;
                Intent intent = new Intent();
                Intent resultIntent = new Intent(context, typeof(MainActivity));
                resultIntent.PutExtra("OpenTaskDetails", true);
                Log.Info("CUSTOM NOTIFICATION UI", "MakeCustomNotification Entered");
                Android.Support.V4.App.TaskStackBuilder stackBuilder = Android.Support.V4.App.TaskStackBuilder.Create(context);

                Java.Lang.Class c = Class.FromType(typeof(MainActivity));
                stackBuilder.AddParentStack(c);
                stackBuilder.AddNextIntent(resultIntent);

               // string alert = intent.GetStringExtra("Alert");
                int number = intent.GetIntExtra("Badge", 0);

                Notification notification = new Notification();

                //long dateInMilliSeconds = CalculateDateInMilliSecondsFromNotificationText(message);
                NotificationManager notificationManager = context.GetSystemService(Context.NotificationService) as NotificationManager;
                PendingIntent resultPendingIntent = stackBuilder.GetPendingIntent(0, (int)PendingIntentFlags.UpdateCurrent);

                if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
                {
                    NotificationChannel chan = new NotificationChannel("id", "ChannelName", NotificationImportance.High);
                    chan.EnableVibration(true);
                    chan.LockscreenVisibility = NotificationVisibility.Public;
                    notificationManager.CreateNotificationChannel(chan);

                    var myBuilder = new Notification.Builder(context, chan.Id)
                        .SetContentTitle(title)
                        .SetContentIntent(resultPendingIntent)
                        .SetContentText(message)
                        .SetSmallIcon(Resource.Drawable.AppIcon);

                    notification = myBuilder.Build();
                }
                else
                {
                    NotificationCompat.Builder builder = new NotificationCompat.Builder(context)
                        .SetAutoCancel(true)
                        // dismiss the notification from the notification area when the user clicks on it
                        .SetContentIntent(resultPendingIntent) // start up this activity when the user clicks the intent.
                        .SetContentTitle(title) // Set the title
                        .SetNumber(number) // Display the count in the Content Info
                        .SetSmallIcon(Resource.Drawable.AppIcon)
                        .SetContentText(message) // the message to display.
                        .SetSound(RingtoneManager.GetDefaultUri(RingtoneType.Notification));

                    notification = builder.Build();
                }

                notificationManager?.Notify(notificationId, notification);
            }
            catch (System.Exception exception)
            {
              
                var properties = new Dictionary<string, string>
                       {
                             { "mainapplication", "makecustomnotfication" },
                       };
                Crashes.TrackError(exception, properties);
            }
        }
    }

    
}