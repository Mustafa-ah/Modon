using Microsoft.AppCenter.Crashes;
using Prism.Navigation;
using Refit;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Maham.Bases;
using Maham.Constants;
using Maham.Models;
using Maham.Service;
using Maham.Service.Model.Request.Login;
using Maham.Service.Model.Response.Tasks;
using Maham.Setting;
using Xamarin.Forms;
using System.Collections;
using System.Linq;
using Maham.Enums;
using Maham.Views;
using Maham.Service.Model.Response;

namespace Maham.ViewModels
{
    public class TasksPageViewModel : BaseViewModel
    {
        #region Fields
        public bool isRtl;
        #endregion
        #region Private Members
        public int CurrentPage { get; set; } = 2;
        private ObservableCollection<TaskTabbedPage> _taskTabbedPages= new ObservableCollection<TaskTabbedPage>();
        private ICommand CurrentPageChangedCommand { get; set; }
       
        private ObservableCollection<TabsResponse> _tabsResponse;

        public TasksMode Taskmode { get; set; }
        #endregion

        #region properties

        public ObservableCollection<TaskTabbedPage> TaskTabbedPages
        {
            get { return _taskTabbedPages; }
            set { SetProperty(ref _taskTabbedPages, value);
            RaisePropertyChanged();}
        }
        public override void OnNavigatedFrom(NavigationParameters parameters)
        {
            base.OnNavigatedFrom(parameters);
        }
        public override void OnNavigatedTo(NavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
        }
        public override void OnNavigatingTo(NavigationParameters parameters)
        {
           
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

        #endregion

        public TasksPageViewModel(INavigationService _NavigationServices) : base(_NavigationServices)
        {

           

            isRtl = new Helpers.Helper().IsRtl;
            //TaskTabbedPage priorities = new TaskTabbedPage();
            TaskTabbedPage empty = new TaskTabbedPage();
            //if (isRtl)
            //{
            //    priorities.TabName = "الاولويات";
            //}
            //else
            //{
            //    priorities.TabName = "Priorities";
            //}
            TaskTabbedPages.Add(empty);
            Init();
            Taskmode = TasksMode.TaskList;

            MessagingCenter.Subscribe<MainTabbedPage, int>(this, "ModeChanged", OnModeChanged);
        }

        private void OnModeChanged(MainTabbedPage arg1, int arg2)
        {
            Taskmode = (TasksMode)arg2;
            
            UpdateTabs();
            //MessagingCenter.Send(this, "ReLoadTasks", arg2);
        }

        private ObservableCollection<TabsResponse> tabsTemp;
        private void Init()
        {
            if (tabsTemp != null)
            {
                return;
            }
            Device.BeginInvokeOnMainThread(async () =>
            {
                isRtl = new Helpers.Helper().IsRtl;
                var tabsTemp = await GetTabs();
                TabsResponse = tabsTemp;
                InitTaskTabbedPages();            
            });
        }


        private void UpdateTabs()
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                try
                {
                    // clear old list not work
                    var tabs = await GetTabs();
                    TaskTabbedPages = new ObservableCollection<TaskTabbedPage>();

                    //  ObservableCollection < TaskTabbedPage > tempTabs = new ObservableCollection<TaskTabbedPage>();

                    foreach (var item in tabs)
                    {
                        var TaskTabbedPage = new TaskTabbedPage();

                        TaskTabbedPage.Id = item.Id;
                        if (isRtl)
                        {
                            TaskTabbedPage.TabName = item.TitleAr;
                            TaskTabbedPages.Insert(0, TaskTabbedPage);
                        }
                        else
                        {
                            TaskTabbedPage.TabName = item.Title;
                            TaskTabbedPages.Add(TaskTabbedPage);
                        }

                    }
                    // TaskTabbedPages = tempTabs;
                    int index_ = isRtl ? TaskTabbedPages.Count - 1 : 0;
                    TaskTabbedPages[index_].IsFirstTab = true;

                    await System.Threading.Tasks.Task.Delay(500);
                    MessagingCenter.Send(this, "SetCurrentTaskPage");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            });
        }

