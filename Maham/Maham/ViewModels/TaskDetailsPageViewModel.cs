using Microsoft.AppCenter.Crashes;

using Plugin.FilePicker;
using Plugin.FilePicker.Abstractions;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Refit;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows.Input;
using Maham.Bases;
using Maham.Constants;
using Maham.Enums;
using Maham.Helpers;
using Maham.Models;
using Maham.Resources;
using Maham.Service;
using Maham.Service.Model.Request.Tasks;
using Maham.Service.Model.Response.Tasks;
using Maham.Setting;
using Maham.Views;

using Xamarin.Forms;
using Maham.Extentions;

using System.Threading.Tasks;
using Maham.Service.General;
using Task = System.Threading.Tasks.Task;
using System.ComponentModel;
using System.Text;
using System.Net;
using Maham.Service.Model.Response;
using Syncfusion.SfChart.XForms;
using Newtonsoft.Json;
using static SQLite.SQLite3;
using Xamarin.Essentials;

namespace Maham.ViewModels
{
    public class DownloadEventArgs : EventArgs
    {
        public bool FileSaved = false;
        public DownloadEventArgs(bool fileSaved)
        {
            FileSaved = fileSaved;
        }
    }
    public class TaskDetailsPageViewModel : BaseViewModel//BindableBase 
    {
        public string _dueDateReason;
        public string DueDateReason
        {
            get => _dueDateReason;
            set
            {
                _dueDateReason = value;
            }
        }

        public event EventHandler<DownloadEventArgs> OnFileDownloaded;

       // private readonly INavigationService _navigationService;
        private readonly INavService navService;
        #region properties
        public bool issourcelable { get; set; }
        public string arbicarrow { get; set; }
        public string commentimage { get; set; }
        public string personcommentimage { get; set; }
        public bool Refrencelablevisble { get; set; }
        public bool Projectlablevisible { get; set; }
        public string sourcetext { get; set; }

        public int CommentHeight { get; set; }

        public bool isBusy { get; set; }
       // public bool canclosed { get; set; }
        public bool CanReassign { get; set; }
        public bool canedit { get; set; }
        public bool canrequestchangeenddateactions { get; set; }
        public bool canrequestchangeenddate { get; set; }
        public bool canrequestchangeenddate_actions { get; set; }
        public bool canrequestemergencycall { get; set; }
        public bool CanDelete { get; set; }
        public bool CanAttachFile { get; set; }

        public bool CanRejectTask { get; set; }

        public bool IsClosedStatus { get; set; }

        //public bool canreturned { get; set; }
        public string Status { get; set; }
        public int assiginid { get; set; }
        public int creatorid { get; set; }
        public string ImageTypee { get; set; }
        public string FileSize { get; set; }
        public string Description { get; set; }
        public string tasktitle { get; set; }
        // public bool supporturgent { get; set; }
        public string alarmimage { get; set; }
        public string priorityname { get; set; }
        public string priorityimage { get; set; }
        public string startdate { get; set; }
        public string enddate { get; set; }
        public string assigneename { get; set; }
        public string assigneeimage { get; set; }
        public string assigneerole { get; set; }
        public string creatorname { get; set; }
        public string creatorimage { get; set; }
        public string creatorrole { get; set; }
        public string commenttext { get; set; }
        public bool EmergencyCallEnabled { get; set; }
        public string EmergencyCallname { get; set; }
        public Value3 EmergencyCall { get; set; }
       // public List<TaskDueDateRequestDto> DueDateslistData { get; set; }
        public string DueDate { get; set; }
        public DateTime DueDate_DT { get; set; }
        public Color DueDateColor { get; set; }
        public DateTime? MaxDate { get; set; }
        private string _dashs = "— — — — — — ";
        // public string Status { get; set; }

        public bool IsTaskOwner { get; set; }
        public bool IsTaskCreator { get; set; }
        #endregion
        #region Commands
        public ICommand downloadfilecommand { get; set; }
        public ICommand addcommentCommand { get; set; }
        public ICommand OpenReassignCommand { get; set; }
        public ICommand editCommand { get; set; }
        public ICommand deleteCommand { get; set; }
        public ICommand CloseTaskCommand { get; set; }
        public ICommand ReturnTaskCommand { get; set; }
        public ICommand CloseCommand { get; set; }
        public ICommand ChangeProgressCommand { get; set; }
        public ICommand RejectTaskCommand { get; set; }
        public ICommand OpenEmergencyCallPopupCommand { get; set; }
        public ICommand addEmergencyCallCommand { get; set; }
        public ICommand addChangeDateRequestCommand { get; set; }
        public ICommand ApproveCommand { get; set; }
        public ICommand RejectCommand { get; set; }
        public ICommand DueDateSelectedCommand { get; set; }
        public ICommand UploadFileCommand { get; set; }
        public ICommand UploadEvidenceCommand { get; set; }
        public ICommand ShowmoreCommand { get; set; }
        public ICommand DelteFileCommand { get; set; }
        public ObservableCollection<ListPopUpModel> PrioritiesListData
        {
            get;
            set;
        }

        #endregion
        #region list

        private ObservableCollection<TaskDueDateRequestDto> dueDateslistData;
        public ObservableCollection<TaskDueDateRequestDto> DueDateslistData
        {
            get {return dueDateslistData;}
            set { dueDateslistData = value; }
        }

        public ObservableCollection<FileDataModel> FilelistData { get; set; }
        ObservableCollection<CommentDto> _commentlist;
        public ObservableCollection<CommentDto> commentslist
        {
            get { return _commentlist; }


            set { SetProperty(ref _commentlist, value); }
        }

        #endregion

        #region Values
        private bool _supporturgent;
        private bool _supporturgentInitailized;

        public bool supporturgent
        {
            get { return _supporturgent; }
            set
            {
                _supporturgent = value;
                if (_supporturgent)
                {

                    alarmimage = "RedAlarm.png";
                }
                else
                {

                    alarmimage = "siren.png";
                }
                if (_supporturgentInitailized)
                {
                    PostSupportUrgent(_supporturgent);
                }
            }
        }

        public int PreviousTaskProgress { get; set; }
        private int _taskProgress;
        public int TaskProgress { get; set; }
        public int SeTaskProgress { get; set; }
        public int StatusId { get; set; }

        public string TaskProgressPercentageImage
        {
            get
            {
                return string.Format("{0} %", TaskProgress);
            }
        }

        public string CloseBtnImage
        {
            get
            {
                if (CanReturnTask)
                {
                    return "ic_highlight_off";
                }
                return "Closed";
            }
        }

        public string ReturnBtnImage
        {
            get
            {
                if (CanReturnTask)
                {
                    return "Returned";
                }
                return "return_disabled";
            }
        }
        public bool IsProgressEnabled { get; set; }
        public bool IsTaskDelayed { get; set; }
        public Color TaskProgressColor { get; set; }
        public ImageSource StatusImage { get; set; }
        public ImageSource ZImage { get; set; }
        public ImageSource TImage { get; set; }
        public ImageSource FImage { get; set; }
        public ImageSource SImage { get; set; }
        public ImageSource HImage { get; set; }
        public ImageSource PImage { get; set; }

