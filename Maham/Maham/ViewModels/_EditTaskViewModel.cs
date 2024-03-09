//using Acr.UserDialogs;
//using Microsoft.AppCenter.Crashes;
//using Plugin.FilePicker;
//using Plugin.Multilingual;
//using Plugin.Permissions;
//using Plugin.Permissions.Abstractions;
//using Prism.Navigation;
//using Refit;
//using Rg.Plugins.Popup.Services;
//using System;
//using System.Collections.Generic;
//using System.Collections.ObjectModel;
//using System.IO;
//using System.Linq;
//using System.Threading.Tasks;
//using System.Windows.Input;
//using Maham.Bases;
//using Maham.Constants;
//using Maham.Enums;
//using Maham.Extentions;
//using Maham.Helpers;
//using Maham.Models;
//using Maham.Models.User;
//using Maham.Resources;
//using Maham.Service;
//using Maham.Service.Model.Request.Tasks;
//using Attachment = Maham.Service.Model.Response.Tasks.Attachment;
//using Maham.Setting;
//using Maham.Views;
//using Xamarin.Forms;
//using Maham.Service.General;
//using Maham.Service.Model.Response.Tasks;
//using Task = System.Threading.Tasks.Task;

//namespace Maham.ViewModels
//{
//    public class _EditTaskViewModel : BaseViewModel
//    {
//        private readonly INavService navService;
//        #region command
//        public ICommand DelteFileCommand { get; set; }
//        public ICommand downloadfilecommand { get; set; }
//        public ICommand ChooseProjectButtonCommand { get; set; }
//        public ICommand OpenProjectButtonCommand { get; set; }
//        public ICommand SearchemployeeButtonCommand { get; set; }
//        public ICommand NaviagationButtonCommand { get; set; }
//        public ICommand selectpriorityCommandEdit { get; set; }
//        public ICommand selectsectorCommandEdit { get; set; }
//        public ICommand selectDepartmentCommand { get; set; }
//        public ICommand opensectorListCommand { get; set; }
//        public ICommand openDepartmentListCommand { get; set; }
//        public ICommand UploadFileCommandEdit { get; set; }
//        public ICommand OpenRefrenceCommand { get; set; }
//        public ICommand StopUploadCommandEdit { get; set; }
//        public ICommand AddCommandEdit { get; set; }
//        public ICommand ClearCommandEdit { get; set; }
//        public ICommand DeleteCommand { get; set; }
//        public ICommand ChooseRefEditCommand { get; set; }
//        public ICommand SupportEmergentEditCommand { get; set; }
//        public ICommand OnSelectStarDateEditCommand { get; set; }
//        public ICommand OnSelectEndDateEditCommand { get; set; }
//        public ICommand CheckPriorityEditCommand { get; set; }
//        public ICommand chooseEmployeeEditCommand { get; set; }
//        public ICommand chooseRefrenceCommand { get; set; }
//        public ICommand deleteassignemployeeCommand { get; set; }
//        public ICommand deletefilecomannd { get; set; }
//        public ICommand EditTaskCommand { get; set; }
//        public ICommand ClearViewCommand { get; set; }

//        #endregion
//        #region object from class
//        public ProjectListModel lastproject { get; set; }
//        NewTaskModel newTaskModelEdit { get; set; }
//        public SectorModel selectedSector { get; set; }
//        public ResponsiblesDDL lastassignemployee { get; set; }
//        public DepartmentModel DepartmentModel { get; set; }
//        public UserGroupListModel lastrefselected { get; set; }
//        #endregion
//        #region proprties
//        public DateTime StartDateTime { get; set; }
//        public DateTime endDateTime { get; set; }
//        public string temppriorityname { get; set; }
//        public int? temprefid { get; set; }
//        public string temprefrencename { get; set; }
//        public string tempstartdate { get; set; }
//        public string tempenddate { get; set; }

//        public int? tempriorityid { get; set; }
//        public string tempprojectname { get; set; }
//        public int? tempprojectid { get; set; }
//        public string temptaskname { get; set; }
//        public string seletedemp { get; set; }
//        public int selectedid { get; set; }
//        public string selectempid { get; set; }
//        public int? projectId { get; set; }
//        public string ProjectName { get; set; }
//        public bool isporjectselect { get; set; }
//        public bool isprojectlistvibibilty { get; set; }
//        public bool refgridVisibility { get; set; }
//        public bool projectgridvisibilty { get; set; }
//        public int statusid { get; set; }
//        public bool isemployeegridclick { get; set; }
//        Service.Model.Response.Tasks.GetTaskByIdResponse taskdata { get; set; }

//        public bool CanReassignTask { get; set; }

//        private int priorityId;

//        public bool CanDeleteTask { get; set; }
//        public string sireneditcheck { get; set; }
//        public byte[] FileEdit { get; set; }
//        public string employeenameEdit { get; set; }
//        public string EmpImage { get; set; }
//        public string ResponsibleID { get; private set; }
//        public string prioritynameEdit { get; set; }
//        public string priorityimageedit { get; set; }
//        public int priorityIdedit { get; set; }
//        public string TaskNameEdit { get; set; }
//        public string FileNameEdit { get; set; }
//        public string FileSizeEdit { get; set; }
//        public bool FilelistVisbility2 { get; set; }
//        public string DepartmentName { get; set; }

//        public string Descriptionedit { get; set; }
//        string _refname;
//        public string referenceEdit
//        {
//            get { return _refname; }
//            set { SetProperty(ref _refname, value); }
//        }
//        public int? refidEdit { get; set; }
//        public string RefCheckedImageEdit { get; set; }
//        public string DescriptionEdit { get; set; }
//        public string ImageTypeEdit { get; set; }
//        public int ResponsibleIDEdit { get; set; }
//        public string StartDateEdit { get; set; }
//        public string StartDatebind2 { get; set; }
//        public string EndDatebind2 { get; set; }
//        public string sectorname { get; set; }
//        public string EndDateEdit { get; set; }
//        public string ImageTypeedit { get; set; }
//        public string FileSizeedit { get; set; }
//        public double valueprogress { get; set; }
//        public string showprogressvalue { get; set; }
//        public string uploadagain { get; set; }
//        public DateTime? MaxDate { get; set; }
//        #endregion
//        #region bool
//        public bool tempsupport { get; set; }
//        public bool isreflable { get; set; }
//        public bool issource { get; set; }
//        public bool isBusy { get; set; }
//        public bool IsSupportedEdit { get; set; }
//        public bool openPriorityList { get; set; }
//        public bool assignemployeelistvisibility { get; set; }
//        public bool prioritylistVisibilitiy { get; set; }
//        public bool SectorListVisiability { get; set; }
//        public bool DepartmentListVisiability { get; set; }
//        public bool RefrenceListVisibility { get; set; }
//        public bool isClickedSector { get; set; }
//        public bool isClickeddepartment { get; set; }
//        public bool ischeckedassignemployee { get; set; }
//        public bool isrefrencechecked { get; set; }
//        public bool stopuploadgrid { get; set; }
//        public bool uploadgridvisbility { get; set; }

