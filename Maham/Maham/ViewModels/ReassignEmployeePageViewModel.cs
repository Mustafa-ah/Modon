using Microsoft.AppCenter.Crashes;

using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Refit;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Maham.Bases;
using Maham.Constants;
using Maham.Models;
using Maham.Resources;
using Maham.Service;
using Maham.Service.General;
using Maham.Setting;
using Xamarin.Forms;

namespace Maham.ViewModels
{
	public class ReassignEmployeePageViewModel : BaseViewModel
    {
        private readonly INavService navService;
        public bool IsCheckedassignemp { get; set; }
        public string ReassignEmpName { get; set; }
        public int ResponsibleID { get; set; }

        public ICommand ApplyCommand { get; set; }
        public ICommand ChooseempCommand { get; set; }
        public ICommand backnavigation { get; set; }
        public ObservableCollection<EmployeeModel> ReassignList { get; set; }
        public ObservableCollection<EmployeeModel> TempReassignList { get; set; }
        public ObservableCollection<EmployeeModel> RefreshList { get; set; }
        public ReassignEmployeePageViewModel(INavService _navService, INavigationService _NavigationServices) : base(_NavigationServices)
        {
            navService = _navService;
            ChooseempCommand = new Command(ChooseempCommandExcute);
            ApplyCommand = new Command(ApplyCommandExcute);
            backnavigation = new Command(backnavigationExcute);
            ReassignList = new ObservableCollection<EmployeeModel>();
            TempReassignList = new ObservableCollection<EmployeeModel>();
            RefreshList = new ObservableCollection<EmployeeModel>();
        }

        private void backnavigationExcute(object obj)
        {
            navService.NavigateBackAsync();
        }

        public void OnReassignEmpNameChanged()
        {
            
            if (string.IsNullOrEmpty(ReassignEmpName)|| string.IsNullOrWhiteSpace(ReassignEmpName))
            {
                if(RefreshList.Count>0)
                {
                    ReassignList = RefreshList;
                 
                }
                ResponsibleID = 0;
            }
            else
            {
                if (ReassignList != null&&ReassignList.Count>0)
                {
                    ReassignList = TempReassignList;
                    List<EmployeeModel> result = ReassignList.Where(x => !string.IsNullOrEmpty(x.name) && x.name.ToLower().Contains(ReassignEmpName.ToLower())).ToList();
                    if (result != null)
                    {
                        try
                        {
                            ReassignList = new ObservableCollection<EmployeeModel>(result);

                        }
                        catch (Exception exception)
                        {
                            
                            var properties = new Dictionary<string, string>
                       {
                             { "reaasignemployeeviewmodel", "onreaasigneeemployeechange" },
                       };
                            Crashes.TrackError(exception, properties);
                        }

                    }

                }
            }
        }
        private async void ApplyCommandExcute(object obj)
        {
            var api = RestService.For<ITaskyApi>(new System.Net.Http.HttpClient(new HttpLoggingHandler()) { BaseAddress = new Uri(Settings.ApiUrl) });
            try
            {

                if(ResponsibleID==0)
                {
                    await Application.Current.MainPage.DisplayAlert(AppResource.failuretext, AppResource.choosereassignemp, AppResource.oktext);
                }
                else
                {
                    var result = await api.ApplyReassign("Bearer " + Settings.AccessToken, int.Parse(Settings.UserId), ResponsibleID, int.Parse(Settings.TaskId));
                    if (result.requestSuccess)
                    {
                        await Application.Current.MainPage.DisplayAlert(AppResource.ReassignSucess, AppResource.Reassignsucessmessage, AppResource.oktext);
                        await navService.NavigateBackAsync();
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert(AppResource.failuretext, AppResource.CantReassignmessage, AppResource.oktext);
                    }
                }
               
            }
            catch (Exception exception)
            {
                var properties = new Dictionary<string, string>
                       {
                             { "ReassiagEmployeePageViewModel", "Apply reassign " },
                       };
                Crashes.TrackError(exception, properties);
            }

            }

        private void ChooseempCommandExcute(object obj)
        {
            var ItemSelected = obj as EmployeeModel;

            if (ItemSelected == null)
            {
                
                return;
            }

            foreach (var item in ReassignList)
            {
                item.IsCheckedemployee = false;
            }
            ItemSelected.IsCheckedemployee = true;

            ReassignEmpName = ItemSelected.name;
            ResponsibleID = ItemSelected.id;
            ReassignList = RefreshList;
            //await ReassiagnEmployeeData();
        }
        public  async Task ReassiagnEmployeeData()
        {
            var api = RestService.For<ITaskyApi>(new System.Net.Http.HttpClient(new HttpLoggingHandler()) { BaseAddress = new Uri(Settings.ApiUrl) });
            try
            {


                var result = await api.GetReassignList("Bearer " + Settings.AccessToken, Settings.UserId);
                if(result.Count>0)
                {
                    foreach (var item in result)
                    {
                        ReassignList.Add(new EmployeeModel { id = item.id, name = item.empName, EmployeeImage = item.photoUrl, Description = item.role });
                    }
                    TempReassignList = ReassignList;
                    RefreshList = ReassignList;
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("", AppResource.noreassignlist, AppResource.oktext);
                }
               
            }
            catch (Exception exception)
            {
                var properties = new Dictionary<string, string>
                       {
                             { "ReassiagEmployeePageViewModel", "assignemployee" },
                       };
                Crashes.TrackError(exception, properties);
            }
        }
        //public async override void OnAppearing()
        //{
        //    base.OnAppearing();
        //   await ReassiagnEmployeeData();
        //}

        public async override Task InitializeAsync(object data)
        {
            await base.InitializeAsync(data);
            await ReassiagnEmployeeData();
        }
    }
}