        public bool ShoulPostprogressToServer { get; set; }

        public bool CanCloseTask { get; set; }
        public bool CanReturnTask { get; set; }

        public string TaskProgressImage
        {
            get { return string.Format("progress{0}", TaskProgress); }
        }

        public TaskDto task { get; set; }
        string path = DependencyService.Get<IFileHelper>().file(AppConstants.AppName);

        #endregion
        public TaskDetailsPageViewModel(INavigationService navigation, INavService _navService, INavigationService _NavigationServices) : base(_NavigationServices)
        {
            DueDate = _dashs;
            DueDate_DT = DateTime.Now;
            DueDateColor = Color.FromHex("#B7BCBD");
            //_navigationService = navigation;
            navService = _navService;
            #region initilize Commands
            DueDateSelectedCommand = new Command(DueDateSelected);
            downloadfilecommand = new Command(downloadfilecommandExcute);
            CloseTaskCommand = new Command(CloseTask);
            ReturnTaskCommand = new Command(ReturnTask);
            CloseCommand = new Command(Close);
            addcommentCommand = new Command(addcommentCommandExcute);
            editCommand = new Command(editCommandExcute);
            deleteCommand = new Command(DeleteCommandExcute);
            OpenReassignCommand = new Command(OpenReassignCommandExcute);
            ChangeProgressCommand = new Command(ChangeProgressCommandExcute);
            RejectTaskCommand = new Command(RejectTaskCommandExcute);
            OpenEmergencyCallPopupCommand = new Command(OpenEmergencyCallPopupCommandExcute);
            addEmergencyCallCommand = new Command(addEmergencyCallCommandExcute);
            addChangeDateRequestCommand = new Command(addChangeDateRequestCommandExcute);
            ApproveCommand = new Command(ApproveCommandExcute);
            RejectCommand = new Command(RejectCommandExcute);
            UploadFileCommand = new Command(uploadFileCommandExcute);
            UploadEvidenceCommand = new Command(uploadEvidenceCommandExcute);
            ShowmoreCommand = new Command(showmoreCommandExcute);
            DelteFileCommand = new Command(DeleteFilecommandExcute);
            PrioritiesListData = new ObservableCollection<ListPopUpModel>();
            ishowmore = false;
            FilelistDataVisible = false;
            #endregion

            IsRTL = Settings.IsRtl;

            #region values
            TaskProgress = 0;
            CommentHeight = 56;
            #endregion
            #region initilize list
            commentslist = new ObservableCollection<CommentDto>();
            #endregion

            MessagingCenter.Subscribe<EditTaskViewModel>(this, "TaskEdited", (sub) =>
            {
                GetTaskData();
            });
            MessagingCenter.Subscribe<EmergencyCallsDDL>(this, "selectEmergencyCall", (selectEmergencyCall) =>
            {
                EmergencyCallname = selectEmergencyCall.Text;
                EmergencyCall = selectEmergencyCall.Value3;
            });
            GetTaskData();
        }


        public override System.Threading.Tasks.Task InitializeAsync(object data)
        {
            if (data != null)
            {
                Settings.TaskId = data.ToString();
            }

            return base.InitializeAsync(data);
        }

        private async void RejectTaskCommandExcute(object obj)
        {
            try
            {
                bool reject = await App.Current.MainPage.DisplayAlert(AppResource.RejectTask, AppResource.RejectMsg, AppResource.Reject, AppResource.canceltext);

                if (reject)
                {
                    var api = RestService.For<ITaskyApi>(new System.Net.Http.HttpClient(new HttpLoggingHandler()) { BaseAddress = new Uri(Settings.ApiUrl) });
                    var rejected = await api.RejectTask("Bearer " + Settings.AccessToken, Settings.TaskId);
                    MessagingCenter.Send(this, "TaskClosedReturned");
                    await navService.NavigateBackAsync();
                }
            }
            catch (Exception exception)
            {
                var properties = new Dictionary<string, string>
                       {
                             { "DetailsTaskViewModel", "addEmergencyCallCommandExcute" },
                       };
                Crashes.TrackError(exception, properties);
            }
           
        }

