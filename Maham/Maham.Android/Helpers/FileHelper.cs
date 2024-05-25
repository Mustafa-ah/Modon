using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Android;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.Core.Content;
using Maham.Droid.Helpers;
using Maham.Helpers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: Xamarin.Forms.Dependency(typeof(FileHelper))]


namespace Maham.Droid.Helpers
{
    public class FileHelper : IFileHelper
    {
        const int RequestStorageId = 0;
        readonly string[] PermissionsStorage = {Manifest.Permission.ReadExternalStorage,Manifest.Permission.WriteExternalStorage};

        public FileHelper()
        {

        }

        public string file(string name)
        {
            string path = Android.OS.Environment.ExternalStorageDirectory.AbsolutePath;
            Java.IO.File externalDir = Plugin.CurrentActivity.CrossCurrentActivity.Current.Activity.GetExternalFilesDir(null);

            // var path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
            // var internalStorageDirectory = new Java.IO.File(Plugin.CurrentActivity.CrossCurrentActivity.Current.Activity.ApplicationContext.FilesDir, name);
            //var path = global::Android.OS.Environment.ExternalStorageDirectory.AbsolutePath;
            if (Build.VERSION.SdkInt >= BuildVersionCodes.Q)
            {
                path = externalDir.AbsolutePath;// Path.Combine(Plugin.CurrentActivity.CrossCurrentActivity.Current.Activity.GetExternalFilesDir(Android.OS.Environment.DirectoryDocuments).AbsolutePath, "TaskDocs");
            }
            var filename1 = Path.Combine(path.ToString(), name);
            return filename1;// internalStorageDirectory.AbsolutePath;
        }

        public void FilePathO(string filePath)
        {
            //var path = global::Android.OS.Environment.ExternalStorageDirectory.AbsolutePath;
            //var filename1 = Path.Combine(path.ToString(), filename);
            //return filename1;
            var bytes = File.ReadAllBytes(filePath);

            //Copy the private file's data to the EXTERNAL PUBLIC location
           // string externalStorageState = global::Android.OS.Environment.ExternalStorageState;
           // var externalPath = global::Android.OS.Environment.ExternalStorageDirectory.Path + "/" + global::Android.OS.Environment.DirectoryDownloads + "/" + filename;
            File.WriteAllBytes(filePath, bytes);

            Java.IO.File file = new Java.IO.File(filePath);
            file.SetReadable(true);

            string application = "";
            string extension = Path.GetExtension(filePath);

            // get mimeTye
            switch (extension.ToLower())
            {
                case ".txt":
                    application = "text/plain";
                    break;
                case ".doc":
                case ".docx":
                    application = "application/msword";
                    break;
                case ".pdf":
                    application = "application/pdf";
                    break;
                case ".xls":
                case ".xlsx":
                    application = "application/vnd.ms-excel";
                    break;
                case ".jpg":
                case ".jpeg":
                case ".png":
                    application = "image/jpeg";
                    break;
                default:
                    application = "*/*";
                    break;
            }

            //Android.Net.Uri uri = Android.Net.Uri.Parse("file://" + filePath);
            Android.Net.Uri uri = Android.Net.Uri.FromFile(file);
            Intent intent = new Intent(Intent.ActionView);
            intent.SetDataAndType(uri, application);
            intent.SetFlags(ActivityFlags.ClearWhenTaskReset | ActivityFlags.NewTask | ActivityFlags.GrantReadUriPermission);

           var ac =  intent.ResolveActivity(Plugin.CurrentActivity.CrossCurrentActivity.Current.Activity.PackageManager);
            if (ac != null)
            {
                //Android.App.Application.Context
                Plugin.CurrentActivity.CrossCurrentActivity.Current.Activity.StartActivity(intent);
            }
            else
            {
                string ok = Setting.Settings.IsRtl ? "موافق" : "OK";
                string msg = Setting.Settings.IsRtl? "لا يوجد تطبيق يمكنه فتح هذا الملف" : "No application available to handle this file";


                AlertDialog.Builder alert = new AlertDialog.Builder(Plugin.CurrentActivity.CrossCurrentActivity.Current.Activity);
                //alert.SetTitle(dialogTitle);
                alert.SetMessage(msg);
                alert.SetPositiveButton(ok, (senderAlert, args) => {
                    
                });

                //alert.SetNegativeButton(dialogNegativeBtnLabel, (senderAlert, args) => {
                //    tcs.SetResult(dialogNegativeBtnLabel);
                //});

                Dialog dialog = alert.Create();
                dialog.Show();


            }
            //Forms.Context.StartActivity(Intent.CreateChooser(intent, "Your title"));
        }

