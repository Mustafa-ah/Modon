using System.Collections.Generic;

using Android.Media;
using Android.App;
using Android.Content;
using Android.OS;
using Maham.Droid.Helpers;
using Maham.Helpers;
using Java.Lang;
using Android.Support.V4.App;
using Notification = Android.App.Notification;
using Microsoft.AppCenter.Crashes;

[assembly: Xamarin.Forms.Dependency(typeof(NotificationHelper))]
namespace Maham.Droid.Helpers
{
    public class NotificationHelper : INotificationHelper
    {
        public void Notify(string title, string body)
        {
            try
            {
                // iconType = SetIconSource((NotificationType)iconType);

                Context context = Application.Context;
                Intent intent = new Intent();
                Intent resultIntent = new Intent(context, typeof(MainActivity));
                resultIntent.PutExtra("OpenTaskDetails", true);
                //Log.Info("CUSTOM NOTIFICATION UI", "MakeCustomNotification Entered");
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
                        .SetContentText(body)
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
                        .SetContentText(body) // the message to display.
                        .SetSound(RingtoneManager.GetDefaultUri(RingtoneType.Notification));

                    notification = builder.Build();
                }

                notificationManager?.Notify(1, notification);
            }
            catch (System.Exception exception)
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