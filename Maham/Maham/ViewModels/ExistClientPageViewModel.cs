using Microsoft.AppCenter.Crashes;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Refit;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Maham.Bases;
using Maham.Constants;
using Maham.Models;
using Maham.Persistence;
using Maham.Service;
using Maham.Service.General;
using Maham.Setting;
using Maham.ViewModels.Authentication;
using Xamarin.Forms;

namespace Maham.ViewModels
{
	public class ExistClientPageViewModel : BaseViewModel
    {
        private SQLiteAsyncConnection sqLiteConnection;
        public static string BaseUrl { get; set; }
        public bool isbusy { get; set; }
        private readonly INavService navService;
        public string code { get; set; }
        private ObservableCollection<Tenants> TenantsList { get; set; }
        public ICommand SubmitCodeCommand { get; set; }
        public ICommand NaviagtionBackCommand { get; set; }
        public ExistClientPageViewModel(INavService _navService, INavigationService _NavigationServices) : base(_NavigationServices)
        {
            navService = _navService;
            TenantsList = new ObservableCollection<Tenants>();
            SubmitCodeCommand = new Command(SubmitCodeCommandExcute);
            NaviagtionBackCommand = new Command(NaviagtionBackCommandExcute);
        }

        private void NaviagtionBackCommandExcute(object obj)
        {
            navService.NavigateBackAsync();
        }

        private async void SubmitCodeCommandExcute(object obj)
        {
            try
            {
                var api = RestService.For<ITaskyApi>(AppConstants.BasicURL);
                isbusy = true;

                var result = await api.GetTenant(code);
                if (result.Success)
                {
                    var x = result.Data.GetValue("id").Value;
                    Settings.AllowSignUp = result.Data.GetValue("allowSignUp").Value;
                    sqLiteConnection = DependencyService.Get<ISQLiteDb>().GetConnectionAsync();
                    await sqLiteConnection.InsertAsync(new Tenants()
                    {
                        userName = result.Data.GetValue("userName").Value,
                        organizationName= result.Data.GetValue("organizationName").Value,
                         email= result.Data.GetValue("email").Value,
                          phoneNumber= result.Data.GetValue("phoneNumber").Value,
                           apiUrl= result.Data.GetValue("apiUrl").Value,
                            code= result.Data.GetValue("code").Value,
                             type= result.Data.GetValue("type").Value
                       
                    });
                    if(result.Data.GetValue("apiUrl").Value!=null)
                    {
                        Settings.HasTenants = true;
                        Settings.TenantTypes = result.Data.GetValue("type").Value;
                        BaseUrl = result.Data.GetValue("apiUrl").Value;
                        Settings.ApiUrl = result.Data.GetValue("apiUrl").Value;
                        await navService.NavigateToAsync<LoginPageViewModel>();
                    }
                    else
                    {
                        Settings.HasTenants = false;
                    }

                }
                else
                {
                    string msg = "Not Valid Code! ";
                    string ok = "Ok";
                    if (Settings.IsRtl)
                    {
                        ok = "مواق";
                        msg = "كود غير صالح";
                    }
                    await Application.Current.MainPage.DisplayAlert("", msg, ok);
                }
                isbusy = false;
            }
            catch (Exception exception)
            {
                isbusy = false;
                var properties = new Dictionary<string, string>
                       {
                             { "ExistClientViewModel", "submitcode" },
                       };
                Crashes.TrackError(exception, properties);
            }
        }

        public async Task Data()
        {
            sqLiteConnection = DependencyService.Get<ISQLiteDb>().GetConnectionAsync();
            var datalist = await sqLiteConnection.Table<Tenants>().ToListAsync() ?? new List<Tenants>();
            TenantsList = new ObservableCollection<Tenants>(datalist);
            if (TenantsList.Count == 0)
            {
                Settings.HasTenants = false;
            }
            else
            {
                Settings.HasTenants = true;

            }

        }
	}
}
