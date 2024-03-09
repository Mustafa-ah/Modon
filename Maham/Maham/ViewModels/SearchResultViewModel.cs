using Microsoft.AppCenter.Crashes;
using Prism.Commands;
using Prism.Mvvm;
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
using Maham.Service;
using Maham.Service.Model.Response.Priorities;
using Maham.Extentions;
using Maham.Setting;
using Xamarin.Forms;
using Maham.Service.General;

namespace Maham.ViewModels
{
    public class SearchResultViewModel : BaseViewModel
    {
        private readonly INavService navService;
        public bool IsBusy { get; set; }
        bool _noresult;
        public bool noresult
        {
            get
            {
                return _noresult;
            }
            set
            {
                SetProperty(ref _noresult, value);
            }
        }
       public bool listhide { get; set; }
        private string _param;
        public string Param
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
        private ObservableCollection<PrioritiesDetails> _task;
        public ObservableCollection<PrioritiesDetails> TasksList
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
        private ObservableCollection<Service.Model.Response.Tasks.SearchResponse> _result;
        public ObservableCollection<Service.Model.Response.Tasks.SearchResponse> ResultList
        {
            get
            {
                return _result;
            }
            set
            {
                SetProperty(ref _result, value);
            }
        }
        public ICommand backnavigationCommand { get; set; }
        public ICommand TaskDetailsCommand { get; set; }
        public SearchResultViewModel(INavService _navService, INavigationService _NavigationServices) : base(_NavigationServices)
        {
            navService = _navService;
            backnavigationCommand = new Command(backnavigationCommandExcute);
            TaskDetailsCommand = new Command(TaskDetailsCommandExcute);
            IsRTL = Settings.IsRtl;
        }

        private async void TaskDetailsCommandExcute(object obj)
        {
            if (obj != null)
            {
                var record = (PrioritiesDetails)obj;
                Settings.TaskId = record.id.ToString();
               // TasksList = new ObservableCollection<PrioritiesDetails>();
                await navService.NavigateToAsync<TaskDetailsPageViewModel>();
            }
        }

        private void backnavigationCommandExcute(object obj)
        {
           // TasksList = new ObservableCollection<PrioritiesDetails>();

            navService.NavigateBackAsync();
        }

        public override async void OnNavigatedTo(NavigationParameters parameters)
        {
            try
            {
                base.OnNavigatedTo(parameters);
                IsBusy = true;
                Param = parameters["query"] as string;
                ResultList = new ObservableCollection<Service.Model.Response.Tasks.SearchResponse>();
                ResultList =await GetSearchResult();
               
                TasksList =LinkSearchData();
                if(TasksList.Count==0)
                {
                    noresult = true;
                    listhide = false;
                }
                else
                {
                    listhide = true;
                    noresult = false;
                }


            }
            catch (Exception e)
            {
                var properties = new Dictionary<string, string>
                {
                    { "PrioritiesDetailsViewModel", "OnNavigatedTo" },
                };
                Crashes.TrackError(e, properties);
                IsBusy = false;
            }
        }

        public ObservableCollection<PrioritiesDetails> LinkSearchData()
        {
            try
            {
                var task = new ObservableCollection<PrioritiesDetails>();
                foreach (var item in ResultList)
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
                        endDate = item.endDate.ToShortDateStringForView(),//.toToShortDateString(),
                        fK_UrgentSupportID = item.fK_UrgentSupportID.ToString(),
                        id = item.id,
                        priority = item.priority,
                        priority_Id = item.priority_Id,
                        progress = item.progress.ToString(),
                        sector = item.sector,
                        startDate = item.startDate.ToShortDateStringForView(),//ToShortDateString(),
                        status = item.status,
                        status_Id = item.status_Id,
                        title = item.title,
                        IsRTL = IsRTL
                    };
                   
                    task.Add(obj);
                }
                noresult= !task.Any();
                IsBusy = false;
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
                IsBusy = false;
            }
        }

        public async Task<ObservableCollection<Service.Model.Response.Tasks.SearchResponse>> GetSearchResult()
        {
            try
            {
                var api = RestService.For<ITaskyApi>(new System.Net.Http.HttpClient(new HttpLoggingHandler()) { BaseAddress = new Uri(Settings.ApiUrl) });
                var result = await api.SearchTask("Bearer " + Settings.AccessToken,  Param, int.Parse(Settings.UserId));
                noresult = !result.Any();
                return result;
            }
            catch (Exception e)
            {
                var properties = new Dictionary<string, string>
                {
                    { "searchresult", "getsearchresult" },
                };
                Crashes.TrackError(e, properties);
                return null;
            }
        }

        public override Task InitializeAsync(object data)
        {
            OnNavigatedTo((NavigationParameters)data);
            return base.InitializeAsync(data);
        }
    }
}
