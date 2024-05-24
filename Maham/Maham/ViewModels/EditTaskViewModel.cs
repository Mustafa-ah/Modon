using Microsoft.AppCenter.Crashes;
using Plugin.FilePicker;
using Prism.Navigation;
using Refit;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Maham.Bases;
using Maham.Constants;
using Maham.Extentions;
using Maham.Helpers;
using Maham.Models;
using Maham.Models.User;
using Maham.Resources;
using Maham.Service;
using Maham.Service.General;
using Maham.Setting;
using Maham.Views;
using Xamarin.Forms;
using Maham.Service.Model.Response.Tasks;
using Task = System.Threading.Tasks.Task;
using System.Net.Http;
using Newtonsoft.Json;
using Xamarin.Essentials;

namespace Maham.ViewModels
{
    public class EditTaskViewModel : BaseViewModel
    {
        private readonly INavService navService;
        public ICommand selectpriorityCommand { get; set; }
        public ICommand selectsourceCommand { get; set; }
        public ICommand UploadFileCommand { get; set; }
        public ICommand openassignemployeeCommand { get; set; }
        public ICommand OpenUserGroupPopupCommand { get; set; }
        public ICommand OpenPositionPopupCommand { get; set; }
        public ICommand OpenSourcePopupCommand { get; set; }
        public ICommand EditTaskCommand { get; set; }
        public ICommand ClearViewCommand { get; set; }
        public ICommand OpenDepartmentsPopupCommand { get; set; }
        public ICommand ClearCommand { get; set; }
        public byte[] _File { get; set; }
        public string sirencheck { get; set; }
        public string employeename { get; set; }
        public string priorityname { get; set; }
        public string priorityimage { get; set; }
        public int priorityId { get; set; }
        public bool priorityChecked { get; set; }
        public string sourcename { get; set; }
        public int sourceId { get; set; }
        public bool ishowmore { get; set; }
        public string TaskName { get; set; }
        public string FileName { get; set; }
        public string FileSize { get; set; }
        public string UserGroupName { get; set; }
        public string UserGroupID { get; set; }
        public string PositionName { get; set; }
        public string PositionID { get; set; }
        public string Description { get; set; }
        public string ImageType { get; set; }
        public string ResponsibleID { get; set; }
        public string ReminderDate { get; set; }
        public DateTime ReminderDate_DT { get; set; }
        public bool ReminderEnabled { get; set; }
        public bool UG_P_Visiblty { get; set; }
        public string UserGroup { get; set; }
        public string Position { get; set; }
        public bool UserGroupVisiblty { get; set; }
        public bool HasUserGroups { get; set; }
        public bool HasPositions { get; set; }
        public bool PositionVisiblty { get; set; }
        public string StartDate { get; set; }
        public DateTime StartDate_DT { get; set; }
        public string EndDate { get; set; }
        public DateTime EndDate_DT { get; set; }
        public Color StartDateColor { get; set; }
        public Color EndDateColor { get; set; }
        public Color ReminderDateColor { get; set; }
        public ICommand CheckPriorityCommand { get; set; }
        public ICommand CheckSourceCommand { get; set; }
        public ICommand chooseEmployeeCommand { get; set; }
        public string DepartmentName { get; set; }

        public string Descriptionedit { get; set; }
        string _refname;
        public string referenceEdit
        {
            get { return _refname; }
            set { SetProperty(ref _refname, value); }
        }
        public int? refidEdit { get; set; }
        public string RefCheckedImageEdit { get; set; }
        public string DescriptionEdit { get; set; }
        public string ImageTypeEdit { get; set; }
        public int ResponsibleIDEdit { get; set; }
        public string StartDateEdit { get; set; }
        public string StartDatebind2 { get; set; }
        public string EndDatebind2 { get; set; }
        public string sectorname { get; set; }
        public string EndDateEdit { get; set; }
        public string ImageTypeedit { get; set; }
        public string FileSizeedit { get; set; }
        public double valueprogress { get; set; }
        public string showprogressvalue { get; set; }
        public string uploadagain { get; set; }
        public DateTime? MaxDate { get; set; }

        public bool tempsupport { get; set; }
        public bool issource { get; set; }
        public bool isBusy { get; set; }
        public bool IsSupportedEdit { get; set; }
        public bool openPriorityList { get; set; }
        public bool assignemployeelistvisibility { get; set; }
        public bool prioritylistVisibilitiy { get; set; }
        public TaskDto task { get; set; }
        public bool SectorListVisiability { get; set; }
        public bool DepartmentListVisiability { get; set; }
        public bool RefrenceListVisibility { get; set; }
        public bool isClickedSector { get; set; }
        public bool isClickeddepartment { get; set; }
        public bool ischeckedassignemployee { get; set; }
        public bool isrefrencechecked { get; set; }
        public bool stopuploadgrid { get; set; }
        public bool uploadgridvisbility { get; set; }
        public string sireneditcheck { get; set; }
        public byte[] FileEdit { get; set; }
        public string FilePath { get; set; }
        public string employeenameEdit { get; set; }
        public string EmpImage { get; set; }
        public string prioritynameEdit { get; set; }
        public string priorityimageedit { get; set; }
        public int priorityIdedit { get; set; }
        public string TaskNameEdit { get; set; }
        public string FileNameEdit { get; set; }
        public string FileSizeEdit { get; set; }

        public bool AsUser { get; set; }
        public bool AsUserGroup { get; set; }

        public ObservableCollection<ListPopUpModel> PrioritiesListData
        {
            get;
            set;
        }

