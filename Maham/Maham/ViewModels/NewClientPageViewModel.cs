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
using Maham.Service.Model.Request.newclient;
using Maham.Setting;
using Xamarin.Forms;

namespace Maham.ViewModels
{
	public class NewClientPageViewModel : BaseViewModel
	{
        private readonly INavService navService;
        public bool isbusy { get; set; }
        public string name { get; set; }
        public string organizationName { get; set; }
        public string email { get; set; }
        public string mobile { get; set; }
        public ICommand RequestCodeCommand { get; set; }
        public ICommand NaviagtionBackCommand { get; set; }
        public NewClientPageViewModel(INavService _navService, INavigationService _NavigationServices) : base(_NavigationServices)
        {
            navService = _navService;
            RequestCodeCommand = new Command(RequestCodeCommandExcute);
            NaviagtionBackCommand = new Command(NaviagtionBackCommandExcute);
        }

        private void NaviagtionBackCommandExcute(object obj)
        {
            navService.NavigateBackAsync();
        }
        private async void RequestCodeCommandExcute(object obj)
        {
            var api = RestService.For<ITaskyApi>(AppConstants.BasicURL);
            try
            {
                isbusy = true;
                var request = new NewClientRequest
                {
                    userName=name,
                    email=email,
                    organizationName=organizationName,
                     phoneNumber=mobile

                };
                //var result = await api.PostNewClient( request);
                //if (result != null)
                //{
                //    await Application.Current.MainPage.DisplayAlert("",
                //        "registration success , Kindly wait for application admin contact ", "ok");
                //    //.ContinueWith(async (e) => { await _navigationservice.NavigateAsync("StarterPage"); });
                //   await navService.NavigateBackAsync();
                //}
                //else
                //{
                // await Application.Current.MainPage.DisplayAlert("", "Please contact admin", "ok");
                //}
                isbusy = false;
            }
            catch (Exception exception)
            {
                isbusy = false;
                var properties = new Dictionary<string, string>
                       {
                             { "newclient", "requestcode" },
                       };
                Crashes.TrackError(exception, properties);
            }
        }
    }
}
