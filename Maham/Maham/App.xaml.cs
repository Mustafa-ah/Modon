using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Plugin.FirebasePushNotification;
using Plugin.Multilingual;
using Prism;
using Prism.Ioc;
using Prism.Navigation;
using Prism.Unity;
using Refit;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;
using Maham.Models;
using Maham.Persistence;
using Maham.Service;
using Maham.Service.Implmentation.Firebase;
using Maham.Service.Implmentation.Login;
using Maham.Service.Implmentation.Registeration;
using Maham.Service.Implmentation.User;
using Maham.Service.Model.Request.Login;
using Maham.Setting;
using Maham.ViewModels;
using Maham.ViewModels.Authentication;
using Maham.Views;
using Maham.Views.Authentication;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Maham.Helpers;
using System.Globalization;
using Maham.Service.General;
using Plugin.Connectivity;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Maham
{
    public partial class App : PrismApplication
    {
        /* 
         * The Xamarin Forms XAML Previewer in Visual Studio uses System.Activator.CreateInstance.
         * This imposes a limitation in which the App class must have a default constructor. 
         * App(IPlatformInitializer initializer = null) cannot be handled by the Activator.
         */
        private SQLiteAsyncConnection connection;

        public App() : this(null) { }

        
        public App(IPlatformInitializer initializer = null) : base(initializer)
        {
        }

        string culture ="en";
        protected override void OnInitialized()
        {

           

            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjHTQxAR8/V1NCaF5cXmZCf1FpRmJGdld5fUVHYVZUTXxaS00DNHVRdkdnWXhfeHRcRWRcWU13XUc=");
           // Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MzAwODhAMzEzNjJlMzMyZTMwRzFkWWRzNnJNRnEzM0QvRlp5MGcrcVdaNGpxSVpxaklKWTAvWm9OUU9VUT0=");
            InitializeComponent();

            //CrossMultilingual.Current.CurrentCultureInfo = new CultureInfo("en");
           
            //AppResource.Culture = CrossMultilingual.Current.DeviceCultureInfo;
             culture = Settings.AppLang;
            if (string.IsNullOrEmpty(culture))
            {
                culture = CrossMultilingual.Current.DeviceCultureInfo.TwoLetterISOLanguageName;
                Settings.AppLang = culture;

                CrossMultilingual.Current.CurrentCultureInfo = CrossMultilingual.Current.DeviceCultureInfo;
            }
            else
            {
                CrossMultilingual.Current.CurrentCultureInfo = new CultureInfo(culture);
            }
           

            InitializeNavigation();

            PreventLinkerFromStrippingCommonLocalizationReferences();
        }
        private async Task InitializeNavigation()
        {
            var navigationService = ExtResolve<INavService>();
            await navigationService.InitializeAsync();
        }
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<INavigationService, Prism.Navigation.PageNavigationService>();
            containerRegistry.Register<INavService, NavService>();
            DependencyService.Register<IFileHelper>();
            //containerRegistry.RegisterPopupNavigationService();
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<LoginPage, LoginPageViewModel>();
            containerRegistry.RegisterForNavigation<ProfilePage>();
            containerRegistry.RegisterForNavigation<VerficationCodePage>();
            containerRegistry.RegisterForNavigation<WelcomePage>();
            containerRegistry.RegisterForNavigation<ResetPassword>();
            containerRegistry.RegisterForNavigation<RegisterationPage>();
            containerRegistry.Register<ITaskyApi>();
            containerRegistry.Register<ILoginService, LoginService>();
            containerRegistry.Register<ISignUpService, SignUpService>();
            containerRegistry.Register<ISubmitVerficationCodeService, SubmitVerficationCodeService>();
            containerRegistry.Register<ISendVerficationCodeService, SendVerficationCodeService>();
            containerRegistry.Register<IGetUserProfileService, GetUserProfileService>();
            containerRegistry.Register<IFirebaseService, FirebaseService>();

            // this.Container.Resolve<ILoginService>();
            containerRegistry.RegisterForNavigation<PriortiesView>();
            containerRegistry.RegisterForNavigation<NewTaskPage, NewTaskPageViewModel>();
            containerRegistry.RegisterForNavigation<PrioritiesDetails, PrioritiesDetailsViewModel>();

            containerRegistry.RegisterForNavigation<EditTask, EditTaskViewModel>();

            containerRegistry.RegisterForNavigation<MainTabbedPage, MainTabbedPageViewModel>();
            containerRegistry.RegisterForNavigation<NotificationsPage, NotificationsPageViewModel>();
            containerRegistry.RegisterForNavigation<TasksPage, TasksPageViewModel>();
            containerRegistry.RegisterForNavigation<DashboardPage, DashboardPageViewModel>();
            containerRegistry.RegisterForNavigation<DashboardChildPage, DashboardChildPageViewModel>();
            Prism.Mvvm.ViewModelLocationProvider.Register<PrioritiesPage, PrioritiesPageViewModel>();
            Prism.Mvvm.ViewModelLocationProvider.Register<NotPrioritiesPage, NotPrioritiesPageViewModel>();
            containerRegistry.RegisterForNavigation<SetttingsPage, SetttingsPageViewModel>();
            containerRegistry.RegisterForNavigation<TaskDetailsPage, TaskDetailsPageViewModel>();
            containerRegistry.RegisterForNavigation<FillterPopup, FillterPopupViewModel>();
            containerRegistry.RegisterForNavigation<popup, popupViewModel>();
            containerRegistry.RegisterForNavigation<DashboardFiltterPopup, DashboardFiltterPopupViewModel>();
            containerRegistry.RegisterForNavigation<ReassignEmployeePage, ReassignEmployeePageViewModel>();
            containerRegistry.RegisterForNavigation<SearchPopup, SearchPopupViewModel>();
            containerRegistry.RegisterForNavigation<SearchResult, SearchResultViewModel>();
            containerRegistry.RegisterForNavigation<ExistClientPage, ExistClientPageViewModel>();
            containerRegistry.RegisterForNavigation<StarterPage, StarterPageViewModel>();
            containerRegistry.RegisterForNavigation<NewClientPage, NewClientPageViewModel>();
            containerRegistry.RegisterForNavigation<AssigneeSearchPopupPage, AssigneeSearchPopupPageViewModel>();
            containerRegistry.RegisterForNavigation<DepartmentsView, DepartmentsViewModel>();
            containerRegistry.RegisterForNavigation<EditSearchPage, EditSearchPageViewModel>();
            containerRegistry.RegisterForNavigation<UserGroupPopUpPage, UserGroupPopUpPageViewModel>();
            containerRegistry.RegisterForNavigation<SourcePopUpPage, SourcePopUpPageViewModel>();
            containerRegistry.RegisterForNavigation<EmergencyCallPopUpPage, EmergencyCallPopUpPageViewModel>();

        }
        public T ExtResolve<T>()
        {
            return this.Container.Resolve<T>();
        }
        public void GetCountainer()
        {
             //this.Container();
        }

        private static void PreventLinkerFromStrippingCommonLocalizationReferences()
        {
            _ = new System.Globalization.GregorianCalendar();
            _ = new System.Globalization.PersianCalendar();
            _ = new System.Globalization.UmAlQuraCalendar();
            new System.Globalization.HijriCalendar();

            new System.Globalization.ChineseLunisolarCalendar();
            new System.Globalization.HebrewCalendar();

            new System.Globalization.JapaneseCalendar();
            new System.Globalization.JapaneseLunisolarCalendar();
            new System.Globalization.KoreanCalendar();
            new System.Globalization.KoreanLunisolarCalendar();
            new System.Globalization.TaiwanCalendar();
            new System.Globalization.TaiwanLunisolarCalendar();
            new System.Globalization.ThaiBuddhistCalendar();
  
            //  _ = new System.Globalization.ThaiBuddhistCalendar();
        }
        protected async override void OnStart()
        {
            try
            {
                // Handle when your app starts
                AppCenter.Start("android=d5d00436-80d6-4f27-8c64-4952740b4013;" +
                    "ios=a2115287-9422-4217-a35e-b7b7a08e4c8e;", typeof(Analytics), typeof(Crashes));


                // rferesh token
                var timer = new System.Threading.Timer(e => RefreshToken(),
                      null,
                      TimeSpan.Zero,
                      TimeSpan.FromMinutes(5));

                connection = DependencyService.Get<ISQLiteDb>().GetConnectionAsync();
                await connection.CreateTableAsync<Tenants>();
                
                //CrossFirebasePushNotification.Current.Subscribe("general");

                CrossFirebasePushNotification.Current.OnTokenRefresh += async (s, p) =>
                {
                   
                    if (!string.IsNullOrEmpty(Settings.UserId))
                    {
                        try
                        {
                            if (Settings.IsLoged)
                            {

                                var api = RestService.For<ITaskyApi>(new System.Net.Http.HttpClient(new HttpLoggingHandler()) { BaseAddress = new Uri(Settings.ApiUrl) });
                                var response = await api.AddUserFireBaseToken("Bearer " + Settings.AccessToken, Settings.FirebaseToken, p.Token,
                                    Settings.UserId, Settings.AppLang);
                                if (response.Success)
                                {

                                    var langResponse = await api.SetPreferredLanguage("Bearer " + Settings.AccessToken, Setting.Settings.UserId, Settings.FirebaseToken, culture);

                                }
                                else
                                {
                                    var propertiesL = new Dictionary<string, string> { { "App.cs: Error msg ", response.Data }, { "UserId", Settings.UserId } };
                                    Analytics.TrackEvent("AddUserFireBaseToken", propertiesL);
                                }
                            }
                        }
                        catch (Exception exception)
                        {
                            var properties = new Dictionary<string, string> { { "app.xaml.cs", "ontokennotfication" } };
                            Crashes.TrackError(exception, properties);
                        }
                    }
                    ////AddUserFireBaseToken uses this as prev token after that we can save te new token
                    Settings.FirebaseToken = p.Token;

                    var properties2 = new Dictionary<string, string> { { "App.cs: FireBase token ", p.Token } };
                    Analytics.TrackEvent("OnTokenRefresh_New3", properties2);
                };

                
                // not display notification when app killed
                CrossFirebasePushNotification.Current.OnNotificationReceived += (s, p) =>
                {
                    try
                    {
                        Settings.TaskId = p.Data["taskId"].ToString();
                    }
                    catch (Exception ex)
                    {
                        Crashes.TrackError(ex);
                    }
                };
            
            }
            catch (Exception exception)
            {
                var properties = new Dictionary<string, string>
                       {
                             { "App.cs", "OnStart " },
                       };
                Crashes.TrackError(exception, properties);
            }
        }
         
        private async void RefreshToken()
        {
            
            if (Settings.IsLoged && CrossConnectivity.Current.IsConnected)
            {
                var api = RestService.For<ITaskyApi>(new System.Net.Http.HttpClient(new HttpLoggingHandler()) { BaseAddress = new Uri(Settings.ApiUrl) });

                try
                {
                    var result = await api.Authenticate(new LoginRequest { email = Settings.UserName, password = Settings.PasswordData });

                    if (result.Authenticated)
                    {
                        Settings.AccessToken = result.Token;
                        Settings.UserId = result.Id;
                        Settings.IsSelfAssigned = false;

                        var properties = new Dictionary<string, string>
                       {
                             { "App.cs new token", result.Token },
                       };
                        Analytics.TrackEvent("TokenRefreshed", properties);
                    }
                    else
                    {
                        LogoutCommandExcute();
                        var properties = new Dictionary<string, string>
                       {
                             { "App.cs", "RefreshToken" },
                       };
                        Analytics.TrackEvent("TokenExpired", properties);
                    }
                }
                catch (Exception exception)
                {
                    var properties = new Dictionary<string, string>
                       {
                             { "App.cs", "refresh token " },
                       };
                    Crashes.TrackError(exception, properties);
                    Analytics.TrackEvent("TokenExpired", properties);
                    //LogoutCommandExcute();
                }
            }
        }


        private async void LogoutCommandExcute()
        {

            try
            {
                var api = RestService.For<ITaskyApi>(new System.Net.Http.HttpClient(new HttpLoggingHandler()) { BaseAddress = new Uri(Settings.ApiUrl) });
                var response = await api.DeleteUserFireBaseToken("Bearer " + Settings.AccessToken, Settings.FirebaseToken, Settings.UserId);
                Settings.UserId = null;
                Settings.UserName = null;
                Settings.AccessToken = null;
                Settings.IsLoged = false;
                Settings.PasswordData = " ";
                //Settings.AppLang = "";
                await NavigationService.NavigateAsync("Maham:///NavigationPage/LoginPage");
            }
            catch (Exception exception)
            {

                var properties = new Dictionary<string, string>
                       {
                             { "settingspageviewmodel", "logout" },
                       };
                Crashes.TrackError(exception, properties);
            }
        }


        protected override void OnSleep()
        {
            base.OnSleep();
            
        }
        protected override void OnResume()
        {
            try
            {
                base.OnResume();
               
            }
            catch (Exception exception)
            {
                var properties = new Dictionary<string, string>
                       {
                             { "App.cs", "OnResume" },
                       };
                Crashes.TrackError(exception, properties);
            }
        }


    }
}
