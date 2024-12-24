
using Microsoft.AppCenter.Crashes;
using Plugin.FilePicker;
using Prism.Navigation;
using Refit;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Maham.Bases;
using Maham.Constants;
using Maham.Enums;
using Maham.Extentions;
using Maham.Helpers;
using Maham.Models;
using Maham.Models.User;
using Maham.Resources;
using Maham.Service;
using Maham.Service.General;
using Maham.Service.Model.Request.Tasks;
using Maham.Setting;
using Maham.Views;
using Xamarin.Forms;
using Maham.Service.Model.Response.Tasks;
using Task = System.Threading.Tasks.Task;
using Newtonsoft.Json;
using Xamarin.Essentials;

namespace Maham.ViewModels
{
    public class NewTaskPageViewModel : BaseViewModel
    {
        private readonly INavService navService;
        public ICommand selectpriorityCommand { get; set; }
        public ICommand selectsourceCommand { get; set; }
        public ICommand UploadFileCommand { get; set; }
        public ICommand openassignemployeeCommand { get; set; }
        public ICommand OpenUserGroupPopupCommand { get; set; }
        public ICommand OpenPositionPopupCommand { get; set; }
        public ICommand OpenSourcePopupCommand { get; set; }
        public ICommand OpenDepartmentsPopupCommand { get; set; }
        public ICommand StopUploadCommand { get; set; }
        public ICommand AddCommand { get; set; }

        public ICommand ClearCommand { get; set; }
        public byte[] File { get; set; }
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
        public ICommand AsUserGroupCommand { get; set; }
        public ICommand AsUserCommand { get; set; }

        public bool AsUser { get; set; }
        public bool AsUserGroup { get; set; }
        public bool CanShoseUserGroup { get; set; }
        //
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
        public ObservableCollection<ResponsiblesDDL> FilteredMyList { get; set; }
        
        private ObservableCollection<FileDataModel> filesList;
        public ObservableCollection<FileDataModel> FilesList 
        {
            get { return filesList; }
            set { filesList = value; RaisePropertyChanged(); }
        }

        public ObservableCollection<AttachmentModel> UploadFile { get; set; }
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
        public bool issource { get; set; }
        public ICommand OnSelectStarDateCommand { get; set; }
        public ICommand OnSelectEndDateCommand { get; set; }
        public ICommand OnSelectReminderDateCommand { get; set; }
        public UserGroupListModel selectUserGroup { get; set; }
        public PositionListModel selectPosition { get; set; }

        public ICommand DelteFileCommand { get; set; }
        public ICommand backnavigation { get; set; }
        public DateTime? MaxDate { get; set; }
        public bool isBusy { get; set; }
        public ICommand ShowmoreCommand { get; set; }

        private string _dashs = "— — — — — — ";

        private Entity selectedEntity;
        public Entity SelectedEntity
        {
            get { return selectedEntity; }
            set { selectedEntity = value; RaisePropertyChanged(); }
        }

