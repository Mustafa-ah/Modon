using Microsoft.AppCenter.Crashes;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Maham.Bases;
using Maham.Constants;
using Maham.Resources;
using Maham.Service;
using Maham.Service.General;
using Maham.Setting;
using Maham.ViewModels.Authentication;
using Xamarin.Forms;

namespace Maham.ViewModels
{
	public class ResetPasswordViewModel : BaseViewModel
	{
        public bool IsBusy { get; set; }
        public string NewPassword { get; set; }
        public string OldPassword { get; set; }
        public string UserName { get; set; }
        public ICommand SubmitNewPaaswordCommand { get; set; }
        public ICommand BackCommand { get; set; }
        private readonly INavService navService;

        public ResetPasswordViewModel(INavService _navService, INavigationService _NavigationServices) : base(_NavigationServices)
        {
            navService = _navService;
            SubmitNewPaaswordCommand = new Command(SubmitNewPaaswordCommandExcute);
            BackCommand = new Command(BackCommandExcute);
        }

        private void BackCommandExcute(object obj)
        {
            //eman
            //_navigationService.NavigateAsync("/NavigationPage/MainTabbedPage?selectedTab=SetttingsPage");
            navService.NavigateBackAsync();
        }

        private async void SubmitNewPaaswordCommandExcute(object obj)
        {
            // Application.Current.MainPage.DisplayAlert("Alert", "not allowed to reset passowrd, contact administrator", "OK");
            var api = RestService.For<ITaskyApi>(new System.Net.Http.HttpClient(new HttpLoggingHandler()) { BaseAddress = new Uri(Settings.ApiUrl) });
            try
            {
                IsBusy = true;

                var result = await api.ResetPassword("Bearer " + Settings.AccessToken,UserName,OldPassword,NewPassword);
                if(result.requestSuccess)
                {
                   await Application.Current.MainPage.DisplayAlert(AppResource.ResetmessageTitle,AppResource.ResetMessageSucess, AppResource.oktext);
                    // await _navigationService.NavigateAsync("Tasky:///NavigationPage/LoginPage");
                    navService.Logout();
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert(AppResource.failuretext, AppResource.resetpassfail, AppResource.oktext);
                }
            }
            catch (Exception exception)
            {
                var properties = new Dictionary<string, string>
                       {
                             { "Reset password", "Submit" },
                       };
                Crashes.TrackError(exception, properties);
            }


        }
    }
}
