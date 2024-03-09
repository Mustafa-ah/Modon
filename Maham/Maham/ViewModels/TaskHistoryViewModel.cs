using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.AppCenter.Crashes;
using Prism.Navigation;
using Refit;
using Maham.Bases;
using Maham.Models;
using Maham.Service;
using Maham.Service.General;
using Maham.Setting;
using Xamarin.Forms;

namespace Maham.ViewModels
{
    class TaskHistoryViewModel : BaseViewModel
    {
        private readonly INavService navService;
        public bool NoHistory { get; set; }
        public bool IsBusy { get; set; }
        public string TaskName { get; set; }
        private List<ChangePerDate> _taskChangesList;
        public List<ChangePerDate> TaskChangesList
        {
            get
            {
                return _taskChangesList;
            }
            set
            {
                SetProperty(ref _taskChangesList, value);
            }
        }
        public ICommand BacknavigationCommand { get; set; }
        public TaskHistoryViewModel(INavService _navService, INavigationService _NavigationServices) : base(_NavigationServices)
        {
            navService = _navService;
            BacknavigationCommand = new Command(BacknavigationCommandExcute);
            IsRTL = Settings.IsRtl;
        }

        private void BacknavigationCommandExcute(object obj)
        {
            navService.NavigateBackAsync();
        }

        public override async Task InitializeAsync(object data)
        {
            try
            {
                IsBusy = true;
                var api = RestService.For<ITaskyApi>(new System.Net.Http.HttpClient(new HttpLoggingHandler()) { BaseAddress = new Uri(Settings.ApiUrl) });
                //var taskChange = await api.GetTaskHistory("Bearer " + Settings.AccessToken, Settings.TaskId);
                //IsBusy = false;
                //TaskName = taskChange.TaskName;
                //TaskChangesList = taskChange.ChangesPerDate;

                NoHistory = TaskChangesList.Count == 0;
            }
            catch (Exception e)
            {
                IsBusy = false;
                var properties = new Dictionary<string, string>
                {
                    { "PrioritiesPageViewModel", "AssignPriority" },
                };
                Crashes.TrackError(e, properties);
            }


            MessagingCenter.Send<TaskHistoryViewModel, List<ChangePerDate>>(this, "DataLoaded", TaskChangesList);
           // return base.InitializeAsync(data);
        }
    }
}
