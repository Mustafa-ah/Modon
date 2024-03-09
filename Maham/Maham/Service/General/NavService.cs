using Microsoft.AppCenter.Crashes;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Maham.Bases;
using Maham.Setting;
using Maham.ViewModels;
using Maham.ViewModels.Authentication;
using Maham.Views;
using Maham.Views.Authentication;
using Xamarin.Forms;

namespace Maham.Service.General
{
    class NavService : INavService
    {

        private readonly Dictionary<Type, Type> _mappings;

        protected Application CurrentApplication => Application.Current;

        public NavService()
        {
            _mappings = new Dictionary<Type, Type>();

            CreatePageViewModelMappings();
        }

        public async Task InitializeAsync()
        {
            // await NavigateToAsync<MainTabbedPageViewModel>();
            // return;
            if (Settings.IsLoged == false)
            {
                await NavigateToAsync<LoginPageViewModel>();
            }
            else
            {
                await NavigateToAsync<MainTabbedPageViewModel>();
            }
        }

        public async Task ClearBackStack()
        {
            await CurrentApplication.MainPage.Navigation.PopToRootAsync();
        }

        public async Task NavigateBackAsync()
        {
            try
            {
                if (CurrentApplication.MainPage is MainTabbedPage mainPage)
                {
                    await mainPage.Navigation.PopAsync();
                }
                else if (CurrentApplication.MainPage != null)
                {
                    await CurrentApplication.MainPage.Navigation.PopAsync();
                }
            }
            catch (Exception exception)
            {
                var properties = new Dictionary<string, string>
                       {
                             { "NavService", "NavigateBackAsync" },
                       };
                Crashes.TrackError(exception, properties);
            }
          
        }

        public virtual Task RemoveLastFromBackStackAsync()
        {
            if (CurrentApplication.MainPage is MainTabbedPage mainPage)
            {
                mainPage.Navigation.RemovePage(
                    mainPage.Navigation.NavigationStack[mainPage.Navigation.NavigationStack.Count - 2]);
            }

            return Task.FromResult(true);
        }

        public async Task PopToRootAsync()
        {
            //if (CurrentApplication.MainPage is MainTabbedPage mainPage)
            //{
            //    await mainPage.Navigation.PopToRootAsync();
            //}
           await CurrentApplication.MainPage.Navigation.PopToRootAsync();
        }

        public Task NavigateToAsync<TViewModel>() where TViewModel : BaseViewModel
        {
            return InternalNavigateToAsync(typeof(TViewModel), null);
        }

        public Task NavigateToAsync<TViewModel>(object parameter) where TViewModel : BaseViewModel
        {
            return InternalNavigateToAsync(typeof(TViewModel), parameter);
        }

        public Task NavigateToAsync(Type viewModelType)
        {
            return InternalNavigateToAsync(viewModelType, null);
        }

        public Task NavigateToAsync(Type viewModelType, object parameter)
        {
            return InternalNavigateToAsync(viewModelType, parameter);
        }

        protected virtual async Task InternalNavigateToAsync(Type viewModelType, object parameter)
        {
            var page = CreatePage(viewModelType, parameter);

            if (page is MainTabbedPage)
            {
                CurrentApplication.MainPage = new TaskyNavigationPage(page)
                {
                    BarBackgroundColor = Color.White,
                    BarTextColor = Color.Black,
                };
            }

            else if(CurrentApplication.MainPage is TaskyNavigationPage)
            {
                await CurrentApplication.MainPage.Navigation.PushAsync(page);
            }
            
            else
            {
                CurrentApplication.MainPage = new TaskyNavigationPage(page)
                {
                    BarBackgroundColor = Color.White,
                    BarTextColor = Color.Black,
                };
            }


            // 

            await (page.BindingContext as BaseViewModel).InitializeAsync(parameter);
        }

        protected Type GetPageTypeForViewModel(Type viewModelType)
        {
            if (!_mappings.ContainsKey(viewModelType))
            {
                throw new KeyNotFoundException($"No map for ${viewModelType} was found on navigation mappings");
            }

            return _mappings[viewModelType];
        }