//        #endregion
//        private bool _dataLoaded;//prevent "OnApperaing" tp loade data twice

//        #region list 
//        public ObservableCollection<ProjectListModel> ProjectList2 { get; set; }
//        public ObservableCollection<ResponsiblesDDL> TempemployeeDemoDataListedit { get; set; }
//        public byte[] FileByte { get; set; }
//        public ObservableCollection<ListPopUpModel> PriorityListData { get; set; }
//        public ObservableCollection<SectorModel> SectorList { get; set; }
//        public ObservableCollection<DepartmentModel> Departmentlist { get; set; }
//        public ObservableCollection<ResponsiblesDDL> assignemployeeList { get; set; }
//        public ObservableCollection<UserGroupListModel> refrenceList { get; set; }
//        public ObservableCollection<FileDataModel> FilesListEdit { get; set; }

//        public ObservableCollection<EmployeeModel> TempemployeeDataList { get; set; }
//        public ObservableCollection<FileDataModel> sharedlist { get; set; }

//        #endregion

//        public _EditTaskViewModel(INavService _navService, INavigationService _NavigationServices) : base(_NavigationServices)
//        {
//            navService = _navService;
//            projectId = 0;

//            downloadfilecommand = new Command(downloadfilecommandExcute);
//            SupportEmergentEditCommand = new Command(SupportEmergentEditCommandExcute);

//            DeleteCommand = new Command(DeleteCommandExcute);
//            OnSelectEndDateEditCommand = new Command(OnSelectEndDateEditCommandExcute);
//            OnSelectStarDateEditCommand = new Command(OnSelectStarDateEditCommandExcute);
//            selectpriorityCommandEdit = new Command(selectpriorityCommandEditExcute);
//            CheckPriorityEditCommand = new Command(CheckPriorityEditCommandExcute);
//            SearchemployeeButtonCommand = new Command(SearchemployeeButtonCommandExcute);
//            chooseEmployeeEditCommand = new Command(chooseEmployeeEditCommandExcute);
//            deleteassignemployeeCommand = new Command(deleteassignemployeeCommandExcute);
//            OpenRefrenceCommand = new Command(OpenRefrenceCommandExcute);
//            chooseRefrenceCommand = new Command(chooseRefrenceCommandExcute);
//            UploadFileCommandEdit = new Command(UploadFileCommandExcute);
//            StopUploadCommandEdit = new Command(stopuploadingComandExcute);
//            EditTaskCommand = new Command(EditTaskCommandExcute);
//            deletefilecomannd = new Command(DeleteFilecommandExcute);
//            ClearViewCommand = new Command(ClearViewCommandExcute);

//            NaviagationButtonCommand = new Command(NaviagationButtonCommandExcute);
//            OpenProjectButtonCommand = new Command(OpenProjectButtonCommandExcute);
//            ChooseProjectButtonCommand = new Command(ChooseProjectButtonCommandExcute);
//            ProjectList2 = new ObservableCollection<ProjectListModel>();
//            sharedlist = new ObservableCollection<FileDataModel>();
//            assignemployeeList = new ObservableCollection<ResponsiblesDDL>();
//            SectorList = new ObservableCollection<SectorModel>();
//            PriorityListData = new ObservableCollection<ListPopUpModel>();
//            Departmentlist = new ObservableCollection<DepartmentModel>();
//            FilesListEdit = new ObservableCollection<FileDataModel>();
           
//            refrenceList = new ObservableCollection<UserGroupListModel>();
//            TempemployeeDemoDataListedit = new ObservableCollection<ResponsiblesDDL>();
//            if (employeenameEdit != null)
//            {
//                assignemployeelistvisibility = false;
//            }

//            uploadgridvisbility = true;
//            sireneditcheck = "siren";
//            MessagingCenter.Subscribe<ProjectListModel>(this, "AddNew", async (project) =>

//            {

//                ProjectName = project.name;
//                projectId = project.id;
//                isporjectselect = false;
//                employeenameEdit = "";
//                var assignee = assignemployeeList.FirstOrDefault(x => x.Value2.ID == ResponsibleID);
//                if (assignee != null)
//                {
//                    assignee.IsCheckedemployee = false;
//                }

//                ResponsibleID = "";
//               await GetAssignEmployee();
//            });
//            MessagingCenter.Subscribe<UserGroupListModel>(this, "selectrefrence", (data) =>

//            {

//                referenceEdit = data.name;
//                //refidEdit = data.id;
//                //selectedid = data.id;
//                isrefrencechecked = false;

//            });
//            MessagingCenter.Subscribe<ResponsiblesDDL>(this, "SelectedEmployeeEdited", (data) =>
//            {
//                lastassignemployee = data;
//                ResponsibleID = data.Value2.ID;
//                employeenameEdit = data.Text;
//                seletedemp = employeenameEdit;
//                selectempid = ResponsibleID;

//                assignemployeelistvisibility = false;
//            });
//            Initialize();
//        }
//        private async void downloadfilecommandExcute(object obj)
//        {
//            try
//            {
//                var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Storage);
//                if (status != PermissionStatus.Granted)
//                {
//                    var results = await CrossPermissions.Current.RequestPermissionsAsync(new[] { Permission.Storage });
//                    status = results[Permission.Storage];
//                }
//                if (status != PermissionStatus.Granted)
//                {
//                    return;
//                }
//                var file = (FileDataModel)obj;
//                var path = DependencyService.Get<IFileHelper>().file(AppConstants.AppName);
//                string filepath = Path.Combine(path, file.FileName);
//                if (File.Exists(filepath))
//                {
//                    DependencyService.Get<IFileHelper>().FilePath(filepath);
//                }
//                else
//                {
//                    isBusy = true;
//                    Attachment attachment = await GetAttchmentbyId(file.AttachmentId);
//                    isBusy = false;
//                    if (attachment.content != null)
//                    {
//                        File.WriteAllBytes(filepath, Convert.FromBase64String(attachment.content));
//                        DependencyService.Get<IFileHelper>().FilePath(filepath);
//                    }

//                }

//            }
//            catch (Exception ee)
//            {

//                var properties = new Dictionary<string, string>
//                                   {
//                                         { "downloadfilecommandExcute", "attachment" },
//                                   };
//                Crashes.TrackError(ee, properties);
//            }
//            //var file = (FileDataModel)obj;
//            //if (file.content != null)
//            //{
//            //    File.WriteAllBytes(file.filepath, file.content);
//            //}
//            //// Application.Current.MainPage.DisplayAlert("", AppResource.dowlnoadSucessfully, AppResource.oktext);
//            ////  CrossFilePicker.Current.OpenFile(file.FileName);
//            //DependencyService.Get<IFileHelper>().FilePath(file.filepath);
//        }

//        private async Task<Attachment> GetAttchmentbyId(int attachmentId)
//        {
//            try
//            {
//                var api = RestService.For<ITaskyApi>(new System.Net.Http.HttpClient(new HttpLoggingHandler()) { BaseAddress = new Uri(Settings.ApiUrl) });

