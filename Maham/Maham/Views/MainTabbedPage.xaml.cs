using Messier16.Forms.Controls;
using Plugin.Badge.Abstractions;
using Prism.Navigation;
using Rg.Plugins.Popup.Services;
using System;
using System.Windows.Input;
using Maham.Bases;
using Maham.CustomControl;
using Maham.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Maham.Setting;
using Maham.Enums;
using Maham.Models;
using Prism.Events;
using Maham.Events;

namespace Maham.Views
{
    public partial class MainTabbedPage : ExtCustomTabbedPage
    {
        public bool isRtl;
        public ICommand ClickCommad { get; set; }
        public TasksMode tasksMode { get; set; }
        public ICommand SearchCommand { get; set; }
        private bool ShouldRefreshIosTabBarIcons;
        FilterTask TaskFilter { get; set; }
        FilterTask DashboardFilter { get; set; }
        public MainTabbedPage()
        {
            InitializeComponent();
            isRtl = new Helpers.Helper().IsRtl;
            InitializeTabs();

            tasksMode = TasksMode.TaskList;
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            Xamarin.Forms.NavigationPage.SetHasBackButton(this, false);
            //NavigationPage.SetHasNavigationBar(this, false); 
            //On<Android>().SetToolbarPlacement(ToolbarPlacement.Bottom);
            On<Xamarin.Forms.PlatformConfiguration.Android>().SetToolbarPlacement(ToolbarPlacement.Bottom);
            On<Xamarin.Forms.PlatformConfiguration.Android>().SetBarSelectedItemColor(Color.FromHex("#00ace6"));//#3399ff
            On<Xamarin.Forms.PlatformConfiguration.Android>().SetBarItemColor(Color.FromHex("#303C56"));
            On<Xamarin.Forms.PlatformConfiguration.Android>().SetIsSwipePagingEnabled(false);
            //FlowDirection = new Helpers.Helper().CurrentLanguage() == 1 ? FlowDirection.LeftToRight : FlowDirection.RightToLeft;
            filtertoolbar.FlowDirection = new Helpers.Helper().CurrentLanguage() == 1 ? FlowDirection.LeftToRight : FlowDirection.RightToLeft;
            ClickCommad = new Command(Click);
            SearchCommand = new Command(SearchCommandExcute);

            //if (Xamarin.Forms.Application.Current.Properties.ContainsKey("tabIndex"))
            //{
            //    int _index = (int)Xamarin.Forms.Application.Current.Properties["tabIndex"];
            //    if (_index != 0)
            //    {
            //        page.CurrentPage = page.Children[_index];
            //    }
            //}

            //this.On<Xamarin.Forms.PlatformConfiguration.Android>().SetToolbarPlacement(ToolbarPlacement.Bottom);

            OnFullMode = Settings.FullMode;

            //save the last filter
            IEventAggregator eve = ((App)App.Current).ExtResolve<IEventAggregator>();
            eve.GetEvent<FilterTasksEvent>().Subscribe(OnFilterTasks);
            eve.GetEvent<FilterDashboardEvent>().Subscribe(OnFilterDashboard);
        }

        private void OnFilterDashboard(FilterTask obj)
        {
            DashboardFilter = GetFilterFromFilter(obj);
        }

        private void OnFilterTasks(FilterTask obj)
        {
            TaskFilter = GetFilterFromFilter(obj);
        }

        private FilterTask GetFilterFromFilter(FilterTask filter)
        {
            return new FilterTask()
            {
                StartDate = filter.StartDate,
                EndDate = filter.EndDate,
                StatusID = filter.StatusID,
                Status = filter.Status,
                SectorID = filter.SectorID,
                SectorName = filter.SectorName,
                ProjectID = filter.ProjectID,
                ProjectName = filter.ProjectName,
                AssigneeID = filter.AssigneeID,
                AssigneeName = filter.AssigneeName,
                PriorityID = filter.PriorityID,
                Priority = filter.Priority,
                TabId = filter.TabId,
                IsActive = filter.IsActive,

                ResponsibleID = filter.ResponsibleID,
                ResponsibleNaem = filter.ResponsibleNaem,

                Entities = filter.Entities,
                EntityId = filter.EntityId,
                EntityName = filter.EntityName,

                SourceId = filter.SourceId,
                SourceName = filter.SourceName,

                SearchTitle = filter.SearchTitle
            };
            
        }

