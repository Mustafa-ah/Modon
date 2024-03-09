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
using Maham.Setting;
using Xamarin.Forms;

namespace Maham.ViewModels
{
	public class projectPopupPageViewModel :BaseViewModel
	{
       
        public ObservableCollection<ProjectListModel> ProjectList { get; set; }
        public ProjectListModel Lastproject { get; set; }
       // public AddTaskModel adddata { get; set; }
        public ICommand ChooseprojCommand { get; set; }
        public projectPopupPageViewModel(INavigationService _NavigationServices) : base(_NavigationServices)
        {
            
            ProjectList = new ObservableCollection<ProjectListModel>();
            ChooseprojCommand = new Command(ChooseProjectCommandExcute);
            Getproject();
        }
        private  void ChooseProjectCommandExcute(object obj)
        {
            var ItemSelected = obj as ProjectListModel;

            if (ItemSelected == null) return;
            if (ItemSelected != Lastproject)
            {
                if (Lastproject != null)
                {
                    Lastproject.isProjectChecked = false;

                }
                ItemSelected.isProjectChecked = true;

            }
            //else
            //{
            //    ItemSelected.isProjectChecked = !ItemSelected.isProjectChecked;

            //}
          
            Lastproject = ItemSelected;
            //Settings.GeneralId = ItemSelected.id == null ? 0 : (int)ItemSelected.id;
            //var navigationParams = new NavigationParameters();
         
            //navigationParams.Add("lastprojectselect", Lastproject);
            //navigationParams.Add("data", adddata);
            MessagingCenter.Send(Lastproject, "AddNew");
            //_navigation.NavigateAsync("MainTabbedPage/NewTaskPage",navigationParams);
            PopupNavigation.Instance.PopAsync();
            //isprojectlistvisble = false;
            //isprojectarrow = false;
            // await GetAssignEmployee();

        }

        //public override void OnNavigatedTo(NavigationParameters parameters)
        //{
        //    base.OnNavigatedTo(parameters);
        //    adddata = parameters["addtaskmodel"] as AddTaskModel;
        //}
        public async void Getproject()
        {
            var api = RestService.For<ITaskyApi>(new System.Net.Http.HttpClient(new HttpLoggingHandler()) { BaseAddress = new Uri(Settings.ApiUrl) });
            try
            {


                ProjectList = await api.GetUserProject(Settings.UserId);
                //ProjectList = new ObservableCollection<ProjectListModel>();
                //ProjectList = new ObservableCollection<ProjectListModel>(result);

                var prevProjectet = ProjectList.FirstOrDefault(p => p.id == Settings.GeneralId);
                if (prevProjectet != null)
                {
                    Lastproject = prevProjectet;
                    prevProjectet.isProjectChecked = true;
                }
                
            }
            catch (Exception exception)
            {
                var properties = new Dictionary<string, string>
                       {
                             { "projectpopuppage", "getprojectlist" },
                       };
                Crashes.TrackError(exception, properties);
            }
        }
    }
}