//                var att = await api.GetTaskAttachment("Bearer " + Settings.AccessToken, attachmentId, int.Parse(Settings.TaskId));
//                return att.Content;
//            }
//            catch (Exception ee)
//            {

//                var properties = new Dictionary<string, string>
//                                   {
//                                         { "GetAttchmentbyId", "attachment" },
//                                   };
//                Crashes.TrackError(ee, properties);
//                return new Attachment();
//            }
//        }

//        private async Task<bool> DeleteAttchment(int attachmentId)
//        {
//            try
//            {
//                var api = RestService.For<ITaskyApi>(new System.Net.Http.HttpClient(new HttpLoggingHandler()) { BaseAddress = new Uri(Settings.ApiUrl) });

//                var att = await api.DeleteAttachment("Bearer " + Settings.AccessToken, attachmentId, int.Parse(Settings.TaskId));
                
//                object responseData = Newtonsoft.Json.JsonConvert.DeserializeObject(att);

//                return ((dynamic)responseData).requestSuccess;
//            }
//            catch (Exception ee)
//            {

//                var properties = new Dictionary<string, string>
//                                   {
//                                         { "GetAttchmentbyId", "attachment" },
//                                   };
//                Crashes.TrackError(ee, properties);
//                return false;
//            }
//        }

//        private async Task<int> AddNewAttchment(AttachmentModel attachment)
//        {
//            try
//            {
//                var api = RestService.For<ITaskyApi>(new System.Net.Http.HttpClient(new HttpLoggingHandler()) { BaseAddress = new Uri(Settings.ApiUrl) });

//                string jsonString = await api.AddAttachment("Bearer " + Settings.AccessToken, attachment, int.Parse(Settings.TaskId));
//                object responseData = Newtonsoft.Json.JsonConvert.DeserializeObject(jsonString);

//                int attachId = ((dynamic)responseData).content;
//                return attachId;
//            }
//            catch (Exception ee)
//            {
//                var properties = new Dictionary<string, string>
//                                   {
//                                         { "GetAttchmentbyId", "attachment" },
//                                   };
//                Crashes.TrackError(ee, properties);
//                return 0;
//            }
//        }

//        private async void ChooseProjectButtonCommandExcute(object obj)
//        {
//            var ItemSelected = obj as ProjectListModel;



//            if (ItemSelected == null) return;
//            if (ItemSelected != lastproject)
//            {
//                if (lastproject != null)
//                {
//                    lastproject.isProjectChecked = false;

//                }
//                ItemSelected.isProjectChecked = true;

//            }
//            else
//            {
//                ItemSelected.isProjectChecked = !ItemSelected.isProjectChecked;

//            }
//            if (ItemSelected.isProjectChecked == false)
//            {
//                ProjectName = " ";
//                projectId = 0;
//                employeenameEdit = "";
//                lastassignemployee.IsCheckedemployee = false;
//                assignemployeelistvisibility = false;
//            }
//            else
//            {
//                ProjectName = ItemSelected.name;
//                projectId = ItemSelected.id;
//            }
//            lastproject = ItemSelected;

//            isprojectlistvibibilty = false;
//            isporjectselect = false;
//            await GetAssignEmployee();
//        }

//        private void OpenProjectButtonCommandExcute(object obj)
//        {
//            //if (isporjectselect)
//            //{
//            //    isprojectlistvibibilty = true;
//            //    assignemployeelistvisibility = false;


//            //}
//            //else
//            //{
//            //    isprojectlistvibibilty = false;
//            //}
//            PopupNavigation.Instance.PushAsync(new projectPopupPage());
//        }

//        private bool _assigneeSearchPopupOpeend;
//        private void SearchemployeeButtonCommandExcute(object obj)
//        {

//            if (_assigneeSearchPopupOpeend)
//            {
//                return;
//            }
//            _assigneeSearchPopupOpeend = true;

//            var newTaskModel = new AddTaskModel
//            {
//                title = TaskNameEdit,
//                urgentSupport = IsSupportedEdit,
//                startDate = StartDateEdit,
//                endDate = EndDateEdit,
//                fK_PriorityID = priorityIdedit,
//                fK_RefrenceID = refidEdit

//            };

//            var navigationParams = new NavigationParameters();

//            Settings.projectId = projectId.ToString();
//            navigationParams.Add("listofedittemp", TempemployeeDemoDataListedit);
//            ///navigationParams.Add("projectnameedit", ProjectName);
//            navigationParams.Add("empid", ResponsibleID);
//            ///navigationParams.Add("editmodel", newTaskModel);
//            ///_navigationService.NavigateAsync("MainTabbedPage?selectedTab=TasksPage/TaskDetailsPage/EditTask/EditSearchPage", navigationParams);
//            //_navigationService.NavigateAsync("EditSearchPage", navigationParams);
//            navService.NavigateToAsync<EditSearchPageViewModel>(navigationParams);
//            // // _navigationService.NavigateAsync("EditSearchPage", navigationParams, true);//
//            _assigneeSearchPopupOpeend = false;
//            //  }

//        }

//        public override void OnNavigatedTo(NavigationParameters parameters)
//        {
//            base.OnNavigatedTo(parameters);
//            if (parameters.Count > 0)
//            {
//                base.OnNavigatedTo(parameters);


//                // proj = parameters["projectnamesave"] as string;
//                var data = parameters["selectedempEdit"] as ResponsiblesDDL;
//               // var editmodel = parameters["editmodel"] as AddTaskModel;
//                lastassignemployee = data;
//                ResponsibleID = data.Value2.ID;
//                employeenameEdit = data.Text;
//                seletedemp = employeenameEdit;
//                selectempid = ResponsibleID;
//                ////tempenddate = editmodel.endDate;
//                ////tempstartdate = editmodel.startDate;
//                //temprefid = editmodel.fK_RefrenceID;
//                //tempriorityid = editmodel.fK_PriorityID;
//                //temptaskname = editmodel.title;
//                //tempsupport = editmodel.urgentSupport;
//                ////tempprojectid = editmodel.fK_ProjectID;
//                ////tempprojectname= ProjectList2.FirstOrDefault(x => x.id == tempprojectid).name;
//                temprefrencename = refrenceList.FirstOrDefault(x => x.id == (temprefid==null?"": temprefid.ToString())).name;
//                #region urgetnt support
//                //IsSupportedEdit = editmodel.urgentSupport;
//                if (IsSupportedEdit)
//                {
//                    sireneditcheck = "redsiren";
//                }
//                else
//                {
//                    sireneditcheck = "siren";
//                }
//                #endregion
//                #region priority ha3ml refctor b3den
//                temppriorityname = PriorityListData.FirstOrDefault(x => x.id == tempriorityid).name;
//                if (temppriorityname == Priotities.Critical.ToString())
//                {
//                    temppriorityname = "darkorange";
//                }
//                else if (temppriorityname == Priotities.Normal.ToString())
//                {
//                    temppriorityname = "blueimage";
//                }
//                else if (temppriorityname == Priotities.High.ToString())
//                {
//                    temppriorityname = "orangeimage";

