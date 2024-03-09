
using Microsoft.AppCenter.Crashes;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Refit;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Maham.Bases;
using Maham.Constants;
using Maham.Service;
using Maham.Service.General;
using Maham.Setting;
using Maham.Views;
using Xamarin.Forms;

namespace Maham.ViewModels
{
	public class DeletePopupPageViewModel : BaseViewModel
	{
        private readonly INavService navService;
        public ICommand DiscardbuttonCommand { get; set; }
        public ICommand DeletebuttonCommand { get; set; }
        public ICommand ClosePopupCommand { get; set; }
        private   bool _isbusy;
        public bool isBusy
        {
            get { return _isbusy; }
            set
            {
                SetProperty(ref _isbusy, value);
            }
        }
        public DeletePopupPageViewModel(INavService _navService, INavigationService _NavigationServices) : base(_NavigationServices)
        {
            navService = _navService;
            DiscardbuttonCommand = new Command(DiscardbuttonCommandExcute);
            DeletebuttonCommand = new Command(DeletebuttonCommandExcute);
            ClosePopupCommand = new Command(ClosePopupCommandExcute);
        }

        private void ClosePopupCommandExcute(object obj)
        {
            PopupNavigation.Instance.PopAsync();
        }

        private async void DeletebuttonCommandExcute(object obj)
        {
            var api = RestService.For<ITaskyApi>(new System.Net.Http.HttpClient(new HttpLoggingHandler()) { BaseAddress = new Uri(Settings.ApiUrl) });
            try
            {
                isBusy = true;
                var response = await api.DeleteTask("Bearer " + Settings.AccessToken, Settings.TaskId);
                if(response.Success)
                {
                    isBusy = false;
                    MessagingCenter.Send(this,"TaskDeleted");
                    //await navService.NavigateToAsync<MainTabbedPageViewModel>();
                    //await _navigationService.NavigateAsync("/NavigationPage/MainTabbedPage?selectedTab=TasksPage");
                    await  PopupNavigation.Instance.PopAsync();
                    await navService.NavigateBackAsync();

                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Failure", "", "Ok");
                }
            }
            catch (Exception exception)
            {
                var properties = new Dictionary<string, string>
                       {
                             { "Deletetaskviewmodel", "deletetask" },
                       };
                Crashes.TrackError(exception, properties);
            }
            finally
            { isBusy = false; }

        }

        private void DiscardbuttonCommandExcute(object obj)
        {
            PopupNavigation.Instance.PopAsync();
        }
    }
}
