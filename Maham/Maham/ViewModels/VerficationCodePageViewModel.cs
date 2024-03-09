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
using Maham.Service;
using Maham.Service.General;
using Maham.Service.Implmentation.Registeration;
using Maham.Service.Model.Request.Registeration;
using Maham.Setting;
using Xamarin.Forms;

namespace Maham.ViewModels
{
	public class VerficationCodePageViewModel : BaseViewModel
    {
        public string verficationcode { get; set; }
        public ICommand SubmitVerificationcodeCommand { get; set; }
        public ICommand SendVerificationcodeCommand { get; set; }
        private readonly INavService navService;
        public bool IsBusy { get; set; }
        public VerficationCodePageViewModel(INavService _navService, INavigationService _NavigationServices) : base(_NavigationServices)
        {
            navService = _navService;
            IsBusy = false;
            SubmitVerificationcodeCommand = new Command(VerificationcodeCommandExcute);
            SendVerificationcodeCommand = new Command(SendVerificationcodeCommandExcute);
        }

        private async void SendVerificationcodeCommandExcute(object obj)
        {
            var api = RestService.For<ITaskyApi>(new System.Net.Http.HttpClient(new HttpLoggingHandler()) { BaseAddress = new Uri(Settings.ApiUrl) });

            try
            {
                //using (UserDialogs.Instance.Loading("wait..."))
                //{
                IsBusy = true;
                 // var result = await api.SendVerficationCode(Settings.UserId);
                    if (true)
                    {
                    IsBusy = false;
                        await Application.Current.MainPage.DisplayAlert("Sent", "Code Send sucessfully", "OK");
                        //UserDialogs.Instance.HideLoading();
                    }
                    else
                    {
                    IsBusy = false;
                        await Application.Current.MainPage.DisplayAlert("Sent", "Press Send code again", "OK");
                        //UserDialogs.Instance.HideLoading();
                    }
                //}
            }
            catch (Exception exception)
            {
                var properties = new Dictionary<string, string>
                {
                    { "VerficationCodeViewMode", "sendverficationcode" },
                };
                Crashes.TrackError(exception, properties);
            }



        }

        private async void VerificationcodeCommandExcute(object obj)
        {
            var api = RestService.For<ITaskyApi>(new System.Net.Http.HttpClient(new HttpLoggingHandler()) { BaseAddress = new Uri(Settings.ApiUrl) });
            try
            {
                //using (UserDialogs.Instance.Loading("wait..."))
                //{
                //IsBusy = true;
                //    var result = await api.SubmitverificationCode(verficationcode, Settings.UserId);
                //    if (result.verified)
                //    {
                //    //
                //    // UserDialogs.Instance.HideLoading();
                //    IsBusy = false;
                //    await navService.PopToRootAsync();
                //        Settings.UserId = result.userId.ToString();
                //    }
                //    else
                //    {
                //    IsBusy = false;
                //        await Application.Current.MainPage.DisplayAlert("Try Agian", "Press Send code again", "OK");
                //    }
                ////}
            }
            catch (Exception exception)
            {
                var properties = new Dictionary<string, string>
                {
                    { "VerficationCodeViewMode", "SubmitVerficationCode" },
                };
                Crashes.TrackError(exception, properties);
            }

        }
    }
}