        public void FilePath(string filePath)
        {

            var bytes = File.ReadAllBytes(filePath);
            File.WriteAllBytes(filePath, bytes);

            Java.IO.File file = new Java.IO.File(filePath);
            file.SetReadable(true);

            string application = "";
            string extension = Path.GetExtension(filePath);

            // get mimeType
            switch (extension.ToLower())
            {
                case ".txt":
                    application = "text/plain";
                    break;
                case ".doc":
                case ".docx":
                    application = "application/msword";
                    break;
                case ".pdf":
                    application = "application/pdf";
                    break;
                case ".xls":
                case ".xlsx":
                    application = "application/vnd.ms-excel";
                    break;
                case ".jpg":
                case ".jpeg":
                case ".png":
                    application = "image/jpeg";
                    break;
                default:
                    application = "*/*";
                    break;
            }

            // Use FileProvider to get a content URI
            Android.Net.Uri uri = FileProvider.GetUriForFile(
                Plugin.CurrentActivity.CrossCurrentActivity.Current.Activity,
                Plugin.CurrentActivity.CrossCurrentActivity.Current.Activity.PackageName + ".fileprovider",
                file);

            Intent intent = new Intent(Intent.ActionView);
            intent.SetDataAndType(uri, application);
            intent.SetFlags(ActivityFlags.ClearWhenTaskReset | ActivityFlags.NewTask | ActivityFlags.GrantReadUriPermission);

            var ac = intent.ResolveActivity(Plugin.CurrentActivity.CrossCurrentActivity.Current.Activity.PackageManager);
            if (ac != null)
            {
                Plugin.CurrentActivity.CrossCurrentActivity.Current.Activity.StartActivity(intent);
            }

            else
            {
                string ok = Setting.Settings.IsRtl ? "موافق" : "OK";
                string msg = Setting.Settings.IsRtl ? "لا يوجد تطبيق يمكنه فتح هذا الملف" : "No application available to handle this file";


                AlertDialog.Builder alert = new AlertDialog.Builder(Plugin.CurrentActivity.CrossCurrentActivity.Current.Activity);
                //alert.SetTitle(dialogTitle);
                alert.SetMessage(msg);
                alert.SetPositiveButton(ok, (senderAlert, args) => {

                });

                //alert.SetNegativeButton(dialogNegativeBtnLabel, (senderAlert, args) => {
                //    tcs.SetResult(dialogNegativeBtnLabel);
                //});

                Dialog dialog = alert.Create();
                dialog.Show();


            }
            //Forms.Context.StartActivity(Intent.CreateChooser(intent, "Your title"));
        }

        public void GetStoragePermission()
        {
            //if (Build.VERSION.SdkInt >= BuildVersionCodes.Tiramisu)
            //{
            //    if (ContextCompat.CheckSelfPermission(Plugin.CurrentActivity.CrossCurrentActivity.Current.AppContext, Manifest.Permission.ReadMediaImages) != PermissionChecker.PermissionGranted)
            //    {
            //        string[] permissions = new string[] { Manifest.Permission.ReadMediaImages, Manifest.Permission.ReadMediaVideo };

            //        Plugin.CurrentActivity.CrossCurrentActivity.Current.Activity.RequestPermissions(permissions, 101); // Request code
            //        //await ((FormsAppCompatActivity)Plugin.CurrentActivity.CrossCurrentActivity.Current.Activity).RequestPermissionsAsync(permissions, 101); // Request code
            //    }
            //}

            if (ContextCompat.CheckSelfPermission(Plugin.CurrentActivity.CrossCurrentActivity.Current.AppContext, Manifest.Permission.WriteExternalStorage) != (int)Permission.Granted)
            {
                // Request permissions
                Plugin.CurrentActivity.CrossCurrentActivity.Current.Activity.RequestPermissions(PermissionsStorage, RequestStorageId);
            }
        }


        //public string CreateAppSpecificDirectory()
        //{
        //    try
        //    {
        //        string externalFilesDirPath = Plugin.CurrentActivity.CrossCurrentActivity.Current.Activity.GetExternalFilesDir(Android.OS.Environment.DirectoryDocuments)?.AbsolutePath;

        //        //string externalFilesDir = "";
        //        //Java.IO.File externalFilesDird = Plugin.CurrentActivity.CrossCurrentActivity.Current.Activity.GetExternalFilesDir(Android.OS.Environment.DirectoryDocuments);
        //        if (externalFilesDirPath != null)
        //        {
        //            return Path.Combine(externalFilesDirPath, "TaskDocs");
        //            //if (!Directory.Exists(path))
        //            //{
        //            //    Directory.CreateDirectory(path);
        //            //}
        //            //else
        //            //{
        //            //    //
        //            //}
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //    }

        //    return global::Android.OS.Environment.ExternalStorageDirectory.AbsolutePath;
        //}

        public void fff()
        {
           // var ff = Plugin.CurrentActivity.CrossCurrentActivity.Current.Activity.GetExternalFilesDir(Android.OS.Environment.DirectoryDocuments);
        }
    }
}