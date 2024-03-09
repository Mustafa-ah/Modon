using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.AppCenter.Crashes;
using Prism.Events;
using Prism.Navigation;
using Refit;
using Maham.Bases;
using Maham.Constants;
using Maham.Events;
using Maham.Extentions;
using Maham.Models;
using Maham.Service;
using Maham.Service.Model.Request.Dashboard;
using Maham.Setting;
using Xamarin.Essentials;
using Xamarin.Forms;
using Newtonsoft.Json;
using Maham.Service.Model.Response;

namespace Maham.ViewModels
{
    public class DashboardChildPageViewModel : BaseViewModel
    {
        #region filds 
        public List<Chart> dashboardRootObjectModelApi;
        public bool currentpage = false;

        #endregion
        #region Private Properties 
        private int _tabIdChild;
        private bool _isBusy;
        private List<Chart> apiData;
        #endregion
        #region Public Properties 
        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                SetProperty(ref _isBusy, value);
            }
        }
        public List<Chart> TabIdChild
        {
            get { return apiData; }
            set { SetProperty(ref apiData, value); }
        }

        public List<Chart> ApiData
        {
            get { return apiData; }
            set
            {
                SetProperty(ref apiData, value);
            }
        }

        public FilterTask Filter { get; set; }
        #endregion
        public DashboardChildPageViewModel(IEventAggregator _eventAggregator, INavigationService _NavigationServices) : base(_NavigationServices)
        {
            dashboardRootObjectModelApi = new List<Chart>();
            ApiData = new List<Chart>();
            Setting.Settings.Dashboardtabs.Add(this);
            Setting.Settings.DashboardFiltter = new Dictionary<string, string>();
            Setting.Settings.DashboardFiltter.Add("priorityID", "0");
            Setting.Settings.DashboardFiltter.Add("StatusID", "0");
            Setting.Settings.LastDashboardFiltter = new Dictionary<string, string>();

            Setting.Settings.LastDashboardFiltter.Add("priorityID", "0");
            Setting.Settings.LastDashboardFiltter.Add("StatusID", "0");

            Filter = new FilterTask();
            _eventAggregator.GetEvent<FilterDashboardEvent>().Subscribe(OnFilterDashboard);

            InitializeAsync(null);
        }

        private void OnFilterDashboard(FilterTask obj)
        {
            Filter = obj;
            if (currentpage)
            {
                MessagingCenter.Send<DashboardChildPageViewModel, int>(this, "RenderDesign", tabId);
            }
        }

        public override void OnAppearing()
        {
            base.OnAppearing();
            currentpage = true;
        }
        public void Appear()
        {
            currentpage = true;
        }

        public async Task Init(int tabId_)
        {
            try
            {
                DashboardFilterDto _Filter = new DashboardFilterDto();



                NotConnected = false;
                if (Connectivity.NetworkAccess != NetworkAccess.Internet)
                {
                    NotConnected = true;
                    return;
                }
                //if (Filter.StartDate == AppConstants.MinDate && Filter.EndDate == AppConstants.MinDate)
                //{
                //    var firstDayOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                //    var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

                //    Filter.StartDate = firstDayOfMonth.ToDateTimeStringForAPI();
                //    Filter.EndDate = lastDayOfMonth.ToDateTimeStringForAPI();
                //}
                //_Filter=Filter
                _Filter.FromDate = Filter.StartDate.ToDateTimeStringForAPI();
                _Filter.ToDate = Filter.EndDate.ToDateTimeStringForAPI();
                _Filter.StatusId = Filter.StatusID;
                _Filter.PriorityId = Filter.PriorityID;
                _Filter.EntityId = Filter.EntityId;
                _Filter.ResponsibleID = Filter.ResponsibleID == null ? new Value2() { ID = Guid.Empty.ToString(), RoleID = "0", Type = -1 } : Filter.ResponsibleID;
                _Filter.SourceId = Filter.SourceId;
                _Filter.title = Filter.SearchTitle;
                var api = RestService.For<ITaskyApi>(new System.Net.Http.HttpClient(new HttpLoggingHandler()) { BaseAddress = new Uri(Settings.ApiUrl) });

                string filterObj = JsonConvert.SerializeObject(_Filter);

                var result = await api.GetCategoryCharts("Bearer " + Settings.AccessToken, tabId_, filterObj);
                //var result = await api.GetCategoryCharts("Bearer " + Settings.AccessToken, tabId_, _Filter.FromDate, _Filter.ToDate, _Filter.StatusId, _Filter.PriorityId, _Filter.EntityId, _Filter.ResponsibleID.ID, _Filter.ResponsibleID.RoleID, _Filter.ResponsibleID.Type, _Filter.SourceId);

                ApiData = (result.Data).ToObject<List<Chart>>();
            }
            catch (Exception exception)
            {
                var properties = new Dictionary<string, string>
                       {
                             { "DashboardChildPageViewModel", "Init" },
                       };
                Crashes.TrackError(exception, properties);
            }
        }
        public override void OnDisappearing()
        {
            base.OnDisappearing();
            var tabName = this.tabName;
            currentpage = false;
        }

    }
}
