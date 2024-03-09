using Microsoft.AppCenter.Crashes;
using Prism.Events;
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
using Maham.Events;
using Maham.Models;
using Maham.Service;
using Maham.Service.General;
using Maham.Service.Implmentation.Priorities;
using Maham.Service.Model.Request.Login;
using Maham.Service.Model.Response.Priorities;
using Maham.Setting;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Maham.ViewModels
{
    public class PrioritiesPageViewModel : BaseViewModel
    {
        #region Fields

       // private INavigationService _navigationService;
        private readonly INavService navService;
        #endregion
        #region Private Member Property
        private ObservableCollection<PrioritiesResponse> _priorities;
        private PrioritiesModel _critical;
        private PrioritiesModel _high;
        private PrioritiesModel _normal;
        private PrioritiesModel _low;

        private bool isRtl;
        private bool _isRefreshing;
        #endregion
        #region Public Member Property
        public ICommand TapCommand { get; set; }
        public ICommand RefreshCommand { get; set; }
       
        public FilterTask TaskFilter { get; set; }
        public bool IsRefreshing
        {
            get
            {
                return _isRefreshing;
            }
            set
            {
                SetProperty(ref _isRefreshing, value);
            }
        }
        public ObservableCollection<PrioritiesResponse> Priorities
        {
            get
            {
                if (_priorities == null)
                {
                    return _priorities = new ObservableCollection<PrioritiesResponse>();
                }

                return _priorities;
            }
            set
            {
                SetProperty(ref _priorities, value);
                 RaisePropertyChanged();
            }
        }
        public PrioritiesModel Critical
        {
            get { return _critical; }
            set
            {
                SetProperty(ref _critical, value);
                RaisePropertyChanged();
            }
        }

        public PrioritiesModel High
        {
            get { return _high; }
            set
            {
                SetProperty(ref _high, value);
            }
        }

        public PrioritiesModel Normal
        {
            get { return _normal; }
            set
            {
                SetProperty(ref _normal, value);
            }
        }



        public PrioritiesModel Low
        {
            get { return _low; }
            set
            {
                SetProperty(ref _low, value);
            }
        }

        #endregion

        public PrioritiesPageViewModel(INavService _navService,
            IEventAggregator _eventAggregator, INavigationService _NavigationServices) : base(_NavigationServices)
        {
            navService = _navService;
            //Low = new PrioritiesModel()
            //{
            //    SectionName = "test",
            //    Completed = "test",
            //    Delayed = "test",
            //    InProgress = "test",
            //    NotStarted = "test",
            //    Returned = "test",
            //    SectionId = 0,
            //    TasksCount = 3
            //};
            //OnAppearing();
            TapCommand = new Command(Tap);
            RefreshCommand = new Command(Refresh);
          //  _navigationService = _NavigationServices;
            TaskFilter = new FilterTask();
            _eventAggregator.GetEvent<FilterTasksEvent>().Subscribe(OnFilterTasks);
            //_iPrioritiesService = iPrioritiesService;
            isRtl = new Helpers.Helper().IsRtl;
            Init(false);

            MessagingCenter.Subscribe<NewTaskPageViewModel>(this, "TaskAdded", (sub) =>
            {
                Init(true);
            });
        }

        private void OnFilterTasks(FilterTask obj)
        {
            TaskFilter = obj;
            Init(true);
        }

        private void Refresh(object obj)
        {
            Init(true);
        }

        ObservableCollection<PrioritiesResponse> tempPriorities;
        private void Init(bool refresh)
        {
            if (refresh)
            {
                IsRefreshing = true;
            }
            else
            {
                if (tempPriorities != null)
                {
                    return;
                }
            }
            
            Device.BeginInvokeOnMainThread(async () =>
            {
                try
                {
                    IsRefreshing = true;
                    tempPriorities = await GetPriorities(TaskFilter);
                    Priorities = tempPriorities;
                    AssignPriority();

                }
                catch (Exception e)
                {
                    var properties = new Dictionary<string, string>
                {
                    { "PrioritiesPageViewModel", "OnAppearing" },
                };
                    Crashes.TrackError(e, properties);

                }
                IsRefreshing = false;
            });
        }
      
        /*
        public override void OnAppearing()
        {
            base.OnAppearing();
            if (tempPriorities != null)
            {
                return;
            }
            Device.BeginInvokeOnMainThread(async () =>
            {
                isRtl = new Helpers.Helper().IsRtl;
                try
                {
                    IsBusy = true;
                    tempPriorities = await GetPriorities();
                    Priorities = tempPriorities;
                    AssignPriority();
                    IsBusy = false;

                }
                catch (Exception e)
                {
                    var properties = new Dictionary<string, string>
                {
                    { "PrioritiesPageViewModel", "OnAppearing" },
                };
                    Crashes.TrackError(e, properties);

                }
            });
        }
        */
        public async Task<ObservableCollection<PrioritiesResponse>> GetPriorities(FilterTask filter)
        {
            var api = RestService.For<ITaskyApi>(new System.Net.Http.HttpClient(new HttpLoggingHandler()) { BaseAddress = new Uri(Settings.ApiUrl) });
            try
            {
                NotConnected = false;
                if (Connectivity.NetworkAccess != NetworkAccess.Internet)
                {
                    NotConnected = true;
                    return null;
                }

                //var result = await api.GetPriorities("Bearer " + Settings.AccessToken, Settings.UserId);
                //var result = await api.GetFilterdPriorities("Bearer " + Settings.AccessToken, Settings.UserId,
                //   FromDate: filter.StartDate,
                //   ToDate: filter.EndDate,
                //   StatusID: filter.StatusID,
                //   priorityID: filter.PriorityID,
                //   SectorID: filter.SectorID,
                //   assigneeID: filter.AssigneeID,
                //   projectID: filter.ProjectID);
                return null;
            }
            catch (Exception e)
            {
                try
                {
                    if (Settings.UserName != null && Settings.PasswordData != null)
                    {
                        LoginRequest user = new LoginRequest()
                        {
                            username = Settings.UserName,
                            password = Settings.PasswordData
                        };

                        var key = await api.Authenticate(user);
                        Settings.AccessToken = key.Token;
                    }

                   //var result = await api.GetFilterdPriorities("Bearer " + Settings.AccessToken, Settings.UserId,
                   //FromDate: filter.StartDate,
                   //ToDate: filter.EndDate,
                   //StatusID: filter.StatusID,
                   //priorityID: filter.PriorityID,
                   //SectorID: filter.SectorID,
                   //assigneeID: filter.AssigneeID,
                   //projectID: filter.ProjectID);
                    return null;
                }
                catch 
                {
                    var properties = new Dictionary<string, string>{{ "PrioritiesPageViewModel", "GetPriorities" },};
                    Crashes.TrackError(e, properties);
                }
                
                return null;
            }
        }
        public void AssignPriority()
        {
            try
            {

                if (Priorities != null)
                {
                    var criticalResponse = Priorities.Where(x => x.sectionName == "Critical").FirstOrDefault();
                    if (criticalResponse != null)
                    {

                        Critical = new PrioritiesModel()
                        {
                            Completed = criticalResponse.completed,
                            Delayed = criticalResponse.delayed,
                            InProgress = criticalResponse.inProgress,
                            NotStarted = criticalResponse.notStarted,
                            Returned = criticalResponse.returned,
                            SectionId = criticalResponse.sectionId,
                            TasksCount = criticalResponse.tasksCount
                        };
                        if (isRtl)
                        {
                            Critical.SectionName = "حرج";
                        }
                        else
                        {
                            Critical.SectionName = criticalResponse.sectionName;
                        }
                    }
                    else
                    {
                        Critical = new PrioritiesModel();
                    }
                   

                    var highResponse = Priorities.Where(x => x.sectionName == "High").FirstOrDefault();
                    if (highResponse != null)
                    {
                        High = new PrioritiesModel()
                        {

                            SectionName = highResponse.sectionName,
                            Completed = highResponse.completed,
                            Delayed = highResponse.delayed,
                            InProgress = highResponse.inProgress,
                            NotStarted = highResponse.notStarted,
                            Returned = highResponse.returned,
                            SectionId = highResponse.sectionId,
                            TasksCount = highResponse.tasksCount
                        };
                        if (isRtl)
                        {
                            High.SectionName = "مرتفع";
                        }
                        else
                        {
                            High.SectionName = highResponse.sectionName;
                        }
                    }
                    else
                    {
                        High = new PrioritiesModel();
                    }
                   

                    var NormalResponse = Priorities.Where(x => x.sectionName == "Normal").FirstOrDefault();
                    if (NormalResponse != null)
                    {
                        Normal = new PrioritiesModel()
                        {
                            SectionName = NormalResponse.sectionName,
                            Completed = NormalResponse.completed,
                            Delayed = NormalResponse.delayed,
                            InProgress = NormalResponse.inProgress,
                            NotStarted = NormalResponse.notStarted,
                            Returned = NormalResponse.returned,
                            SectionId = NormalResponse.sectionId,
                            TasksCount = NormalResponse.tasksCount
                        };
                        if (isRtl)
                        {
                            Normal.SectionName = "متوسط";
                        }
                        else
                        {
                            Normal.SectionName = NormalResponse.sectionName;
                        }
                    }
                    else
                    {
                        Normal = new PrioritiesModel();
                    }
                    var LowResponse = Priorities.Where(x => x.sectionName == "Low").FirstOrDefault();
                    if (LowResponse != null)
                    {
                        Low = new PrioritiesModel()
                        {
                            SectionName = LowResponse.sectionName,
                            Completed = LowResponse.completed,
                            Delayed = LowResponse.delayed,
                            InProgress = LowResponse.inProgress,
                            NotStarted = LowResponse.notStarted,
                            Returned = LowResponse.returned,
                            SectionId = LowResponse.sectionId,
                            TasksCount = LowResponse.tasksCount
                        };
                        if (isRtl)
                        {
                            Low.SectionName = "منخفض";
                        }
                        else
                        {
                            Low.SectionName = LowResponse.sectionName;
                        }
                    }
                    else
                    {
                        Low = new PrioritiesModel();
                    }
                }
            }
            catch (Exception e)
            {
                var properties = new Dictionary<string, string>
                {
                    { "PrioritiesPageViewModel", "AssignPriority" },
                };
                Crashes.TrackError(e, properties);
            }
        }
        public async void Tap(object item)
        {
            try
            {
                PrioritiesModel taskdata = item as PrioritiesModel;

                if (taskdata != null && taskdata.TasksCount > 0)
                {
                    //var newObject = item as PrioritiesModel;
                    var navigationParams = new NavigationParameters
                    {
                        { "item", taskdata },
                        { "filterdata", TaskFilter }
                    };
                    MessagingCenter.Send<BaseViewModel>(this, "RefreshTabBarIconsInIOS");
                    await navService.NavigateToAsync<PrioritiesDetailsViewModel>(navigationParams);
                }

            }
            catch (Exception e)
            {
                var properties = new Dictionary<string, string>
                {
                    { "PrioritiesPageViewModel", "Tap" },
                };
                Crashes.TrackError(e, properties);
            }
        }
        
    }
}
