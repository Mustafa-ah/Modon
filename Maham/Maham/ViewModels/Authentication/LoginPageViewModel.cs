using Microsoft.AppCenter.Crashes;
using Plugin.Connectivity;
using Prism.Navigation;
using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Maham.Bases;
using Maham.Constants;
using Maham.Resources;
using Maham.Service;
using Maham.Service.General;
using Maham.Service.Implmentation.Login;
using Maham.Service.Model.Request.Login;
using Maham.Setting;
using Xamarin.Forms;
using Unity;

namespace Maham.ViewModels.Authentication
{
    public class LoginPageViewModel : BaseViewModel
    {
        private ILoginService _loginService;
        public string username { get; set; }
        public string password { get; set; }
        public ICommand SingUpCommand { get; set; }
        public ICommand showpasswordCommand { get; set; }
        public ICommand SingInCommand { get; set; }
        public bool ishow { get; set; }
        public bool IsBusy { get; set; }
        public bool AllowSignup { get; set; }
        public ICommand ForgetPasswordCommand { get; set; }
        private readonly INavService navService;
        [InjectionConstructor]
        public LoginPageViewModel(INavService _navService, INavigationService _NavigationServices):base(_NavigationServices)
        {
            // _loginService = loginService;
            IsBusy = false;
            navService = _navService;
            SingUpCommand = new Command(SignupCommandExcute);
            SingInCommand = new Command(SignInCommandExcute);
            ForgetPasswordCommand = new Command(ForgetPasswordCommandExcute);
            AllowSignup = false;// Settings.AllowSignUp;

            Settings.ApiUrl = AppConstants.BasicURL;
        }



        private void ForgetPasswordCommandExcute(object obj)
        {
            // _navigationService.NavigateAsync("ResetPassword");
            Application.Current.MainPage.DisplayAlert("", "will be soon", "ok");
        }

        private async void SignInCommandExcute(object obj)
        {
            if (!String.IsNullOrEmpty(username) && !String.IsNullOrEmpty(password))
            {
              
                var api = RestService.For<ITaskyApi>(new System.Net.Http.HttpClient(new HttpLoggingHandler()) { BaseAddress = new Uri(Settings.ApiUrl) });
                try
                {
                    IsBusy = true;
                    if (CrossConnectivity.Current.IsConnected)
                    {
                        var result = await api.Authenticate(new LoginRequest { email = username, password = password });
                        if (result!=null)
                        {
                            Settings.AccessToken = result.Token;
                            Settings.UserId = result.Id;
                            Settings.UserName = username;
                            Settings.ArabicUserName = result.ArabicName;
                            //arabicName
                            Settings.PasswordData = password;
                            Settings.IsSelfAssigned = false;
                            Settings.IsLoged = true;
                            Settings.HasUserGroups = result.HasUserGroups;
                            Settings.HasPositions = result.HasPositions;
                            if (result.UserGroup != null)
                                Settings.UserGroup = result.UserGroup.Value.ToString();
                            else
                                Settings.UserGroup = "";
                            if (result.Position != null)
                                Settings.Position = result.Position.Value.ToString();
                            else
                                Settings.Position = "";
                            Settings.IsSuperAdmin = result.IsSuperAdmin;
                            Settings.Ability = result.Ability;
                            Settings.UserGroupList = result.UserGroupList;
                            Settings.IsEntityManager = result.IsEntityManager;
                            Settings.creatorPrivilege = new List<string>(new string[] { "changeprogress", "changepriority", "requestchangeenddateactions", "changeenddate", "reassign", "close", "return", "delete", "edit", "details", "attachfile" });
                            Settings.assigneePrivilege = new List<string>(new string[] { "changeprogress", "details", "requestchangeenddate", "attachfile" });
                            try
                            {
                                
                                if (Settings.AllowPushNotification)
                                {
                                    var response = await api.AddUserFireBaseToken("Bearer " + Settings.AccessToken,
                                        String.Empty, Settings.FirebaseToken, Settings.UserId, Settings.AppLang);
                                    if (response.Success)
                                    {
                                        await api.SetPreferredLanguage("Bearer " + Settings.AccessToken, Settings.UserId, Settings.FirebaseToken, Settings.AppLang);

                                        await navService.NavigateToAsync<MainTabbedPageViewModel>();

                                    }
                                    else
                                    {
                                        await Application.Current.MainPage.DisplayAlert($"Request success = {response.Success}", $"{response.Data}", "Ok");

                                    }
                                }
                                else
                                {
                                    await navService.NavigateToAsync<MainTabbedPageViewModel>();
                                }
                                
                            }
                            catch (Exception exception)
                            {
                                var properties = new Dictionary<string, string>
                                    {
                               { "loginpageviewmodel", "signincommand" },
                                   };
                                Crashes.TrackError(exception, properties);
                                await navService.NavigateToAsync<MainTabbedPageViewModel>();
                            }

                            // await _navigationService.NavigateAsync("NewTaskPage");

                        }
                        else
                        {
                            IsBusy = false;
                            await Application.Current.MainPage.DisplayAlert(AppResource.WrongCredentials, AppResource.WrongUserOrPass, AppResource.oktext);
                        }
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert(" ", AppResource.notConnectedMsg, AppResource.oktext);
                    }
                }

                catch (Exception exception)
                {
                    await Application.Current.MainPage.DisplayAlert(AppResource.WrongCredentials, AppResource.WrongUserOrPass, AppResource.oktext);
                    var properties = new Dictionary<string, string>
                       {
                             { "LoginPageViewModel", "Signincommand" },
                       };
                    Crashes.TrackError(exception, properties);
                }
                finally
                {
                    IsBusy = false;
                }
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert(AppResource.WrongCredentials, AppResource.WrongUserOrPass, AppResource.oktext);
            }
        }

        
        private void SignupCommandExcute(object obj)
        {
            navService.NavigateToAsync<RegisterationPageViewModel>();
        }


        public override Task InitializeAsync(object data)
        {
            return base.InitializeAsync(data);
        }
    }
}
