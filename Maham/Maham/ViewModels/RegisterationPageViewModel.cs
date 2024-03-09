using Microsoft.AppCenter.Crashes;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
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
	public class RegisterationPageViewModel : BaseViewModel
    {
        public ICommand RegisterCommand { get; set; }
        private readonly INavService navService;

        public string UserName { get; set; }
        string image;
        public string CheckImage
        {
            get { return image; }
            set { image = value; RaisePropertyChanged(); }

        }
        public bool isagree { get; set; }
        public bool Agreed { get; set; }
        public bool IsBusy { get; set; }
        public string Email { get; set; }
        public bool EmailValid => Email?.Length >= 2;
        public string Password { get; set; }
        public ICommand AgreeCommand { get; set; }
        public ICommand BackCommand { get; set; }
        public RegisterationPageViewModel(INavService _navService, INavigationService _NavigationServices) : base(_NavigationServices)
        {
            navService = _navService;
            IsBusy = false;
            isagree = false;
            CheckImage = "unchecked";
            Agreed = false;
            RegisterCommand = new Command(RegisterCommandExcute);
            AgreeCommand = new Command(AgreeCommandExcute);
            BackCommand = new Command(BackCommandExcute);
        }

        private void BackCommandExcute(object obj)
        {
            navService.NavigateBackAsync();
        }

        private void AgreeCommandExcute(object obj)
        {
            //isagree = true;

            var agree = obj;

        }

        private async void RegisterCommandExcute(object obj)
        {
           

           if (!String.IsNullOrEmpty(UserName) && !String.IsNullOrEmpty(Password) && CheckEmailValid())
           {
                    Settings.AccessToken = "";
                    Settings.UserId = "";
                 if (isagree)
                 {
                   try
                    {
                        //using (UserDialogs.Instance.Loading("wait..."))
                        //{
                        IsBusy = true;
                            var body = new RegisterationRequest { email = Email, userName = UserName, password = Password };
                            var api = RestService.For<ITaskyApi>(new System.Net.Http.HttpClient(new HttpLoggingHandler()) { BaseAddress = new Uri(Settings.ApiUrl) });
                            var result = await api.SignUp(body);
                        if (result.RequestSuccess)
                        {
                            Settings.UserId = result.userId;

                            if (!result.verified)
                            {
                                IsBusy = false;
                                await navService.NavigateToAsync<VerficationCodePageViewModel>();
                            }
                        }
                        else
                        {
                            IsBusy = false;
                            string msg = "Email or User Name already exist! ";
                            string ok = "Ok";
                            if (Settings.IsRtl)
                            {
                                ok = "مواق";
                                msg = "البريد الالكترونى او اسم المستخدم  مسجل من قبل";
                            }
                            await Application.Current.MainPage.DisplayAlert("", msg, ok);
                        }
                           
                        //}

                    }
                    catch (Exception exception)
                    {
                        IsBusy = false;
                        var properties = new Dictionary<string, string>
                      {
                          { "RegisterationViewModel", "SignUp" },
                       };
                        Crashes.TrackError(exception, properties);
                    }
                 }
                    else
                    {
                    string msg = "You have to Agree the License ";
                    string ok = "Ok";
                    string title = "Agree license";
                    if (Settings.IsRtl)
                    {
                        ok = "مواق";
                        msg = "يجب ان توافق على الشروط و الاحكام";
                        title = "يرجى الموافقة";
                    }
                    await Application.Current.MainPage.DisplayAlert(title, msg, ok);
                    }
              
               
                        
                    
           }
               
              
            
          
            
        }
        private bool CheckEmailValid()
        {
            var isValid = Regex.IsMatch(Email, AppConstants.EmailValidationRule);
            return (!string.IsNullOrEmpty(Email) && isValid);
        }
    }
}


