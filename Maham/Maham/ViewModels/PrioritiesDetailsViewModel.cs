using Microsoft.AppCenter.Crashes;
using Prism.Events;
using Prism.Navigation;
using Refit;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Maham.Bases;
using Maham.Constants;
using Maham.Events;
using Maham.Extentions;
using Maham.Models;
using Maham.Service;
using Maham.Service.General;
using Maham.Service.Model.Response.Priorities;
using Maham.Setting;
using Xamarin.Forms;

namespace Maham.ViewModels
{
    public class PrioritiesDetailsViewModel : BaseViewModel
    {
        #region fields
        public string section_Id;
        //private INavigationService _navigationService;
        private readonly INavService navService;
        #endregion
        #region Private Properties
        private bool _isBusy;
        private bool _IsRefreshing;
        private PrioritiesModel _param;
        private ObservableCollection<PrioritiesDetails> _task;
        private ObservableCollection<PriorirtiesDetailsResponse> _response;
        #endregion
        #region Public Properties
        public ICommand backnavigationCommand { get; set; }
        public ICommand taskdetailsCommand { get; set; }
        public ICommand RefreshTasksCommand { get; set; }
        public string SectionName { get; set; }
        public int TasksCount { get; set; }
        public PrioritiesModel Param
        {
            get
            {
                return _param;
            }
            set
            {
                SetProperty(ref _param, value);
            }
        }
        public bool IsBusy
        {
            get
            {
                return _isBusy;
            }
            set
            {
                SetProperty(ref _isBusy, value);
            }
        }

        public ObservableCollection<PriorirtiesDetailsResponse> Response
        {
            get
            {
                return _response;
            }
            set
            {
                SetProperty(ref _response, value);
            }
        }
        public ObservableCollection<PrioritiesDetails> Tasks
        {
            get
            {
                return _task;
            }
            set
            {
                SetProperty(ref _task, value);
            }
        }
       

        public bool IsRefreshing
        {
            get { return _IsRefreshing; }
            set { _IsRefreshing = value; RaisePropertyChanged(nameof(IsRefreshing)); }
        }
        public FilterTask TaskFilter { get; set; }

        #endregion

        public PrioritiesDetailsViewModel(INavService _navService,
            IEventAggregator _eventAggregator, INavigationService _NavigationServices) : base(_NavigationServices)
        {
            backnavigationCommand = new Command(backnavigation);
            taskdetailsCommand = new Command(taskdetailsCommandExcute);
            RefreshTasksCommand = new Command(RefreshTasksCommandExcute);
            // _navigationService = _NavigationServices;
            navService = _navService;
            TaskFilter = new FilterTask();
            _eventAggregator.GetEvent<FilterTasksEvent>().Subscribe(OnFilterPriorities);

            IsRTL = Settings.IsRtl;
        }

        private async void RefreshTasksCommandExcute(object obj)
        {
            try
            {
                IsRefreshing = true;
                Response = await GetTasks(TaskFilter);
                Tasks = LinkData();
                IsRefreshing = false;
            }
            catch (Exception)
            {
                IsRefreshing = false;
            }
           
        }

        private void OnFilterPriorities(FilterTask obj)
        {
            TaskFilter = obj;
        }
        // make refctor one page  Arrow
        private async void taskdetailsCommandExcute(object obj)
        {
            if (obj != null)
            {
                var record = (PrioritiesDetails)obj;
                Settings.TaskId = record.id.ToString();
               // IsBusy = true;
                ////await _navigationService.NavigateAsync("NavigationPage/MainTabbedPage?selectedTab=TasksPage/TaskDetailsPage");
                //await _navigationService.NavigateAsync("TaskDetailsPage");
                await navService.NavigateToAsync<TaskDetailsPageViewModel>();
            }
        }