        public async Task<ObservableCollection<TabsResponse>> GetTabs()
        {
            try
            {
              
                var api = RestService.For<ITaskyApi>(new System.Net.Http.HttpClient(new HttpLoggingHandler()) { BaseAddress = new Uri(Settings.ApiUrl) });


                ResultData<TabsResponse> result = null;

                switch (Taskmode)
                {
                    case TasksMode.TaskList:
                        //result = await api.GetAllTaskListViews("Bearer " + Settings.AccessToken);
                        var jsonData = await api.GetAllTaskListViews("Bearer " + Settings.AccessToken);
                        result = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultData<TabsResponse>>(jsonData);
                        break;
                    case TasksMode.TaskListUserGroup:
                        //result = await api.GetAllTaskListUserGroupViews("Bearer " + Settings.AccessToken);
                        var jsonData2 = await api.GetAllTaskListUserGroupViews("Bearer " + Settings.AccessToken);
                        result = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultData<TabsResponse>>(jsonData2);
                        break;
                    case TasksMode.ClosedTasksList:
                        //result = await api.GetAllClosedTasks("Bearer " + Settings.AccessToken);
                        var jsonData3 = await api.GetAllClosedTasks("Bearer " + Settings.AccessToken);
                        result = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultData<TabsResponse>>(jsonData3);
                        break;
                    default:
                        break;
                }
                 // var result = await api.GetTabs("Bearer " + Settings.AccessToken, Settings.UserId);
                

                ObservableCollection<TabsResponse> collection = new ObservableCollection<TabsResponse>();

                if (result.Success)
                {
                    
                    foreach (var item in result.Data)
                    {
                        collection.Add(item);
                    }
                }
                

                return collection;// result;

            }
            catch (Exception e)
            {
                try
                {
                    var api = RestService.For<ITaskyApi>(new System.Net.Http.HttpClient(new HttpLoggingHandler()) { BaseAddress = new Uri(Settings.ApiUrl) });
                    if (Settings.UserName != null && Settings.PasswordData != null)
                    {
                        LoginRequest user = new LoginRequest()
                        {
                            username = Settings.UserName,
                            password = Settings.PasswordData
                        };

                        var key = await api.Authenticate(user);
                        Settings.AccessToken = key.Token;
                       // var result = await api.GetTabs("Bearer " + Settings.AccessToken, Settings.UserId);
                       // return result;
                    }
                }
                catch (Exception ex)
                {
                    Crashes.TrackError(ex);
                }
                
                    var properties = new Dictionary<string, string> { { "TasksPageViewModel", "GetTabs" } };
                    Crashes.TrackError(e, properties);
                    return null;  
            }
        }
        public void InitTaskTabbedPages()
        {
            try
            {
                
                if (TabsResponse.Count > 0)
                {
                    var firstTab = TabsResponse[0];

                    foreach (var item in TabsResponse)
                    {
                        if (item == firstTab)
                        {
                            TaskTabbedPages[0].Id = firstTab.Id;
                            string tabTitle = isRtl ? item.TitleAr : firstTab.Title;
                            TaskTabbedPages[0].TabName = tabTitle;
                            continue;
                        }

                        TaskTabbedPage notPriorities = new TaskTabbedPage();

                        notPriorities.Id = item.Id;

                        notPriorities.IsFirstTab = false;

                        
                        if (isRtl)
                        {
                            notPriorities.TabName = item.TitleAr;
                            TaskTabbedPages.Insert(0, notPriorities);
                        }
                        else
                        {
                            notPriorities.TabName = item.Title;
                            TaskTabbedPages.Add(notPriorities);
                        }

                    }

                    int index_ = isRtl ? TaskTabbedPages.Count - 1 : 0;
                    TaskTabbedPages[index_].IsFirstTab = true;

                    MessagingCenter.Send(this, "SetCurrentTaskPage");
                }
                
            }
            catch (Exception e)
            {
                var properties = new Dictionary<string, string>
                {
                    { "TasksPageViewModel", "InitTaskTabbedPages" },
                };
                Crashes.TrackError(e, properties);
            }
        }

    }
}
