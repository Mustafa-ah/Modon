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
using Maham.Service.Model.Response.Tasks;
using Task = System.Threading.Tasks.Task;

namespace Maham.ViewModels
{
	public class EmergencyCallPopUpPageViewModel : BaseViewModel
    {
       
        public ICommand ChooseRefCommand { get; set; }
        public EmergencyCallsDDL selectEmergencyCall { get; set; }
        public ObservableCollection<EmergencyCallsDDL> EmergencyCallList { get; set; }
        public EmergencyCallPopUpPageViewModel(INavigationService _NavigationServices) : base(_NavigationServices)
        {
            EmergencyCallList = new ObservableCollection<EmergencyCallsDDL>();
            ChooseRefCommand = new Command(ChooseRefCommandEXcute);
            IsRTL = Settings.IsRtl;
            GetEmergencyCall();
        }
        private void ChooseRefCommandEXcute(object obj)
        {
            var item = obj as EmergencyCallsDDL;
            var ItemSelected = obj as EmergencyCallsDDL;
           
            if (ItemSelected == null) return;
            if (ItemSelected != selectEmergencyCall)
            {
                if (selectEmergencyCall != null)
                {
                    selectEmergencyCall.IsCheckedemployee = false;
                }
                ItemSelected.IsCheckedemployee = true;
            }
            selectEmergencyCall = ItemSelected;
            MessagingCenter.Send(selectEmergencyCall, "selectEmergencyCall");
            PopupNavigation.Instance.PopAsync();

        }
        public async Task GetEmergencyCall()
        {
            try
            {
                List<EmergencyCallsDDL> _x = new List<EmergencyCallsDDL>();
                _x = Settings.EmergencyCallList;
                EmergencyCallList = new ObservableCollection<EmergencyCallsDDL>();
                    foreach (var item in _x)
                {
                    string txt = IsRTL ? item.ArabicText : item.Text;
                    EmergencyCallList.Add(new EmergencyCallsDDL { Value3 = item.Value3, Text = txt, IsCheckedemployee = false });
                }

                var prevEmergencyCall = EmergencyCallList.FirstOrDefault(p => p.Value3.ID == Settings.EmergencyCall.ID && p.Value3.RoleID == Settings.EmergencyCall.RoleID && p.Value3.EntityID == Settings.EmergencyCall.EntityID);
                if (prevEmergencyCall != null)
                {
                    selectEmergencyCall = prevEmergencyCall;
                    prevEmergencyCall.IsCheckedemployee = true;
                }
            }
            catch (Exception exception)
            {
                var properties = new Dictionary<string, string>
                       {
                             { "EmergencyCallpopup", "getEmergencyCalls" },
                       };
                Crashes.TrackError(exception, properties);
            }
        }
       
    }
}