        private void DueDateSelected(object result)
        {
            DueDate = DueDate_DT.ToShortDateStringForView();

            DueDateColor = Color.Black;

        }
        private void OpenEmergencyCallPopupCommandExcute(object obj)
        {
            Settings.EmergencyCall = EmergencyCall;
            PopupNavigation.Instance.PushAsync(new EmergencyCallPopUpPage());
        }
        private async void addEmergencyCallCommandExcute(object obj)
        {
            var api = RestService.For<ITaskyApi>(new System.Net.Http.HttpClient(new HttpLoggingHandler()) { BaseAddress = new Uri(Settings.ApiUrl) });
            try
            {
                isBusy = true;
                var result = new Service.Model.Response.Result(true);
                bool changed = false;
                if (EmergencyCallEnabled != task.NeedsErcall)
                { result = await api.UpdateTaskERCallStatus("Bearer " + Settings.AccessToken, task.Id, EmergencyCallEnabled);
                    changed = true;
                }
                if (EmergencyCallEnabled)
                {
                    if (!Equal(EmergencyCall, task.TaskErcallDto))
                    { result = await api.UpdateTaskERCallEntityManagerRole("Bearer " + Settings.AccessToken, task.Id, EmergencyCall);
                        changed = true;
                    }
                }
                else
                    EmergencyCallname = "";
                if(changed)
                await Application.Current.MainPage.DisplayAlert(AppResource.emergencycalltext, AppResource.RequestSuccess, AppResource.oktext);

                isBusy = false;
            }
            catch (Exception exception)
            {
                await Application.Current.MainPage.DisplayAlert(AppResource.failuretext, AppResource.RequestFail, AppResource.oktext);
                isBusy = false;

                var properties = new Dictionary<string, string>
                       {
                             { "DetailsTaskViewModel", "addEmergencyCallCommandExcute" },
                       };
                Crashes.TrackError(exception, properties);
            }
                
        }
        private async void addChangeDateRequestCommandExcute(object obj)
        {
            var api = RestService.For<ITaskyApi>(new System.Net.Http.HttpClient(new HttpLoggingHandler()) { BaseAddress = new Uri(Settings.ApiUrl) });
            try
            {
                isBusy = true;
                if (DueDate == _dashs)
                { await Application.Current.MainPage.DisplayAlert(AppResource.failuretext, AppResource.DueDateRequired, AppResource.oktext);
                    isBusy = false;
                    return;
                }
                else if (DueDate.ToDateTimeFromShortDate() < DateTime.Now.Date)
                {
                    await Application.Current.MainPage.DisplayAlert(AppResource.failuretext, AppResource.DueDate_Now, AppResource.oktext);
                    isBusy = false;
                    return;
                }
                else if (task.StartDate > DueDate.ToDateTimeFromShortDate())
                {
                    await Application.Current.MainPage.DisplayAlert(AppResource.failuretext, AppResource.DueDate_StartDate, AppResource.oktext);
                    isBusy = false;
                    return;
                }
                else if (DueDateReason==null || DueDateReason.Trim()=="")
                {
                    await Application.Current.MainPage.DisplayAlert(AppResource.failuretext, AppResource.Reasonreq, AppResource.oktext);
                    isBusy = false;
                    return;
                }

                var taskDueDateRequestDto = new TaskDueDateRequestDto();
                taskDueDateRequestDto.DueDate = DueDate_DT;
                taskDueDateRequestDto.TaskId = task.Id;
                taskDueDateRequestDto.Reason = DueDateReason;
                var result = await api.CreateDueDateRequest("Bearer " + Settings.AccessToken, taskDueDateRequestDto, task.Id);

                await Application.Current.MainPage.DisplayAlert(AppResource.RequestChangeDueDate, AppResource.RequestSuccess, AppResource.oktext);
                //await GetDueDateslistData();

                canrequestchangeenddate = false;

                isBusy = false;
            }
            catch (Exception exception)
            {
                await Application.Current.MainPage.DisplayAlert(AppResource.failuretext, AppResource.RequestFail, AppResource.oktext);
                isBusy = false;
                var properties = new Dictionary<string, string>
                       {
                             { "DetailsTaskViewModel", "addChangeDateRequestCommandExcute" },
                       };
                Crashes.TrackError(exception, properties);
            }

        }
        private async void ApproveCommandExcute(object obj)
        {
            isBusy = true;
            var dueDateRequest = (TaskDueDateRequestDto)obj;
            var api = RestService.For<ITaskyApi>(new System.Net.Http.HttpClient(new HttpLoggingHandler()) { BaseAddress = new Uri(Settings.ApiUrl) });
            try
            {
                var result =  await api.UpdateDueDateRequest("Bearer " + Settings.AccessToken, dueDateRequest.Id, (int)DueDateRequestStatus.Approved);
                await Application.Current.MainPage.DisplayAlert(AppResource.RequestChangeDueDate, AppResource.RequestSuccess, AppResource.oktext);

                //var DueDateslist = await GetDueDateslistData();
                //DueDateslistData = new ObservableCollection<TaskDueDateRequestDto>(DueDateslist);
                MessagingCenter.Send(this, "TaskClosedReturned");
                await GetTaskData();

                isBusy = false;
            }
            catch (Exception exception)
            {

                await Application.Current.MainPage.DisplayAlert(AppResource.failuretext, AppResource.RequestFail, AppResource.oktext);
                isBusy = false;
                var properties = new Dictionary<string, string>
                       {
                             { "DetailsTaskViewModel", "ApproveCommandExcute" },
                       };
                Crashes.TrackError(exception, properties);
            }

        }
        private async void RejectCommandExcute(object obj)
        {
            isBusy = true;
            var dueDateRequest = (TaskDueDateRequestDto)obj;
            var api = RestService.For<ITaskyApi>(new System.Net.Http.HttpClient(new HttpLoggingHandler()) { BaseAddress = new Uri(Settings.ApiUrl) });
            try
            {
                var result = await api.UpdateDueDateRequest("Bearer " + Settings.AccessToken, dueDateRequest.Id, (int)DueDateRequestStatus.Rejected);
                await Application.Current.MainPage.DisplayAlert(AppResource.RequestChangeDueDate, AppResource.RequestSuccess, AppResource.oktext);

                var DueDateslist = await GetDueDateslistData();
                DueDateslistData = new ObservableCollection<TaskDueDateRequestDto>(DueDateslist);

                isBusy = false;
            }
            catch (Exception exception)
            {

                await Application.Current.MainPage.DisplayAlert(AppResource.failuretext, AppResource.RequestFail, AppResource.oktext);
                isBusy = false;
                var properties = new Dictionary<string, string>
                       {
                             { "DetailsTaskViewModel", "RejectCommandExcute" },
                       };
                Crashes.TrackError(exception, properties);
            }

        }
        private bool Equal(Value3 first, Value3 second)
        {
            if(first==null && second == null)
                return true;
            else if (first != null && second == null)
                return false;
            else if (first == null && second != null)
                return false;
            else if (first.ID == second.ID && first.EntityID == second.EntityID && first.RoleID == second.RoleID)
                return true;
            return false;
        }
        private void ChangeProgressCommandExcute(object obj)
        {
            int _progress = TaskProgress;
           
            if (obj != null)
            {
                int.TryParse(obj.ToString(), out _progress);
            }
           
            UpdateTaskProgress(_progress);

            if (_progress == 100)
            {
                IsProgressEnabled = false;
            }
        }

        //public void OnTaskProgressChanged()
        //{
        //    UpdateTaskProgress(TaskProgress);
        //}

        private async void downloadfilecommandExcute(object obj)
        {
            try
            {
                if (!await PremissionGranted())
                {
                    return;
                }
                var file = (FileDataModel)obj;
                string filepath = Path.Combine(path, file.FileName);
                if (File.Exists(filepath))
                {
                    DependencyService.Get<IFileHelper>().FilePath(filepath);
                }
                else
                {
                    isBusy = true;
                    var api = RestService.For<ITaskyApi>(new System.Net.Http.HttpClient(new HttpLoggingHandler()) { BaseAddress = new Uri(Settings.ApiUrl) });
                    var _result = (await api.DownloadFile("Bearer " + Settings.AccessToken, file.AttachmentId.ToString())).Data;

                    if (!String.IsNullOrEmpty(_result))
                    {
                        File.WriteAllBytes(filepath, Convert.FromBase64String(Convert.ToString(_result)));
                        DependencyService.Get<IFileHelper>().FilePath(filepath);
                    }
                   
                    //WebClient webClient = new WebClient();
                    //webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(Completed);
                    //webClient.DownloadFileAsync(new Uri(Settings.ApiUrl + '/' + DownloadFile), filepath );
                    await Application.Current.MainPage.DisplayAlert("", AppResource.dowlnoadSucessfully, AppResource.oktext);
                    //DependencyService.Get<IFileHelper>().FilePath(filepath);
                    //byte[] bytes = Encoding.ASCII.GetBytes(DownloadFile);
                    //File.WriteAllText(filepath, DownloadFile);

                    isBusy = false;

                }

            }
            catch (Exception ee)
            {

                var properties = new Dictionary<string, string>
                                   {
                                         { "GetTaskData", "attachment" },
                                   };
                Crashes.TrackError(ee, properties);
            }


        }
        private void Completed(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                if (OnFileDownloaded != null)
                    OnFileDownloaded.Invoke(this, new DownloadEventArgs(false));
            }
            else
            {
                if (OnFileDownloaded != null)
                    OnFileDownloaded.Invoke(this, new DownloadEventArgs(true));
            }
        }
        private async Task<Attachment> GetAttchmentbyId(Guid attachmentId)
        {
            try
            {
                var api = RestService.For<ITaskyApi>(new System.Net.Http.HttpClient(new HttpLoggingHandler()) { BaseAddress = new Uri(Settings.ApiUrl) });

                var att = await api.GetTaskAttachment("Bearer " + Settings.AccessToken, attachmentId.ToString());
                return new Attachment();// att.Content;
            }
            catch (Exception ee)
            {

                var properties = new Dictionary<string, string>
                                   {
                                         { "GetAttchmentbyId", "attachment" },
                                   };
                Crashes.TrackError(ee, properties);
                return new Attachment();
            }
        }
        public void openfile(string filename)
        {


        }
        private void OpenReassignCommandExcute(object obj)
        {
            //_navigationService.NavigateAsync("MainTabbedPage?selectedTab=TasksPage/TaskDetailsPage/ReassignEmployeePage");
            // _navigationService.NavigateAsync("TaskDetailsPage/ReassignEmployeePage");
            navService.NavigateToAsync<ReassignEmployeePageViewModel>();
        }


