using Microsoft.AppCenter.Crashes;
using Prism.Events;
using Prism.Navigation;
using Refit;
using Syncfusion.SfGauge.XForms;
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
using Maham.Service.Model.Response.Tasks;
using Maham.Setting;
using Xamarin.Forms;
using Maham.Service.Model.Response;
using System.Net.Http;
using System.Net.Http.Headers;
using Maham.Enums;
using Maham.Views;
using Maham.Service.Model.Request.Tasks;
using Maham.Helpers;

namespace Maham.ViewModels
{
    public class NotPrioritiesPageViewModel : BaseViewModel
    {
        #region fields
        //private NotPrioritiesTabContentApi _oldNotPrioritiesTabContentApi;
        // private INavigationService _navigationService;
        private readonly INavService navService;

        public FilterTask TaskFilter { get; set; }
        public bool FullMode { get; set; }
        public int TaskMode { get; set; }
        private int pageNumber = 1;
        private int pageSize = 10;
        public Guid? TabId { get; set; }
        public int  CurrentSectionsPage { get; set; }
        #endregion
        #region Private Properties
        private ObservableCollection<NotPrioritiesTabContentApi> _notPrioritiesTabContent;
        private ObservableCollection<TabsResponse> _tabsResponse;
      
        private bool _isBusy;
        private bool _isRefreshing;

        #endregion
        #region Public Properties
        public bool currentPage;
        public ICommand DeleteRowCommand { get; set; }
        public ICommand EditRowCommand { get; set; }
        public ICommand TaskTappedCommand { get; set; }
        public ICommand RefreshCommand { get; set; }
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
        public ImageSource ArrowImage
        {
            get
            {

                if (IsRTL)
                {
                    return "left_arrow.png";
                }
                else
                {
                    return "RightArrow.png";
                }

            }
        }
        public ObservableCollection<NotPrioritiesTabContentApi> NotPrioritiesTabContent
        {
            get
            {
                return _notPrioritiesTabContent;
            }
            set
            {
                SetProperty(ref _notPrioritiesTabContent, value);
            }
        }
       
        public ObservableCollection<TabsResponse> TabsResponse
        {
            get
            {
                if (_tabsResponse == null)
                {
                    return _tabsResponse = new ObservableCollection<TabsResponse>();
                }

                return _tabsResponse;
            }
            set
            {
                SetProperty(ref _tabsResponse, value);
            }
        }
       
        public Command SelectNotPrioritiesTabContentCommand { get; private set; }
        #endregion
        #region Working with list
        public TabContentViewModel _oldTabContentViewModel { get; set; }

        private ObservableCollection<TabContentViewModel> items;
        public ObservableCollection<TabContentViewModel> Items
        {
            get => items;

            set => SetProperty(ref items, value);
        }


        public Command<TabContentViewModel> RefreshItemsCommand { get; set; }

        #endregion

        public bool NoData { get; set; }
        public bool ShouldReloadData { get; set; }
        public NotPrioritiesPageViewModel(INavService _navService, IEventAggregator _eventAggregator, INavigationService _NavigationServices) : base(_NavigationServices)
        {
            navService = _navService;
            _notPrioritiesTabContent = new ObservableCollection<NotPrioritiesTabContentApi>();
            SelectNotPrioritiesTabContentCommand = new Command(SelectNotPrioritiesTabContent);

            items = new ObservableCollection<TabContentViewModel>();
            Items = new ObservableCollection<TabContentViewModel>();

            DeleteRowCommand = new Command(DeleteRow);
            EditRowCommand = new Command(EditRow);
            TaskTappedCommand = new Command(TaskTapped);
            RefreshCommand = new Command(Refresh);

            RefreshItemsCommand = new Command<TabContentViewModel>(async (item) => await ExecuteRefreshItemsCommand(item));

            Settings.Tabs.Add(this);
            TaskFilter = new FilterTask();
            _eventAggregator.GetEvent<FilterTasksEvent>().Subscribe(OnFilterTasks);
            IsRTL = new Helpers.Helper().IsRtl;

            MessagingCenter.Subscribe<NewTaskPageViewModel>(this, "TaskAdded", (sub) =>
            {
                GetData(TabId);
            });

            MessagingCenter.Subscribe<EditTaskViewModel>(this, "TaskEdited", (sub) =>
            {
                GetData(TabId);
            });

            MessagingCenter.Subscribe<MainTabbedPage, bool>(this, "FullModeChanged", (s, a) =>
            {
                FullMode = a;
                GetData(TabId);
            });

            MessagingCenter.Subscribe<TasksPageViewModel, int>(this, "ReLoadTasks", (s, a) =>
           {
               TaskMode = a;
               GetData(TabId);
           });

            MessagingCenter.Subscribe<DeletePopupPageViewModel>(this, "TaskDeleted", (sub) =>
            {
                GetData(TabId);
            });

            MessagingCenter.Subscribe<TaskDetailsPageViewModel>(this, "TaskClosedReturned", (sub) =>
            {
                ShouldReloadData = true;
                //GetData(TabId);
            });

            //
            InitializeAsync(null);

            FullMode = Settings.FullMode;
            TaskMode = (int)TasksMode.TaskList;


        }