//                }
//                else
//                {
//                    temppriorityname = "purpleimage";
//                }
//                #endregion
//                assignemployeelistvisibility = false;

               
//            }
//            else
//            {
//                return;
//            }
//        }
//        private void NaviagationButtonCommandExcute(object obj)
//        {
//            //_navigationService.NavigateAsync("MainTabbedPage?selectedTab=TasksPage/TaskDetailsPage");
//           // _navigationService.NavigateAsync("TaskDetailsPage");
//            // _navigationService.GoBackAsync();
//            navService.NavigateBackAsync();
//        }



//        private void OnSelectStarDateEditCommandExcute(object obj)
//        {
//            UserDialogs.Instance.DatePrompt(new DatePromptConfig { MaximumDate = MaxDate, OnAction = (result) => SetStartDate(result), IsCancellable = true });
//        }

//        private void SetStartDate(DatePromptResult result)
//        {
//            if (result.Ok)
//            {
//                StartDateEdit = result.SelectedDate.ToShortDateStringForView();// DateConverterView(result.SelectedDate);
//                StartDatebind2 = result.SelectedDate.ToDateTimeStringForAPI();// DateConverter(result.SelectedDate);
//                StartDateTime = result.SelectedDate;

//            }
//        }

//        private void OnSelectEndDateEditCommandExcute(object obj)
//        {
//            UserDialogs.Instance.DatePrompt(new DatePromptConfig { MaximumDate = MaxDate, OnAction = (result) => SetEndDate(result), IsCancellable = true });
//        }

//        private void SetEndDate(DatePromptResult result)
//        {
//            if (result.Ok)
//            {
//                EndDateEdit = result.SelectedDate.ToShortDateStringForView();// DateConverterView(result.SelectedDate);
//                EndDatebind2 = result.SelectedDate.ToDateTimeStringForAPI();// DateConverter(result.SelectedDate);
//                endDateTime = result.SelectedDate;
//            }
//        }

//        public void OnemployeenameEditChanged()
//        {
//            assignemployeelistvisibility = true;
//            if (string.IsNullOrEmpty(employeenameEdit))
//            {
//                assignemployeeList = TempemployeeDemoDataListedit;
//                assignemployeelistvisibility = false;
//                ResponsibleID = "";
//            }
//            else
//            {
//                if (assignemployeeList != null)
//                {
//                    assignemployeeList = TempemployeeDemoDataListedit;
//                    List<ResponsiblesDDL> result = assignemployeeList.Where(x => !string.IsNullOrEmpty(x.Text) && x.Text.ToLower().Contains(employeenameEdit.ToLower())).ToList();
//                    if (result != null)
//                    {
//                        try
//                        {
//                            assignemployeeList = new ObservableCollection<ResponsiblesDDL>(result);
//                            if (assignemployeeList.Count < 2)
//                            {
//                                assignemployeelistvisibility = false;
//                            }
//                        }
//                        catch (Exception exception)
//                        {
//                            var properties = new Dictionary<string, string>
//                       {
//                             { "_EditTaskViewModel", "assignemployeetextchanged" },
//                       };
//                            Crashes.TrackError(exception, properties);
//                        }

//                    }

//                }
//            }
//        }
//        private void SupportEmergentEditCommandExcute(object obj)
//        {
//            var checkedvalue = obj;
//            if (IsSupportedEdit)
//            {
//                sireneditcheck = "redsiren";

//            }
//            else
//            {
//                sireneditcheck = "siren";

//            }

//        }

//        private async void DeleteFilecommandExcute(object obj)
//        {
//            try
//            {
//                bool delete_ = await Application.Current.MainPage.DisplayAlert("", AppResource.DeleteMsg, AppResource.oktext,AppResource.canceltext);
//                if (delete_)
//                {
//                    isBusy = true;
//                    var item = (FileDataModel)obj;
//                    int index_ = FilesListEdit.IndexOf(item);
//                    FilesListEdit.RemoveAt(index_);

//                    //refresh repeater view
//                    FilesListEdit = new ObservableCollection<FileDataModel>(FilesListEdit);

//                    bool deleted = await DeleteAttchment(item.AttachmentId);
//                    isBusy = false;
//                }
                
//            }
//            catch (Exception exception)
//            {
//                isBusy = false;
//                var properties = new Dictionary<string, string>
//                       {
//                             { "_EditTaskViewModel", "DeleteFilecommandExcute" },
//                       };
//                Crashes.TrackError(exception, properties);
//            }

//        }

//        private void DeleteCommandExcute(object obj)
//        {
//            PopupNavigation.Instance.PushAsync(new DeletePopupPage());

//        }

//        private void ClearViewCommandExcute(object obj)
//        {
//             navService.NavigateBackAsync();
//            // _navigationService.GoBackAsync();
//        }

//        private async void EditTaskCommandExcute(object obj)
//        {
//            if (String.IsNullOrEmpty(TaskNameEdit) || String.IsNullOrWhiteSpace(TaskNameEdit) || String.IsNullOrEmpty(StartDateEdit)
//                         || String.IsNullOrEmpty(EndDateEdit) || String.IsNullOrEmpty(ResponsibleID))
//            {
//                await Application.Current.MainPage.DisplayAlert(AppResource.alerttext, AppResource.alertfilledalldata, AppResource.oktext);
//                return;
//            }

//            if (StartDateEdit.ToDateTimeFromShortDate() <= EndDateEdit.ToDateTimeFromShortDate())
//            {

//                var api = RestService.For<ITaskyApi>(new System.Net.Http.HttpClient(new HttpLoggingHandler()) { BaseAddress = new Uri(Settings.ApiUrl) });
//                var newTaskModel1 = new AddTaskModel
//                {
//                    title = TaskNameEdit,
//                    isActive = true,
//                    id = int.Parse(Settings.TaskId),
//                    fK_ProjectID = projectId == 0 ? projectId = null : projectId,
//                    creationDate = DateTime.Now.ToDateTimeStringForAPI(),
//                    urgentSupport = IsSupportedEdit,
//                    startDate = StartDatebind2,
//                    endDate = EndDatebind2,
//                    description = DescriptionEdit,
//                    fK_RefrenceID = refidEdit == 0 ? refidEdit = null : refidEdit,
//                    fK_PriorityID = priorityIdedit,
//                    fK_SectorID = 1,
//                    fK_StatusID = statusid,
//                    ModificationDate = DateTime.Now.ToDateTimeStringForAPI(),
//                    FK_ModifiedBy = int.Parse(Settings.UserId),
//                    fK_CreatedBy = int.Parse(Settings.UserId),
//                    progress = 0,
//                    fK_ResponsibleID = ResponsibleID
//                };

//                // attachment =new List<AttachmentModel>( UploadFile)
//                var request1 = new AddTaskRequest
//                {
//                    task = newTaskModel1
//                };

//                try
//                {
//                    isBusy = true;
//                    //var result = await api.UpdateTask("Bearer " + Settings.AccessToken, request1,
//                    //    int.Parse(Settings.UserId), false);