        protected Page CreatePage(Type viewModelType, object parameter)
        {
            Type pageType = GetPageTypeForViewModel(viewModelType);

            if (pageType == null)
            {
                throw new Exception($"Mapping type for {viewModelType} is not a page");
            }

            Page page = Activator.CreateInstance(pageType) as Page;

            return page;
        }

        private void CreatePageViewModelMappings()
        {

            _mappings.Add(typeof(LoginPageViewModel), typeof(LoginPage));
            _mappings.Add(typeof(RegisterationPageViewModel), typeof(RegisterationPage));
            _mappings.Add(typeof(VerficationCodePageViewModel), typeof(VerficationCodePage));
            _mappings.Add(typeof(ProfilePageViewModel), typeof(ProfilePage));
            _mappings.Add(typeof(ResetPasswordViewModel), typeof(ResetPassword));
            _mappings.Add(typeof(NewTaskPageViewModel), typeof(NewTaskPage));
            _mappings.Add(typeof(PrioritiesDetailsViewModel), typeof(PrioritiesDetails));
            _mappings.Add(typeof(EditTaskViewModel), typeof(EditTask));
            _mappings.Add(typeof(MainTabbedPageViewModel), typeof(MainTabbedPage));
            _mappings.Add(typeof(NotificationsPageViewModel), typeof(NotificationsPage));
            _mappings.Add(typeof(TasksPageViewModel), typeof(TasksPage));
            _mappings.Add(typeof(DashboardPageViewModel), typeof(DashboardPage));
            _mappings.Add(typeof(DashboardChildPageViewModel), typeof(DashboardChildPage));
            _mappings.Add(typeof(SetttingsPageViewModel), typeof(SetttingsPage));
            _mappings.Add(typeof(TaskDetailsPageViewModel), typeof(TaskDetailsPage));
            _mappings.Add(typeof(FillterPopupViewModel), typeof(FillterPopup));
            _mappings.Add(typeof(popupViewModel), typeof(popup));
            _mappings.Add(typeof(DashboardFiltterPopupViewModel), typeof(DashboardFiltterPopup));
            _mappings.Add(typeof(ReassignEmployeePageViewModel), typeof(ReassignEmployeePage));
            _mappings.Add(typeof(SearchPopupViewModel), typeof(SearchPopup));
            _mappings.Add(typeof(SearchResultViewModel), typeof(SearchResult));
            _mappings.Add(typeof(ExistClientPageViewModel), typeof(ExistClientPage));
            _mappings.Add(typeof(StarterPageViewModel), typeof(StarterPage));
            _mappings.Add(typeof(NewClientPageViewModel), typeof(NewClientPage));
            _mappings.Add(typeof(AssigneeSearchPopupPageViewModel), typeof(AssigneeSearchPopupPage));
            _mappings.Add(typeof(EditSearchPageViewModel), typeof(EditSearchPage));
            _mappings.Add(typeof(UserGroupPopUpPageViewModel), typeof(UserGroupPopUpPage));
            _mappings.Add(typeof(SourcePopUpPageViewModel), typeof(SourcePopUpPage));
            _mappings.Add(typeof(EmergencyCallPopUpPageViewModel), typeof(EmergencyCallPopUpPage));
            _mappings.Add(typeof(TaskHistoryViewModel), typeof(TaskHistoryView));
            _mappings.Add(typeof(DepartmentsViewModel), typeof(DepartmentsView));
        }

        public void Logout()
        {
            var page = CreatePage(typeof (LoginPageViewModel), null);
            CurrentApplication.MainPage = new TaskyNavigationPage(page)
            {
                BarBackgroundColor = Color.White,
                BarTextColor = Color.Black,
            };
        }

        public async Task ShowPopupPage(PopupPage page)
        {
           await PopupNavigation.Instance.PushAsync(page);
        }
    }
}
