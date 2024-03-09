using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Maham.Bases;
using Maham.Service.General;
using Xamarin.Forms;

namespace Maham.ViewModels
{
	public class StarterPageViewModel : BaseViewModel
    {
        private readonly INavService navService;
        public ICommand NewClientcommand { get; set; }
        public ICommand OldClientCommand { get; set; }
        public StarterPageViewModel(INavService _navService, INavigationService _NavigationServices) : base(_NavigationServices)
        {
            navService = _navService;
            NewClientcommand = new Command(NewClientcommandExcute);
            OldClientCommand = new Command(OldClientCommandExcute);
        }

        private void OldClientCommandExcute(object obj)
        {
            navService.NavigateToAsync<NewClientPageViewModel>();
        }

        private void NewClientcommandExcute(object obj)
        {
            navService.NavigateToAsync<ExistClientPageViewModel>();
        }
    }
}