//                    //if (result.requestSuccess)
//                    //{
//                    //    await Application.Current.MainPage.DisplayAlert(AppResource.edittasktitle, AppResource.edittasksuccesstext, AppResource.oktext);

//                    //    await navService.NavigateBackAsync();
//                    //    // await _navigationService.GoBackAsync();

//                    //}
//                    //else
//                    //{
//                    //    isBusy = false;
//                    //    await Application.Current.MainPage.DisplayAlert(AppResource.failuretext, AppResource.edittaskfailuretext, AppResource.oktext);
//                    //}

//                }
//                catch (Exception exception)
//                {
//                    isBusy = false;
//                    var properties = new Dictionary<string, string>
//                       {
//                             { "_EditTaskViewModel", "posteditatsk" },
//                       };
//                    Crashes.TrackError(exception, properties);
//                }
//            }
//            else
//            {
//                await Application.Current.MainPage.DisplayAlert(AppResource.invailddate, AppResource.Comparedatetext, AppResource.oktext);
//            }

//        }


//        private async Task<bool> FileSizeExceededTheLimit(int size)
//        {
//            try
//            {
//                int mSize = size / 1024;
//                bool _size = mSize > AppConstants.MaxFileSize;

//                if (_size)
//                {
//                    string msg_ar = $"يجب ان لا يتخطى حجم المرفقات عن {AppConstants.MaxFileSize} ميجا";
//                    string msg_en = $"Attachments size must not Exceed {AppConstants.MaxFileSize} M";
//                    string msg_ = Setting.Settings.IsRtl ? msg_ar : msg_en;
//                    await Application.Current.MainPage.DisplayAlert(AppResource.alerttext, msg_, AppResource.oktext);

//                }

//                return _size;
//            }
//            catch
//            {
//                return false;
//            }
//        }

//        private async void stopuploadingComandExcute(object obj)
//        {
//            try
//            {
//                bool premissionGranted = await PremissionGranted();
//                if (!premissionGranted)
//                {
//                    return;
//                }
//               // uploadagain = "Upload again";
//               //stopuploadgrid = true;
//               // uploadgridvisbility = false;
//               // FilelistVisbility2 = true;
//                var file = await CrossFilePicker.Current.PickFile();
//                if (file == null)
//                {

//                    FileNameEdit = AppResource.nofiletext;
//                    uploadagain = AppResource.uploadagintext;
//                    return;
//                }
//                if (file != null)
//                {
//                    int filesize = file.DataArray.Length;
//                    var filelength = filesize / 1024;

//                    bool fileSizeExceededTheLimit = await FileSizeExceededTheLimit(filelength);
//                    if (fileSizeExceededTheLimit)
//                    {
//                        return;
//                    }

//                    // chek file extiontion 
//                    var fileExtension = Path.GetExtension(file.FileName);
//                    if (Utility.NotSupportedExtension(fileExtension))
//                    {
//                        await Application.Current.MainPage.DisplayAlert(AppResource.alerttext, AppResource.notSupportedFile, AppResource.oktext);
//                        return;
//                    }

//                    FileNameEdit = file.FileName;
//                    uploadagain = AppResource.uploadagintext;
//                    uploadgridvisbility = false;
//                    stopuploadgrid = true;
//                    FilelistVisbility2 = true;
//                    FileByte = file.DataArray;
                    

//                    FileSizeedit = Convert.ToString(filelength) + "KB";
//                    showprogressvalue = "100%";
//                    valueprogress = 1.0;
//                    if (filelength == 1024)
//                    {
//                        filelength = filelength / 1024;
//                        FileSizeedit = Convert.ToString(filelength) + "MB";
//                    }


//                    //post file to server 
//                    isBusy = true;
//                    int attacheId = await AddNewAttchment(new AttachmentModel()
//                    {
//                        title = FileNameEdit,
//                        content = FileByte,
//                        fK_CreatedBy = int.Parse(Settings.UserId),
//                        creationDate = DateTime.Now
//                    });
//                    isBusy = false;


//                    if (fileExtension.Equals(".png") || fileExtension.Equals(".jpg"))
//                    {
//                        ImageTypeedit = "picture";
//                    }
//                    else
//                    {
//                        ImageTypeedit = "pdf";
//                    }

//                    FilesListEdit.Add(new FileDataModel { FileName = FileNameEdit, image = ImageTypeedit, FileTime = DateTime.Now.ToShortDateStringForView(), FileSize = FileSizeedit, AttachmentId = attacheId });

//                    FilesListEdit = new ObservableCollection<FileDataModel>(FilesListEdit);
//                }
//            }
//            catch (Exception exception)
//            {
//                var properties = new Dictionary<string, string>
//                       {
//                             { "_EditTaskViewModel", "stopuploadingComandExcute" },
//                       };
//                Crashes.TrackError(exception, properties);
//            }

//        }

//        private async void UploadFileCommandExcute(object obj)
//        {
//            try
//            {
//                bool premissionGranted = await PremissionGranted();
//                if (!premissionGranted)
//                {
//                    return;
//                }
//                //stopuploadgrid = true;
//                //uploadgridvisbility = false;
//                //uploadagain = AppResource.uploadfileagaintext;
//                //showprogressvalue = "0%";
//                //valueprogress = 0.0;
//                var file = await CrossFilePicker.Current.PickFile();

//                if (file == null)
//                {

//                    FileNameEdit = AppResource.nofiletext;
//                    uploadagain = AppResource.uploadagintext;
//                    return;
//                }
//                if (file != null)
//                {
//                    int filesize = file.DataArray.Length;
//                    var filelength = filesize / 1024;

//                    bool fileSizeExceededTheLimit = await FileSizeExceededTheLimit(filelength);
//                    if (fileSizeExceededTheLimit)
//                    {
//                        return;
//                    }

//                    // chek file extiontion 
//                    var fileExtension = Path.GetExtension(file.FileName);
//                    if (Utility.NotSupportedExtension(fileExtension))
//                    {
//                        await Application.Current.MainPage.DisplayAlert(AppResource.alerttext, AppResource.notSupportedFile, AppResource.oktext);
//                        return;
//                    }
//                    FileNameEdit = file.FileName;
//                    uploadagain = AppResource.uploadagintext;
//                    uploadgridvisbility = false;
//                    stopuploadgrid = true;
//                    FilelistVisbility2 = true;
//                    FileByte = file.DataArray;

//                    FileSizeedit = Convert.ToString(filelength) + "KB";
//                    showprogressvalue = "100%";
//                    valueprogress = 1.0;
//                    if (filelength == 1024)
//                    {
//                        filelength = filelength / 1024;
//                        FileSizeedit = Convert.ToString(filelength) + "MB";
//                    }