        public ObservableCollection<ListPopUpModel> UserGroupListData
        {
            get;
            set;
        }
        public ObservableCollection<ListPopUpModel> PositionListData
        {
            get;
            set;
        }
        public static ObservableCollection<FileDataModel> sharedlist { get; set; }
        public double progressvalue { get; set; }
        public string uploadtext { get; set; }
        bool _isclicked;
        public bool isClicked { get; set; }
        public bool isreflable { get; set; }
        public ResponsiblesDDL Lastemployee { get; set; }
        public string showprogress { get; set; }
        public ObservableCollection<ResponsiblesDDL> MyList
        {
            get;
            set;

        }
        private ObservableCollection<FileDataModel> filesList { get; set; }
        public ObservableCollection<FileDataModel> FilesList
        {
            get { return filesList; }
            set { filesList = value; RaisePropertyChanged(); }
        }
        public ObservableCollection<ResponsiblesDDL> TempemployeeDemoDataList { get; set; }
        private bool _isvisableupload;
        public bool isvisableupload
        {
            get { return _isvisableupload; }
            set { _isvisableupload = value; RaisePropertyChanged(); }
        }
        public bool FilelistVisbility { get; set; }
        public bool listVisiblty
        {

            get; set;
        }
        public bool prioritylist
        {

            get; set;
        }
        public bool sourcelist
        {

            get; set;
        }
        public bool arrowprioritycheck { get; set; }
        public bool arrowsourcecheck { get; set; }

        private bool _isvisableuploadview1;
        public bool isvisableuploadview1
        {
            get { return _isvisableuploadview1; }
            set
            {
                _isvisableuploadview1 = value;
                RaisePropertyChanged();
            }
        }

        private Entity selectedEntity;
        public Entity SelectedEntity
        {
            get { return selectedEntity; }
            set { selectedEntity = value; RaisePropertyChanged(); }
        }

        public ICommand downloadfilecommand { get; set; }
        public ICommand OnSelectStarDateCommand { get; set; }
        public ICommand OnSelectEndDateCommand { get; set; }
        public ICommand OnSelectReminderDateCommand { get; set; }
        public UserGroupListModel selectUserGroup { get; set; }
        public PositionListModel selectPosition { get; set; }

        public ICommand DelteFileCommand { get; set; }
        public ICommand backnavigation { get; set; }
        public ICommand ShowmoreCommand { get; set; }
        private string _dashs = "— — — — — — ";
        public EditTaskViewModel(INavService _navService, INavigationService _NavigationServices) : base(_NavigationServices)
        {
            MyList = new ObservableCollection<ResponsiblesDDL>();
            isvisableupload = true;
            isvisableuploadview1 = false;
            FilelistVisbility = false;
            isClicked = false;
            arrowprioritycheck = false;
            arrowsourcecheck = false;
            ReminderEnabled = false;


            HasUserGroups = Settings.HasUserGroups;
            HasPositions = Settings.HasPositions;
            UserGroup = Settings.UserGroup;
            Position = Settings.Position;

            IsRTL = Settings.IsRtl;

            AsUserGroup = HasUserGroups;
            //AsUser = AsUserGroup ? false : HasPositions;
            AsUser = !AsUserGroup;// && HasPositions;

            UserGroupVisiblty = (!string.IsNullOrEmpty(UserGroup) && !string.IsNullOrEmpty(Position)) || HasUserGroups;
            PositionVisiblty = (!string.IsNullOrEmpty(UserGroup) && !string.IsNullOrEmpty(Position)) || HasPositions;

            if (!string.IsNullOrEmpty(UserGroup) && !HasUserGroups && string.IsNullOrEmpty(Position))
            {
                UserGroupID = UserGroup;
                GetAssignEmployeeByUserGroupID(UserGroupID);
            }
            else
            {
                UserGroupID = "";
                UserGroupName = AppResource.selectUserGroup;
            }
            if (!string.IsNullOrEmpty(Position) && !HasPositions && string.IsNullOrEmpty(UserGroup))
            {
                PositionID = Position;
                GetAssignEmployeeByPositionID(PositionID);
            }
            else
            {
                PositionID = "";
                PositionName = AppResource.selectPosition;
            }

            ishowmore = false;
            sirencheck = "siren";
            priorityname = AppResource.selectpriority;
            priorityChecked = false;
            priorityimage = "#98aab4";
            sourcename = AppResource.selectSource;
            StartDate = _dashs;
            EndDate = _dashs;
            ReminderDate = _dashs;

            StartDate_DT = DateTime.Now;
            EndDate_DT = DateTime.Now;
            ReminderDate_DT = DateTime.Now;

            uploadtext = AppResource.uploadfileagaintext;
            navService = _navService;

            downloadfilecommand = new Command(downloadfilecommandExcute);
            OnSelectStarDateCommand = new Command(ShowDatePicker);
            OnSelectEndDateCommand = new Command(ShowEndDatePicker);
            OnSelectReminderDateCommand = new Command(ShowReminderDatePicker);
            DelteFileCommand = new Command(DeleteFilecommandExcute);
            selectpriorityCommand = new Command(selectpriorityCommandExcute);
            selectsourceCommand = new Command(selectsourceCommandExcute);
            UploadFileCommand = new Command(uploadFileCommandExcute);
            backnavigation = new Command(backnavigationExcute);
            EditTaskCommand = new Command(EditTaskCommandExcute);
            ClearCommand = new Command(ClearCommandExcute);
            OpenUserGroupPopupCommand = new Command(OpenUserGroupPopupCommandExcute);
            OpenPositionPopupCommand = new Command(OpenPositionPopupCommandExcute);
            OpenSourcePopupCommand = new Command(OpenSourcePopupCommandExcute);
            OpenDepartmentsPopupCommand = new Command(OpenDepartmentsPopupCommandExcute);

            FilesList = new ObservableCollection<FileDataModel>();
            sharedlist = new ObservableCollection<FileDataModel>();

            TempemployeeDemoDataList = new ObservableCollection<ResponsiblesDDL>();
            TempemployeeDemoDataList = MyList;
            PrioritiesListData = new ObservableCollection<ListPopUpModel>();

            UserGroupListData = new ObservableCollection<ListPopUpModel>();
            PositionListData = new ObservableCollection<ListPopUpModel>();
            openassignemployeeCommand = new Command(openassignemployeeCommandExcute);
            CheckPriorityCommand = new Command(CheckPriorityCommandExcute);
            chooseEmployeeCommand = new Command(ChooseEmployeeCommandExcute);
            ShowmoreCommand = new Command(ShowmoreCommandExcute);
            if (FilesList.Count == 0)
            {
                FilelistVisbility = false;
                ishowmore = false;
            }
            MessagingCenter.Subscribe<ResponsiblesDDL>(this, "SelectedEmployee", (emp) =>
            {
                Lastemployee = emp;
                ResponsibleID = emp.Value2.ID;
                employeename = emp.Text;
                listVisiblty = false;
            });
            MessagingCenter.Subscribe<UserGroupListModel>(this, "selectUserGroup", (selectUserGroup) =>
            {
                UserGroupName = selectUserGroup.name;
                UserGroupID = selectUserGroup.id;
                GetAssignEmployeeByUserGroupID(UserGroupID);

                PositionName = AppResource.selectPosition;
                PositionID = "";

                SelectedEntity = new Entity();
                AsUserGroup = true;
                AsUser = false;
            });
            MessagingCenter.Subscribe<ListModel>(this, "selectPosition", (selectPosition) =>
            {
                PositionName = selectPosition.name;
                PositionID = selectPosition.id;
                GetAssignEmployeeByPositionID(PositionID);

                UserGroupName = AppResource.selectUserGroup;
                UserGroupID = "";

                SelectedEntity = new Entity();

                AsUserGroup = false;
                AsUser = true;
            });


            MessagingCenter.Subscribe<ListPopUpModel>(this, "selectSource", (selectSource) =>
            {
                sourcename = selectSource.name;
                sourceId = selectSource.id;
            });

            MessagingCenter.Subscribe<DepartmentsViewModel, object>(this, "EntitySelected", OnEntitySelected);

            StartDateColor = Color.FromHex("#B7BCBD");
            EndDateColor = Color.FromHex("#B7BCBD");


            SelectedEntity = new Entity();

            Initilaize();
        }

