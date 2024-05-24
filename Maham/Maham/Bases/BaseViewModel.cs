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
using Microsoft.AppCenter.Crashes;

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

        //public async Task<bool> RequestStoragePermission()
        //{
        //    var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Storage);

        //    if (status != Plugin.Permissions.Abstractions.PermissionStatus.Granted)
        //    {
        //       // var request = await CrossPermissions.Current.RequestPermissionsAsync<StoragePermission>();
        //        PermissionStatus statusd = await CrossPermissions.Current.RequestPermissionAsync<StoragePermission>();
        //        if (request.ContainsKey(Permission.Storage) && request[Permission.Storage] == Plugin.Permissions.Abstractions.PermissionStatus.Granted)
        //        {
        //            // Permission granted, access storage here
        //            Console.WriteLine("Storage permission granted");
        //            return true;
        //        }
        //        else
        //        {
        //            // Permission denied, inform user
        //            Console.WriteLine("Storage permission denied");
        //            return false;
        //        }
        //    }
        //    else
        //    {
        //        // Permission already granted, access storage here
        //        Console.WriteLine("Storage permission already granted");
        //        return true;
        //    }
        //}
        public async Task<bool> StoragPremissionGranted()
        {
            try
            {
                bool granted = false;

                await Device.InvokeOnMainThreadAsync(async () =>
                {
                   //var status = await Permissions.CheckStatusAsync<Permissions.StorageRead>();
                   // if (status != PermissionStatus.Granted)
                   // {
                   //     status = await Permissions.RequestAsync<Permissions.StorageWrite>();
                   //     status = await Permissions.RequestAsync<Permissions.StorageRead>();
                   //     granted = status == PermissionStatus.Granted;
                   // }

                    //var writeStatus = await Permissions.CheckStatusAsync<Permissions.StorageWrite>();
                    //var readStatus = await Permissions.CheckStatusAsync<Permissions.StorageRead>();

                    //if (writeStatus == PermissionStatus.Granted && readStatus == PermissionStatus.Granted)
                    //{
                    //    // Permissions have already been granted
                    //    // Perform your storage-related operations
                    //}
                    //else
                    //{
                    //    // Permissions have not been granted
                    //    // Request the permissions
                    //    var permissionsToRequest = new List<string>();

                    //    if (writeStatus != PermissionStatus.Granted)
                    //        permissionsToRequest.Add<Permissions.StorageWrite>();

                    //    if (readStatus != PermissionStatus.Granted)
                    //        permissionsToRequest.Add<Permissions.StorageRead>();

                    //    var results = await Permissions.RequestAsync(permissionsToRequest.ToArray());

                    //    if (results.ContainsKey(typeof(Permissions.StorageWrite)))
                    //        writeStatus = results[typeof(Permissions.StorageWrite)];

                    //    if (results.ContainsKey(typeof(Permissions.StorageRead)))
                    //        readStatus = results[typeof(Permissions.StorageRead)];

                    //    if (writeStatus == PermissionStatus.Granted && readStatus == PermissionStatus.Granted)
                    //    {
                    //        // Permissions have been granted
                    //        // Perform your storage-related operations
                    //    }
                    //    else
                    //    {
                    //        // Permissions have been denied
                    //        // Handle the denial or notify the user
                    //    }
                    //}
                });


                return granted;
            }
            catch (Exception ex)
            {
                 Crashes.TrackError(ex);
                return false;
            }
        }
    }
}
