using Microsoft.AppCenter.Crashes;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tasky.Bases;
using Tasky.Setting;
using Tasky.Views;

namespace Tasky.ViewModels
{
	public class WelcomePageViewModel : BaseViewModel
    {
     
     
        public WelcomePageViewModel()
        {
           
        }
        public override async void OnAppearing()
        {
            base.OnAppearing();
            //await Task.Delay(3000);
            //await _navigationService.NavigateAsync("WelcomePage");
            try
            {
                await Task.Delay(100);
                string x = Settings.AccessToken;
                var xss = Settings.AccessToken;
                var y = Settings.UserId;
                await _navigationService.NavigateAsync("/NavigationPage/MainTabbedPage");

            }
            catch (Exception exception)
            {
                
                var properties = new Dictionary<string, string>
                       {
                             { "welcomepageviewmodel", "onappearing" },
                       };
                Crashes.TrackError(exception, properties);
            }

        }

        public override async void OnNavigatedTo(NavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            
        }
    }
}

