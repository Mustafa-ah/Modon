using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Foundation;
using Maham.Helpers;
using Maham.iOS.Helper;
using UIKit;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(FileHelper))]
namespace Maham.iOS.Helper
{
    public class FileHelper : IFileHelper
    {
        public string file(string name)
        {
            var path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var filename1 = Path.Combine(path.ToString(), name);
            return filename1;
        }

        public void FilePath(string filepath)
        {
            //var path = Environment.SpecialFolder.Personal;
            //var filename1 = Path.Combine(path.ToString(), filename);
            //return filename1;
            var PreviewController = UIDocumentInteractionController.FromUrl(NSUrl.FromFilename(filepath));
            PreviewController.Delegate = new UIDocumentInteractionControllerDelegateClass(UIApplication.SharedApplication.KeyWindow.RootViewController);
            Device.BeginInvokeOnMainThread(() =>
            {
                PreviewController.PresentPreview(true);
            });
        }
    }
}