        private void InitializeTabs()
        {
            NotificationsPage tabNotification = new NotificationsPage 
            {
                Title = Maham.Resources.AppResource.Notification,
                TabIndex = 0,
                Icon = "notification_0.png",
                BackgroundColor = Color.FromHex("#E5ECED")
            };

            tabNotification.SetBinding(TabBadge.BadgeTextProperty, new Binding("NotificationCount"));

            //TabBadge.SetBadgeText(tabNotification, "99+");
            TabBadge.SetBadgeColor(tabNotification, Color.Red);
            TabBadge.SetBadgePosition(tabNotification, BadgePosition.PositionCenter);
           // TabBadge.SetBadgeMargin(tabNotification, new Thickness(5, 15, 6, 0));

            TasksPage tabTasks = new TasksPage
            {
                Title = Maham.Resources.AppResource.Tasks,
                TabIndex = 1,
                Icon = "TasksIcon.png",
                BackgroundColor = Color.FromHex("#E5ECED")
            };

            DashboardPage tabDashboard = new DashboardPage
            {
                Title = Maham.Resources.AppResource.Dashboard,
                TabIndex = 2,
                Icon = "DashboardIcon.png",
                BackgroundColor = Color.FromHex("#E5ECED")
            };

            SetttingsPage taSettting = new SetttingsPage
            {
                Title = Maham.Resources.AppResource.Settings,
                TabIndex = 3,
                Icon = "SettingIcon.png",
                BackgroundColor = Color.FromHex("#E5ECED")
            };

            if (isRtl)
            {
                Children.Add(taSettting);
                Children.Add(tabDashboard);
                Children.Add(tabTasks);
                Children.Add(tabNotification);

                SelectedItem = tabNotification;
            }
            else
            {
                Children.Add(tabNotification);
                Children.Add(tabTasks);
                Children.Add(tabDashboard);
                Children.Add(taSettting);
            }

        }

        private bool _SearchPopupOpeend;
        private async void SearchCommandExcute(object obj)
        {
            if (_SearchPopupOpeend)
            {
                return;
            }
            _SearchPopupOpeend = true;
            //await  ((App)App.Current).ExtResolve<INavigationService>().NavigateAsync("SearchPopup");

            await Navigation.PushAsync(new SearchPopup());
            _SearchPopupOpeend = false;
        }


        protected override void OnAppearing()
        {
            base.OnAppearing();
           
            if (ShouldRefreshIosTabBarIcons)
            {
                RefreshTabBarIconsInIOS();
                ShouldRefreshIosTabBarIcons = false;
            }
            MessagingCenter.Subscribe<BaseViewModel>(this, "RefreshTabBarIconsInIOS", (s) => { ShouldRefreshIosTabBarIcons = true; });
        }

        protected override void OnDisappearing()
        {
            MessagingCenter.Unsubscribe<BaseViewModel>(this, "RefreshTabBarIconsInIOS");
            base.OnDisappearing();
        }
        private string _mainTitle;
        public string MainTitle
        {
            get { return _mainTitle; }
            set
            {
                _mainTitle = value; 
                OnPropertyChanged();
            }
        }
        private bool _filterIsVisible;
        public bool FilterIsVisible
        {
            get { return _filterIsVisible; }
            set
            {
                _filterIsVisible = value; ;
                OnPropertyChanged();
            }
        }
        private bool _SearchIsVisible;
        private int CurrentIndex;

        public bool SearchIsVisible
        {
            get { return _SearchIsVisible; }
            set
            {
                _SearchIsVisible = value;
                OnPropertyChanged();
            }
        }

        private bool _fullModeIsVisiable;

        public bool FullModeIsVisiable
        {
            get { return _fullModeIsVisiable; }
            set
            {
                _fullModeIsVisiable = value;
                OnPropertyChanged();
            }
        }

        private bool _menuIsVisiable;

        public bool MenuIsVisible
        {
            get { return _menuIsVisiable; }
            set
            {
                _menuIsVisiable = value;
                OnPropertyChanged();
            }
        }

        private bool _onFullMode;
        public bool OnFullMode
        {
            get { return _onFullMode; }
            set
            {
                _onFullMode = value;
                OnPropertyChanged();
            }
        }