        private async void OnEntitySelected(DepartmentsViewModel arg1, object arg2)
        {
            SelectedEntity = arg2 as Entity;
            ResponsibleID = "";
            employeename = "";
            listVisiblty = false;
            await GetAssignEmployeeEntitiyId(arg1.SelectedIDs.ToArray());

        }

        private void OpenDepartmentsPopupCommandExcute(object obj)
        {
            Settings.SelectedEntity = SelectedEntity;
            string userId = !string.IsNullOrEmpty(PositionID) ? PositionID : UserGroupID;
            PopupNavigation.Instance.PushAsync(new DepartmentsView(AsUser, userId, true));
        }

        private async void downloadfilecommandExcute(object obj)
        {
            try
            {
                var status = await Permissions.CheckStatusAsync<Permissions.StorageRead>();
                if (status != PermissionStatus.Granted)
                {
                    status = await Permissions.RequestAsync<Permissions.StorageRead>();
                    status = await Permissions.RequestAsync<Permissions.StorageWrite>();
                }
                if (status != PermissionStatus.Granted)
                {
                    return;
                }
                var file = (FileDataModel)obj;
                var path = DependencyService.Get<IFileHelper>().file(AppConstants.AppName);
                string filepath = Path.Combine(path, file.FileName);
                if (File.Exists(filepath))
                {
                    DependencyService.Get<IFileHelper>().FilePath(filepath);
                }
                else
                {
                    isBusy = true;
                    AttachmentDto attachment = await GetAttchmentbyId(file.AttachmentId);
                    isBusy = false;
                    if (attachment.Path != null)
                    {
                        DependencyService.Get<IFileHelper>().FilePath(attachment.Path);
                    }

                }

            }
            catch (Exception ee)
            {

                var properties = new Dictionary<string, string>
                                   {
                                         { "downloadfilecommandExcute", "attachment" },
                                   };
                Crashes.TrackError(ee, properties);
            }
            //var file = (FileDataModel)obj;
            //if (file.content != null)
            //{
            //    File.WriteAllBytes(file.filepath, file.content);
            //}
            //// Application.Current.MainPage.DisplayAlert("", AppResource.dowlnoadSucessfully, AppResource.oktext);
            ////  CrossFilePicker.Current.OpenFile(file.FileName);
            //DependencyService.Get<IFileHelper>().FilePath(file.filepath);
        }

        private async Task<AttachmentDto> GetAttchmentbyId(Guid attachmentId)
        {
            try
            {
                var api = RestService.For<ITaskyApi>(new HttpClient(new HttpLoggingHandler()) { BaseAddress = new Uri(Settings.ApiUrl) });

                var result = await api.GetTaskAttachment("Bearer " + Settings.AccessToken, attachmentId.ToString());

                AttachmentDto x = new AttachmentDto();
               // x = (result.Data).ToObject<AttachmentDto>();
                x = JsonConvert.DeserializeObject<AttachmentDto>(Convert.ToString(result.Data));
                return x;
            }
            catch (Exception ee)
            {

                var properties = new Dictionary<string, string>
                                   {
                                         { "GetAttchmentbyId", "attachment" },
                                   };
                Crashes.TrackError(ee, properties);
                return new AttachmentDto();
            }
        }