        public NewTaskPageViewModel(INavService _navService, INavigationService _NavigationServices) : base(_NavigationServices)
        {
            MyList = new ObservableCollection<ResponsiblesDDL>();
            FilteredMyList = new ObservableCollection<ResponsiblesDDL>();
            isvisableupload = true;
            isvisableuploadview1 = false;
            FilelistVisbility = false;
            isClicked = false;
            arrowprioritycheck = false;
            arrowsourcecheck = false;
            ReminderEnabled = false;

            HasUserGroups = Settings.HasUserGroups;
            HasPositions = true;

            UserGroup = Settings.UserGroup;
            Position = Settings.Position;

            AsUserGroup = (!string.IsNullOrEmpty(UserGroup));
            AsUser = !AsUserGroup;

            UserGroupVisiblty = !string.IsNullOrEmpty(UserGroup);
            CanShoseUserGroup = UserGroupVisiblty;

            PositionVisiblty = (HasPositions || !string.IsNullOrEmpty(Position)) && !UserGroupVisiblty;

            if (!string.IsNullOrEmpty(UserGroup) && !HasUserGroups && string.IsNullOrEmpty(Position))
            {
                UserGroupID = UserGroup;
                //GetAssignEmployeeByUserGroupID(UserGroupID);
            }
            else
            {
                UserGroupID = "";
                UserGroupName = AppResource.selectUserGroup;
            }
            if (!string.IsNullOrEmpty(Position) && !HasPositions && string.IsNullOrEmpty(UserGroup))
            {
                PositionID = Position;
                //GetAssignEmployeeByPositionID(PositionID);
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

            OnSelectStarDateCommand = new Command(ShowDatePicker);
            OnSelectEndDateCommand = new Command(ShowEndDatePicker);
            OnSelectReminderDateCommand = new Command(ShowReminderDatePicker);
            DelteFileCommand = new Command(DeleteFileCommandExcute);
            selectpriorityCommand = new Command(selectpriorityCommandExcute);
            selectsourceCommand = new Command(selectsourceCommandExcute);
            UploadFileCommand = new Command(uploadFileCommandExcute);
            StopUploadCommand = new Command(stopUploadCommandExcute);
            backnavigation = new Command(backnavigationExcute);
            AddCommand = new Command(AddCommandExcute);
            ClearCommand = new Command(ClearCommandExcute);
            OpenUserGroupPopupCommand = new Command(OpenUserGroupPopupCommandExcute);
            OpenPositionPopupCommand = new Command(OpenPositionPopupCommandExcute);
            OpenSourcePopupCommand = new Command(OpenSourcePopupCommandExcute);
            OpenDepartmentsPopupCommand = new Command(OpenDepartmentsPopupCommandExcute);
            FilesList = new ObservableCollection<FileDataModel>();
            sharedlist = new ObservableCollection<FileDataModel>();

            UploadFile = new ObservableCollection<AttachmentModel>();
            PrioritiesListData = new ObservableCollection<ListPopUpModel>();

            UserGroupListData = new ObservableCollection<ListPopUpModel>();
            PositionListData = new ObservableCollection<ListPopUpModel>();
            openassignemployeeCommand = new Command(openassignemployeeCommandExcute);
            CheckPriorityCommand = new Command(CheckPriorityCommandExcute);
            chooseEmployeeCommand = new Command(ChooseEmployeeCommandExcute);
            ShowmoreCommand = new Command(ShowmoreCommandExcute);

            IsRTL = Settings.IsRtl;

            AsUserGroupCommand = new Command(AsUserGroupCommandExcute);
            AsUserCommand = new Command(AsUserCommandExcute);

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
                //GetAssignEmployeeByUserGroupID(UserGroupID);

                PositionName = AppResource.selectPosition;
                PositionID = "";

                SelectedEntity = new Entity();// { Text = _dashs };

            });
            MessagingCenter.Subscribe<ListModel>(this, "selectPosition", (selectPosition) =>
            {
                PositionName = selectPosition.name;
                PositionID = selectPosition.id;
                //GetAssignEmployeeByPositionID(PositionID);

                UserGroupName = AppResource.selectUserGroup;
                UserGroupID = "";

                SelectedEntity = new Entity();// { Text = _dashs };

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
            string userId = AsUser ? PositionID : UserGroupID;
            PopupNavigation.Instance.PushAsync(new DepartmentsView(AsUser, userId, false));
        }


        private void AsUserGroupCommandExcute(object obj)
        {
            AsUserGroup = true;
            AsUser = false;
            UserGroupVisiblty = true;
            PositionVisiblty= false;
            PositionName = AppResource.selectPosition;
            PositionID = "";
        }

        private void AsUserCommandExcute(object obj)
        {
            AsUser = true;
            AsUserGroup = false;
            UserGroupVisiblty = false;
            PositionVisiblty= true;
            UserGroupName = AppResource.selectUserGroup;
            UserGroupID = "";
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
        private void DeleteFileCommandExcute(object obj)
        {
            try
            {
                var item = (FileDataModel)obj;
                int index_ = FilesList.IndexOf(item);
                FilesList.Remove(item);

                if (FilesList.Count == 0)
                {
                    FilelistVisbility = false;
                }

                UploadFile.RemoveAt(index_);
            }
            catch (Exception exception)
            {
                var properties = new Dictionary<string, string>
                       {
                             { "NewTaskViewModel", "DeleteFileCommandExcute" },
                       };
                Crashes.TrackError(exception, properties);
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
        public async Task GetAssignEmployeeEntitiyId(string[] IDs)
        {
            var api = RestService.For<ITaskyApi>(new System.Net.Http.HttpClient(new HttpLoggingHandler()) { BaseAddress = new Uri(Settings.ApiUrl) });
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
                TempemployeeDemoDataList = new ObservableCollection<ResponsiblesDDL>(MyList.Select(y=>new ResponsiblesDDL {
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


        public async Task GetPriority()
        {
            var api = RestService.For<ITaskyApi>(new System.Net.Http.HttpClient(new HttpLoggingHandler()) { BaseAddress = new Uri(Settings.ApiUrl) });
            try
            {


                var result = await api.GetPriority("Bearer " + Settings.AccessToken);
                List<ListPopUpModel> x = new List<ListPopUpModel>();
                //x = (result.Data).ToObject<List<ListPopUpModel>>();
                x = JsonConvert.DeserializeObject<List<ListPopUpModel>>(Convert.ToString(result.Data));
                foreach (var item in x)
                {
                    PrioritiesListData.Add(new ListPopUpModel { id = item.id, name = IsRTL ? item.nameAr : item.name, image2 = item.image2 });
                }
            }
            catch (Exception exception)
            {
                var properties = new Dictionary<string, string>
                       {
                             { "NewTaskViewModel", "getpriorities" },
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
            PopupNavigation.Instance.PushAsync(new UserGroupPopUpPage());
        }
        private void OpenPositionPopupCommandExcute(object obj)
        {
            Settings.GeneralId_string = PositionID;
            PopupNavigation.Instance.PushAsync(new PositionPopUpPage());
        }
        private void OpenSourcePopupCommandExcute(object obj)
        {
            Settings.GeneralId = sourceId;

            if (AsUserGroup && !string.IsNullOrEmpty(UserGroupID))
            {
                PopupNavigation.Instance.PushAsync(new SourcePopUpPage(UserGroupID, (int)Enums.Privilege.Assign));
            }
            else if (AsUser && !string.IsNullOrEmpty(PositionID))
            {
                PopupNavigation.Instance.PushAsync(new SourcePopUpPage(null, (int)Enums.Privilege.Assign));
            }
            
        }
        private void ClearCommandExcute(object obj)
        {
            if (!string.IsNullOrEmpty(UserGroup) && !HasUserGroups && string.IsNullOrEmpty(Position))
            {
                UserGroupID = UserGroup;
            }
            else
            {
                UserGroupID = "";
                UserGroupName = AppResource.selectUserGroup;
            }
            if (!string.IsNullOrEmpty(Position) && !HasPositions && string.IsNullOrEmpty(UserGroup))
            {
                PositionID = Position;
            }
            else
            {
                PositionID = "";
                PositionName = AppResource.selectPosition;
            }

            TaskName = "";
            UserGroupName = "";
            Description = "";
            employeename = "";
            ResponsibleID = "";
            sourcename = "";
            sourcename = AppResource.selectSource;
            sourceId = 0;
            FileName = AppResource.filenametext;
            priorityname = AppResource.selectpriority;
            priorityChecked = false;
            StartDate = "— — — — — — ";
            EndDate = "— — — — — — ";
            ReminderDate = "— — — — — — ";
            Lastemployee = null;
            var _Priorities = PrioritiesListData.Where(x => x.IsChecked == true);
            foreach (var item in _Priorities)
            {
                item.IsChecked = false;
            }

            FilesList.Clear();
            listVisiblty = false;
            ReminderEnabled = false;
            UserGroupListData.Clear();
            sirencheck = "siren";
        }
        bool _adding = false;
        private async void AddCommandExcute(object obj)
        {
            if (_adding)
            {
                return;
            }
            _adding = true;

            if (UserGroupVisiblty && PositionVisiblty && UserGroupID == "" && PositionID == "")
            {
                _adding = false;
                await Application.Current.MainPage.DisplayAlert(AppResource.alerttext, AppResource.UserGroup_Position_Validation, AppResource.oktext);
            }
            else if (PositionVisiblty && UserGroupID == "" && PositionID == "")
            {
                _adding = false;
                await Application.Current.MainPage.DisplayAlert(AppResource.alerttext, AppResource.Position_Validation, AppResource.oktext);
            }
            else if (UserGroupVisiblty && UserGroupID == "" && PositionID == "")
            {
                _adding = false;
                await Application.Current.MainPage.DisplayAlert(AppResource.alerttext, AppResource.UserGroup_Validation, AppResource.oktext);
            }
            else if (String.IsNullOrEmpty(TaskName) || String.IsNullOrWhiteSpace(TaskName) || sourceId == 0
                || String.IsNullOrEmpty(StartDate) || StartDate.Equals(_dashs) || priorityId == 0
                  || String.IsNullOrEmpty(EndDate) || EndDate.Equals(_dashs) || String.IsNullOrEmpty(ResponsibleID))
            {
                await Application.Current.MainPage.DisplayAlert(AppResource.alerttext, AppResource.alertfilledalldata, AppResource.oktext);
                _adding = false;
            }
            else if (StartDate.ToDateTimeFromShortDate() < DateTime.Now.Date)
            {
                _adding = false;
                await Application.Current.MainPage.DisplayAlert(AppResource.invailddate, AppResource.StartDate_Now, AppResource.oktext);
            }

            else if (ReminderEnabled && ReminderDate.ToDateTimeFromShortDate() < StartDate.ToDateTimeFromShortDate())
            {
                _adding = false;
                await Application.Current.MainPage.DisplayAlert(AppResource.invailddate, AppResource.ReminderDate_Start, AppResource.oktext);
            }
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
                var api = RestService.For<ITaskyApi>(new System.Net.Http.HttpClient(new HttpLoggingHandler()) { BaseAddress = new Uri(Settings.ApiUrl) });
                try
                {
                    var newTaskModel = new TaskDto();
                    newTaskModel.Title = TaskName;
                    newTaskModel.StartDate = StartDate_DT;
                    newTaskModel.EndDate = EndDate_DT;
                    newTaskModel.PriorityId = priorityId;
                    newTaskModel.ResponsibleID = new List<Service.Model.Response.Value2>(){Lastemployee.Value2};
                    newTaskModel.SourceId = sourceId;
                    newTaskModel.Description = Description;
                    newTaskModel.ReminderEnabled = ReminderEnabled;
                    newTaskModel.ReminderDate = ReminderEnabled ? ReminderDate_DT : (DateTime?)null;
                    newTaskModel.AssignorUserGroupId = !String.IsNullOrEmpty(UserGroupID) ? Guid.Parse(UserGroupID) : (Guid?)null;
                    newTaskModel.AssignorEntityUserId = !String.IsNullOrEmpty(PositionID) ? Guid.Parse(PositionID) : (Guid?)null;

                    isBusy = true;
                    var res = await api.newtask("Bearer " + Settings.AccessToken, newTaskModel);

                    if (res.Success)
                    {
                        if (UploadFile.Count > 0)
                        {
                            var files = new List<StreamPart>();
                            foreach (var item in UploadFile)
                            {
                                files.Add(new StreamPart(new MemoryStream(item.content), item.title));
                            }

                            TaskDto x = new TaskDto();
                            //x = (res.Data).ToObject<TaskDto>();
                            x = JsonConvert.DeserializeObject<TaskDto>(Convert.ToString(res.Data));
                            res = await api.UploadTaskAttachment("Bearer " + Settings.AccessToken, x.Id.ToString(), false, files);

                            if (res.Success)
                            {
                                await Application.Current.MainPage.DisplayAlert(AppResource.addtextalerttitle, AppResource.taskaddedtext, AppResource.oktext);

                                // await navService.NavigateToAsync<MainTabbedPageViewModel>();
                                await navService.NavigateBackAsync();
                                MessagingCenter.Send(this, "TaskAdded");
                            }
                            else
                            {
                                await Application.Current.MainPage.DisplayAlert(AppResource.failuretext, AppResource.tasknotadded, AppResource.oktext);
                            }
                        }
                        else
                        {
                            await Application.Current.MainPage.DisplayAlert(AppResource.addtextalerttitle, AppResource.taskaddedtext, AppResource.oktext);
                            await navService.NavigateBackAsync();
                            MessagingCenter.Send(this, "TaskAdded");
                        }
                    }
                    else if (res.Code == "not_unique_Task")
                    {
                        await Application.Current.MainPage.DisplayAlert(AppResource.failuretext, AppResource.UniqueTaskName, AppResource.oktext);
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert(AppResource.failuretext, AppResource.tasknotadded, AppResource.oktext);
                    }
                    isBusy = false;
                    _adding = false;
                }
                catch (Exception exception)
                {
                    var properties = new Dictionary<string, string>
                       {
                             { "NewTaskViewModel", "Addatask" },
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
                int _size = UploadFile.Sum(f => f.FileSize) / (1024 * 1024);
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
                if (TempemployeeDemoDataList != null)
                {
                    MyList = new ObservableCollection<ResponsiblesDDL>(TempemployeeDemoDataList.Select(y => new ResponsiblesDDL
                    {
                        IsCheckedemployee = y.IsCheckedemployee,
                        Text = y.Text,
                        Value2 = y.Value2
                    }));
                }
               
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
                    List<ResponsiblesDDL> result = MyList.Where(x => !string.IsNullOrEmpty(x.Text) && x.Text.ToLower().Contains(employeename.ToLower())).ToList();
                    if (result != null)
                    {
                        try
                        {

                            FilteredMyList = new ObservableCollection<ResponsiblesDDL>(result.Select(y => new ResponsiblesDDL
                            {
                                IsCheckedemployee = y.IsCheckedemployee,
                                Text = y.Text,
                                Value2 = y.Value2
                            }));
                            var task_ = FilteredMyList.FirstOrDefault(x => x.Value2.ID == ResponsibleID);
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
                             { "newtaskpageviewmodel", "onemployeenamechanged" },
                       };
                            Crashes.TrackError(exception, properties);
                        }

                    }
                    else
                    {
                        FilteredMyList = new ObservableCollection<ResponsiblesDDL>();
                        listVisiblty = false;
                    }

                }
            }
        }
        private async void stopUploadCommandExcute(object obj)
        {
            try
            {
                bool premissionGranted = await PremissionGranted();
                if (!premissionGranted)
                {
                    return;
                }

                //uploadtext = AppResource.uploadagintext;
                //isvisableuploadview1 = true;
                //isvisableupload = false;

                //FilelistVisbility = true;

                var file = await CrossFilePicker.Current.PickFile();
                if (file == null)
                {
                    FileName = AppResource.nofiletext;
                    uploadtext = AppResource.uploadagintext;
                    return;
                }
                if (file != null)
                {
                    // chek file extiontion 
                    var fileExtension = Path.GetExtension(file.FileName);
                    if (Utility.NotSupportedExtension(fileExtension))
                    {
                        await Application.Current.MainPage.DisplayAlert(AppResource.alerttext, AppResource.notSupportedFile, AppResource.oktext);
                        return;
                    }

                    FileName = file.FileName;
                    uploadtext = AppResource.uploadagintext;
                    isvisableupload = false;
                    isvisableuploadview1 = true;
                    FilelistVisbility = true;
                    File = file.DataArray;
                    int filesize = file.DataArray.Length;
                    var filelength = filesize / 1024;

                    FileSize = Convert.ToString(filelength) + "KB";
                    showprogress = "100%";
                    progressvalue = 1.0;
                    if (filelength == 1024)
                    {
                        filelength = filelength / 1024;
                        FileSize = Convert.ToString(filelength) + "MB";
                    }

                    UploadFile.Add(new AttachmentModel { title = FileName, content = File, FileSize = filesize });


                    if (fileExtension.Equals(".png") || fileExtension.Equals(".jpg"))
                    {
                        ImageType = "picture";
                    }
                    else
                    {
                        ImageType = "pdf";
                    }

                    FilesList.Add(new FileDataModel { FileName = FileName, FileTime = DateTime.Now.ToShortDateStringForView(), image = ImageType, FileSize = FileSize });
                    if (FilesList.Count > 2)
                    {
                        ishowmore = true;
                    }
                    else
                    {
                        ishowmore = false;
                    }
                }
            }
            catch (Exception exception)
            {

                var properties = new Dictionary<string, string>
                       {
                             { "newtaskpageviewmodel", "stopUploadCommandExcute" },
                       };
                Crashes.TrackError(exception, properties);
            }

        }
        private async void uploadFileCommandExcute(object obj)
        {
            try
            {
                bool premissionGranted = await PremissionGranted();
                if (!premissionGranted)
                {
                    return;
                }

                //isvisableuploadview1 = true;
                //isvisableupload = false;
                //showprogress = "0%";
                //progressvalue = 0.0;


                var file = await CrossFilePicker.Current.PickFile();
                if (file == null)
                {
                    FileName = AppResource.nofiletext;
                    uploadtext = AppResource.uploadagintext;
                    return;
                }
                if (file != null)
                {
                    // chek file extiontion 
                    var fileExtension = Path.GetExtension(file.FileName);
                    if (Utility.NotSupportedExtension(fileExtension))
                    {
                        await Application.Current.MainPage.DisplayAlert(AppResource.alerttext, AppResource.notSupportedFile, AppResource.oktext);
                        return;
                    }

                    FileName = file.FileName;
                    uploadtext = AppResource.uploadagintext;
                    isvisableupload = false;
                    isvisableuploadview1 = true;
                    FilelistVisbility = true;
                    File = file.DataArray;
                    int filesize = file.DataArray.Length;
                    var filelength = filesize / 1024;

                    FileSize = Convert.ToString(filelength) + "KB";
                    showprogress = "100%";
                    progressvalue = 1.0;
                    if (filelength == 1024)
                    {
                        filelength = filelength / 1024;
                        FileSize = Convert.ToString(filelength) + "MB";
                    }

                    UploadFile.Add(new AttachmentModel { title = FileName, content = File, FileSize = filesize });


                    if (fileExtension.Equals(".png") || fileExtension.Equals(".jpg") || fileExtension.Equals(".jpeg"))
                    {
                        ImageType = "picture";
                    }
                    else
                    {
                        ImageType = "pdf";
                    }
                    await CrossFilePicker.Current.SaveFile(file);
                    FilesList.Add(new FileDataModel { FileName = FileName, FileTime = DateTime.Now.ToShortDateStringForView(), image = ImageType, FileSize = FileSize });
                    if (FilesList.Count > 2)
                    {
                        ishowmore = true;
                    }
                    else
                    {
                        ishowmore = false;
                    }
                }
            }
            catch (Exception exception)
            {
                isBusy = false;
                var properties = new Dictionary<string, string>
                       {
                             { "newtaskpageviewmodel", "uploadFileCommandExcute" },
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
            }
            catch (Exception exception)
            {
                var properties = new Dictionary<string, string>
                       {
                             { "NewTaskViewModel", "Initilaize" },
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
    }
}