        public void OnOnFullModeChanged()
        {
            MessagingCenter.Send(this, "FullModeChanged", OnFullMode);
            Settings.FullMode = OnFullMode;
        }


        private void ExtCustomTabbedPage_CurrentPageChanged(object sender, System.EventArgs e)
        {
            this.MainTitle = this.CurrentPage.Title;
            CurrentIndex = page.TabIndex;

            if (CurrentPage.TabIndex == 1)//tasks
            {
                FilterIsVisible = true;
                SearchIsVisible = false;
                FullModeIsVisiable = true;
                MenuIsVisible = true;
            }
            else if (CurrentPage.TabIndex == 2)// dashboard
            {
                FilterIsVisible = true;
                SearchIsVisible = false;
                FullModeIsVisiable = false;
                MenuIsVisible = false;
            }
            else 
            {
                FilterIsVisible = false;
                SearchIsVisible = false;
                FullModeIsVisiable = false;
                MenuIsVisible = false;
            }
          
            
            //int index = page.Children.IndexOf(page.CurrentPage);
           
            //if (index != 0)
            //{
            //    if (Xamarin.Forms.Application.Current.Properties.ContainsKey("tabIndex"))
            //    {
            //        Xamarin.Forms.Application.Current.Properties["tabIndex"] = index;
            //    }
            //    else
            //    {
            //        Xamarin.Forms.Application.Current.Properties.Add("tabIndex", index);
            //    }
            //}


        }

        //prevnt oppening more than one view 
        private bool _filterPopupOpeend;
        private async void Click(object obj)
        {
            if (_filterPopupOpeend)
            {
                return;
            }
            _filterPopupOpeend = true;
           await PopupNavigation.Instance.PushAsync(new FillterPopup());
            if (CurrentPage.TabIndex == 1)//.Title == "Tasks" || this.CurrentPage.Title == "المهام"
            {
                //PopupNavigation.Instance.PushAsync(new FillterPopup());

                MessagingCenter.Send<MainTabbedPage, dynamic>(this, "FilterDashbord", new {IsDashboard=false,TaskMode= (int)tasksMode, LastFilter =  TaskFilter});
            }
            else if (CurrentPage.TabIndex == 2)
            {
                MessagingCenter.Send<MainTabbedPage, dynamic>(this, "FilterDashbord", new { IsDashboard = true, TaskMode = (int)tasksMode, LastFilter = DashboardFilter });
                //PopupNavigation.Instance.PushAsync(new DashboardFiltterPopup());
            }
            _filterPopupOpeend = false;
        }

        private async void RefreshTabBarIconsInIOS()
        {
            try
            {
                // in ios we have placed a TabbedPage into a NavigationPage that's couasing hiding tab bar icons
                switch (Device.RuntimePlatform)
                {
                    case Device.iOS:
                        Xamarin.Forms.NavigationPage.SetHasNavigationBar(this, false);
                        await System.Threading.Tasks.Task.Delay(50);
                        Xamarin.Forms.NavigationPage.SetHasNavigationBar(this, true);
                        break;

                    default:
                        
                        break;
                }

            }
            catch 
            {
                
            }
        }

        private async void Menu_Tapped(object obj, EventArgs args)
        {
            //
            string taskListText = Maham.Resources.AppResource.TaskList;
            string taskListUserGroupText = Maham.Resources.AppResource.TaskListUserGroub;
            string ClosedTasksListsText = Maham.Resources.AppResource.ClosedTasksLists;

            var result = await App.Current.MainPage.DisplayActionSheet(Maham.Resources.AppResource.ChooseTasksMode,
                Maham.Resources.AppResource.canceltext, null, taskListText, taskListUserGroupText, ClosedTasksListsText);

            TasksMode prev = tasksMode;

            if (result == taskListText)
            {
                tasksMode = TasksMode.TaskList;
            }
            else if (result == taskListUserGroupText)
            {
                tasksMode = TasksMode.TaskListUserGroup;
            }
            else if(result == ClosedTasksListsText)
            {
                tasksMode = TasksMode.ClosedTasksList;
            }

            if (tasksMode != prev)
            {
                MessagingCenter.Send(this, "ModeChanged", (int)tasksMode);
            }
           
        }

        //
       
    }
}