        private async Task<AttachmentDto> AddNewAttchment(AttachmentDto attachment)
        {
            try
            {
                var api = RestService.For<ITaskyApi>(new HttpClient(new HttpLoggingHandler()) { BaseAddress = new Uri(Settings.ApiUrl) });

                var result = await api.AddAttachment("Bearer " + Settings.AccessToken, attachment);
                AttachmentDto x = new AttachmentDto();
                //x = (result.Data).ToObject<AttachmentDto>();
                x = JsonConvert.DeserializeObject<AttachmentDto>(Convert.ToString(result.Data));
                return x;
            }
            catch (Exception ee)
            {
                var properties = new Dictionary<string, string>
                                   {
                                         { "GetAttchmentbyId", "attachment" },
                                   };
                Crashes.TrackError(ee, properties);
                return new AttachmentDto();
            }
        }
        private async Task<bool> FileSizeExceededTheLimit(int size)
        {
            try
            {
                int mSize = size / 1024;
                bool _size = mSize > AppConstants.MaxFileSize;

                if (_size)
                {
                    string msg_ar = $"يجب ان لا يتخطى حجم المرفقات عن {AppConstants.MaxFileSize} ميجا";
                    string msg_en = $"Attachments size must not Exceed {AppConstants.MaxFileSize} M";
                    string msg_ = Setting.Settings.IsRtl ? msg_ar : msg_en;
                    await Application.Current.MainPage.DisplayAlert(AppResource.alerttext, msg_, AppResource.oktext);

                }

                return _size;
            }
            catch
            {
                return false;
            }
        }


        private bool _assigneeSearchPopupOpeend;
        private async void openassignemployeeCommandExcute(object obj)
        {
            if (_assigneeSearchPopupOpeend)
            {
                return;
            }
            _assigneeSearchPopupOpeend = true;
            NavParameters = new NavigationParameters();
            NavParameters.Add("Listofemp", MyList);
            NavParameters.Add("empid", ResponsibleID);
            await navService.NavigateToAsync<AssigneeSearchPopupPageViewModel>(NavParameters);
            _assigneeSearchPopupOpeend = false;
        }
        public override void OnNavigatedTo(NavigationParameters parameters)
        {

            if (parameters != null && parameters.Count > 0)
            {
                base.OnNavigatedTo(parameters);
                if (parameters["selectedemp"] != null)
                {
                    var data = parameters["selectedemp"] as ResponsiblesDDL;
                    Lastemployee = data;
                    ResponsibleID = data.Value2.ID;
                    employeename = data.Text;
                    listVisiblty = false;
                }
            }
            else
            {
                return;
            }

        }
        private void backnavigationExcute(object obj)
        {
            navService.NavigateBackAsync();
        }

        private async void DeleteFilecommandExcute(object obj)
        {
            try
            {
                bool delete_ = await Application.Current.MainPage.DisplayAlert("", AppResource.DeleteMsg, AppResource.oktext, AppResource.canceltext);
                if (delete_)
                {
                    isBusy = true;
                    var item = (FileDataModel)obj;
                    int index_ = FilesList.IndexOf(item);
                    FilesList.RemoveAt(index_);

                    //refresh repeater view
                    FilesList = new ObservableCollection<FileDataModel>(FilesList);

                    if (FilesList.Count > 2)
                    {
                        ishowmore = true;
                    }
                    else
                    {
                        ishowmore = false;
                    }
                    bool deleted = await DeleteAttchment(item.AttachmentId);
                    isBusy = false;
                }

            }
            catch (Exception exception)
            {
                isBusy = false;
                var properties = new Dictionary<string, string>
                       {
                             { "_EditTaskViewModel", "DeleteFilecommandExcute" },
                       };
                Crashes.TrackError(exception, properties);
            }

        }

        private async Task<bool> DeleteAttchment(Guid attachmentId)
        {
            try
            {
                var api = RestService.For<ITaskyApi>(new HttpClient(new HttpLoggingHandler()) { BaseAddress = new Uri(Settings.ApiUrl) });

                var result = await api.DeleteAttachment("Bearer " + Settings.AccessToken, attachmentId.ToString());

                return result.Success;
            }
            catch (Exception ee)
            {

                var properties = new Dictionary<string, string>
                                   {
                                         { "GetAttchmentbyId", "attachment" },
                                   };
                Crashes.TrackError(ee, properties);
                return false;
            }
        }
        private void ShowmoreCommandExcute(object obj)
        {
            sharedlist = FilesList;

            PopupNavigation.Instance.PushAsync(new popup(sharedlist));
        }

        private void ShowDatePicker(object obj)
        {
            StartDate = StartDate_DT.ToShortDateStringForView();
            StartDateColor = Color.Black;
        }

        private void ShowEndDatePicker(object obj)
        {
            EndDate = EndDate_DT.ToShortDateStringForView();
            EndDateColor = Color.Black;
        }

        private void ShowReminderDatePicker(object obj)
        {
            if (ReminderEnabled)
            {
                ReminderDate = ReminderDate_DT.ToShortDateStringForView();
                ReminderDateColor = Color.Black;
            }
        }

