using Microsoft.AppCenter.Crashes;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using Maham.Bases;
using Maham.Constants;
using Maham.Service;
using Maham.Setting;
using System.Windows.Input;
using Xamarin.Forms;
using Maham.Enums;

namespace Maham.ViewModels
{
    public class MainTabbedPageViewModel : BaseViewModel
    {

        // public bool OnFullMode { get; set; }
        
        public MainTabbedPageViewModel(INavigationService _NavigationServices) : base(_NavigationServices)
        {

            //  MainTitle = "xxxxxxxxxxx";

        }

       
        private string _mainTitle;

        public string MainTitle
        {
            get { return _mainTitle; }
            set {
                _mainTitle = value;/* SetProperty(ref _mainTitle, value)*/;
                RaisePropertyChanged();
            }
        }
        
    }
}
