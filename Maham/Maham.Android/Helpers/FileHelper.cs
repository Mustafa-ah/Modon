using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Maham.Droid.Helpers;
using Maham.Helpers;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(FileHelper))]


namespace Maham.Droid.Helpers
{
    public class FileHelper : IFileHelper
    {
        public FileHelper()
        {

        }

        public string file(string name)
        {
           // var path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
            var path = global::Android.OS.Environment.ExternalStorageDirectory.AbsolutePath;
            var filename1 = Path.Combine(path.ToString(), name);
            return filename1;
        }

        public void FilePath(string filePath)
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

           var ac =  intent.ResolveActivity(Forms.Context.PackageManager);
            if (ac != null)
            {
                //Android.App.Application.Context
                Forms.Context.StartActivity(intent);
            }
            else
            {
                string ok = Setting.Settings.IsRtl ? "موافق" : "OK";
                string msg = Setting.Settings.IsRtl? "لا يوجد تطبيق يمكنه فتح هذا الملف" : "No application available to handle this file";


                Android.App.AlertDialog.Builder alert = new Android.App.AlertDialog.Builder(Android.App.Application.Context);
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
    }
}