        private void ChooseEmployeeCommandExcute(object obj)
        {
            var ItemSelected = obj as ResponsiblesDDL;

            if (ItemSelected == null) return;

            foreach (var item in MyList)
            {
                item.IsCheckedemployee = false;
            }

            if (ItemSelected != Lastemployee)
            {
                if (Lastemployee != null)
                {
                    Lastemployee.IsCheckedemployee = false;
                }
                ItemSelected.IsCheckedemployee = true;

            }
            else
            {
                ItemSelected.IsCheckedemployee = !ItemSelected.IsCheckedemployee;
            }
            if (ItemSelected.IsCheckedemployee == false)
            {
                employeename = "";
                ResponsibleID = "";
            }
            else
            {
                employeename = ItemSelected.Text;
                ResponsibleID = ItemSelected.Value2.ID;
            }
            Lastemployee = ItemSelected;

            listVisiblty = false;


        }
        public async Task GetAssignEmployeeByPositionID(string ID)
        {
            var api = RestService.For<ITaskyApi>(new HttpClient(new HttpLoggingHandler()) { BaseAddress = new Uri(Settings.ApiUrl) });
            try
            {
                MyList.Clear();
                var result = await api.GetPositionAssignees("Bearer " + Settings.AccessToken, ID);
                List<ResponsiblesDDL> x = new List<ResponsiblesDDL>();
                //x = (result.Data).ToObject<List<ResponsiblesDDL>>();
                x = JsonConvert.DeserializeObject<ResponsiblesDDL>(Convert.ToString(result.Data));
                foreach (var item in x)
                {
                    var txt = IsRTL ? item.ArabicText : item.Text;
                    MyList.Add(new ResponsiblesDDL { Text = txt, Value2 = item.Value2 });
                }
                TempemployeeDemoDataList = MyList;
            }
            catch (Exception exception)
            {
                var properties = new Dictionary<string, string>
                       {
                             { "NewTaskViewModel", "assignemployee" },
                       };
                Crashes.TrackError(exception, properties);
            }
        }
        public async Task GetAssignEmployeeByUserGroupID(string ID)
        {
            var api = RestService.For<ITaskyApi>(new HttpClient(new HttpLoggingHandler()) { BaseAddress = new Uri(Settings.ApiUrl) });
            try
            {
                MyList.Clear();
                var result = await api.GetUserGroupAssignees("Bearer " + Settings.AccessToken, ID, (int)Enums.Privilege.Reassign);
                List<ResponsiblesDDL> x = new List<ResponsiblesDDL>();
                //   x = (result.Data).ToObject<List<ResponsiblesDDL>>();
                x = JsonConvert.DeserializeObject<List<ResponsiblesDDL>>(Convert.ToString(result.Data));
                foreach (var item in x)
                {
                    MyList.Add(new ResponsiblesDDL { Text = item.Text, Value2 = item.Value2 });
                }
                TempemployeeDemoDataList = MyList;
            }
            catch (Exception exception)
            {
                var properties = new Dictionary<string, string>
                       {
                             { "NewTaskViewModel", "assignemployee" },
                       };
                Crashes.TrackError(exception, properties);
            }
        }
       
