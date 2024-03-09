using Microsoft.AppCenter.Crashes;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Refit;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Maham.Bases;
using Maham.Models.User;
using Maham.Service;
using Maham.Setting;
using Xamarin.Forms;
using Maham.Models;

namespace Maham.ViewModels
{
	public class UserGroupPopUpPageViewModel : BaseViewModel
    {
       
        public ICommand ChooseRefCommand { get; set; }
        public UserGroupListModel selectUserGroup { get; set; }
        public ObservableCollection<UserGroupListModel> UserGroupList { get; set; }
        public UserGroupPopUpPageViewModel(INavigationService _NavigationServices) : base(_NavigationServices)
        {
            UserGroupList = new ObservableCollection<UserGroupListModel>();
            ChooseRefCommand = new Command(ChooseRefCommandEXcute);
            GetUserGroup(Settings.UserId);
        }
        private void ChooseRefCommandEXcute(object obj)
        {
            var item = obj as UserGroupListModel;
            var ItemSelected = obj as UserGroupListModel;
           
            if (ItemSelected == null) return;
            if (ItemSelected != selectUserGroup)
            {
                if (selectUserGroup != null)
                {
                    selectUserGroup.IsCheckedRefe = false;
                }
                ItemSelected.IsCheckedRefe = true;

            }
            //else
            //{
            //    ItemSelected.IsCheckedRefe = !ItemSelected.IsCheckedRefe;
            //}
            selectUserGroup = ItemSelected;
            MessagingCenter.Send(selectUserGroup, "selectUserGroup");
          
            PopupNavigation.Instance.PopAsync();
            // MessagingCenter.Send(new UserGroupListModel() { id = item.id, name = Refname }, "RefPopUp");
            // PopupNavigation.Instance.PopAsync();

        }
        public async Task GetUserGroup(string userId)   
        {
            var api = RestService.For<ITaskyApi>(new System.Net.Http.HttpClient(new HttpLoggingHandler()) { BaseAddress = new Uri(Settings.ApiUrl) });
            try
            {

                //GetUserGroupsList
                var result = await api.GetUserGroupsList("Bearer " + Settings.AccessToken, Guid.Empty);
                List<UserGroupListModel> _x = new List<UserGroupListModel>();
                _x = (result.Data).ToObject<List<UserGroupListModel>>();
                UserGroupList = new ObservableCollection<UserGroupListModel>();
                    foreach (var item in _x)
                {
                    UserGroupList.Add(new UserGroupListModel { id = item.id.ToString(), name = item.name, checkImage = "uncheck" });
                }

                var prevUserGroup = UserGroupList.FirstOrDefault(p => p.id == Settings.GeneralId_string);
                if (prevUserGroup != null)
                {
                    selectUserGroup = prevUserGroup;
                    prevUserGroup.IsCheckedRefe = true;
                }
            }
            catch (Exception exception)
            {
                var properties = new Dictionary<string, string>
                       {
                             { "UserGrouppopup", "getUserGroups" },
                       };
                Crashes.TrackError(exception, properties);
            }
        }
       
    }
}