//                    //post file to server 
//                    isBusy = true;
//                    int attacheId = await AddNewAttchment(new AttachmentModel()
//                    {
//                        title = FileNameEdit,
//                        content = FileByte,
//                        fK_CreatedBy = int.Parse(Settings.UserId),
//                        creationDate = DateTime.Now
//                    });
//                    isBusy = false;


                    
//                    if (fileExtension.Equals(".png") || fileExtension.Equals(".jpg"))
//                    {
//                        ImageTypeedit = "picture";
//                    }
//                    else
//                    {
//                        ImageTypeedit = "pdf";
//                    }
//                    // await  CrossFilePicker.Current.SaveFile(file);
//                    FilesListEdit.Add(new FileDataModel { FileName = FileNameEdit, content = FileByte, image = ImageTypeedit, FileTime = DateTime.Now.ToShortDateStringForView(), FileSize = FileSizeedit, AttachmentId = attacheId });

//                    //refresh repeater view
//                    FilesListEdit = new ObservableCollection<FileDataModel>(FilesListEdit);
//                }
//            }
//            catch (Exception exception)
//            {
//                var properties = new Dictionary<string, string>
//                       {
//                             { "_EditTaskViewModel", "UploadFileCommandExcute" },
//                       };
//                Crashes.TrackError(exception, properties);
//            }
           
//        }

//        private async Task<bool> PremissionGranted()
//        {
//            var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Storage);
//            if (status != PermissionStatus.Granted)
//            {
//                var results = await CrossPermissions.Current.RequestPermissionsAsync(new[] { Permission.Storage });
//                status = results[Permission.Storage];
//            }

//            return status == PermissionStatus.Granted;
//        }

//        private void chooseRefrenceCommandExcute(object obj)
//        {
//            var item = obj as UserGroupListModel;
//            var ItemSelected = obj as UserGroupListModel;
//            SectorListVisiability = true;
//            if (ItemSelected == null) return;
//            if (ItemSelected != lastrefselected)
//            {
//                if (lastrefselected != null)
//                {
//                    lastrefselected.IsCheckedRefe = false;
//                }


//            }
//            else
//            {
//                ItemSelected.IsCheckedRefe = !ItemSelected.IsCheckedRefe;
//            }
//            lastrefselected = ItemSelected;
//            referenceEdit = ItemSelected.name;
//            //refidEdit = ItemSelected.id;
//            RefrenceListVisibility = false;
//            isrefrencechecked = false;
//        }

//        private void OpenRefrenceCommandExcute(object obj)
//        {
//            //if (isrefrencechecked)
//            //{
//            //    RefrenceListVisibility = true;
//            //}
//            //else
//            //{
//            //    RefrenceListVisibility = false;
//            //}
//            PopupNavigation.Instance.PushAsync(new UserGroupPopUpPage());
//        }

//        private void deleteassignemployeeCommandExcute(object obj)
//        {
//            EmpImage = "user.svg";
//            employeenameEdit = "";

//        }

//        private void chooseEmployeeEditCommandExcute(object obj)
//        {
//            var ItemSelected = obj as ResponsiblesDDL;

//            if (ItemSelected == null) return;

//            foreach (var item in assignemployeeList)
//            {
//                item.IsCheckedemployee = false;
//            }

//            if (ItemSelected != lastassignemployee)
//            {
//                if (lastassignemployee != null)
//                {
//                    lastassignemployee.IsCheckedemployee = false;
//                }
//                ItemSelected.IsCheckedemployee = true;


//            }
//            else
//            {
//                ItemSelected.IsCheckedemployee = !ItemSelected.IsCheckedemployee;
//            }
//            if (ItemSelected.IsCheckedemployee == false)
//            {
//                ResponsibleID = "";
//                employeenameEdit = "";

//            }
//            else
//            {
//                ResponsibleID = ItemSelected.Value2.ID;
//                employeenameEdit = ItemSelected.Text;

//            }
//            lastassignemployee = ItemSelected;

//            assignemployeelistvisibility = false;
//        }


//        private void CheckPriorityEditCommandExcute(object obj)
//        {
//            var item = obj as ListPopUpModel;
//            var ItemSelected = obj as ListPopUpModel;
//            SectorListVisiability = true;
//            if (ItemSelected == null) return;

//            foreach (var pro in PriorityListData)
//            {
//                pro.IsChecked = false;
//            }

//            ItemSelected.IsChecked = true;
//            prioritynameEdit = ItemSelected.name;
//            priorityimageedit = ItemSelected.image2;
//            priorityIdedit = ItemSelected.id;
//            prioritylistVisibilitiy = false;
//            openPriorityList = false;
//        }

//        private void selectpriorityCommandEditExcute(object obj)
//        {
//            if (obj.ToString() == "1")
//            {
//                openPriorityList = !openPriorityList;
//            }
//            prioritylistVisibilitiy = !prioritylistVisibilitiy;
//        }
//        public async void Initialize()
//        {
//            if (_dataLoaded)
//            {
//                return;
//            }
//            #region priority data
//            PriorityListData.Add(new ListPopUpModel { id = 1, name = AppResource.lowenumtext, image2 = "purpleimage" });

//            PriorityListData.Add(new ListPopUpModel { id = 2, name = AppResource.normalenumtext, image2 = "blueimage" });

//            PriorityListData.Add(new ListPopUpModel { id = 3, name = AppResource.highenumtext, image2 = "orangeimage" });
//            PriorityListData.Add(new ListPopUpModel { id = 4, name = AppResource.crtiticlenumtext, image2 = "darkorange" });

//            #endregion
//            if (Settings.TenantTypes == ((int)TenantTypeEnum.Project).ToString())
//            {

//                projectgridvisibilty = true;
//                refgridVisibility = false;
//                await Getproject();
//            }
//            else if (Settings.TenantTypes == ((int)TenantTypeEnum.Refrence).ToString() || Settings.TenantTypes == ((int)TenantTypeEnum.Maham).ToString())
//            {
//                await GetRefrence();
//                projectgridvisibilty = false;
//                refgridVisibility = true;
//                if (Settings.TenantTypes == ((int)TenantTypeEnum.Maham).ToString())
//                {
//                    issource = true;
//                    isreflable = false;
//                }
//                else
//                {
//                    issource = false;
//                    isreflable = true;
//                }


//            }
//            await GetTaskData();

//            await GetRefrence();
//            await GetAssignEmployee();
//            _dataLoaded = true;
//            base.OnAppearing();
//        }
//        public async Task Getproject()
//        {
//            var api = RestService.For<ITaskyApi>(new System.Net.Http.HttpClient(new HttpLoggingHandler()) { BaseAddress = new Uri(Settings.ApiUrl) });
//            try
//            {
//                ProjectList2.Clear();

//                var result = await api.GetUserProject(Settings.UserId);

//                ProjectList2 = new ObservableCollection<ProjectListModel>(result);
//            }
//            catch (Exception exception)
//            {
//                var properties = new Dictionary<string, string>
//                       {
//                             { "NewTaskViewModel", "getprojectlist" },
//                       };
//                Crashes.TrackError(exception, properties);
//            }
//        }

//        public async Task GetAssignEmployee()
//        {
//            var api = RestService.For<ITaskyApi>(new System.Net.Http.HttpClient(new HttpLoggingHandler()) { BaseAddress = new Uri(Settings.ApiUrl) });
//            try
//            {