        public async Task GetPriority()
        {
            var api = RestService.For<ITaskyApi>(new HttpClient(new HttpLoggingHandler()) { BaseAddress = new Uri(Settings.ApiUrl) });
            try
            {


                var result = await api.GetPriority("Bearer " + Settings.AccessToken);
                List<ListPopUpModel> x = new List<ListPopUpModel>();
                //x = (result.Data).ToObject<List<ListPopUpModel>>();
                x = JsonConvert.DeserializeObject<List<ListPopUpModel>>(Convert.ToString(result.Data));
                foreach (var item in x)
                {
                    PrioritiesListData.Add(new ListPopUpModel { id = item.id, name = Settings.IsRtl ? item.nameAr : item.name, image2 = item.image2 });
                }
            }
            catch (Exception exception)
            {
                var properties = new Dictionary<string, string>
                       {
                             { "EditTaskViewModel", "getpriorities" },
                       };
                Crashes.TrackError(exception, properties);
            }
        }
        private void CheckPriorityCommandExcute(object obj)
        {
            var ItemSelected = obj as ListPopUpModel;
            if (ItemSelected == null) return;

            foreach (var item in PrioritiesListData)
            {
                item.IsChecked = false;
            }
            ItemSelected.IsChecked = true;
            priorityChecked = true;
            priorityname = ItemSelected.name;
            priorityimage = ItemSelected.image2;
            prioritylist = false;
            priorityId = ItemSelected.id;

            arrowprioritycheck = false;
        }
        private void OpenUserGroupPopupCommandExcute(object obj)
        {
            Settings.GeneralId_string = UserGroupID;
            AsUser = false;
            PopupNavigation.Instance.PushAsync(new UserGroupPopUpPage());
        }
        private void OpenPositionPopupCommandExcute(object obj)
        {
            Settings.GeneralId_string = PositionID;
            AsUser = true;
            PopupNavigation.Instance.PushAsync(new PositionPopUpPage());
        }
        private void OpenSourcePopupCommandExcute(object obj)
        {
            Settings.GeneralId = sourceId;
            if (AsUserGroup && !string.IsNullOrEmpty(UserGroupID))
            {
                PopupNavigation.Instance.PushAsync(new SourcePopUpPage(UserGroupID, (int)Enums.Privilege.Reassign));
            }
            else if (AsUser && !string.IsNullOrEmpty(PositionID))
            {
                PopupNavigation.Instance.PushAsync(new SourcePopUpPage(null, (int)Enums.Privilege.Reassign));
            }
        }
        private void ClearCommandExcute(object obj)
        {
            navService.NavigateBackAsync();
        }
        bool _adding = false;
        private async void EditTaskCommandExcute(object obj)
        {
            if (_adding)
            {
                return;
            }
            _adding = true;

            if (UserGroupVisiblty && PositionVisiblty && UserGroupID == "" && PositionID == "")
            {
                _adding = false;
                await Application.Current.MainPage.DisplayAlert(AppResource.invailddate, AppResource.UserGroup_Position_Validation, AppResource.oktext);
            }
            else if (PositionVisiblty && UserGroupID == "" && PositionID == "")
            {
                _adding = false;
                await Application.Current.MainPage.DisplayAlert(AppResource.invailddate, AppResource.Position_Validation, AppResource.oktext);
            }
            else if (UserGroupVisiblty && UserGroupID == "" && PositionID == "")
            {
                _adding = false;
                await Application.Current.MainPage.DisplayAlert(AppResource.invailddate, AppResource.UserGroup_Validation, AppResource.oktext);
            }
            else if (String.IsNullOrEmpty(TaskName) || String.IsNullOrWhiteSpace(TaskName) || sourceId == 0
                || String.IsNullOrEmpty(StartDate) || StartDate.Equals(_dashs) || priorityId == 0
                  || String.IsNullOrEmpty(EndDate) || EndDate.Equals(_dashs) || String.IsNullOrEmpty(ResponsibleID))
            {
                await Application.Current.MainPage.DisplayAlert(AppResource.alerttext, AppResource.alertfilledalldata, AppResource.oktext);
                _adding = false;
            }
            //else if (StartDate.ToDateTimeFromShortDate() < DateTime.Now.Date)
            //{
            //    _adding = false;
            //    await Application.Current.MainPage.DisplayAlert(AppResource.invailddate, AppResource.StartDate_Now, AppResource.oktext);
            //}

            else if (ReminderEnabled && ReminderDate.ToDateTimeFromShortDate() < StartDate.ToDateTimeFromShortDate())
            {
                _adding = false;
                await Application.Current.MainPage.DisplayAlert(AppResource.invailddate, AppResource.ReminderDate_Start, AppResource.oktext);
            }
            //else if (EndDate.ToDateTimeFromShortDate() < task.EndDate)
            //{
            //    _adding = false;
            //    await Application.Current.MainPage.DisplayAlert(AppResource.invailddate, AppResource.EndDate_Original, AppResource.oktext);
            //}
            else if (StartDate.ToDateTimeFromShortDate() > EndDate.ToDateTimeFromShortDate())
            {
                _adding = false;
                await Application.Current.MainPage.DisplayAlert(AppResource.invailddate, AppResource.Comparedatetext, AppResource.oktext);
            }
            else if (EndDate.ToDateTimeFromShortDate() < DateTime.Now.Date)
            {
                _adding = false;
                await Application.Current.MainPage.DisplayAlert(AppResource.invailddate, AppResource.EndDate_Now, AppResource.oktext);
            }
            else if (ReminderEnabled && ReminderDate.ToDateTimeFromShortDate() > EndDate.ToDateTimeFromShortDate())
            {
                _adding = false;
                await Application.Current.MainPage.DisplayAlert(AppResource.invailddate, AppResource.ReminderDate_End, AppResource.oktext);
            }
            //No Errors
            else
            {
                task.Title = TaskName;
                task.StartDate = StartDate_DT;
                task.EndDate = EndDate_DT;
                task.PriorityId = priorityId;
                task.ResponsibleID = Lastemployee.Value2;
                task.SourceId = sourceId;
                task.Description = Description;
                task.ReminderEnabled = ReminderEnabled;
                task.ReminderDate = ReminderEnabled ? ReminderDate_DT : (DateTime?)null;
                task.AssignorUserGroupId = !String.IsNullOrEmpty(UserGroupID) ? Guid.Parse(UserGroupID) : (Guid?)null;
                task.AssignorEntityUserId = !String.IsNullOrEmpty(PositionID) ? Guid.Parse(PositionID) : (Guid?)null;
                var api = RestService.For<ITaskyApi>(new HttpClient(new HttpLoggingHandler()) { BaseAddress = new Uri(Settings.ApiUrl) });
                try
                {

                    isBusy = true;
                    var res = await api.UpdateTask("Bearer " + Settings.AccessToken, task);

                    if (res.Success)
                    {
                        await Application.Current.MainPage.DisplayAlert(AppResource.edittasktitle, AppResource.edittasksuccesstext, AppResource.oktext);
                        await navService.NavigateBackAsync();
                        MessagingCenter.Send(this, "TaskEdited");
                    }
                    else
                    {

                        await Application.Current.MainPage.DisplayAlert(AppResource.failuretext, AppResource.edittaskfailuretext, AppResource.oktext);
                    }
                    isBusy = false;
                    _adding = false;
                }
                catch (Exception exception)
                {
                    var properties = new Dictionary<string, string>
                       {
                             { "EditTaskViewModel", "Addatask" },
                       };
                    Crashes.TrackError(exception, properties);
                }
                finally
                { isBusy = false; _adding = false; }
            }
        }
        private bool FileSizeExceededTheLimit()
        {
            try
            {
                int _size = FilesList.Sum(f => f.FileLenth) / (1024 * 1024);
                return _size > AppConstants.MaxFileSize;
            }
            catch
            {
                return false;
            }
        }
        public void OnemployeenameChanged()
        {
            // StackTrace stackTrace = new 
            listVisiblty = true;
            if (string.IsNullOrEmpty(employeename) || string.IsNullOrWhiteSpace(employeename))
            {
                MyList = TempemployeeDemoDataList;
                listVisiblty = false;
                //var asignee = MyList.FirstOrDefault(x => x.id == ResponsibleID);
                //if (asignee != null)
                //{
                //    asignee.IsCheckedemployee = false;
                //}
                ResponsibleID = "";
            }
            else
            {
                if (MyList != null && MyList.Count > 0)
                {
                    MyList = TempemployeeDemoDataList;
                    List<ResponsiblesDDL> result = MyList.Where(x => !string.IsNullOrEmpty(x.Text) && x.Text.ToLower().Contains(employeename.ToLower())).ToList();
                    if (result != null)
                    {
                        try
                        {
                            MyList = new ObservableCollection<ResponsiblesDDL>(result);
                            var task_ = MyList.FirstOrDefault(x => x.Value2.ID == ResponsibleID);
                            if (task_ != null)
                            {
                                task_.IsCheckedemployee = true;
                            }
                        }
                        catch (Exception exception)
                        {
                            isBusy = false;

                            var properties = new Dictionary<string, string>
                       {
                             { "EditTaskViewModel", "onemployeenamechanged" },
                       };
                            Crashes.TrackError(exception, properties);
                        }

                    }

                }
            }
        }
        private async void uploadFileCommandExcute(object obj)
        {
            try
            {
                isBusy = true;
                bool premissionGranted = await PremissionGranted();
                if (!premissionGranted)
                {
                    isBusy = false;
                    return;
                }

                var file = await CrossFilePicker.Current.PickFile();
                if (file == null)
                {
                    FileName = AppResource.nofiletext;
                    isBusy = false;
                    return;
                }
                if (file != null)
                {
                    // chek file extiontion 
                    var fileExtension = Path.GetExtension(file.FileName);
                    if (Utility.NotSupportedExtension(fileExtension))
                    {
                        await Application.Current.MainPage.DisplayAlert(AppResource.alerttext, AppResource.notSupportedFile, AppResource.oktext);
                        isBusy = false;
                        return;
                    }

                    FileName = file.FileName;
                    _File = file.DataArray;
                    int filesize = file.DataArray.Length;
                    var filelength = filesize / 1024;

                    FileSize = Convert.ToString(filelength) + "KB";
                    if (filelength >= 1024)
                    {
                        filelength = filelength / 1024;
                        FileSize = Convert.ToString(filelength) + "MB";
                    }


                    if (fileExtension.Equals(".png") || fileExtension.Equals(".jpg") || fileExtension.Equals(".jpeg"))
                    {
                        ImageType = "picture";
                    }
                    else
                    {
                        ImageType = "pdf";
                    }
                    await CrossFilePicker.Current.SaveFile(file);
                    var files = new List<StreamPart>();
                    files.Add(new StreamPart(new MemoryStream(_File), FileName));
                    var api = RestService.For<ITaskyApi>(new HttpClient(new HttpLoggingHandler()) { BaseAddress = new Uri(Settings.ApiUrl) });
                    var res = await api.UploadTaskAttachment("Bearer " + Settings.AccessToken, Settings.TaskId.ToString(), false, files);

                    FilelistVisbility = true;
                    
                    Guid ID = Guid.Parse(res.Data);
                    FilesList.Add(new FileDataModel { FileName = FileName, FileTime = DateTime.Now.ToShortDateStringForView(), image = ImageType, FileSize = FileSize, AttachmentId = ID });
                    if (FilesList.Count > 2)
                    {
                        ishowmore = true;
                    }
                    else
                    {
                        ishowmore = false;
                    }
                }
                isBusy = false;
            }
            catch (Exception exception)
            {
                isBusy = false;
                var properties = new Dictionary<string, string>
                       {
                             { "TaskDetails", "uploadFileCommandExcute" },
                       };
                Crashes.TrackError(exception, properties);
            }
        }
        private async Task<bool> PremissionGranted()
        {
            var status = await Permissions.CheckStatusAsync<Permissions.StorageRead>();
            if (status != PermissionStatus.Granted)
            {
                status = await Permissions.RequestAsync<Permissions.StorageRead>();
                status = await Permissions.RequestAsync<Permissions.StorageWrite>();
            }

            return status == PermissionStatus.Granted;
        }
        private void selectpriorityCommandExcute(object obj)
        {
            if (obj.ToString() == "1")
            {
                arrowprioritycheck = !arrowprioritycheck;
            }
            prioritylist = !prioritylist;
        }
        private void selectsourceCommandExcute(object obj)
        {
            if (obj.ToString() == "1")
            {
                arrowsourcecheck = !arrowsourcecheck;
            }
            sourcelist = !sourcelist;
        }
        private async void Initilaize()
        {
            try
            {
                await GetPriority();
                await GetTaskData();
                await GetESelectedEntityName();
            }
            catch (Exception exception)
            {
                var properties = new Dictionary<string, string>
                       {
                             { "EditTaskViewModel", "Initilaize" },
                       };
                Crashes.TrackError(exception, properties);
            }
        }
        public override void OnAppearing()
        {
            if (FilesList.Count == 0)
            {
                FilelistVisbility = false;
                ishowmore = false;
            }
            OnNavigatedTo(NavParameters);
            base.OnAppearing();
        }
        public async Task GetTaskData()
        {
            var api = RestService.For<ITaskyApi>(new HttpClient(new HttpLoggingHandler()) { BaseAddress = new Uri(Settings.ApiUrl) });
            try
            {
                isBusy = true;
                string language = IsRTL ? "sa" : "us";
                var result = await api.GetTaskByIdForEdit("Bearer " + Settings.AccessToken, Settings.TaskId, language);
                if (result.Success)
                {


                    isBusy = false;
                    task = new TaskDto();
                    //task = (result.Data).ToObject<TaskDto>();
                    task = JsonConvert.DeserializeObject<TaskDto>(Convert.ToString(result.Data));
                    TaskName = task.Title;
                    StartDate = task.StartDate.ToShortDateStringForView();
                    StartDate_DT = task.StartDate;
                    StartDateColor = Color.Black;

                    EndDate = task.EndDate.ToShortDateStringForView();
                    EndDate_DT = task.EndDate;
                    EndDateColor = Color.Black;

                    var _priority = PrioritiesListData.FirstOrDefault(x => x.id == task.PriorityId);
                    _priority.IsChecked = true;
                    priorityChecked = true;
                    priorityname = _priority.name;
                    priorityimage = _priority.image2;
                    prioritylist = false;
                    priorityId = _priority.id;

                    sourcename = task.SourceDisplayName;
                    sourceId = task.SourceId;

                    Description = task.Description;
                    if (task.ReminderEnabled != null)
                        ReminderEnabled = task.ReminderEnabled.Value;
                    else
                        ReminderEnabled = false;
                    if (ReminderEnabled)
                    {
                        ReminderDate = task.ReminderDate.Value.ToShortDateStringForView();
                        ReminderDate_DT = task.ReminderDate.Value;
                        ReminderDateColor = Color.Black;
                    }
                    if (!String.IsNullOrEmpty(task.AssignorUserGroupId.ToString()))
                    {
                        UserGroupName = task.AssignorUserGroup;
                        UserGroupID = task.AssignorUserGroupId.ToString();
                        GetAssignEmployeeByUserGroupID(UserGroupID);
                        AsUserGroup = true;
                        AsUser = false;
                    }
                    if (!String.IsNullOrEmpty(task.AssignorEntityUser))
                    {
                        PositionName = task.AssignorEntityUser;
                        PositionID = task.AssignorEntityUserId.ToString();
                        GetAssignEmployeeByPositionID(PositionID);
                        AsUser = true;
                        AsUserGroup = false;
                    }

                    Lastemployee = new ResponsiblesDDL()
                    {
                        Text = task.Assignee,
                        Value2 = task.ResponsibleID,
                        IsCheckedemployee = true,
                    };
                    ResponsibleID = Lastemployee.Value2.ID;
                    employeename = IsRTL? task.ArabicName : Lastemployee.Text;




                    //FilesList = new ObservableCollection<FileDataModel>();
                    //FilesList.Clear();

                    var path = DependencyService.Get<IFileHelper>().file(AppConstants.AppName);
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    string filepath;
                    foreach (var item in task.Attachment)
                    {
                        var checkextension = Path.GetExtension(item.Name);
                        if (checkextension.Equals(".png") || checkextension.Equals(".jpg") || checkextension.Equals(".jpeg"))
                        {
                            ImageTypeedit = "picture";
                        }
                        else
                        {
                            ImageTypeedit = "pdf";
                        }
                        filepath = Path.Combine(path, item.Name);
                        FilesList.Add(new FileDataModel { FileName = item.Name,FileTime=item.CreatedAt.ToShortDateStringForView(), filepath = filepath, image = ImageTypeedit, AttachmentId = item.Id });

                    }
                    if (FilesList.Count > 0)
                    {
                        FilelistVisbility = true;
                    }
                    if (FilesList.Count > 2)
                    {
                        ishowmore = true;
                    }
                    else
                    {
                        ishowmore = false;
                    }
                }


                listVisiblty = false;
                isBusy = false;
            }
            catch (Exception exception)
            {
                var properties = new Dictionary<string, string>
                       {
                             { "_EditTaskViewModel", "gettaskbyid" },
                       };
                Crashes.TrackError(exception, properties);
            }

        }

