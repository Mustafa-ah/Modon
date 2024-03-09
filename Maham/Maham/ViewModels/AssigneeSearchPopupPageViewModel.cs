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
using Maham.Models;
using Maham.Service;
using Maham.Service.General;
using Maham.Setting;
using Xamarin.Forms;
using Maham.Service.Model.Response.Tasks;
using Task = System.Threading.Tasks.Task;

namespace Maham.ViewModels
{
	public class AssigneeSearchPopupPageViewModel :BaseViewModel
	{
        private readonly INavService navService;
        public bool isaddpage { get; set; }
        public string selecetedid { get; set; }
        public string Employeename { get; set; }
        public ResponsiblesDDL Lastemployee1 { get; set; }
        public ICommand chooseemp { get; set; }
        public ICommand Search { get; set; }
        public ICommand back { get; set;}
        public ObservableCollection<ResponsiblesDDL> Myemployeelist { get; set; }
        public ObservableCollection<ResponsiblesDDL> EmployeeDemoDataList { get; set; }
        public AssigneeSearchPopupPageViewModel(INavService _navService, INavigationService _NavigationServices) : base(_NavigationServices)
        {
            navService = _navService;
            Myemployeelist = new ObservableCollection<ResponsiblesDDL>();
            EmployeeDemoDataList = new ObservableCollection<ResponsiblesDDL>();
            Search = new Command(SearchCommandExcute);
            back = new Command(backcommandexcute);
            chooseemp = new Command(chooseempExcute);
        }

        private void backcommandexcute(object obj)
        {
            navService.NavigateBackAsync();
        }

        private void SearchCommandExcute(object obj)
        {
            try
            {
                Myemployeelist = new ObservableCollection<ResponsiblesDDL>();
                EmployeeDemoDataList = new ObservableCollection<ResponsiblesDDL>();
                if(Lastemployee1!=null)
                MessagingCenter.Send(Lastemployee1, "SelectedEmployee");

            }
            catch (Exception e)
            {
            }
            navService.NavigateBackAsync();
        }
        public override void OnNavigatedTo(NavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            try
            {
                var data = parameters["Listofemp"] as ObservableCollection<ResponsiblesDDL>;
                Myemployeelist.Clear();

                Myemployeelist = data;
               

                foreach (var item in Myemployeelist)
                {
                    EmployeeDemoDataList.Add(item);
                }
               
                if (parameters.ContainsKey("empid"))
                {
                    selecetedid = parameters["empid"].ToString();
                    Myemployeelist.FirstOrDefault(x => x.Value2.ID == selecetedid).IsCheckedemployee = true;
                    Employeename = Myemployeelist.FirstOrDefault(x => x.Value2.ID == selecetedid).Text;

                }
                else
                {
                    selecetedid = "";
                }

            }
            catch (Exception exception)
            {
                // isBusy = false;
                var properties = new Dictionary<string, string>
                       {
                             { "newtaskpageviewmodel", "OnNavigatedTo" },
                       };
                Crashes.TrackError(exception, properties);
            }

        }
        private void chooseempExcute(object obj)
        {
            var ItemSelected = obj as ResponsiblesDDL;
         
            if (ItemSelected == null) return;

            foreach (var item in Myemployeelist)
            {
                item.IsCheckedemployee = false;
            }

            ItemSelected.IsCheckedemployee = true;
            selecetedid = ItemSelected.Value2.ID;
            Employeename = ItemSelected.Text;
            Lastemployee1 = ItemSelected;
            
        }
        public void OnEmployeenameChanged()
        {
            try
            {
                if (string.IsNullOrEmpty(Employeename) || string.IsNullOrWhiteSpace(Employeename))
                {
                    //if (TempemployeeDemoDataList.Count > 0)
                    //{
                    //    Myemployeelist = TempemployeeDemoDataList;
                    //}

                    //var asignee = Myemployeelist.FirstOrDefault(x => x.id == selecetedid);
                    //if (asignee != null)
                    //{
                    //    asignee.IsCheckedemployee = false;
                    //}
                    //selecetedid = 0;
                    Lastemployee1 = new ResponsiblesDDL();
                }
                else
                {
                    if (Myemployeelist != null)
                    {
                        
                        List<ResponsiblesDDL> result = EmployeeDemoDataList.Where(x => !string.IsNullOrEmpty(x.Text) && x.Text.ToLower().Contains(Employeename.ToLower())).ToList();
                        if (result != null)
                        {
                            try
                            {
                                Myemployeelist = new ObservableCollection<ResponsiblesDDL>(result);

                                var task_ = Myemployeelist.FirstOrDefault(x => x.Value2.ID == selecetedid);
                                if (task_ != null)
                                {
                                    task_.IsCheckedemployee = true;
                                    Lastemployee1 = task_;
                                }
                            }
                            catch (Exception exception)
                            {
                                // isBusy = false;
                                var properties = new Dictionary<string, string>
                       {
                             { "newtaskpageviewmodel", "onemployeenamechanged" },
                       };
                                Crashes.TrackError(exception, properties);
                            }

                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
          
        }

        public override Task InitializeAsync(object data)
        {
            NavParameters = (NavigationParameters)data;
            OnNavigatedTo(NavParameters);
            return base.InitializeAsync(data);
        }
    }
}
