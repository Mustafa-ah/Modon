using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Input;
using SQLite;
using Maham.Bases;
using Maham.Models;
using Maham.Persistence;
using Maham.Service.Implmentation.Firebase;
using Xamarin.Forms;
using Microsoft.AppCenter.Crashes;
using Refit;
using Maham.Service;
using Maham.Service.Model.Response.Notification;
using Maham.Setting;
using Xamarin.Essentials;
using Maham.Service.General;
using Newtonsoft.Json;

namespace Maham.ViewModels
{
    public class NotificationsPageViewModel : BaseViewModel
    {
        // private SQLiteAsyncConnection sqLiteConnection;
        private ObservableCollection<NotificationDTO> _notificationCollection = new ObservableCollection<NotificationDTO>();
        private bool _isThereNotNotifications = false;
        private string _username;
        private NotificationDTO _SelectedNotification;

        private int pageNumber = 1;
        private bool hasMorePages = true;
        private bool isLTR;

        private readonly INavService navService;
        public ObservableCollection<NotificationDTO> NotificationCollection
        {
            get { return _notificationCollection; }
            set { _notificationCollection = value; RaisePropertyChanged(nameof(NotificationCollection)); }
        }

        public bool IsThereNotNotifications
        {
            get { return _isThereNotNotifications; }
            set { _isThereNotNotifications = value; RaisePropertyChanged(nameof(IsThereNotNotifications)); }
        }

        private bool _isThereNotifications = false;
        public bool IsThereNotifications
        {
            get { return _isThereNotifications; }
            set { _isThereNotifications = value; RaisePropertyChanged(nameof(IsThereNotifications)); }
        }

        public string Username
        {
            get { return _username; }
            set { _username = value; RaisePropertyChanged(nameof(Username)); }
        }


        public NotificationDTO SelectedNotification
        {
            get { return _SelectedNotification; }
            set { _SelectedNotification = value; RaisePropertyChanged(nameof(SelectedNotification)); }
        }

        private string _NotificationCount;

        public string NotificationCount
        {
            get { return _NotificationCount; }
            set { _NotificationCount = value; RaisePropertyChanged(nameof(NotificationCount)); }
        }

        private bool _IsRefreshing;

        public bool IsRefreshing
        {
            get { return _IsRefreshing; }
            set { _IsRefreshing = value; RaisePropertyChanged(nameof(IsRefreshing)); }
        }


        #region Commands
        public ICommand NotificationCollectionRefreshCommand { get; set; }
        public ICommand NotificationSelectedCommand { get; set; }
        public ICommand OnAppearingCommand { get; set; }
        #endregion

        public NotificationsPageViewModel(INavService _navService, INavigationService _NavigationServices) : base(_NavigationServices)
        {
            navService = _navService;
            InitCommand();

            isLTR = !Settings.IsRtl;

            Username = isLTR ? Settings.UserName : Settings.ArabicUserName;
        }


        void InitCommand()
        {
            NotificationCollectionRefreshCommand = new DelegateCommand(async () => await RefreshPage());
            NotificationSelectedCommand = new DelegateCommand(async () => await NotificationSelected());
            OnAppearingCommand = new DelegateCommand(OnAppearing);
        }

        private async Task NotificationSelected()
        {
            try
            {

                Setting.Settings.TaskId = SelectedNotification.TaskId.ToString();
                var api = RestService.For<ITaskyApi>(new System.Net.Http.HttpClient(new HttpLoggingHandler()) { BaseAddress = new Uri(Settings.ApiUrl) });
                var res = await api.MarkNotificationAsRead("Bearer " + Settings.AccessToken, SelectedNotification.Id.ToString());
                if (!res.Success)
                {
                    return;
                }
                await navService.NavigateToAsync<TaskDetailsPageViewModel>();
            }
            catch (Exception e)
            {

            }

        }

