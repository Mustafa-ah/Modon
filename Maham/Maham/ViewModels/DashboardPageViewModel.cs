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
using Maham.Setting;
using Xamarin.Forms;

namespace Maham.ViewModels
{
    public class DashboardPageViewModel : BaseViewModel
    {
        #region Fields
        public bool isRtl;
        public int OverallTabId { get; set; }
        #endregion
        #region Private Members
        private ObservableCollection<DashboardChildModel> _dashboardChildModels;
        private ICommand CurrentPageChangedCommand { get; set; }
        private List<DashboardContentApi> _tabsResponse;
        #endregion
        #region Public properties

        public ObservableCollection<DashboardChildModel> DashboardChildModels
        {
            get
            {
                if (_dashboardChildModels == null)
                {
                    return _dashboardChildModels = new ObservableCollection<DashboardChildModel>();
                }

                return _dashboardChildModels;
            }
            set { SetProperty(ref _dashboardChildModels, value); }
        }

        public List<DashboardContentApi> TabsResponse
        {
            get
            {
                if (_tabsResponse == null)
                {
                    return _tabsResponse = new List<DashboardContentApi>();
                }
                return _tabsResponse;
            }
            set
            {
                SetProperty(ref _tabsResponse, value);
            }
        }
        #endregion

        public DashboardPageViewModel(INavigationService _NavigationServices) : base(_NavigationServices)
        {
            isRtl = new Helpers.Helper().IsRtl;
            OverallTabId = 1;
            string tabName_ = isRtl ? "صورة شاملة" : "Overall";
            DashboardChildModels = new ObservableCollection<DashboardChildModel> {
                new DashboardChildModel{ TabName = tabName_, TabId = OverallTabId}
            };
            OnAppearing();
        }
       
        public override void OnAppearing()
        {
            if (DashboardChildModels == null || DashboardChildModels.Count < 2)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    try
                    {
                        var tabsTemp = await GetTabs();
                        TabsResponse = tabsTemp;
                        InitTaskTabbedPages();
                    }
                    catch (Exception e)
                    {
                        Crashes.TrackError(e);
                    }
                    

                });
            }
            base.OnAppearing();
        }
        public async Task<List<DashboardContentApi>> GetTabs()
        {
            try
            {
                var api = RestService.For<ITaskyApi>(new System.Net.Http.HttpClient(new HttpLoggingHandler()) { BaseAddress = new Uri(Settings.ApiUrl) });
                var result = await api.GetDashboardTabs("Bearer " + Settings.AccessToken);
                if (result.Success)
                {
                    var x = new List<DashboardContentApi>();
                    x = (result.Data).ToObject<List<DashboardContentApi>>();
                    return x;
                }
                else return new List<DashboardContentApi>();
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
                        var result = await api.GetDashboardTabs("Bearer " + Settings.AccessToken);
                        if (result.Success)
                        {
                            var x = new List<DashboardContentApi>();
                            x = (result.Data).ToObject<List<DashboardContentApi>>();
                            return x;
                        }
                        else return new List<DashboardContentApi>();
                    }
                   
                    return null;
                }
                catch
                {
                    var properties = new Dictionary<string, string> { { "DashboardPageViewModel", "GetTabs" }, };
                    Crashes.TrackError(e, properties);
                    return null;
                }
                
                
            }
        }
        public void InitTaskTabbedPages()
        {
            try
            {
                // _dashboardChildModels.Clear();
                // DashboardChildModels.Clear();
               // DashboardChildModels.Clear();
                var viewModels = TabsResponse;
                foreach (var content in viewModels)
                {
                    if (content.categoryId == OverallTabId)
                    {
                        continue;
                    }
                    DashboardChildModel dashboardChildModel = new DashboardChildModel();
                    if (isRtl)
                    {
                        dashboardChildModel.TabName = content.categoryNameAr;
                        dashboardChildModel.TabId = content.categoryId;
                        DashboardChildModels.Insert(0, dashboardChildModel);
                    }
                    else
                    {
                        dashboardChildModel.TabName = content.categoryName;
                        dashboardChildModel.TabId = content.categoryId;
                        DashboardChildModels.Add(dashboardChildModel);
                    }
                   
                }

                //foreach (var item in Settings.Dashboardtabs)
                //{
                //    item.Appear();
                //}
                //foreach (var item in Settings.DashboardtabsPage)
                //{
                //    item.Appear();
                //}
                //if (Setting.Settings.Dashboardtabs.Count > 0)
                //{
                //    Setting.Settings.Dashboardtabs[0].Appear();
                //}
                //if (Setting.Settings.DashboardtabsPage.Count > 0)
                //{
                //    Setting.Settings.DashboardtabsPage[0].Appear();
                //}

                 MessagingCenter.Send<object>(this, "SetCurrentPage");
            }
            catch (Exception e)
            {
                var x = e.StackTrace;
                var properties = new Dictionary<string, string>
                {
                    { "DashboardPageViewModel", "InitTaskTabbedPages" },
                };
                Crashes.TrackError(e, properties);
                
            }
        }
    }
}