//                assignemployeeList.Clear();
//                var result = await api.GetEmployees("Bearer " + Settings.AccessToken);
//                List<ResponsiblesDDL> x = new List<ResponsiblesDDL>();
//                x = (result.Data).ToObject<List<ResponsiblesDDL>>();
//                foreach (var item in x)
//                {
//                    assignemployeeList.Add(new ResponsiblesDDL { Text = item.Text, Value2 = item.Value2 });
//                }
//                TempemployeeDemoDataListedit = assignemployeeList;
//                var assigne_ = assignemployeeList.FirstOrDefault(_x => _x.Value2.ID == ResponsibleID);
//                if (assigne_ != null)
//                {
//                    assigne_.IsCheckedemployee = true;
//                    employeenameEdit = assigne_.Text;
//                    ischeckedassignemployee = true;
//                    assignemployeelistvisibility = false;
//                }
//            }
//            catch (Exception exception)
//            {
//                var properties = new Dictionary<string, string>
//                       {
//                             { "_EditTaskViewModel", "assignemployee" },
//                       };
//                Crashes.TrackError(exception, properties);
//            }
//        }

//        public async Task GetRefrence()
//        {
//            var api = RestService.For<ITaskyApi>(new System.Net.Http.HttpClient(new HttpLoggingHandler()) { BaseAddress = new Uri(Settings.ApiUrl) });
//            try
//            {

//                var result = await api.GetSource("Bearer " + Settings.AccessToken);
//                List<ListPopUpModel> _x = new List<ListPopUpModel>();
//                _x = (result.Data).ToObject<List<ListPopUpModel>>();
//                foreach (var item in _x)
//                {
//                    refrenceList.Add(new UserGroupListModel { id = item.id.ToString(), name = item.name, IsCheckedRefe = false });
//                }
//                var data = refrenceList.FirstOrDefault(x => x.id == (refidEdit == null ? "" : refidEdit.ToString()));
//                if (data != null)
//                {
//                    data.IsCheckedRefe = true;
//                }

//                var refEdit_ = refrenceList.FirstOrDefault(x => x.id == (refidEdit == null ? "" : refidEdit.ToString()));
//                if (refEdit_ != null)
//                {
//                    referenceEdit = refEdit_.name;
//                }

//            }
//            catch (Exception exception)
//            {
//                var properties = new Dictionary<string, string>
//                       {
//                             { "_EditTaskViewModel", "getrefrences" },
//                       };
//                Crashes.TrackError(exception, properties);
//            }
//        }
//        public async Task GetTaskData()
//        {
//            var culture = CrossMultilingual.Current.CurrentCultureInfo;
//            var api = RestService.For<ITaskyApi>(new System.Net.Http.HttpClient(new HttpLoggingHandler()) { BaseAddress = new Uri(Settings.ApiUrl) });
//            try
//            {

//                if (taskdata != null)
//                {
//                    StartDateEdit = taskdata.task.startDate.ToShortDateStringForView();// DateConverterView(taskdata.task.startDate);
//                    EndDateEdit = taskdata.task.endDate.ToShortDateStringForView();// DateConverterView(taskdata.task.endDate);


//                    TaskNameEdit = taskdata.task.title;
//                    DescriptionEdit = taskdata.task.description;

//                    IsSupportedEdit = taskdata.task.urgentSupport;
//                    assignemployeelistvisibility = false;
//                    statusid = taskdata.task.fK_StatusID;
//                    CanDeleteTask = taskdata.task.canDeleteTask;
//                    CanReassignTask = taskdata.task.canReAssignTask;
//                    priorityId = taskdata.task.fK_PriorityID;
//                    prioritynameEdit = taskdata.task.priority;
//                    projectId = taskdata.task.FK_ProjectID;
//                    ProjectName = taskdata.task.ProjectName;
//                    #region priority
//                    if (priorityId == (int)Priotities.Critical)
//                    {
//                        priorityimageedit = "darkorange";
//                        prioritynameEdit = AppResource.crtiticlenumtext;
//                    }
//                    else if (priorityId == (int)Priotities.Normal)
//                    {
//                        priorityimageedit = "blueimage";
//                        prioritynameEdit = AppResource.normalenumtext;
//                    }
//                    else if (priorityId == (int)Priotities.High)
//                    {
//                        priorityimageedit = "orangeimage";
//                        prioritynameEdit = AppResource.highenumtext;

//                    }
//                    else
//                    {
//                        priorityimageedit = "purpleimage";
//                        prioritynameEdit = AppResource.lowenumtext;
//                    }
//                    #endregion
//                    #region refrence
//                    if (referenceEdit != null)
//                    {
//                        referenceEdit = taskdata.task.reference;
//                        refidEdit = taskdata.task.fK_RefrenceID;
//                        isrefrencechecked = true;
//                    }

//                    //if (referenceedit != null)
//                    //{

//                    //}
//                    #endregion

//                    if (seletedemp != null)
//                    {
//                        employeenameEdit = seletedemp;
//                        ResponsibleID = selectempid;
//                    }

//                    if (IsSupportedEdit)
//                    {
//                        IsSupportedEdit = true;
//                        sireneditcheck = "redsiren";
//                    }
//                    else
//                    {
//                        IsSupportedEdit = false;
//                        sireneditcheck = "siren";
//                    }
//                }
//                else
//                {
//                    isBusy = true;
//                    var result = await api.GetTaskByIdForEdit("Bearer " + Settings.AccessToken, Settings.TaskId, Settings.UserId);
//                    if (result.task != null)
//                    {
//                        taskdata = result;


//                        isBusy = false;
//                        CanDeleteTask = result.task.canDeleteTask;
//                        statusid = result.task.fK_StatusID;
//                        if (temptaskname != null)
//                        {
//                            TaskNameEdit = temptaskname;
//                        }
//                        else
//                        {
//                            TaskNameEdit = result.task.title;

//                        }

//                        DescriptionEdit = result.task.description;
//                        if (tempstartdate != null)
//                        {
//                            StartDatebind2 = tempstartdate;
//                            StartDateEdit = tempstartdate;//.ToShortDateStringFromApIDateTime();
//                        }
//                        else
//                        {

//                            StartDateEdit = result.task.startDate.ToShortDateStringForView();// DateConverterView(result.task.startDate);
//                            StartDatebind2 = result.task.startDate.ToDateTimeStringForAPI();// DateConverter(result.task.startDate);
//                        }
//                        if (tempenddate != null)
//                        {
//                            EndDatebind2 = tempenddate;
//                            EndDateEdit = tempenddate;//.ToShortDateStringFromApIDateTime();

//                        }
//                        else
//                        {
//                            EndDatebind2 = result.task.endDate.ToDateTimeStringForAPI();// DateConverter(result.task.endDate);
//                            EndDateEdit = result.task.endDate.ToShortDateStringForView();// DateConverterView(result.task.endDate);

//                        }
//                        if (!String.IsNullOrEmpty(selectempid))
//                        {
//                            ResponsibleID = selectempid;
//                        }
//                        else
//                        {
//                            ResponsibleID = result.task.assigneeID;
//                        }
//                        if (tempsupport)
//                        {
//                            IsSupportedEdit = tempsupport;
//                        }
//                        else
//                        {
//                            IsSupportedEdit = result.task.urgentSupport;
//                        }

