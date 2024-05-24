using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Media;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using Plugin.FirebasePushNotification;
using Prism;
using Prism.Ioc;
using System;
using Maham.Service.General;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms;
using Maham.Helpers;
using System.IO;
using Newtonsoft.Json;
using Maham.Constants;
using System.Linq;

namespace Maham.Droid
{
    [Activity(Exported = true, Label = "Maham", Icon = "@drawable/AppIcon", Theme = "@style/MainTheme.Splash", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            AndroidEnvironment.UnhandledExceptionRaiser += (sender, args) =>
            {
                string path = DependencyService.Get<IFileHelper>().file(AppConstants.AppName);
                string filepath = Path.Combine(path, DateTime.Now.ToLongTimeString());
                var newExc = new ApplicationException("AndroidEnvironment_UnhandledExceptionRaiser", args.Exception);
                DependencyService.Get<IFileHelper>().FilePath(filepath); ;
                File.WriteAllText(filepath, JsonConvert.SerializeObject(newExc));
            };

            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            //.EmulateBackPressed = OnBackPressed;
            SetTheme(Resource.Style.MainTheme);
            base.OnCreate(bundle);

            MainApplication.activity = this;

            //  global::Rg.Plugins.Popup.Popup.Init(this, bundle);

            Rg.Plugins.Popup.Popup.Init(this);

            Xamarin.Essentials.Platform.Init(this, bundle);
            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App(new AndroidInitializer()));

            App.Current.On<Xamarin.Forms.PlatformConfiguration.Android>().UseWindowSoftInputModeAdjust(WindowSoftInputModeAdjust.Resize);

            FirebasePushNotificationManager.ProcessIntent(this, Intent);
            FirebasePushNotificationManager.Initialize(this, false);
            //ConfigNotification();

            Plugin.CurrentActivity.CrossCurrentActivity.Current.Init(this, bundle);

            // notify user after crash
            if (Intent.GetBooleanExtra("crash", false))
            {
                ShowCrashMsg();
            }
            else if (Intent.GetBooleanExtra("OpenTaskDetails", false))
            {
                OpentaskDetails();
                //((App)App.Current).ExtResolve<INavService>().NavigateToAsync<ViewModels.TaskDetailsPageViewModel>();
            }

            
        }

        //public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        //{
        //    if (Xamarin.Essentials.DeviceInfo.Version.Major >= 13 && (permissions.Where(p => p.Equals("android.permission.WRITE_EXTERNAL_STORAGE")).Any() || permissions.Where(p => p.Equals("android.permission.READ_EXTERNAL_STORAGE")).Any()))
        //    {
        //        var wIdx = Array.IndexOf(permissions, "android.permission.WRITE_EXTERNAL_STORAGE");
        //        var rIdx = Array.IndexOf(permissions, "android.permission.READ_EXTERNAL_STORAGE");

        //        if (wIdx != -1 && wIdx < permissions.Length) grantResults[wIdx] = Permission.Granted;
        //        if (rIdx != -1 && rIdx < permissions.Length) grantResults[rIdx] = Permission.Granted;
        //    }

        //    Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        //    PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);

        //    base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        //}

        protected override void OnNewIntent(Intent intent)
        {
            base.OnNewIntent(intent);
            OpentaskDetails();
        }
        private async void OpentaskDetails()
        {
            await System.Threading.Tasks.Task.Delay(700);
            //Xamarin.Forms.MessagingCenter.Send("true", "OpenTaskDetails");
            await ((App)App.Current).ExtResolve<INavService>().NavigateToAsync<ViewModels.TaskDetailsPageViewModel>();
        }

        private async void ShowCrashMsg()
        {
            await System.Threading.Tasks.Task.Delay(4000);
            string msg = Maham.Resources.AppResource.CrashMessage;

            RunOnUiThread(() => {
                for (int i = 0; i < 2; i++)
                {
                    Toast.MakeText(this, msg, ToastLength.Long).Show();
                }
            });
            
        }
        void ConfigNotification()
        {
            FirebasePushNotificationManager.SoundUri = RingtoneManager.GetDefaultUri(RingtoneType.Notification);
            FirebasePushNotificationManager.IconResource = Resource.Drawable.TasksIcon;
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            //Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            //if (Xamarin.Essentials.DeviceInfo.Version.Major >= 13 && (permissions.Where(p => p.Equals("android.permission.WRITE_EXTERNAL_STORAGE")).Any() || permissions.Where(p => p.Equals("android.permission.READ_EXTERNAL_STORAGE")).Any()))
            //{
            //    var wIdx = Array.IndexOf(permissions, "android.permission.WRITE_EXTERNAL_STORAGE");
            //    var rIdx = Array.IndexOf(permissions, "android.permission.READ_EXTERNAL_STORAGE");

            //    if (wIdx != -1 && wIdx < permissions.Length) grantResults[wIdx] = Permission.Granted;
            //    if (rIdx != -1 && rIdx < permissions.Length) grantResults[rIdx] = Permission.Granted;
            //}

            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            //  base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        public override void OnBackPressed()
        {
            if (Rg.Plugins.Popup.Popup.SendBackPressed(base.OnBackPressed))
            {
                // Do something if there are some pages in the `PopupStack`
            }
            else
            {
                // Do something if there are not any pages in the `PopupStack`
            }
        }
    }

            
    public class AndroidInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IContainerRegistry container)
        {
            // Register any platform specific implementations
        }
    }

   
}