        public async Task GetAssignEmployeeEntitiyId(string[] IDs)
        {
            var api = RestService.For<ITaskyApi>(new HttpClient(new HttpLoggingHandler()) { BaseAddress = new Uri(Settings.ApiUrl) });
            try
            {
                MyList.Clear();
                var result = await api.GetPositionAssignOnChange("Bearer " + Settings.AccessToken, IDs);
                List<ResponsiblesDDL> x = new List<ResponsiblesDDL>();
                //x = (result.Data).ToObject<List<ResponsiblesDDL>>();
                x = JsonConvert.DeserializeObject<List<ResponsiblesDDL>>(Convert.ToString(result.Data));
                foreach (var item in x)
                {
                    string txt = IsRTL ? item.ArabicText : item.Text;
                    MyList.Add(new ResponsiblesDDL { Text = txt, Value2 = item.Value2 });
                }
                TempemployeeDemoDataList = new ObservableCollection<ResponsiblesDDL>(MyList.Select(y => new ResponsiblesDDL
                {
                    IsCheckedemployee = y.IsCheckedemployee,
                    Text = y.Text,
                    Value2 = y.Value2
                }));
            }
            catch (Exception exception)
            {
                var properties = new Dictionary<string, string>
                       {
                             { "NewTaskViewModel", "assignemployee" },
                       };
                Crashes.TrackError(exception, properties);
            }
        }