        #region Commands actions

        public async void Close(object obj)
        {
            // await _navigationService.NavigateAsync("/NavigationPage/MainTabbedPage?selectedTab=TasksPage");
            // _navigationService.NavigateAsync("MainTabbedPage?selectedTab=NotPrioritiesPage");
            await navService.NavigateBackAsync();
            //bool bcak_ = await _navigationService.GoBackAsync();
            // if (!bcak_)
            // {
            //   await _navigationService.NavigateAsync("MainTabbedPage?selectedTab=TasksPage");
            // }
        }

        //prevnt oppening more than one view 
        private bool _editViewpOpeend;

        public string FileName { get; private set; }

        private byte[] _File;
        private ObservableCollection<FileDataModel> sharedlist;

        public bool FilelistDataVisible { get;  set; }

        public bool ishowmore { get; set; }

        private async void editCommandExcute(object obj)
        {
            if (_editViewpOpeend)
            {
                return;
            }
            _editViewpOpeend = true;
            //  await _navigationService.NavigateAsync("EditTask");
            await navService.NavigateToAsync<EditTaskViewModel>();
            ////await _navigationService.NavigateAsync("TaskDetailsPage/EditTask");
            ////await  _navigationService.NavigateAsync("MainTabbedPage?selectedTab=TasksPage/TaskDetailsPage/EditTask");
            _editViewpOpeend = false;
        }
        private void DeleteCommandExcute(object obj)
        {
            PopupNavigation.Instance.PushAsync(new DeletePopupPage());

        }
        private async void addcommentCommandExcute(object obj)
        {
            try
            {
                isBusy = true;
                var api = RestService.For<ITaskyApi>(new System.Net.Http.HttpClient(new HttpLoggingHandler()) { BaseAddress = new Uri(Settings.ApiUrl) });
                if (String.IsNullOrEmpty(commenttext) || string.IsNullOrWhiteSpace(commenttext))
                {
                    await Application.Current.MainPage.DisplayAlert(AppResource.failuretext, AppResource.CommenttextMessage, AppResource.oktext);
                    isBusy = false;
                }
                else
                {
                    var Comment = new CommentDto
                    {
                        Text = commenttext,

                    };
                    var result = await api.Addataskcomment("Bearer " + Settings.AccessToken,
                        Comment, Settings.TaskId);
                    if (result.Success)
                    {
                        commenttext = "";
                        commentslist.Add(Comment);

                        result = await api.GetTaskComments("Bearer " + Settings.AccessToken, Settings.TaskId);
                        if (result.Success)
                        {
                            ObservableCollection<CommentDto> commentDtos = new ObservableCollection<CommentDto>();
                            //commentDtos = (result.Data).ToObject<ObservableCollection<CommentDto>>();
                            commentDtos = JsonConvert.DeserializeObject<ObservableCollection<CommentDto>>(Convert.ToString(result.Data));
                            foreach (var item in commentDtos)
                            {
                                item.photoUrl = "user";
                                CommentHeight = SetCommentHeight(item.Text);
                                item.CommentHeight = CommentHeight;
                                item.backgroundimage = commentimage;
                            }

                            commentslist = commentDtos;
                        }
                        
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert(AppResource.failuretext, AppResource.addcommentfail, AppResource.oktext);
                    }
                }

            }
            catch (Exception exception)
            {
                var properties = new Dictionary<string, string>
                       {
                             { "taskdetailspage", "addtask" },
                       };
                Crashes.TrackError(exception, properties);
            }
            finally
            {
                isBusy = false;
            }
        }
        private async void ReturnTask(object obj)
        {
            try
            {//bool da hayge men api 
                // tp prevent to appear many time 
                if (CanReturnTask)
                {
                    var sucess = await Application.Current.MainPage.DisplayAlert("", AppResource.returnquestion + " " + task.Title + "?", AppResource.oktext, AppResource.canceltext);
                    if (sucess)
                    {

                        if (CanReturnTask)
                        {
                            isBusy = true;
                            CanCloseTask = false;
                            StatusId = (int)SatausEnum.Returned;
                            SwitchStatus();
                            var api = RestService.For<ITaskyApi>(new System.Net.Http.HttpClient(new HttpLoggingHandler()) { BaseAddress = new Uri(Settings.ApiUrl) });
                            //ShoulPostprogressToServer = true;

                            //  UpdateTaskProgress(false);
                            var result = await api.UpdateTaskReturned("Bearer " + Settings.AccessToken, Settings.TaskId);
                            if (result.Success)
                            {
                                isBusy = false;
                                CanCloseTask = false;
                                CanReturnTask = false;
                                MessagingCenter.Send(this, "TaskClosedReturned");
                                await navService.NavigateBackAsync();
                                //await _navigationService.NavigateAsync("/NavigationPage/MainTabbedPage?selectedTab=TasksPage");
                            }

                        }

                    }
                    else
                    {
                        return;
                    }

                }



            }
            catch (Exception exception)
            {
                ShoulPostprogressToServer = true;
                var properties = new Dictionary<string, string>
                       {
                             { "taskDetailsviewmodel", "ReturnTask" },
                       };
                Crashes.TrackError(exception, properties);
            }
        }

