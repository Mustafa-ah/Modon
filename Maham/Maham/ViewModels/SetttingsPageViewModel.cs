using Microsoft.AppCenter.Crashes;
using Plugin.FirebasePushNotification;
using Prism.Navigation;
using Prism.Services;
using Refit;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using Maham.Bases;
using Maham.Service;
using Maham.Service.General;
using Maham.Setting;
using Maham.Views;
using Xamarin.Forms;

namespace Maham.ViewModels
{
    public class SetttingsPageViewModel : BaseViewModel
    {
        private readonly INavService navService;
        private IPageDialogService _dialogService;

        public ICommand ProfileCommand { get; set; }
        public ICommand LanguageCommand { get; set; }
        public ICommand logoutCommand { get; set; }
        public ICommand ResetPasswordCommand { get; set; }
        public ICommand ToggleNotificationCommand { get; set; }

        private bool _allowPushNotification;

        public bool AllowPushNotification
        {
            get { return _allowPushNotification; }
            set { _allowPushNotification = value; Settings.AllowPushNotification = value; }
        }

        private bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                SetProperty(ref _isBusy, value);
            }
        }

        public SetttingsPageViewModel(INavService _navService,
            IPageDialogService dialogService, INavigationService _NavigationServices) : base(_NavigationServices)
        {
            navService = _navService;
            
            _dialogService = dialogService;
            ProfileCommand = new Command(Profile);
            LanguageCommand = new Command(Language);
            logoutCommand = new Command(logoutCommandExcute);
            ResetPasswordCommand = new Command(ResetPasswordCommandExcute);
            ToggleNotificationCommand = new Command(ToggleNotificationCommandExcute);
            InitializeAsync(null);

            AllowPushNotification = Settings.AllowPushNotification;
        }

        private async void ToggleNotificationCommandExcute(object obj)
        {
            try
            {
                var api = RestService.For<ITaskyApi>(new System.Net.Http.HttpClient(new HttpLoggingHandler()) { BaseAddress = new Uri(Settings.ApiUrl) });
                
                if (AllowPushNotification)
                {
                     CrossFirebasePushNotification.Current.RegisterForPushNotifications();
                }
                else
                {
                    CrossFirebasePushNotification.Current.UnregisterForPushNotifications();
                    var response = await api.DeleteUserFireBaseToken("Bearer " + Settings.AccessToken, Settings.FirebaseToken, Settings.UserId);
                }
            }
            catch (Exception exception)
            {
                Crashes.TrackError(exception);
            }
           
        }

        private void ResetPasswordCommandExcute(object obj)
        {
            MessagingCenter.Send<BaseViewModel>(this, "RefreshTabBarIconsInIOS");
            navService.NavigateToAsync<ResetPasswordViewModel>();
        }

        private async void logoutCommandExcute(object obj)
        {
            
            try
            {
                IsBusy = true;
                   var api = RestService.For<ITaskyApi>(new System.Net.Http.HttpClient(new HttpLoggingHandler()) { BaseAddress = new Uri(Settings.ApiUrl) });
                var response = await api.DeleteUserFireBaseToken("Bearer "+ Settings.AccessToken, Settings.FirebaseToken, Settings.UserId);
            }
            catch (Exception exception)
            {
                var properties = new Dictionary<string, string>
                       {
                             { "settingspageviewmodel", "logout" },
                       };
                Crashes.TrackError(exception, properties);
            }
            finally
            {
                Settings.UserId = null;
                Settings.UserName = null;
                Settings.AccessToken = null;
                Settings.IsLoged = false;
                Settings.PasswordData = " ";
                // Settings.AppLang = "";

                IsBusy = false;
                navService.Logout();
            }

            
        }

        private async void Language(object obj)
        {
           await navService.ShowPopupPage(new ChangeAppLanguagePopup());
            #region Ready for delete
            /* 
           string msg = "Language";
           string cancel = "Cancel";
           string arabic = "Arabic";
           string english = "English";
           if (new Helpers.Helper().CurrentLanguage() == 2)
           {
                msg = "اللغة";
                cancel = "الغاء";
                arabic = "عربى";
                english = "انجليزى";
           }
           var action = await _dialogService.DisplayActionSheetAsync(msg, cancel, null, arabic, english);

           if (action == arabic)
           {
               string culture = "ar";
               if (new Helpers.Helper().CurrentLanguage() != 2)
               {
                   Settings.AppLang = culture;
                   CrossMultilingual.Current.CurrentCultureInfo = new CultureInfo(culture);
                   AppResource.Culture = CrossMultilingual.Current.CurrentCultureInfo;
                   await navService.NavigateToAsync<MainTabbedPageViewModel>();

               }            
           }
           else if(action == english)
           {
               if (new Helpers.Helper().CurrentLanguage() == 2)
               {
                   string culture = "en";
                   Settings.AppLang = culture;
                   CrossMultilingual.Current.CurrentCultureInfo = new CultureInfo(culture);

                   AppResource.Culture = CrossMultilingual.Current.CurrentCultureInfo;
                   await navService.NavigateToAsync<MainTabbedPageViewModel>();
               }
           }
           */
            #endregion
        }

        private async void Profile(object obj)
        {
            MessagingCenter.Send<BaseViewModel>(this, "RefreshTabBarIconsInIOS");
          // await _navigationService.NavigateAsync("ProfilePage");
            await navService.NavigateToAsync<ProfilePageViewModel>();
        }

        public override async void AddTask(object obj)
        {
            await navService.NavigateToAsync<NewTaskPageViewModel>();
        }
    }
}
