using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Maham.Bases;
using Maham.Models;
using Xamarin.Forms;
using System.Threading.Tasks;
using Refit;
using Maham.Setting;
using Maham.Service;
using Microsoft.AppCenter.Crashes;
using Maham.Resources;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System.IO;
using Maham.Helpers;
using Maham.Constants;

namespace Maham.ViewModels
{
	public class popupViewModel : BaseViewModel
	{
        
        public ObservableCollection<FileDataModel> file { get; set; }
        public ICommand deletcommand { get; set; }
        public ICommand downloadfilecommand { get; set; }
        public popupViewModel(INavigationService _NavigationServices) : base(_NavigationServices)
        {
        
            // FileList = new ObservableCollection<FileDataModel>();
            deletcommand = new Command(DeletecommandExcute);
            downloadfilecommand = new Command(downloadfilecommandExcute);


        }

        private async void DeletecommandExcute(object obj)
        {
            try
            {
                bool delete_ = await Application.Current.MainPage.DisplayAlert("", AppResource.DeleteMsg, AppResource.oktext, AppResource.canceltext);
                if (delete_)
                {
                    var item = (FileDataModel)obj;
                    file.Remove(item);

                    file = new ObservableCollection<FileDataModel>(file);

                    bool deleted = await DeleteAttchment(item.AttachmentId);
                    if (file.Count == 0)
                    {
                        PopupNavigation.Instance.PopAsync();
                    }
                }

            }
            catch (Exception exception)
            {
                var properties = new Dictionary<string, string>
                       {
                             { "popupViewModel", "DeleteFilecommandExcute" },
                       };
                Crashes.TrackError(exception, properties);
            }
        }

        private async Task<bool> DeleteAttchment(Guid attachmentId)
        {
            try
            {
                var api = RestService.For<ITaskyApi>(new System.Net.Http.HttpClient(new HttpLoggingHandler()) { BaseAddress = new Uri(Settings.ApiUrl) });

                var result = await api.DeleteAttachment("Bearer " + Settings.AccessToken, attachmentId.ToString());

                return result.Success;
            }
            catch (Exception ee)
            {

                var properties = new Dictionary<string, string>
                                   {
                                         { "GetAttchmentbyId", "attachment" },
                                   };
                Crashes.TrackError(ee, properties);
                return false;
            }
        }

        private async void downloadfilecommandExcute(object obj)
        {
            try
            {
                var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Storage);
                if (status != PermissionStatus.Granted)
                {
                    var results = await CrossPermissions.Current.RequestPermissionsAsync(new[] { Permission.Storage });
                    status = results[Permission.Storage];
                }
                if (status != PermissionStatus.Granted)
                {
                    return;
                }
                var file = (FileDataModel)obj;
                string path = DependencyService.Get<IFileHelper>().file(AppConstants.AppName);
                string filepath = Path.Combine(path, file.FileName);
                if (File.Exists(filepath))
                {
                    DependencyService.Get<IFileHelper>().FilePath(filepath);
                }
                else
                {
                    var api = RestService.For<ITaskyApi>(new System.Net.Http.HttpClient(new HttpLoggingHandler()) { BaseAddress = new Uri(Settings.ApiUrl) });
                    var _result = (await api.DownloadFile("Bearer " + Settings.AccessToken, file.AttachmentId.ToString())).Data;

                    if (!String.IsNullOrEmpty(_result))
                    {
                        File.WriteAllBytes(filepath, Convert.FromBase64String(_result));
                        DependencyService.Get<IFileHelper>().FilePath(filepath);
                    }
                    await Application.Current.MainPage.DisplayAlert("", AppResource.dowlnoadSucessfully, AppResource.oktext);

                }

            }
            catch (Exception ee)
            {

                var properties = new Dictionary<string, string>
                                   {
                                         { "GetTaskData", "attachment" },
                                   };
                Crashes.TrackError(ee, properties);
            }


        }
    }
}