        private async void CloseTask(object obj)
        {
            try
            {

                if (CanCloseTask)
                {

                    var sucess = await Application.Current.MainPage.DisplayAlert("", AppResource.closequestion + " " + task.Title + "?", AppResource.oktext, AppResource.canceltext);
                    if (sucess)
                    {
                        CanReturnTask = false;
                        if (CanCloseTask)
                        {
                            isBusy = true;
                            StatusId = (int)SatausEnum.Closed;
                            SwitchStatus();
                            var api = RestService.For<ITaskyApi>(new System.Net.Http.HttpClient(new HttpLoggingHandler()) { BaseAddress = new Uri(Settings.ApiUrl) });
                            int.TryParse(Settings.TaskId, out int _taskId);
                            //ShoulPostprogressToServer = true;
                            // UpdateTaskProgress(false);
                            var result = await api.UpdateTaskClosed("Bearer " + Settings.AccessToken, Settings.TaskId);
                            if (result.Success)
                            {
                                IsProgressEnabled = false;
                                isBusy = false;
                                MessagingCenter.Send(this, "TaskClosedReturned");
                                await navService.NavigateBackAsync();
                                //await _navigationService.NavigateAsync("/NavigationPage/MainTabbedPage?selectedTab=TasksPage");
                            }
                            IsProgressEnabled = false;
                            CanCloseTask = false;
                            CanReturnTask = false;
                            CanDelete = Can("delete", "task") && StatusId != (int)SatausEnum.Completed && IsTaskCreator;

                        }


                    }
                    else
                    {
                        return;
                    }
                }


            }
            catch (Exception exception)
            {
                var properties = new Dictionary<string, string>
                       {
                             { "taskDetailsviewmodel", "CloseTask" },
                       };
                Crashes.TrackError(exception, properties);
            }
        }
        #endregion

        //public override System.Threading.Tasks.Task InitializeAsync(object data)
        //{
        //    ShoulPostprogressToServer = true;

        //    GetTaskData();
        //    return base.InitializeAsync(data);
        //}

        public override void OnAppearing()
        {
            base.OnAppearing();
            ShoulPostprogressToServer = true;


        }