        private void OnFilterTasks(FilterTask obj)
        {
            TaskFilter = obj;
            GetData(TabId);
        }

        private void Refresh(object obj)
        {
            IsRefreshing = true;
            pageNumber = 1;
            GetData(TabId);
        }

        private async void TaskTapped(object obj)
        {
            if (obj != null)
            {
                TaskViewModel record = (TaskViewModel)obj;
                Settings.TaskId = record.Id.ToString();
                // IsBusy = true;
                //UnExpandAllItems();
                //  await StoragPremissionGranted();
                DependencyService.Get<IFileHelper>().GetStoragePermission();
                await navService.NavigateToAsync<TaskDetailsPageViewModel>();
                // await _navigationService.NavigateAsync("TaskDetailsPage");
                // await _navigationService.NavigateAsync("MainTabbedPage?selectedTab=TasksPage/TaskDetailsPage");
                //IsBusy = false;
            }
        }

        //  ObservableCollection<TabsResponse> _tempTabsResponses;

        public override void OnAppearing()
        {
            base.OnAppearing();

            if ((Items.Count == 0 && TabId != null) || ShouldReloadData)
            {
                GetData(TabId);
                ShouldReloadData = false;
            }
        }

        public void GetData(Guid? ID = null)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                try
                {
                    if (IsBusy)
                    {
                        return;
                    }
                    IsBusy = true;

                    CurrentSectionsPage = 0;

                   List< UserTasksResponse> uerTasksResponse = await GetFilterdTasks(TaskFilter, ID, CurrentSectionsPage);

                    LinkData(uerTasksResponse);

                    AddItemsToItemsList();

                    currentPage = true;
                    IsBusy = false;
                    IsRefreshing = false;
                }
                catch (Exception exception)
                {
                    IsBusy = false;
                    var properties = new Dictionary<string, string> { { "NotPrioritiesPageViewModel", "GetData" }, };
                    Crashes.TrackError(exception, properties);
                }
            });
        }


        private List<Guid> GetEntitiesString(List<Entity> entites)
        {
            List<Guid> str = new List<Guid>();

            if (entites == null )
            {
                return str;
            }
            if (entites.Count == 0)
            {
                return str;
            }
            for (int i = 0; i < entites.Count; i++)
            {
                str.Add(entites[i].Value);
            }
            
            return str;
        }

        private async Task<List<UserTasksResponse>> GetFilterdTasks(FilterTask filter, Guid? ID, int page)
        {
            try
            {
                List<UserTasksResponse> collection = new List<UserTasksResponse>();
                Id = ID.HasValue ? ID.Value : Id;

                if (Id != Guid.Empty)
                {

                    Guid[] entites = new Guid[] { Guid.Empty };// GetEntitiesString(filter.Entities);

                    var api = RestService.For<ITaskyApi>(new System.Net.Http.HttpClient(new HttpLoggingHandler()) { BaseAddress = new Uri(Settings.ApiUrl) });
                    var result = await api.GetAllTaskListViewSections("Bearer " + Settings.AccessToken, Id, page, pageSize);

                    CanLoadMoreSections = result.ListData[1] > (pageSize * (page + 1));//page start from 0

                   //List<Section> sections = (result.ListData[0]).ToObject<List<Section>>();
                    List<Section> sections = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Section>>(Convert.ToString(result.ListData[0]));

                    foreach (var item in sections)
                    {
                        UserTasksResponse userTasksResponse = new UserTasksResponse();
                        userTasksResponse.sectionID = item.Id;
                        userTasksResponse.FieldType = item.FieldType;
                        userTasksResponse.sectionName = item.Title;
                        userTasksResponse.sectionNameAr = item.TitleAr;
                        var count = 0;

                        var tasksDetails = await GetTasks(item, 0);

                        userTasksResponse.tasks = tasksDetails.Data;
                        count = (int)tasksDetails.Count;
                        userTasksResponse.tasksCount = count.ToString();

                        collection.Add(userTasksResponse);

                    }


                }

                return collection;// result;
            }
            catch (Exception exception)
            {
                IsBusy = false;
                var properties = new Dictionary<string, string>
                       {
                             { "notprioritiespageviewmodel", "getfilltertask" },
                       };
                Crashes.TrackError(exception, properties);
                return new List<UserTasksResponse>();
            }

        }

       
        private void SelectNotPrioritiesTabContent(object obj)
        {
            string dd = obj.ToString();
            //throw new NotImplementedException();
        }

        public async Task<ObservableCollection<UserTasksResponse>> GetUserTasks()
        {
            try
            {
                var api = RestService.For<ITaskyApi>(new System.Net.Http.HttpClient(new HttpLoggingHandler()) { BaseAddress = new Uri(Settings.ApiUrl) });
                var result = await api.GetUserTasks("Bearer " + Settings.AccessToken, Settings.UserId, tabId);
                return result;
            }
            catch (Exception exception)
            {
                IsBusy = false;
                var properties = new Dictionary<string, string>
                       {
                             { "notpriortiespageviewmodel", "getusertask" },
                       };
                Crashes.TrackError(exception, properties);
                return null;
            }
        }
        public async Task<ObservableCollection<UserTasksResponse>> GetUserTasksWithFiltter()
        {
            try
            {
                var api = RestService.For<ITaskyApi>(new System.Net.Http.HttpClient(new HttpLoggingHandler()) { BaseAddress = new Uri(Settings.ApiUrl) });
                var result = await api.GetUserTasks("Bearer " + Settings.AccessToken, Settings.UserId, tabId);
                return result;
            }
            catch (Exception exception)
            {
                IsBusy = false;
                var properties = new Dictionary<string, string>
                       {
                             { "notprioritiespageviewmodel", "getusertaskwithfillter" },
                       };
                Crashes.TrackError(exception, properties);
                return null;
            }
        }
        public bool isExpanded = false;
        private async System.Threading.Tasks.Task ExecuteRefreshItemsCommand(TabContentViewModel item)
        {
            try
            {
                
                if (_oldTabContentViewModel == item)
                {

                    if (item.Expanded == true)
                    {
                        item.Expanded = false;
                    }
                    else
                    {
                        item.Expanded = true;
                    }

                    // click twice on the same item will hide it
                    // item.Expanded = !item.Expanded;
                }
                else
                {
                    if (_oldTabContentViewModel != null)
                    {
                        // hide previous selected item
                        _oldTabContentViewModel.Expanded = false;
                    }
                    // show selected item
                    item.Expanded = true;
                }

                _oldTabContentViewModel = item;

            }
            catch (Exception exception)
            {
                IsBusy = false;
                var properties = new Dictionary<string, string>
                       {
                             { "notpriortiespageviewmodel", "excuterefeshitemcommand" },
                       };
                Crashes.TrackError(exception, properties);
            }
        }
        private void UnExpandAllItems()
        {
            foreach (var _item in Items)
            {
                _item.Expanded = false;
            }
        }
        public void AddItemsToItemsList()
        {
            try
            {

                Items = new ObservableCollection<TabContentViewModel>();

                if (NotPrioritiesTabContent != null && NotPrioritiesTabContent.Count > 0)
                {
                    foreach (var tabContent in NotPrioritiesTabContent)
                    {
                        Items.Add(new TabContentViewModel(this, tabContent));
                    }
                 
                    NoData = false;
                }
                else
                {
                    IsEmpty = true;
                    NoData = true;
                }
            }
            catch (Exception exception)
            {
                IsBusy = false;
                var properties = new Dictionary<string, string>
                       {
                             { "notpriortiespageviewmodel", "excuteloaditemcommand" },
                       };
                Crashes.TrackError(exception, properties);
            }
            finally
            {
                IsBusy = false;
            }
        }
        public void LinkData(List<UserTasksResponse> userTasksResponses)
        {
            try
            {
                if (NotPrioritiesTabContent == null)
                {
                    NotPrioritiesTabContent = new ObservableCollection<NotPrioritiesTabContentApi>();
                }
                else
                {
                    NotPrioritiesTabContent.Clear();
                }

                foreach (var item in userTasksResponses)
                {
                    NotPrioritiesTabContentApi tabContent = new NotPrioritiesTabContentApi();
                    if (IsRTL)
                    {
                        tabContent.SectionName = item.sectionNameAr;
                    }
                    else
                    {
                        tabContent.SectionName = item.sectionName;
                    }
                    tabContent.SectionID = item.sectionID;
                    tabContent.FieldType = item.FieldType;
                    tabContent.TaskCount = item.tasksCount;

                    if (item.tasks != null)
                    {
                        foreach (var taskItem in item.tasks)
                        {
                            if (IsRTL)
                            {
                              //  taskItem.status = Helpers.QuickTranslator.TranslateStatus(taskItem.status_Id);
                            }
                            TaskItem Inastance = new TaskItem()
                            {
                                AssignedTo = taskItem.Assignee,
                                //AssignedTo_ID = taskItem.assignedTo_ID,
                                Average_progress = taskItem.Progress,
                                Description = taskItem.Description,
                                FK_UrgentSupportID = "taskItem.fK_UrgentSupportID",
                                Flag = taskItem.ArabicName,
                                //Flag_id = taskItem.flag_id,
                                Id = taskItem.Id,
                                Priority = taskItem.PriorityId.ToString(),
                                Priority_Id = taskItem.PriorityId,
                                Progress = taskItem.Progress.ToString(),
                                Sector = taskItem.Sector,
                                Status = taskItem.StatusId.ToString(),
                                Status_Id = taskItem.StatusId,
                                Title = taskItem.Title,
                            };
                            Inastance.StartDate = taskItem.StartDate.ToShortDateStringForView();//.ToShortDateStringFromApIDateTime();// DateTime.Parse(taskItem.startDate, CultureInfo.InvariantCulture).ToShortDateString();
                            Inastance.EndDate = taskItem.EndDate.ToShortDateStringForView();//.ToShortDateStringFromApIDateTime();// DateTime.Parse(taskItem.endDate, CultureInfo.InvariantCulture).ToShortDateString();
                            tabContent.Tasks.Add(Inastance);
                        }
                    }
                    
                    NotPrioritiesTabContent.Add(tabContent);
                }
            }
            catch (Exception exception)
            {
                IsBusy = false;

                var properties = new Dictionary<string, string>
                       {
                             { "notpriortiespageviewmodel", "linkdata" },
                       };
                Crashes.TrackError(exception, properties);
            }

        }
        public override async void AddTask(object obj)
        {
            IsBusy = true;
            await navService.NavigateToAsync<NewTaskPageViewModel>();
            IsBusy = false;
        }
        public override void OnDisappearing()
        {
            base.OnDisappearing();
            //   UnExpandAllItems();
            this.NotPrioritiesTabContent = null;

            this.TabsResponse = null;
            this.Items = null;
            this.items = null;
            currentPage = false;
        }
        public override void OnNavigatedFrom(NavigationParameters parameters)
        {
            base.OnNavigatedFrom(parameters);
            UnExpandAllItems();
            this.NotPrioritiesTabContent = null;

            this.TabsResponse = null;
            this.Items = null;
            this.items = null;
        }
        private async void EditRow(object obj)
        {

            if (obj != null)
            {
                TaskViewModel record = (TaskViewModel)obj;
                Settings.TaskId = record.Id.ToString();
                IsBusy = true;
                UnExpandAllItems();
                await navService.NavigateToAsync<EditTaskViewModel>();
                IsBusy = false;
            }
        }
        private async void DeleteRow(object obj)
        {
            try
            {
                if (obj != null)
                {
                    TaskViewModel record = (TaskViewModel)obj;
                    bool alertResult = await Application.Current.MainPage.DisplayAlert("Warring", "Are you sure delete this task", "Yes", "No");
                    if (alertResult)
                    {
                        IsBusy = true;
                        var apiResult = await DeleteTask(record.Id.ToString());
                        if (apiResult.Success)
                        {
                            foreach (var item in NotPrioritiesTabContent)
                            {
                                if (item.Tasks.Count > 0)
                                {
                                    TaskItem deletedItem = item.Tasks.Where(x => x.Id == record.Id).FirstOrDefault();
                                    if (deletedItem != null)
                                    {
                                        item.Tasks.Remove(deletedItem);
                                        item.TaskCount = (Convert.ToInt32(item.TaskCount) - 1).ToString();
                                    }
                                }
                            }
                            foreach (var tabContent in Items)
                            {

                                if (tabContent.Count > 0)
                                {
                                    var deletedItem = tabContent.Where(x => x.Id == record.Id).FirstOrDefault();

                                    if (deletedItem != null)
                                    {
                                        tabContent.TabContentTasks.Remove(deletedItem);
                                        tabContent.Remove(deletedItem);
                                        tabContent.TaskCount = (Convert.ToInt32(tabContent.TaskCount) - 1).ToString();
                                    }
                                }
                            }

                            await Application.Current.MainPage.DisplayAlert("Successfully", "Task has been deleted", "Ok");
                        }
                        else
                        {
                            await Application.Current.MainPage.DisplayAlert("Warring", "", "Ok");//
                        }
                        IsBusy = false;
                    }
                }

            }
            catch (Exception exception)
            {
                IsBusy = false;
                var properties = new Dictionary<string, string>
                {
                    { "NotPrioritiesPageViewModel", "DeleteRow" },
                };
                Crashes.TrackError(exception, properties);
            }
        }
        private async Task<Result> DeleteTask(string taskId)
        {
            try
            {
                var api = RestService.For<ITaskyApi>(new System.Net.Http.HttpClient(new HttpLoggingHandler()) { BaseAddress = new Uri(Settings.ApiUrl) });
                var result = await api.DeleteTask("Bearer " + Settings.AccessToken, taskId);
                return result;
            }
            catch (Exception exception)
            {
                IsBusy = false;
                var properties = new Dictionary<string, string>
                       {
                             { "notpriortiespageviewmodel", "detetask" },
                       };
                Crashes.TrackError(exception, properties);
                return null;
            }
        }

        public async Task<bool> LoadMoreTasks(TaskViewModel taskViewModel)
        {
            try
            {
                if (_oldTabContentViewModel != null)
                {
                    var count = _oldTabContentViewModel.Tasks.Count;
                    var index = Items.IndexOf(_oldTabContentViewModel);
                    var tab = items[index];

                    // string entites = GetEntitiesString(TaskFilter.Entities);
                    Guid[] entites = new Guid[1];

                    if (tab != null && _oldTabContentViewModel.Tasks.Count() > 0 && taskViewModel.TaskItem == _oldTabContentViewModel.Tasks[count - 1])
                    {
                        if (int.Parse(tab.TaskCount) == count)
                            return true;
                        pageNumber = count / pageSize;
                        var api = RestService.For<ITaskyApi>(new System.Net.Http.HttpClient(new HttpLoggingHandler()) { BaseAddress = new Uri(Settings.ApiUrl) });
                        List<TaskDto> tasks = new List<TaskDto>();

                        var tasksDetails = await GetTasks(new Section() { Id = _oldTabContentViewModel.SectionID, FieldType = _oldTabContentViewModel.FieldType }, pageNumber);
                        tasks = tasksDetails.Data;

                        if (tasks.Count == 0)
                        {
                            return true;
                        }
                        NotPrioritiesTabContentApi tabContent = new NotPrioritiesTabContentApi();
                        tabContent.SectionName = tab.SectionName;
                        tabContent.SectionID = tab.SectionID;
                        tabContent.FieldType = tab.FieldType;
                        tabContent.TaskCount = tab.TaskCount;
                        tabContent.Tasks = tab.Tasks;

                        foreach (var taskItem in tasks)
                        {
                            if (IsRTL)
                            {
                               // taskItem.status = Helpers.QuickTranslator.TranslateStatus(taskItem.status_Id);
                            }
                            TaskItem Inastance = new TaskItem()
                            {
                                AssignedTo = taskItem.Assignee,
                                //AssignedTo_ID = taskItem.assignedTo_ID,
                                Average_progress = taskItem.Progress,
                                Description = taskItem.Description,
                                FK_UrgentSupportID = "taskItem.fK_UrgentSupportID",
                                Flag = taskItem.ArabicName,
                                //Flag_id = taskItem.flag_id,
                                Id = taskItem.Id,
                                Priority = taskItem.PriorityId.ToString(),
                                Priority_Id = taskItem.PriorityId,
                                Progress = taskItem.Progress.ToString(),
                                Sector = taskItem.Sector,
                                Status = taskItem.StatusId.ToString(),
                                Status_Id = taskItem.StatusId,
                                Title = taskItem.Title,
                            };
                            Inastance.StartDate = taskItem.StartDate.ToShortDateStringForView();//.ToShortDateStringFromApIDateTime();// DateTime.Parse(taskItem.startDate, CultureInfo.InvariantCulture).ToShortDateString();
                            Inastance.EndDate = taskItem.EndDate.ToShortDateStringForView();//.ToShortDateStringFromApIDateTime();// DateTime.Parse(taskItem.endDate, CultureInfo.InvariantCulture).ToShortDateString();
                            tabContent.Tasks.Add(Inastance);
                        }
                        var x = new TabContentViewModel(this, tabContent);
                        x.Expanded = _oldTabContentViewModel.Expanded;
                        _oldTabContentViewModel = x;
                        Items[index] = x;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
           
        }

        public bool CanLoadMoreSections { get; set; }
        public async Task<bool> LoadMoreSections()
        {
            try
            {
                if (!CanLoadMoreSections)
                {
                    return false;
                }
                
                CurrentSectionsPage++;

                List<UserTasksResponse> uerTasksResponse = await GetFilterdTasks(TaskFilter, TabId, CurrentSectionsPage);

                LinkData(uerTasksResponse);

                foreach (var tabContent in NotPrioritiesTabContent)
                {
                    Items.Add(new TabContentViewModel(this, tabContent));
                }

                return true;
            }
            catch (Exception ex)
            {
                IsRefreshing = false;
                return false;
            }
        }


        //test

        private async Task<ResultData<TaskDto>> GetTasks(Section item, int pageNumber)
        {
            try
            {
                FilterDto filterDto = TaskFilter.GetFilterDto();

                var entites = GetEntitiesString(TaskFilter.Entities);

                Guid entityId = Guid.Empty;
                Guid userGroupId = Guid.Empty;

                if (TaskMode == (int)TasksMode.TaskList)
                {
                    entityId = TaskFilter.EntityId;
                }
                else if (TaskMode == (int)TasksMode.TaskListUserGroup)
                {
                    userGroupId = TaskFilter.EntityId;
                }

                filterDto.Entities = entites;
                filterDto.UserGroupId = userGroupId;

                //string startDateApi = TaskFilter.StartDate.ToDateTimeStringForAPI();
                //string entDateApi = TaskFilter.EndDate.ToDateTimeStringForAPI();



                var api = RestService.For<ITaskyApi>(new System.Net.Http.HttpClient(new HttpLoggingHandler()) { BaseAddress = new Uri(Settings.ApiUrl) });
                var result = await api.GetAllTaskListViewSectionData("Bearer " + Settings.AccessToken, filterDto, pageNumber, 10, item.Id, item.FieldType, Settings.UserId, FullMode);

                // string url = $"/api/View/GetAllTaskListViewSectionData?Page={pageNumber}&PageSize=10&ID={item.Id}&FieldType={item.FieldType}&UserID={Setting.Settings.UserId}&FullMode={FullMode}&StatusId={TaskFilter.StatusID}&PriorityId={TaskFilter.PriorityID}&Entities={entites}&SourceId={TaskFilter.SourceId}&UserGroupId={userGroupId}&SearchTask={TaskFilter.SearchTitle}&FromDate={startDateApi}&ToDate={entDateApi}&ResponsibleID.ID={TaskFilter.ResponsibleID.ID}&ResponsibleID.RoleID={TaskFilter.ResponsibleID.RoleID}&ResponsibleID.Type={TaskFilter.ResponsibleID.Type}";
               // string url = $"/api/View/GetAllTaskListViewSectionData?Page={pageNumber}&PageSize=10&ID={item.Id}&FieldType={item.FieldType}&UserID={Setting.Settings.UserId}&FullMode={FullMode}";

               // string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Service.Model.Response.Tasks.Task>(resu)

                return result;

                //HttpContent httpContent = new StringContent(json);
                //using (var httpClient = new HttpClient())
                //{
                //    httpClient.DefaultRequestHeaders.Authorization
                //             = new AuthenticationHeaderValue("Bearer", Settings.AccessToken);
                //    httpClient.DefaultRequestHeaders.Add("Content-Type", "application/json");
                //    httpClient.BaseAddress = new Uri(Constants.AppConstants.BasicURL);

                //    using (var response = await httpClient.PostAsync(url, httpContent))
                //    {
                //        string responseData = await response.Content.ReadAsStringAsync();

                //        return Newtonsoft.Json.JsonConvert.DeserializeObject<ResultData<Service.Model.Response.Tasks.Task>>(responseData);
                //    }
                //}
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return new ResultData<TaskDto>();
        }
    }
}
