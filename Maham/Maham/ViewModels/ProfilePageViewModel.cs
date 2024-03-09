using Microsoft.AppCenter.Crashes;
using Prism.Navigation;
using Refit;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Maham.Bases;
using Maham.Service;
using Maham.Service.General;
using Maham.Service.Model.Response.User;
using Maham.Setting;
using Xamarin.Forms;

namespace Maham.ViewModels
{
    public class ProfilePageViewModel : BaseViewModel
    {
       
        public bool isenabled { get; set; }
        public bool Issaved { get; set; }
        //public bool IsBusy { get; set; }
        private bool _isRtl;
        private bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                SetProperty(ref _isBusy, value);
            }
        }
        public GetUserProfileResponse profiledata { get; set; }
        public bool IsEdit { get; set; }
        private ImageSource _myimage;
        public ImageSource MyImage
        {
            get { return _myimage; }
            set
            {
                SetProperty(ref _myimage, value);          
            }
        }

        public string name { get; set; }
        public string fullName { get; set; }
        public string email { get; set; }
        public string phoneNumber { get; set; }
        public string profileimage { get; set; }
        public byte[] imagebyte { get; set; }
        public string id { get; set; }
        //public ICommand TakePhoto { get; set; }
        //public ICommand EditCommand { get; set; }
        //public ICommand SaveCommand { get; set; }
       // private readonly INavigationService _navigationService;
        private readonly INavService navService;
        public ICommand BackCommand { get; set; }

        public string VersionNumber { get; set; }

        public ProfilePageViewModel(INavService _navService, INavigationService _NavigationServices) : base(_NavigationServices)
        {
            navService = _navService;
            isenabled = false;
            Issaved = false;
            IsEdit = false;
            IsBusy = false;
           // _navigationService = navigationService;
            //TakePhoto = new Command(TakePhotoComandExcute);
            //EditCommand = new Command(EditCommandExcute);
            //SaveCommand = new Command(SaveCommandExcute);
            BackCommand = new Command(BackCommandExcute);
        
            MyImage = "user.svg";
            profileimage = "user.svg";          // _getUserProfileService = getUserProfileService;
            VersionNumber = "v1.0.24(71)";

            _isRtl = Settings.IsRtl;
        }
     
        private void BackCommandExcute(object obj)
        {
           // _navigationService.NavigateAsync("MainTabbedPage");
           // _navigationService.GoBackAsync(null,true);
            navService.NavigateBackAsync();
        }

        #region old
        //private async void SaveCommandExcute(object obj)
        //{
        //    isenabled = false;
        //    Issaved = false;
        //    IsEdit = false;
        //    //here 

        //     await UpdateProfileDate();
        //}

        //private void EditCommandExcute(object obj)
        //{
        //    isenabled = true;
        //    Issaved = true;
        //    IsEdit = false;
        //}

        //private async void TakePhotoComandExcute(object obj)
        //{

        //     string TitleOfimageAlert = _isRtl?  "اختار صورة" : "Choose Image" ;
        //    string ChooseFromLibraryButtonName = _isRtl? "اختر من المعرض" : "Choose from library";
        //     string TakePhotoButtonName = _isRtl? "التقاط صورة" : "Take photo" ;
        //    string DeletePhotoButtonName = _isRtl? "مسح الصورة" : "Delete photo";
        //    string CancelButtonName = _isRtl? "الغاء" : "Cancel photo";

        //var selectedOption = await Application.Current.MainPage.DisplayActionSheet(
        //        TitleOfimageAlert,
        //        CancelButtonName,
        //        DeletePhotoButtonName,
        //        new[] {
        //            ChooseFromLibraryButtonName,
        //            TakePhotoButtonName
        //        });
        //    if (DeletePhotoButtonName.Equals(selectedOption))
        //    {
        //        MyImage = "user.svg";
        //        DeleteUserProfilePhoto();
        //    }
        //    else if (ChooseFromLibraryButtonName.Equals(selectedOption))
        //    {
        //        TakePictureFromLibrary();
        //    }
        //    else if (TakePhotoButtonName.Equals(selectedOption))
        //    {
        //        TakePictureFromCamera();
        //    }
        //    //switch (selectedOption)
        //    //{
        //    //    case CancelButtonName:
        //    //        break;
        //    //    case DeletePhotoButtonName:
        //    //        MyImage = "user.svg";
        //    //        break;
        //    //    case ChooseFromLibraryButtonName:
        //    //        TakePictureFromLibrary();
        //    //        break;
        //    //    case TakePhotoButtonName:
        //    //        TakePictureFromCamera();
        //    //        break;
        //    //}
        //}

        //private async void DeleteUserProfilePhoto()
        //{
        //    try
        //    {
        //        var api = RestService.For<ITaskyApi>(new System.Net.Http.HttpClient(new HttpLoggingHandler()) { BaseAddress = new Uri(Settings.ApiUrl) });
        //        var result = await api.DeleteUserPhoto("Bearer " + Settings.AccessToken, Settings.UserName);
        //    }
        //    catch (Exception exception)
        //    {
        //        Crashes.TrackError(exception);
        //    }
        //}

        //private async Task<bool> StoragePremissionGranted()
        //{
        //    var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Storage);
        //    if (status != PermissionStatus.Granted)
        //    {
        //        var results = await CrossPermissions.Current.RequestPermissionsAsync(new[] { Permission.Storage });
        //        status = results[Permission.Storage];
        //    }

        //    return status == PermissionStatus.Granted;
        //}

        //private async Task<bool> CameraPremissionGranted()
        //{
        //    var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Camera);
        //    if (status != PermissionStatus.Granted)
        //    {
        //        var results = await CrossPermissions.Current.RequestPermissionsAsync(new[] { Permission.Camera });
        //        status = results[Permission.Camera];
        //    }

        //    return status == PermissionStatus.Granted;
        //}

        //private async void TakePictureFromCamera()
        //{
        //    try
        //    {
        //        bool cGranted = await CameraPremissionGranted();
        //        if (!cGranted)
        //        {
        //            //await Application.Current.MainPage.DisplayAlert("No Camera", ":( No camera available.", "OK");
        //            return;
        //        }

        //        bool sGranted = await StoragePremissionGranted();
        //        if (!sGranted)
        //        {
        //            //await Application.Current.MainPage.DisplayAlert("No Camera", ":( No camera available.", "OK");
        //            return;
        //        }

        //        await CrossMedia.Current.Initialize();

        //        if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
        //        {
        //            await Application.Current.MainPage.DisplayAlert("No Camera", ":( No camera available.", "OK");
        //            return;
        //        }

        //        string dat = DateTime.Now.ToShortDateStringForView();
        //        var ImageFile = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions()
        //        {
        //            Directory = "AgentAway",
        //            Name = $"{dat}.png",
        //            PhotoSize = Plugin.Media.Abstractions.PhotoSize.Small
        //        });

        //        //byte[] resizedImage = await CrossImageResizer.Current.ResizeImageWithAspectRatioAsync(File.ReadAllBytes(ImageFile.Path), 500, 500);
        //        if (ImageFile != null)
        //        {

        //            IsBusy = true;

        //            profileimage = ImageFile.Path;

        //            string message;
        //            var api = RestService.For<ITaskyApi>(new System.Net.Http.HttpClient(new HttpLoggingHandler()) { BaseAddress = new Uri(Settings.ApiUrl) });
        //            try
        //            {


        //                var data = new UploadProfileImageRequest { id = id, photo = File.ReadAllBytes(ImageFile.Path) };
        //                var result = await api.UploadProfileImage("Bearer " + Settings.AccessToken, data);
        //                if (result.requestSuccess)
        //                {

        //                    MyImage = result.Content.photoUrl;
        //                    IsBusy = false;
        //                }
        //                else
        //                {
        //                    message = result.errorMsg;
        //                    await Application.Current.MainPage.DisplayAlert(AppResource.failuretext, AppResource.imageuploadfilled, AppResource.oktext);
        //                    MyImage = "user.svg";
        //                }

        //            }
        //            catch (Exception exception)
        //            {
        //                MyImage = "user.svg";
        //                await Application.Current.MainPage.DisplayAlert(AppResource.failuretext, exception.Message, AppResource.oktext);
        //                var properties = new Dictionary<string, string>{{ "ProfileViewModel", "Takephoto" },};
        //                Crashes.TrackError(exception, properties);
        //                IsBusy = false;

        //            }

        //        }
        //        else
        //        {
        //            return;
        //        }
        //        // await DisplayAlert("File Location", file.Path, "OK");

        //    }
        //    catch (Exception exception)
        //    {
        //        MyImage = "user.svg";
        //        await Application.Current.MainPage.DisplayAlert(AppResource.failuretext, exception.Message, AppResource.oktext);
        //        var properties = new Dictionary<string, string> { { "ProfileViewModel", "TakePictureFromCamera" }, };
        //        Crashes.TrackError(exception, properties);
        //        IsBusy = false;
        //    }

        //}

        //private async void TakePictureFromLibrary()
        //{
        //    bool pGranted = await StoragePremissionGranted();
        //    if (!pGranted)
        //    {
        //        return;
        //    }
        //    await CrossMedia.Current.Initialize();

        //    if (!CrossMedia.Current.IsPickPhotoSupported)
        //    {
        //        //throw new NotSupportedException("This device is not supporting photo picking");
        //    }

        //    var ImageFile = await CrossMedia.Current.PickPhotoAsync();
        //    if (ImageFile != null)
        //    {
        //        IsBusy = true;


        //        // profileimage = ImageFile.Path;
        //        //MyImage = ImageFile.Path;
        //        var api = RestService.For<ITaskyApi>(new System.Net.Http.HttpClient(new HttpLoggingHandler()) { BaseAddress = new Uri(Settings.ApiUrl) });
        //        try
        //        {


        //            var result = await api.UploadProfileImage("Bearer " + Settings.AccessToken, new UploadProfileImageRequest { id = id, photo = File.ReadAllBytes(ImageFile.Path) });
        //            if(result.requestSuccess)
        //            {

        //                MyImage = result.Content.photoUrl;
        //                IsBusy = false;
        //            }
        //            else
        //            {
        //              var  message = result.errorMsg;
        //                await Application.Current.MainPage.DisplayAlert(AppResource.failuretext, AppResource.imageuploadfilled, AppResource.oktext);
        //                MyImage = "user.svg";
        //            }
        //        }
        //        catch (Exception exception)
        //        {
        //            MyImage = "user.svg";
        //            await Application.Current.MainPage.DisplayAlert(AppResource.failuretext, exception.Message, AppResource.oktext);
        //            var properties = new Dictionary<string, string>
        //        {
        //            { "ProfileViewModel", "Pickimagefromgallery" },
        //        };
        //            Crashes.TrackError(exception, properties);
        //        }

        //    }
        //    else
        //    {
        //        return;
        //    }
        //    // }
        //    //UserDialogs.Instance.HideLoading();
        //}
        //public static string Base64Encode(string plainText)
        //{
        //    var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
        //    return System.Convert.ToBase64String(plainTextBytes);
        //}
        //public static string Base64Decode(string base64EncodedData)
        //{
        //    var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
        //    return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        //}
        #endregion

        private async Task GetProfileData()
        {
            var api = RestService.For<ITaskyApi>(new System.Net.Http.HttpClient(new HttpLoggingHandler()) { BaseAddress = new Uri(Settings.ApiUrl) });
            try
            {
                IsBusy = true;
                var result = await api.GetProfileUser("Bearer " + Settings.AccessToken, Settings.UserId);
                if (result != null)
                {
                    profiledata = (result.Data).ToObject<GetUserProfileResponse>();
                    if (profiledata.photoUrl == null)
                    {
                        MyImage = "user.svg";

                    }
                    else
                    {
                        MyImage = profiledata.photoUrl;

                    }

                    id = profiledata.id;
                    imagebyte = profiledata.photo;
                    name = profiledata.name;
                    // MyImage = ImageSource.FromStream(() => new MemoryStream(result.photo));
                    fullName = _isRtl ?profiledata.arabicName : profiledata.fullName;
                    email = profiledata.email;
                    phoneNumber = profiledata.Mobile;

                }

            }
            catch (Exception exception)
            {
                var properties = new Dictionary<string, string>
                {
                    { "profileViewModel", "GetProfileData" },
                };
                Crashes.TrackError(exception, properties);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public override async Task InitializeAsync(object data)
        {
            await GetProfileData();
        }
    }
    
}
