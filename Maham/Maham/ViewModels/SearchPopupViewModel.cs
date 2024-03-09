using Microsoft.AppCenter.Crashes;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Refit;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Maham.Bases;
using Maham.Constants;
using Maham.Enums;
using Maham.Resources;
using Maham.Service;
using Maham.Service.General;
using Maham.Service.Model.Response.Tasks;
using Maham.Setting;
using Xamarin.Forms;

namespace Maham.ViewModels
{
    public class SearchPopupViewModel : BaseViewModel
    {
        private readonly INavService navService;
        public string queryname { get; set; }
        public string placeholdertext { get; set; }
        public bool isBusy { get; set; }
        public ICommand SearchCommand { get; set; }
        public ICommand CloseCommand { get; set; }
        public ObservableCollection<SearchResponse> ResultList { get; set; }
        public SearchPopupViewModel(INavService _navService, INavigationService _NavigationServices) : base(_NavigationServices)
        {
            navService = _navService;
            SearchCommand = new Command(SearchCommandExcute);
            CloseCommand = new Command(CloseCommandExcute);
            ResultList = new ObservableCollection<SearchResponse>();

            placeholdertext = AppResource.saerchplaceholder;
        }

        private void CloseCommandExcute(object obj)
        {
            navService.NavigateBackAsync();
        }

        private async void SearchCommandExcute(object obj)
        {
            try
            {
               
             if(String.IsNullOrEmpty(queryname)||String.IsNullOrWhiteSpace(queryname))
                {
                    await Application.Current.MainPage.DisplayAlert(AppResource.failuretext, AppResource.serachnamemessage, AppResource.oktext);
                }
                  else
                {
                    var navigationParams = new NavigationParameters();
                    navigationParams.Add("query", queryname);
                    await navService.NavigateToAsync<SearchResultViewModel>(navigationParams);
                }
                    
               
            }
            catch (Exception exception)
            {
                var properties = new Dictionary<string, string>
                       {
                             { "searchpopup", "searchcoomand" },
                       };
                Crashes.TrackError(exception, properties);
            }
        }
       
    }
}