        private async Task RefreshPage()
        {
            try
            {
                IsRefreshing = true;
                pageNumber = 1;

                var NotificationHistory = await GetNotificationHistory();
                List<NotificationDTO> notificationsDTO = NotificationHistory.NotificationList ?? new List<NotificationDTO>();
                hasMorePages = NotificationHistory.HasMorePages;
                if (notificationsDTO.Count == 0)
                {
                    hasMorePages = false;
                    IsThereNotNotifications = true;
                    IsThereNotifications = false;
                }
                else
                {
                    NotificationCount = NotificationHistory.UnreadCount == 0 ? "" : $"{NotificationHistory.UnreadCount}";
                    IsThereNotifications = true;
                    IsThereNotNotifications = false;
                    //notificationsDTO.Reverse();
                    NotificationCollection = new ObservableCollection<NotificationDTO>();

                    //english
                    if (isLTR)
                    {
                        foreach (var notificationDTo in notificationsDTO)
                        {
                            NotificationCollection.Add(new NotificationDTO()
                            {
                                PriorityId = notificationDTo.PriorityId,
                                NotificationType = notificationDTo.NotificationType ?? 1,
                                Message = notificationDTo.Message,
                                CreatedBy = notificationDTo.CreatedBy,
                                Title = notificationDTo.Title,
                                TaskId = notificationDTo.TaskId??Guid.Empty,
                                IsReaded = notificationDTo.IsReaded,
                                Id = notificationDTo.Id
                            });

                        }
                    }
                    else
                    {
                        foreach (var notificationDTo in notificationsDTO)
                        {
                            NotificationCollection.Add(new NotificationDTO()
                            {
                                PriorityId = notificationDTo.PriorityId,
                                NotificationType = notificationDTo.NotificationType ?? 1,
                                Message = notificationDTo.MessageAr,
                                CreatedBy = notificationDTo.CreatedBy,
                                Title = notificationDTo.TitleAr,
                                TaskId = notificationDTo.TaskId ?? Guid.Empty,
                                IsReaded = notificationDTo.IsReaded,
                                Id = notificationDTo.Id
                            });

                        }
                    }


                }

                IsRefreshing = false;
            }
            catch (Exception exception)
            {
                IsRefreshing = false;
                var properties = new Dictionary<string, string>
                       {
                             { "notificationpageviewmodel", "refreshpage" },
                       };
                Crashes.TrackError(exception, properties);
            }
        }

        override async public void OnAppearing()
        {
            base.OnAppearing();
            await RefreshPage();
        }
        public async override void BackOnLine()
        {
            await RefreshPage();
        }
        private async Task<NotificationHistoryResponse> GetNotificationHistory()
        {
            var api = RestService.For<ITaskyApi>(new System.Net.Http.HttpClient(new HttpLoggingHandler()) { BaseAddress = new Uri(Settings.ApiUrl) });
            var NotificationHistory = await api.GetNotificationHistory("Bearer " + Settings.AccessToken,
                Settings.UserId, pageNumber);

            if (!NotificationHistory.Success)
            {
                return null;
            }
            else
            {
                NotificationHistoryResponse x = new NotificationHistoryResponse();
                try
                {
                    x = (NotificationHistory.Data).ToObject<NotificationHistoryResponse>();
                }
                catch (Exception e)
                {
                }
                return x;
            }

        }

        public override async void AddTask(object obj)
        {
            await navService.NavigateToAsync<NewTaskPageViewModel>();
        }

        public async Task LoadMoreItems()
        {
            try
            {
                IsRefreshing = true;
                if (IsRefreshing)
                {
                    if (hasMorePages)
                    {
                        pageNumber++;
                        NotificationHistoryResponse notificationHistoryResponse = await GetNotificationHistory();
                        hasMorePages = notificationHistoryResponse.HasMorePages;
                        if (notificationHistoryResponse.NotificationList != null)
                        {
                            foreach (var notificationDTo in notificationHistoryResponse.NotificationList)
                            {
                                if (isLTR)
                                {
                                    NotificationCollection.Add(new NotificationDTO()
                                    {
                                        PriorityId = notificationDTo.PriorityId,
                                        NotificationType = notificationDTo.NotificationType ?? 1,
                                        Message = notificationDTo.Message,
                                        CreatedBy = notificationDTo.CreatedBy,
                                        Title = notificationDTo.Title,
                                        TaskId = notificationDTo.TaskId ?? Guid.Empty,
                                        IsReaded = notificationDTo.IsReaded,
                                        Id = notificationDTo.Id
                                    });;
                                }
                                else
                                {
                                    NotificationCollection.Add(new NotificationDTO()
                                    {
                                        PriorityId = notificationDTo.PriorityId,
                                        NotificationType = notificationDTo.NotificationType ?? 1,
                                        Message = notificationDTo.MessageAr,
                                        CreatedBy = notificationDTo.CreatedBy,
                                        Title = notificationDTo.TitleAr,
                                        TaskId = notificationDTo.TaskId ?? Guid.Empty,
                                        IsReaded = notificationDTo.IsReaded,
                                        Id = notificationDTo.Id
                                    });
                                }
                            }
                        }
                    }
                }
                IsRefreshing = false;
            }
            catch (Exception exception)
            {
                IsRefreshing = false;
                var properties = new Dictionary<string, string>
                       {
                             { "notificationpageviewmodel", "LoadMoreItems" },
                       };
                Crashes.TrackError(exception, properties);
            }

        }
    }
}
