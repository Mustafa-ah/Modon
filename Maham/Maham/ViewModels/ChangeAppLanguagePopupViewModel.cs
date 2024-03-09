using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Plugin.Multilingual;
using Prism.Navigation;
using Refit;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using System.Windows.Input;
using Maham.Bases;
using Maham.Resources;
using Maham.Service;
using Maham.Service.General;
using Maham.Setting;
using Xamarin.Forms;

namespace Maham.ViewModels
{
    class ChangeAppLanguagePopupViewModel : BaseViewModel
    {
        private readonly INavService navService;

        public ICommand ClosePopupCommand { get; set; }
        public ICommand ToArabicCommand { get; set; }
        public ICommand ToEnglishCommand { get; set; }

        private bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                SetProperty(ref _isBusy, value);
            }
        }

        public ChangeAppLanguagePopupViewModel(INavService _navService, INavigationService _NavigationServices) : base(_NavigationServices)
        {
            navService = _navService;

            ClosePopupCommand = new Command(ClosePopupCommandExcute);
            ToArabicCommand = new Command(ToArabicCommandExcute);
            ToEnglishCommand = new Command(ToEnglishCommandExcute);
        }

        private void ClosePopupCommandExcute(object obj)
        {
             PopupNavigation.Instance.PopAsync();
        }

        private async void ToArabicCommandExcute(object obj)
        {
            try
            {
                if (NotConnected)
                {
                    await Application.Current.MainPage.DisplayAlert("Connection Error", "please make sure you have internet connection", "Ok");
                    return;
                }
                if (new Helpers.Helper().CurrentLanguage() != 2)
                {
                    string culture = "ar";
                  await  ChangeCulture(culture);
                }
                else
                {
                    await PopupNavigation.Instance.PopAsync();
                }
                
            }
            catch (Exception e)
            {
                IsBusy = false;
                await Application.Current.MainPage.DisplayAlert(AppResource.failuretext, AppResource.GeneralErorr, AppResource.oktext);
                var properties = new Dictionary<string, string>
                {
                    { "ChangeAppLanguagePopupViewModel", "ToArabicCommandExcute" },
                };
                Crashes.TrackError(e, properties);
            }
        }

        private async void ToEnglishCommandExcute(object obj)
        {
            try
            {
                if (NotConnected)
                {
                    await Application.Current.MainPage.DisplayAlert("خطأ فى الاتصال", "من فضلك تأكد من اتصالك بالانترنت", "موافق");
                    return;
                }
                if (new Helpers.Helper().CurrentLanguage() == 2)
                {
                    string culture = "en";
                  await  ChangeCulture(culture);
                }
                else
                {
                    await PopupNavigation.Instance.PopAsync();
                }
                
            }
            catch (Exception e)
            {
                IsBusy = false;
                await Application.Current.MainPage.DisplayAlert(AppResource.failuretext, AppResource.GeneralErorr, AppResource.oktext);
                var properties = new Dictionary<string, string>
                {
                    { "ChangeAppLanguagePopupViewModel", "ToEnglishCommandExcute" },
                };
                Crashes.TrackError(e, properties);
            }
        }

        private async Task ChangeCulture(string culture)
        {
            if (Settings.AllowPushNotification)
            {
                IsBusy = true;
                var api = RestService.For<ITaskyApi>(new System.Net.Http.HttpClient(new HttpLoggingHandler()) { BaseAddress = new Uri(Settings.ApiUrl) });
                var response = await api.SetPreferredLanguage("Bearer " + Settings.AccessToken, Setting.Settings.UserId, Settings.FirebaseToken, culture);
                IsBusy = false;
                if (!response.Success)
                {
                    await Application.Current.MainPage.DisplayAlert(AppResource.failuretext, AppResource.GeneralErorr, AppResource.oktext);
                    var properties2 = new Dictionary<string, string>
                       {
                             { "ChangeAppLanguagePopupViewModel: Error msg ", response.Data },{"UserId", Settings.UserId }
                       };
                    Analytics.TrackEvent("ChangeCulture", properties2);
                    return;
                }
            }
            
            Settings.AppLang = culture;
            CrossMultilingual.Current.CurrentCultureInfo = new CultureInfo(culture);
            AppResource.Culture = CrossMultilingual.Current.CurrentCultureInfo;
            await navService.NavigateToAsync<MainTabbedPageViewModel>();
            await PopupNavigation.Instance.PopAsync();
        }
    }
}
