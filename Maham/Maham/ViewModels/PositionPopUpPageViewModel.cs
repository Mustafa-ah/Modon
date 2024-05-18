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
using Maham.Service.Model.Response.Notification;
using Newtonsoft.Json;

namespace Maham.ViewModels
{
	public class PositionPopUpPageViewModel : BaseViewModel
    {
       
        public ICommand ChooseRefCommand { get; set; }
        public ListModel selectPosition { get; set; }
        public ObservableCollection<ListModel> PositionList { get; set; }
        public PositionPopUpPageViewModel(INavigationService _NavigationServices) : base(_NavigationServices)
        {
            PositionList = new ObservableCollection<ListModel>();
            ChooseRefCommand = new Command(ChooseRefCommandEXcute);
            GetPosition(Settings.UserId);
        }
        private void ChooseRefCommandEXcute(object obj)
        {
            var item = obj as ListModel;
            var ItemSelected = obj as ListModel;
           
            if (ItemSelected == null) return;
            if (ItemSelected != selectPosition)
            {
                if (selectPosition != null)
                {
                    selectPosition.IsCheckedRefe = false;
                }
                ItemSelected.IsCheckedRefe = true;

            }
            //else
            //{
            //    ItemSelected.IsCheckedRefe = !ItemSelected.IsCheckedRefe;
            //}
            selectPosition = ItemSelected;
            MessagingCenter.Send(selectPosition, "selectPosition");
          
            PopupNavigation.Instance.PopAsync();
            // MessagingCenter.Send(new ListModel() { id = item.id, name = Refname }, "RefPopUp");
            // PopupNavigation.Instance.PopAsync();

        }
        public async Task GetPosition(string userId)   
        {
            var api = RestService.For<ITaskyApi>(new System.Net.Http.HttpClient(new HttpLoggingHandler()) { BaseAddress = new Uri(Settings.ApiUrl) });
            try
            {


                var result = await api.GetPositionsList("Bearer " + Settings.AccessToken, Guid.Empty);
                List<PositionListModel> _x = new List<PositionListModel>();
                //_x = (result.Data).ToObject<List<PositionListModel>>();
                _x = JsonConvert.DeserializeObject<List<PositionListModel>>(Convert.ToString(result.Data));
                PositionList = new ObservableCollection<ListModel>();
                    foreach (var item in _x)
                {
                    item.checkImage = "uncheck";
                    PositionList.Add(new ListModel { id = item.id.ToString(), name = item.roledisplayName, checkImage = "uncheck" });
                }

                var prevPosition = PositionList.FirstOrDefault(p => p.id == Settings.GeneralId_string);
                if (prevPosition != null)
                {
                    selectPosition = prevPosition;
                    prevPosition.IsCheckedRefe = true;
                }
            }
            catch (Exception exception)
            {
                var properties = new Dictionary<string, string>
                       {
                             { "Positionpopup", "getPositions" },
                       };
                Crashes.TrackError(exception, properties);
            }
        }
       
    }
}
