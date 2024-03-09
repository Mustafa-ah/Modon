using Prism;
using Prism.AppModel;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Maham.Setting;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Maham.Bases
{
    public class BaseViewModel : BindableBase, INavigationAware, IDestructible, IPageLifecycleAware
    {
     
        public ICommand AddTaskCommand { get; set; }
        public NavigationParameters NavParameters { get; set; }
        public string tabName;
        public int tabId;
        public Guid Id { get; set; }
        bool notConnected = false;
        public bool NotConnected
        {
            get { return notConnected; }
            set
            {
                SetProperty(ref notConnected, value);
                RaisePropertyChanged(nameof(NotConnected));
            }
        }

        bool isEmpty = false;
        public bool IsEmpty
        {
            get { return isEmpty; }
            set
            {
                SetProperty(ref isEmpty, value);
            }
        }

        public bool IsRTL { get; set; }

        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private string _notComnnectedMsg;
        public string NotConnectedMsg
        {
            get { return _notComnnectedMsg; }
            set { SetProperty(ref _notComnnectedMsg, value); }
        }

        public BaseViewModel(INavigationService _NavigationServices)
        {
            SwitchConnectivity(Connectivity.NetworkAccess, true);
            AddTaskCommand = new Command(AddTask);
        }
        public virtual void OnAppearing()
        {
            NotConnectedMsg = Resources.AppResource.notConnectedMsg;
            Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;
        }
        public virtual void OnDisappearing()
        {
            Connectivity.ConnectivityChanged -= Connectivity_ConnectivityChanged;
        }

        public virtual void OnNavigatedFrom(NavigationParameters parameters)
        {

        }

        public virtual void OnNavigatedTo(NavigationParameters parameters)
        {

        }

        public virtual void OnNavigatingTo(NavigationParameters parameters)
        {

        }

        public virtual void Destroy()
        {

        }

        public virtual Task InitializeAsync(object data)
        {
            NotConnectedMsg = Resources.AppResource.notConnectedMsg;
            Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;
            return Task.FromResult(false);
        }

        void Connectivity_ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            var access = e.NetworkAccess;

            SwitchConnectivity(access , false);
        }

        private void SwitchConnectivity(NetworkAccess access, bool initilaize)
        {
            switch (access)
            {
                case NetworkAccess.Unknown:
                    NotConnected = false;
                    break;
                case NetworkAccess.None:
                    NotConnected = true;
                    break;
                case NetworkAccess.Local:
                    NotConnected = false;
                    break;
                case NetworkAccess.ConstrainedInternet:
                    NotConnected = false;
                    break;
                case NetworkAccess.Internet:
                    NotConnected = false;
                    if (!initilaize)
                    {
                        BackOnLine();
                    }
                    break;
                default:
                    NotConnected = false;
                    break;
            }
        }

        public virtual void BackOnLine()
        {

        }

        public virtual void AddTask(object obj)
        {
           
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
           // throw new NotImplementedException();
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
           // throw new NotImplementedException();
        }
    }
}
