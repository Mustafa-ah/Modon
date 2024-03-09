using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Maham.Bases;

namespace Maham.Service.General
{
    public interface INavService
    {
        Task InitializeAsync();

        Task NavigateToAsync<TViewModel>() where TViewModel : BaseViewModel;

        Task NavigateToAsync<TViewModel>(object parameter) where TViewModel : BaseViewModel;

        Task NavigateToAsync(Type viewModelType);

        Task ClearBackStack();

        Task NavigateToAsync(Type viewModelType, object parameter);

        Task NavigateBackAsync();

        Task RemoveLastFromBackStackAsync();

        Task PopToRootAsync();

        void Logout();

        Task ShowPopupPage(PopupPage page);
    }
}