        public async Task GetPriority()
        {
            var api = RestService.For<ITaskyApi>(new System.Net.Http.HttpClient(new HttpLoggingHandler()) { BaseAddress = new Uri(Settings.ApiUrl) });
            try
            {


                var result = await api.GetPriority("Bearer " + Settings.AccessToken);
                List<ListPopUpModel> x = new List<ListPopUpModel>();
                //  x = (result.Data).ToObject<List<ListPopUpModel>>();
                x = JsonConvert.DeserializeObject <List<ListPopUpModel>>(Convert.ToString(result.Data));
                foreach (var item in x)
                {
                    PrioritiesListData.Add(new ListPopUpModel { id = item.id, name = IsRTL? item.nameAr : item.name, image2 = item.image2 });
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
        private int initilaizedTaskProgress;
        public async Task GetTaskData()
        {

            //await PremissionGranted();
            var api = RestService.For<ITaskyApi>(new System.Net.Http.HttpClient(new HttpLoggingHandler()) { BaseAddress = new Uri(Settings.ApiUrl) });
            try
            {
                isBusy = true;
                string language = IsRTL ? "sa" : "us";
                var result = await api.GetTaskByIdForEdit("Bearer " + Settings.AccessToken, Settings.TaskId, language);
                //task = (result.Data).ToObject<TaskDto>();
                if (result.Success)
                {
                    isBusy = false;

                    task = new TaskDto();
                    task = Newtonsoft.Json.JsonConvert.DeserializeObject<TaskDto>(Convert.ToString(result.Data));
                    StatusId = task.StatusId;

                    CanReassign = false;

                    IsTaskOwner = task.ResponsibleID.ID.ToString() == Settings.UserId;
                    IsTaskCreator = task.CreatedBy.ToString() == Settings.UserId;//(task.CreatedBy == Guid.Parse(Settings.UserId))
                    IsClosedStatus = StatusId == (int)SatausEnum.Closed;
                    CanRejectTask = CanReject(task);

                    canrequestchangeenddateactions = Can("requestchangeenddateactions", "task") && StatusId != (int)SatausEnum.Completed && StatusId != (int)SatausEnum.Closed;
                    canrequestchangeenddate = Can("requestchangeenddate", "task") && StatusId != (int)SatausEnum.Completed && StatusId != (int)SatausEnum.Closed && task.CreatedBy.ToString() != Settings.UserId;
                    canrequestchangeenddate_actions = canrequestchangeenddateactions || canrequestchangeenddate;

                    canrequestemergencycall = Settings.IsEntityManager && StatusId != (int)SatausEnum.Completed && StatusId != (int)SatausEnum.Closed;// Can("requestemergencycall", "task");

                    canedit = CanEditTask();
                    //(Can("edit", "task") && StatusId != (int)SatausEnum.Completed && StatusId != (int)SatausEnum.Closed && !IsTaskOwner)|| IsTaskCreator;

                    CanDelete = Can("delete", "task") && StatusId != (int)SatausEnum.Completed && IsTaskCreator;
                    CanAttachFile = Can("attachfile", "task");

                    tasktitle = task.Title;
                    await GetPriority();
                    var _priority = PrioritiesListData.FirstOrDefault(x => x.id == task.PriorityId);
                    priorityname = _priority.name;
                    priorityimage = _priority.image2;

                    startdate = task.StartDate.ToShortDateStringForView();
                    enddate = task.EndDate.ToShortDateStringForView();
                    creatorimage = "user";
                    creatorname = IsRTL ? task.CreatorArabicName : task.Creator;
                    creatorrole = task.As;

                    assigneeimage = "user";
                    assigneename = IsRTL? task.ArabicName : task.Assignee;
                    assigneerole = task.AssigneeRoleName;

                    IsTaskDelayed = StatusId == (int)SatausEnum.Delayed;
                    SwitchStatus();
                    initilaizedTaskProgress = PreviousTaskProgress = task.Progress;
                    
                    TaskProgress = task.Progress;
                    IsProgressEnabled = StatusId != (int)SatausEnum.Completed && StatusId != (int)SatausEnum.Closed && Can("changeprogress", "task");

                    CanCloseTask = Can("close", "task") && StatusId == (int)SatausEnum.Completed;// && IsTaskCreator;
                    CanReturnTask = Can("return", "task") && StatusId == (int)SatausEnum.Completed;//&& IsTaskCreator;

                    sourcetext = task.SourceDisplayName;

                    Description = task.Description;

                    EmergencyCallEnabled = task.NeedsErcall;
                    EmergencyCall = task.TaskErcallDto;

                    var _EmergencyCalls = await api.GetEntitiesERCallList("Bearer " + Settings.AccessToken);
                    List<EmergencyCallsDDL> _x = new List<EmergencyCallsDDL>();
                    //_x = (_EmergencyCalls.Data).ToObject<List<EmergencyCallsDDL>>();
                    _x = JsonConvert.DeserializeObject <List<EmergencyCallsDDL>>(Convert.ToString(_EmergencyCalls.Data));
                    Settings.EmergencyCallList = _x;

                    var EC = _x.FirstOrDefault(x=>Equal(x.Value3, EmergencyCall));
                    if (EC != null)
                        EmergencyCallname = _x.FirstOrDefault(x => Equal(x.Value3, EmergencyCall)).Text;

                    if (IsTaskCreator)
                    {
                      var DueDateslist =  await GetDueDateslistData();
                       DueDateslistData = new ObservableCollection<TaskDueDateRequestDto>(DueDateslist);
                    }
                    else
                    {
                        //check if can request change end date
                        canrequestchangeenddate = await CanRequesChangeDueDate();
                    }

                    personcommentimage = "user";
                    commenttext = "";
                    #region file
                    try
                    {
                        FilelistData = new ObservableCollection<FileDataModel>();
                        FilelistData.Clear();

                        if (!Directory.Exists(path) && await PremissionGranted())
                        {
                            Directory.CreateDirectory(path);
                            //DependencyService.Get<IFileHelper>().CreateAppSpecificDirectory();
                        }

                        string filepath;
                        foreach (var item in task.Attachment)
                        {
                            if (item.IsEvidence != null && item.IsEvidence.Value)
                            {
                                ImageTypee = "search_icon";
                            }
                            else
                            {
                                ImageTypee = "attach_icon";
                            }
                            filepath = Path.Combine(path, item.Name);
                            FilelistData.Add(new FileDataModel { FileName = item.Name, filepath = filepath, FileTime = item.CreatedAt.ToShortDateStringForView(), image = ImageTypee, AttachmentId = item.Id });

                        }

                        if (FilelistData.Count == 0)
                        {
                            FilelistDataVisible = false;
                            ishowmore = false;
                        }
                        else
                        {
                            FilelistDataVisible = true;
                            if (FilelistData.Count > 2)
                            {
                                ishowmore = true;
                            }
                            else
                            {
                                ishowmore = false;
                            }
                        }

                    }
                    catch (Exception ee)
                    {

                        var properties = new Dictionary<string, string>
                                   {
                                         { "GetTaskData", "attachment" },
                                   };
                        Crashes.TrackError(ee, properties);
                    }


                    // var datapath = FileSystem.AppDataDirectory;
                    #endregion

                    #region check background for comment and arrow arbic
                    if (!IsRTL)
                    {
                        commentimage = "commentbackground";
                        arbicarrow = "blurArrow";


                    }
                    else
                    {
                        commentimage = "arabicchat";
                        arbicarrow = "arabicarrow";
                    }
                    #endregion

                    #region Comments
                    result = await api.GetTaskComments("Bearer " + Settings.AccessToken, Settings.TaskId);
                    if (result.Success)
                    {
                        ObservableCollection<CommentDto> commentDtos = new ObservableCollection<CommentDto>();
                        //commentDtos = (result.Data).ToObject<ObservableCollection<CommentDto>>();
                        commentDtos = JsonConvert.DeserializeObject <ObservableCollection<CommentDto>>(Convert.ToString(result.Data));
                        foreach (var item in commentDtos)
                        {
                            item.photoUrl = "user";
                            CommentHeight = SetCommentHeight(item.Text);
                            item.CommentHeight = CommentHeight;
                            item.backgroundimage = commentimage;
                        }

                        commentslist = commentDtos;
                    }
                    #endregion
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert(AppResource.failuretext, AppResource.loadTaskErorr, AppResource.oktext);
                }
                isBusy = false;

               // DependencyService.Get<IFileHelper>().GetStoragePermission();
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                    //DependencyService.Get<IFileHelper>().CreateAppSpecificDirectory();
                }
            }
            catch (Exception exception)
            {
                var properties = new Dictionary<string, string>
                       {
                             { "DetailsTaskViewModel", "gettaskbyid" },
                       };
                Crashes.TrackError(exception, properties);
            }

        }

        private bool CanEditTask()
        {
            try
            {
                if (StatusId == (int)SatausEnum.Completed || StatusId == (int)SatausEnum.Closed)
                {
                    return false;
                }

                return  Can("edit", "task") && IsTaskCreator;
            }
            catch
            {
                return false;
            }
        }

        private bool CanReject(TaskDto taskDto)
        {
            try
            {
                bool canRejectTask = IsTaskOwner && !IsTaskCreator && StatusId != (int)SatausEnum.Completed && StatusId != (int)SatausEnum.Closed;
              
                if (canRejectTask && taskDto.TaskAssignment != null && taskDto.TaskAssignment.Count > 0)
                {
                    var assigner = taskDto.TaskAssignment.First();

                    canRejectTask = assigner.CreatedAt.AddDays(2) >= DateTime.Now;
                }
              
                return canRejectTask;
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<TaskDueDateRequestDto>> GetDueDateslistData()
        {
            List<TaskDueDateRequestDto> DueDateslist = new List<TaskDueDateRequestDto>();

            var api = RestService.For<ITaskyApi>(new System.Net.Http.HttpClient(new HttpLoggingHandler()) { BaseAddress = new Uri(Settings.ApiUrl) });
            try
            {
                var _DueDates = await api.GetTaskDueDateRequests("Bearer " + Settings.AccessToken, 0, 5, task.Id);
                // DueDateslist = (_DueDates.Data).ToObject<List<TaskDueDateRequestDto>>();
                DueDateslist = JsonConvert.DeserializeObject <List<TaskDueDateRequestDto>>(Convert.ToString(_DueDates.Data));
                foreach (var item in DueDateslist)
                {
                    
                    if (item.StatusId == (int)DueDateRequestStatus.Approved)
                    {
                        item.StatusName = AppResource.Approved;
                        item.StatusColor = Color.Green;
                    }
                    else if (item.StatusId == (int)DueDateRequestStatus.Rejected)
                    {
                        item.StatusName = AppResource.Rejected;
                        item.StatusColor = Color.Red;
                    }
                    else if (item.StatusId == (int)DueDateRequestStatus.Pending)
                    {
                        item.StatusName = AppResource.Pending;
                        item.StatusColor = Color.Orange;
                        item.HasButton = true;
                    }
                    item.DueDateString = item.DueDate.ToShortDateStringForView();
                }
            }
            catch (Exception exception)
            {
                var properties = new Dictionary<string, string>
                       {
                             { "DetailsTaskViewModel", "GetDueDateslistData" },
                       };
                Crashes.TrackError(exception, properties);
            }
            return DueDateslist;
        }

        private async Task<bool> CanRequesChangeDueDate()
        {
            try
            {
                var lst = await GetDueDateslistData();

                return !lst.Any(r => r.StatusId == (int)DueDateRequestStatus.Pending);
            }
            catch
            {
                return true;
            }
        }

        private void ChangeStatusId(int _progress)
        {
            try
            {
                if (_progress == 100)
                {
                    StatusId = (int)SatausEnum.Completed;

                    CanCloseTask = Can("close", "task");// && IsTaskCreator; ;
                    CanReturnTask = Can("return", "task");// && IsTaskCreator; ;

                    IsProgressEnabled = false;
                }
                else if (_progress == 0)
                {
                    StatusId = (int)SatausEnum.NotStarted;
                }
                else
                {
                    StatusId = (int)SatausEnum.InProgress;
                }

                if (task != null)
                {
                    IsTaskDelayed = (task.EndDate.Date < DateTime.Now.Date && TaskProgress < 100) || (task.StartDate.Date < DateTime.Now.Date && TaskProgress == 0);
                }

                SwitchStatus();
            }
            catch (Exception exception)
            {
                var properties = new Dictionary<string, string>
                       {
                             { "taskDetailsviewmodel", "ChangeStatusId" },
                       };
                Crashes.TrackError(exception, properties);
            }

        }

        public override void OnDisappearing()
        {
            base.OnDisappearing();
            ShoulPostprogressToServer = true;
        }

        private int StatusIdImage;
        private void SwitchStatus()
        {
            switch (StatusId)
            {
                case (int)SatausEnum.Returned:
                    Status = AppResource.returnedTask;
                    StatusImage = "Returned.png";
                    TaskProgressColor = (Color)App.Current.Resources["BlueDark"];
                    break;

                case (int)SatausEnum.Closed:
                    Status = AppResource.Completed;
                    StatusImage = "Closed.png";
                    TaskProgressColor = (Color)App.Current.Resources["Greenlight1"];
                    break;

                case (int)SatausEnum.NotStarted:
                    Status = AppResource.notStartedTask;
                    StatusImage = "DidnotStart.png";
                    TaskProgressColor = (Color)App.Current.Resources["lightgrey5"];
                    break;

                case (int)SatausEnum.InProgress:
                    Status = AppResource.inProgressTask;
                    StatusImage = "InProgress.png";
                    TaskProgressColor = (Color)App.Current.Resources["lightblue3"];
                    break;

                case (int)SatausEnum.Completed:
                    Status = AppResource.completedTask;
                    StatusImage = "Completed.png";
                    TaskProgressColor = (Color)App.Current.Resources["Greenlight2"];

                    CanCloseTask = Can("close", "task");// && IsTaskCreator; ;
                    CanReturnTask = Can("return", "task");// && IsTaskCreator; ;

                   // MessagingCenter.Send(this, "TaskClosedReturned");
                    break;

                case (int)SatausEnum.Delayed:
                    if (TaskProgress == 0)
                    {
                        StatusImage = "DidnotStart.png";
                    }
                    else
                    {
                        StatusImage = "InProgress.png";
                    }
                    Status = AppResource.taskDelayed;
                    break;

                default:
                    Status = "";
                    StatusImage = "DidnotStart.png";
                    TaskProgressColor = (Color)App.Current.Resources["BlueDark"];
                    break;
            }


            StatusIdImage = StatusId;

            if (IsTaskDelayed && TaskProgress != 100)
            {
                TaskProgressColor = (Color)App.Current.Resources["redcolor"];
                StatusIdImage = (int)SatausEnum.Delayed;
            }
            else if (StatusIdImage == (int)SatausEnum.Closed || StatusIdImage == (int)SatausEnum.Returned)
            {
                StatusIdImage = (int)SatausEnum.Completed;
            }

            ZImage = $"z{StatusIdImage}.png";
            TImage = $"t{StatusIdImage}.png";
            FImage = $"f{StatusIdImage}.png";
            SImage = $"s{StatusIdImage}.png";
            HImage = $"h{StatusIdImage}.png";

        }

        private bool Can(string action, string module)
        {

            bool has = false;
            try
            {
                if (module == "task" && task != null)
                {
                    if ((task.AssignorUserGroupId != null && Settings.UserGroupList.Contains(task.AssignorUserGroupId.Value)) || IsTaskCreator)
                    {
                        has = Settings.creatorPrivilege.Contains(action);
                    }
                    if (!has && (task.ResponsibleID.Type == (int)SelectListItemType.User && Settings.UserId == task.ResponsibleID.ID) || (task.ResponsibleID.Type == (int)SelectListItemType.UserGroup && Settings.UserGroupList.Contains(Guid.Parse(task.ResponsibleID.ID))))
                    {
                        has = Settings.assigneePrivilege.Contains(action);
                    }
                }
                if (!has)
                    has = Settings.IsSuperAdmin || (Settings.Ability.Where(x => x.FkModule.Name.ToLower() == module
                                                                                && x.FkPrivilege.Name.ToLower() == action).Count() > 0 && IsTaskCreator);
            }
            catch (Exception e)
            {

                
            }
            return has;
        }

        private async void UpdateTaskProgress(int Value)
        {
            try
            {
                if (Value != initilaizedTaskProgress)
                {
                    if (ShoulPostprogressToServer)
                    {

                        ShoulPostprogressToServer = false;
                        isBusy = true;
                        var api = RestService.For<ITaskyApi>(new System.Net.Http.HttpClient(new HttpLoggingHandler()) { BaseAddress = new Uri(Settings.ApiUrl) });
                        var result = await api.UpdateTaskProgress("Bearer " + Settings.AccessToken, Settings.TaskId, Value);

                        if (!result.Success)
                        {
                            TaskProgress = PreviousTaskProgress;
                            IsProgressEnabled = true;
                            await Application.Current.MainPage.DisplayAlert(AppResource.failuretext, AppResource.EvedinceError, AppResource.oktext);
                        }
                        else
                        {
                            PreviousTaskProgress = TaskProgress;
                            TaskProgress = initilaizedTaskProgress = Value;
                            var ChangedTask = new TaskDto();
                            //ChangedTask = (result.Data).ToObject<TaskDto>();
                            ChangedTask = JsonConvert.DeserializeObject<TaskDto>(Convert.ToString(result.Data));
                            StatusId = ChangedTask.StatusId;
                            SwitchStatus();
                        }
                            

                        isBusy = false;
                        ShoulPostprogressToServer = true;
                        MessagingCenter.Send(this, "TaskClosedReturned");
                    }
                }

            }
            catch (Exception exception)
            {
                ShoulPostprogressToServer = true;
                var properties = new Dictionary<string, string>
                       {
                             { "taskDetailsviewmodel", "UpdateTaskProgress" },
                       };
                Crashes.TrackError(exception, properties);
            }
        }

        private async void PostSupportUrgent(bool support)
        {
            try
            {
                var api = RestService.For<ITaskyApi>(new System.Net.Http.HttpClient(new HttpLoggingHandler()) { BaseAddress = new Uri(Settings.ApiUrl) });
                int.TryParse(Settings.TaskId, out int _taskId);
                await api.UpdateTaskUrgent("Bearer " + Settings.AccessToken,
                   _taskId, support, int.Parse(Settings.UserId));
            }
            catch (Exception exception)
            {
                var properties = new Dictionary<string, string>
                       {
                             { "taskDetailsviewmodel", "PostSupporturgent" },
                       };
                Crashes.TrackError(exception, properties);
            }

        }

        private string TranslatePriorites(int statusId)

        {

            string status = "";

            try

            {

                Dictionary<int, string> valuePairs = new Dictionary<int, string>()

                {

                    {4,"حرج"},

                    {3,"مرتفع"},

                    {2,"متوسط"},

                    {1,"منخفض"}

                };


                if (valuePairs.ContainsKey(statusId))

                {

                    status = valuePairs[statusId];

                }

                return status;

            }

            catch

            {

                return status;

            }

        }

        private string TranslateStatus(int statusId)

        {

            string status = "";

            try

            {

                Dictionary<int, string> valuePairs = new Dictionary<int, string>()

                {

                    {4,"مكتملة"},

                    {3,"متأخرة"},

                    {12,"مرتجعة"},

                    {11,"مغلقة"},

                    { 2,"جارية"},

                    {1," لم تبدأ"}

                };


                if (valuePairs.ContainsKey(statusId))

                {

                    status = valuePairs[statusId];

                }

                return status;

            }

            catch

            {

                return status;

            }

        }

        private int SetCommentHeight(string word)
        {
            int maxWordLenghtInLine = 40;
            int height_ = 56;
            if (word == null)
            {
                return height_;
            }

            char back = '\n';

            int backCount = word.Count(c => c == back);

            if (word.Length > maxWordLenghtInLine)
            {
                height_ = ((word.Length / 40) + backCount) * 20;
            }
            else
            {
                height_ = (backCount + 2) * 22;
            }
           
            height_ = height_ < 56 ? 56 : height_;

            return height_;
        }
        private async Task<bool> PremissionGranted()
        {
            if (Device.RuntimePlatform == Device.Android && (int)DeviceInfo.Version.Major >= 29)
            {
                // Android 10 (API level 29) or greater
                return true;
            }
            var status = await Permissions.CheckStatusAsync<Permissions.StorageRead>();
            if (status != PermissionStatus.Granted)
            {
                status = await Permissions.RequestAsync<Permissions.StorageRead>();
                status = await Permissions.RequestAsync<Permissions.StorageWrite>();
               //var s = await CrossPermissions.Current.RequestPermissionAsync<StoragePermission>();
                //return s == Plugin.Permissions.Abstractions.PermissionStatus.Granted;
            }

            return status == PermissionStatus.Granted;
            //return true;
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

                    ImageTypee = "attach_icon";

                    await CrossFilePicker.Current.SaveFile(file);
                    var files = new List<StreamPart>();
                    files.Add(new StreamPart(new MemoryStream(_File), FileName));
                    var api = RestService.For<ITaskyApi>(new System.Net.Http.HttpClient(new HttpLoggingHandler()) { BaseAddress = new Uri(Settings.ApiUrl) });
                    var res = await api.UploadTaskAttachment("Bearer " + Settings.AccessToken, Settings.TaskId.ToString(), false, files);
                    Guid ID = Guid.Parse(res.Data);
                    FilelistData.Add(new FileDataModel { FileName = FileName, FileTime = DateTime.Now.ToShortDateStringForView(), image = ImageTypee, FileSize = FileSize, AttachmentId = ID });
                    if (FilelistData.Count == 0)
                    {
                        FilelistDataVisible = false;
                        ishowmore = false;
                    }
                    else
                    {
                        FilelistDataVisible = true;
                        if (FilelistData.Count > 2)
                        {
                            ishowmore = true;
                        }
                        else
                        {
                            ishowmore = false;
                        }
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
        private async void uploadEvidenceCommandExcute(object obj)
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


                    ImageTypee = "search_icon";

                    await CrossFilePicker.Current.SaveFile(file);
                    var files = new List<StreamPart>();
                    files.Add(new StreamPart(new MemoryStream(_File), FileName));
                    var api = RestService.For<ITaskyApi>(new System.Net.Http.HttpClient(new HttpLoggingHandler()) { BaseAddress = new Uri(Settings.ApiUrl) });
                    var res = await api.UploadTaskAttachment("Bearer " + Settings.AccessToken, Settings.TaskId.ToString(), true, files);
                    Guid ID = Guid.Parse(res.Data);
                    FilelistData.Add(new FileDataModel { FileName = FileName, FileTime = DateTime.Now.ToShortDateStringForView(), image = ImageTypee, FileSize = FileSize,AttachmentId= ID });
                    if (FilelistData.Count == 0)
                    {
                        FilelistDataVisible = false;
                        ishowmore = false;
                    }
                    else
                    {
                        FilelistDataVisible = true;
                        if (FilelistData.Count > 2)
                        {
                            ishowmore = true;
                        }
                        else
                        {
                            ishowmore = false;
                        }
                    }
                }
                isBusy = false;
            }
            catch (Exception exception)
            {
                isBusy = false;
                var properties = new Dictionary<string, string>
                       {
                             { "TaskDetails", "uploadEvidenceCommandExcute" },
                       };
                Crashes.TrackError(exception, properties);
            }
        }
        private void showmoreCommandExcute(object obj)
        {
            sharedlist = FilelistData;
            PopupNavigation.Instance.PushAsync(new popup(sharedlist));
        }
        private async void DeleteFilecommandExcute(object obj)
        {
            try
            {
                if (IsClosedStatus)
                {
                    return;
                }
                bool delete_ = await Application.Current.MainPage.DisplayAlert("", AppResource.DeleteMsg, AppResource.oktext, AppResource.canceltext);
                if (delete_)
                {
                    isBusy = true;
                    var item = (FileDataModel)obj;
                    int index_ = FilelistData.IndexOf(item);
                    FilelistData.RemoveAt(index_);

                    //refresh repeater view
                    FilelistData = new ObservableCollection<FileDataModel>(FilelistData);


                    if (FilelistData.Count == 0)
                    {
                        FilelistDataVisible = false;
                        ishowmore = false;
                    }
                    else
                    {
                        FilelistDataVisible = true;
                        if (FilelistData.Count > 2)
                        {
                            ishowmore = true;
                        }
                        else
                        {
                            ishowmore = false;
                        }
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
                             { "TaskDetails", "DeleteFilecommandExcute" },
                       };
                Crashes.TrackError(exception, properties);
            }

        }
        private async Task<bool> DeleteAttchment(Guid attachmentId)
        {
            try
            {
                var api = RestService.For<ITaskyApi>(new System.Net.Http.HttpClient(new HttpLoggingHandler()) { BaseAddress = new Uri(Settings.ApiUrl) });

                var result = await api.DeleteAttachment("Bearer " + Settings.AccessToken, attachmentId.ToString());

                return result.Success;
            }
            catch (Exception ee)
            {

                var properties = new Dictionary<string, string>
                                   {
                                         { "TaskDetails", "DeleteAttchment" },
                                   };
                Crashes.TrackError(ee, properties);
                return false;
            }
        }
    }
}
