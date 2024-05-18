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
using Maham.Service.Model.Response.User;
using Newtonsoft.Json;

namespace Maham.ViewModels
{
	public class SourcePopUpPageViewModel : BaseViewModel
    {
       
        public ICommand ChooseRefCommand { get; set; }
        public ListPopUpModel selectSource { get; set; }

        public ObservableCollection<ListPopUpModel> SourceList { get; set; }
        public SourcePopUpPageViewModel(INavigationService _NavigationServices) : base(_NavigationServices)
        {
            SourceList = new ObservableCollection<ListPopUpModel>();
            ChooseRefCommand = new Command(ChooseRefCommandEXcute);
        }
        private void ChooseRefCommandEXcute(object obj)
        {
            var item = obj as ListPopUpModel;
            var ItemSelected = obj as ListPopUpModel;
           
            if (ItemSelected == null) return;
            if (ItemSelected != selectSource)
            {
                if (selectSource != null)
                {
                    selectSource.IsChecked = false;
                }
                ItemSelected.IsChecked = true;

            }
            //else
            //{
            //    ItemSelected.IsCheckedRefe = !ItemSelected.IsCheckedRefe;
            //}
            selectSource = ItemSelected;
            MessagingCenter.Send(selectSource, "selectSource");
          
            PopupNavigation.Instance.PopAsync();
            // MessagingCenter.Send(new SourceListModel() { id = item.id, name = Refname }, "RefPopUp");
            // PopupNavigation.Instance.PopAsync();

        }
        public bool Isbusy { get; set; }
        public async Task GetSource(string userid, int? privilege = null)
        {
            var api = RestService.For<ITaskyApi>(new System.Net.Http.HttpClient(new HttpLoggingHandler()) { BaseAddress = new Uri(Settings.ApiUrl) });
            try
            {

                Isbusy = true;
                var result = await api.GetSource("Bearer " + Settings.AccessToken, userid, privilege);
                List<ListPopUpModel> _x = new List<ListPopUpModel>();
                //_x = (result.Data).ToObject<List<ListPopUpModel>>();
                _x = JsonConvert.DeserializeObject<List<ListPopUpModel>>(Convert.ToString(result.Data));
                SourceList = new ObservableCollection<ListPopUpModel>();
                    foreach (var item in _x)
                {
                    SourceList.Add(new ListPopUpModel { id = item.id, name = Settings.IsRtl ? item.nameAr : item.name, checkImage = "uncheck" });
                }

                var prevSource = SourceList.FirstOrDefault(p => p.id == Settings.GeneralId);
                if (prevSource != null)
                {
                    selectSource = prevSource;
                    prevSource.IsChecked = true;
                }
            }
            catch (Exception exception)
            {
                var properties = new Dictionary<string, string>
                       {
                             { "Sourcepopup", "getSources" },
                       };
                Crashes.TrackError(exception, properties);
            }
            Isbusy = false;
        }
       
    }
}