//                        CanReassignTask = result.task.canReAssignTask;

//                        if (result.task.FK_ProjectID == null)
//                        {
//                            projectId = 0;
//                        }
//                        else
//                        {
//                            projectId = result.task.FK_ProjectID;
//                        }
//                        ProjectName = result.task.ProjectName;
//                        if (IsSupportedEdit)
//                        {

//                            sireneditcheck = "redsiren.png";
//                        }
//                        else
//                        {
//                            IsSupportedEdit = false;
//                            sireneditcheck = "siren.png";
//                        }

//                        //if (bool.TryParse(result.task.urgentSupport, out bool isssupport2))
//                        //return isssupport2;
//                        assignemployeelistvisibility = false;
//                        #region priority
//                        if (tempriorityid != 0 && tempriorityid != null)
//                        {
//                            temppriorityname = PriorityListData.FirstOrDefault(x => x.id == tempriorityid).name;
//                            prioritynameEdit = temppriorityname;

//                            if (tempriorityid == 4)
//                            {
//                                priorityimageedit = "darkorange";
//                                priorityIdedit = 4;
//                                var data = PriorityListData.FirstOrDefault(x => x.id == result.task.fK_PriorityID).IsChecked = true;
//                            }
//                            else if (tempriorityid == 2)
//                            {
//                                priorityIdedit = 2;
//                                priorityimageedit = "blueimage";
//                                var data = PriorityListData.FirstOrDefault(x => x.id == result.task.fK_PriorityID).IsChecked = true;
//                            }
//                            else if (tempriorityid == 3)
//                            {
//                                priorityIdedit = 3;
//                                priorityimageedit = "orangeimage";
//                                var data = PriorityListData.FirstOrDefault(x => x.id == result.task.fK_PriorityID).IsChecked = true;


//                            }
//                            else
//                            {
//                                priorityIdedit = 1;
//                                priorityimageedit = "purpleimage";
//                                var data = PriorityListData.FirstOrDefault(x => x.id == result.task.fK_PriorityID).IsChecked = true;
//                                //PriorityListData.FirstOrDefault().IsChecked = true;
//                            }
//                        }

//                        else
//                        {
//                            prioritynameEdit = result.task.priority;
//                            if (result.task.fK_PriorityID == (int)Priotities.Critical)
//                            {
//                                priorityimageedit = "darkorange";
//                                priorityIdedit = (int)Priotities.Critical;
//                                prioritynameEdit = AppResource.crtiticlenumtext;
//                                var data = PriorityListData.FirstOrDefault(x => x.id == result.task.fK_PriorityID).IsChecked = true;
//                            }
//                            else if (result.task.fK_PriorityID == (int)Priotities.Normal)
//                            {
//                                priorityIdedit = (int)Priotities.Normal;
//                                priorityimageedit = "blueimage";
//                                prioritynameEdit = AppResource.normalenumtext;
//                                var data = PriorityListData.FirstOrDefault(x => x.id == result.task.fK_PriorityID).IsChecked = true;
//                            }
//                            else if (result.task.fK_PriorityID == (int)Priotities.High)
//                            {
//                                priorityIdedit = (int)Priotities.High;
//                                prioritynameEdit = AppResource.highenumtext;
//                                priorityimageedit = "orangeimage";
//                                var data = PriorityListData.FirstOrDefault(x => x.id == result.task.fK_PriorityID).IsChecked = true;


//                            }
//                            else
//                            {
//                                priorityIdedit = (int)Priotities.Low;
//                                prioritynameEdit = AppResource.lowenumtext;
//                                priorityimageedit = "purpleimage";
//                                var data = PriorityListData.FirstOrDefault(x => x.id == result.task.fK_PriorityID).IsChecked = true;
//                                //PriorityListData.FirstOrDefault().IsChecked = true;
//                            }

//                        }
//                        #endregion
//                        #region refrence data
//                        if (temprefid != 0 && temprefid != null)
//                        {
//                            refidEdit = temprefid;
//                            temprefrencename = refrenceList.FirstOrDefault(x => x.id == (temprefid == null ? "" : temprefid.ToString())).name;
//                        }
//                        else
//                        {
//                            if (selectedid != 0)
//                            {
//                                refidEdit = selectedid;
//                            }
//                            else
//                            {
//                                refidEdit = result.task.fK_RefrenceID;
//                            }
//                        }




//                        //var refname = refrenceList.Where(x => x.id == result.task.fK_RefrenceID).SingleOrDefault()?.name;
//                        // referenceedit = result.task.reference;
//                        // refrenceList.FirstOrDefault(x => x.id == result.task.fK_RefrenceID).IsCheckedRefe = true;
//                        #endregion

//                        if (seletedemp != null)
//                        {
//                            employeenameEdit = seletedemp;
//                            ResponsibleID = selectempid;
//                        }

//                        FilesListEdit = new ObservableCollection<FileDataModel>();
//                        FilesListEdit.Clear();

//                        var path = DependencyService.Get<IFileHelper>().file(AppConstants.AppName);
//                        if (!Directory.Exists(path))
//                        {
//                            Directory.CreateDirectory(path);
//                        }
                        
//                        string filepath;
//                        foreach (var item in result.attachments)
//                        {
//                            var checkextension = Path.GetExtension(item.title);
//                            if (checkextension.Equals(".png") || checkextension.Equals(".jpg") || checkextension.Equals(".jpeg"))
//                            {
//                                ImageTypeedit = "picture";
//                            }
//                            else
//                            {
//                                ImageTypeedit = "pdf";
//                            }
//                            filepath = Path.Combine(path, item.title);
//                            FilesListEdit.Add(new FileDataModel { FileName = item.title, filepath = filepath, FileTime = item.creationDate.ToShortDateStringForView(), image = ImageTypeedit, AttachmentId = item.id });
                            
//                        }
//                        if (FilesListEdit.Count > 0)
//                        {
//                            FilelistVisbility2 = true;
//                        }
//                        if (IsSupportedEdit)
//                        {
//                            sireneditcheck = "redsiren";
//                        }
//                        else
//                        {
//                            sireneditcheck = "siren";
//                        }

//                    }
//                    else
//                    {
//                        sireneditcheck = "siren";
//                        referenceEdit = AppResource.choosereftext;
//                        prioritynameEdit = AppResource.lowenumtext;
//                        priorityIdedit = 1;
//                        priorityimageedit = "purpleimage";
//                        StartDateEdit = "— — — — — — ";
//                        EndDateEdit = "— — — — — — ";
//                        ImageTypeedit = "picture";
//                        uploadagain = AppResource.uploadfileagaintext;
//                        FileNameEdit = AppResource.filenametext;

//                    }
//                }

//                isBusy = false;
//            }
//            catch (Exception exception)
//            {
//                var properties = new Dictionary<string, string>
//                       {
//                             { "_EditTaskViewModel", "gettaskbyid" },
//                       };
//                Crashes.TrackError(exception, properties);
//            }

//        }

//    }
//}