        private async Task GetESelectedEntityName()
        {
            string userId = Settings.UserId;

          //  List<Entity> lst = new List<Entity>();
            var api = RestService.For<ITaskyApi>(new HttpClient(new HttpLoggingHandler()) { BaseAddress = new Uri(Settings.ApiUrl) });
            try
            {
                var data_ = await api.GetEntityIdByUserId("Bearer " + Settings.AccessToken, userId);

                string  entityId = data_.Data;

                string positionId = !string.IsNullOrEmpty(PositionID) ? PositionID : UserGroupID;

                if (AsUser)
                {
                    var result = await api.GetPositionAssign("Bearer " + Settings.AccessToken, positionId);

                    SetSelectedEntity(result.Data.FirstOrDefault(), entityId);
                }
                else
                {
                    var result = await api.GetUserGroupAssignees("Bearer " + Settings.AccessToken, positionId, (int)Enums.Privilege.Reassign);

                    SetSelectedEntity(result.Data.FirstOrDefault(), entityId);
                }
                

               
            }
            catch (Exception exception)
            {
                var properties = new Dictionary<string, string>
                       {
                             { "filterpopviewmodel", "getsectordatalist" },
                       };
                Crashes.TrackError(exception, properties);
            }

            //return name;
        }

      
        void SetSelectedEntity(Entity entity, string entityId)
        {
            if (entity == null)
            {
                return;
            }
            if (entity.Value.ToString() == entityId)
            {
                SelectedEntity = entity;
            }
            if (entity.Childern == null)
            {
                return;
            }
            foreach (var item in entity.Childern)
            {
                SetSelectedEntity(item, entityId);
            }
        }
    }
}