        public async override void OnNavigatedTo(NavigationParameters parameters)
        {
            try
            {
                base.OnNavigatedTo(parameters);
                if (parameters.Count == 0)
                {
                    return;
                }
                IsBusy = true;
                var temp2 = parameters["item"];
                Param = parameters["item"] as PrioritiesModel;
                SectionName = Param.SectionName;
                TasksCount = Param.TasksCount;
                Response = new ObservableCollection<PriorirtiesDetailsResponse>();
                TaskFilter = parameters["filterdata"] as FilterTask;
                Response = await GetTasks(TaskFilter);
                Tasks = LinkData();
                IsBusy = false;
            }
            catch (Exception e)
            {
                IsBusy = false;
                var properties = new Dictionary<string, string>
                {
                    { "PrioritiesDetailsViewModel", "OnNavigatedTo" },
                };
                Crashes.TrackError(e, properties);
            }
        }
        public override void OnAppearing()
        {
            base.OnAppearing();
        }
        public async Task<ObservableCollection<PriorirtiesDetailsResponse>> GetTasks(FilterTask filter)
        {
            try
            {
                var api = RestService.For<ITaskyApi>(new System.Net.Http.HttpClient(new HttpLoggingHandler()) { BaseAddress = new Uri(Settings.ApiUrl) });

                var result = await api.GetPrioritiesDetails(accessToken: "Bearer " + Settings.AccessToken, 
                    userId: Settings.UserId,
                    priorityId: Param.SectionId.ToString(),
                    SectorID: filter.SectorID,
                    StatusID: filter.StatusID,
                    FromDate: filter.StartDate.ToDateTimeStringForAPI(),
                    ToDate: filter.EndDate.ToDateTimeStringForAPI(),
                    AssigneeID: filter.AssigneeID,
                    projectID: filter.ProjectID);
               
                return result;
            }
            catch (Exception e)
            {
                var properties = new Dictionary<string, string>
                {
                    { "PrioritiesDetailsViewModel", "GetTasks" },
                };
                Crashes.TrackError(e, properties);
                return null;
            }
        }
        public ObservableCollection<PrioritiesDetails> LinkData()
        {
            try
            {
                var task = new ObservableCollection<PrioritiesDetails>();
                foreach (var item in Response)
                {
                    if (IsRTL)
                    {
                        item.status = Helpers.QuickTranslator.TranslateStatus(item.status_Id);
                    }
                    PrioritiesDetails obj = new PrioritiesDetails()
                    {
                        assignedTo = item.assignedTo,
                        assignedTo_ID = item.assignedTo_ID,
                        average_progress = item.average_progress,
                        description = item.description,
                        endDate = item.endDate.ToShortDateStringForView(),
                        fK_UrgentSupportID = item.fK_UrgentSupportID,
                        id = item.id,
                        priority = item.priority,
                        priority_Id = item.priority_Id,
                        progress = item.progress,
                        sector = item.sector,
                        startDate = item.startDate.ToShortDateStringForView(),
                        status = item.status,
                        status_Id = item.status_Id,
                        title = item.title,
                        IsRTL = IsRTL
                    };
                   // obj.endDate = item.endDate;//DateTime.Parse(item.endDate, CultureInfo.InvariantCulture).ToShortDateString();
                    //obj.startDate = item.startDate.ToShortDateStringFromApIDateTime();//DateTime.Parse(item.startDate, CultureInfo.InvariantCulture).ToShortDateString();
                    task.Add(obj);
                }
                return task;
            }
            catch (Exception e)
            {

                var properties = new Dictionary<string, string>
                {
                    { "PrioritiesDetailsViewModel", "LinkData" },
                };
                Crashes.TrackError(e, properties);
                return null;
            }
        }
        private void backnavigation(object obj)
        {
            //eman
            // _navigationService.NavigateAsync("/NavigationPage/MainTabbedPage?selectedTab=TasksPage");
            navService.NavigateBackAsync();
        }

        public override Task InitializeAsync(object data)
        {
            OnNavigatedTo((NavigationParameters)data);
            return base.InitializeAsync(data);
        }
    }
}
