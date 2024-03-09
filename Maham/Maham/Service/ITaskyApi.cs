using Refit;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Maham.Models;
using Maham.Models.User;
using Maham.Service.Model.Request.Dashboard;
using Maham.Service.Model.Request.Login;
using Maham.Service.Model.Request.Registeration;
using Maham.Service.Model.Request.Tasks;
using Maham.Service.Model.Request.User;
using Maham.Service.Model.Response;
using Maham.Service.Model.Response.Login;
using Maham.Service.Model.Response.Priorities;
using Maham.Service.Model.Response.Registeration;
using Maham.Service.Model.Response.Tasks;
using Maham.Service.Model.Response.User;
using Maham.Service.Model.Response.orgnization;
using Maham.Service.Model.Response.newclient;
using Maham.Service.Model.Request.newclient;
using Maham.Service.Model.Response.Notification;
using Maham.Service.Model;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Maham.Service
{
    public interface ITaskyApi
    {
        #region AUTHENTICATION API ENDPOINTS
        [Post("/api/Account/Authenticate")]
        Task<LoginResponse> Authenticate([Body]LoginRequest request);
        #endregion
        #region Registeration
        [Post("/Tasky/User/SignUp")]
        Task<RegisterationResponse> SignUp([Body]RegisterationRequest request);
        [Post("/Tasky/User/SubmitVerificationCode?verificationCode={verificationCode}&userId={userId}")]
        Task<SubmitVerficationCodeResponse> SubmitverificationCode(string verificationCode, string userId);
        [Post("/Tasky/User/SendVerificationCode?userId={userId}")]
        Task<SendVerficationCodeResponse> SendVerficationCode(string userId);
        #endregion
        #region User
        [Get("/api/User/Get?ID={ID}")]
        Task<Result> GetProfileUser([Header("Authorization")] string accessToken, string ID);

        [Post("/Tasky/User/UpdateUserPhoto")]
        Task<UploadProfileImageResponse> UploadProfileImage([Header("Authorization")] string accessToken, [Body]UploadProfileImageRequest request);

        [Post("/Tasky/User/UpdateUserProfile")]
        Task<UpdateUserProfileDataResponse> UpdateProfileData([Header("Authorization")] string accessToken, [Body]UpdateUserProfileRequest request);


        [Post("/Tasky/User/ResetPassowrd?userName={userName}&password={password}&newPassword={newPassword}")]
        Task<ResetPsswordResponse> ResetPassword([Header("Authorization")] string accessToken,string userName, string password,string newPassword);


        [Post("/Tasky/User/DeleteUserPhoto?userName={userName}")]
        Task<ServiceResponse> DeleteUserPhoto([Header("Authorization")]string accessToken, string userName);

        #endregion
        #region Exist orgnization
        [Get("/api/Account/GetTenentApiURL?code={code}")]
        Task<Result> GetTenant(string code);
        [Post("/Tasky/Tenant/RequestRegister")]
        Task <NewClientResponse> PostNewClient([Body]NewClientRequest request);
        #endregion
        #region get priority
        [Get("/api/Priority/GetPrioritysList")]
        Task<Result> GetPriority([Header("Authorization")] string accessToken);
        #endregion
        #region get Refrences
        [Get("/api/TaskSource/GetTaskSourcesList?userGroupID={userGroupID}&privilege={privilege}")]
        Task<Result> GetSource([Header("Authorization")] string accessToken, string userGroupID = null, int? privilege = null);
        #endregion
        #region get project
        [Get("/Tasky/Projects/GetProjects")]
        Task<ObservableCollection<ProjectListModel>> GetprojectList();
        [Get("/Tasky/Projects/GetUserProjects?userId={userId}")]
        Task<ObservableCollection<ProjectListModel>> GetUserProject(string userId);
        #endregion
        #region get assign employee
        [Get("/api/Task/NEWGetResponsiblesDDL_Mobile?Page={Page}&PageSize={PageSize}")]
        Task<Result> GetEmployees([Header("Authorization")] string accessToken, int Page, int PageSize);

        [Get("/api/Task/GetUserGroupAssignees?Id={Id}&privilege={privilege}")]
        Task<ResultData<Entity>> GetUserGroupAssignees([Header("Authorization")] string accessToken, string Id, int privilege);

        [Get("/api/Task/GetPositionAssignees?Id={Id}")]
        Task<Result> GetPositionAssignees([Header("Authorization")] string accessToken, string Id);

        [Post("/api/Task/GetPositionAssignOnChange")]
        Task<Result> GetPositionAssignOnChange([Header("Authorization")] string accessToken, [Body] string[] IDs);

        [Get("/api/Task/GetPositionAssign?Id={Id}")]
        Task<ResultData<Entity>> GetPositionAssign([Header("Authorization")] string accessToken, string Id);
        #endregion
        #region get user group
        [Get("/api/UserGroup/GetUserGroupsListByCurrentUserID?taskid={taskid}")]
        Task<Result> GetUserGroupsList([Header("Authorization")] string accessToken, Guid taskid);
        #endregion
        #region get position
        [Get("/api/EntityUser/GetUserPositionsByCurrentUserID?taskid={ID}")]
        Task<Result> GetPositionsList([Header("Authorization")] string accessToken, Guid ID);
        #endregion
        #region get Reassaign employee
        [Post("/Tasky/User/GetReAssigneeOf?userId={userId}")]
        Task<ObservableCollection<AssiginEmployeeList>> GetReassignList([Header("Authorization")] string accessToken, string userId);
        #endregion
        #region Apply Reassign
        [Post("/Tasky/User/ApplyReAssign?assignedByUser={assignedByUser}&assignedToUser={assignedToUser}&taskId={taskId}")]
        Task<ReassignApplyResponse> ApplyReassign([Header("Authorization")] string accessToken, int assignedByUser, int assignedToUser,int taskId);
        #endregion
        #region Priorities
        [Post("/Tasky/Task/GetTasksPerPrioirites?userId={userId}")]
        Task<ObservableCollection<PrioritiesResponse>> GetPriorities([Header("Authorization")] string accessToken, string userId);

        [Post("/Tasky/Task/GetTasksPerPrioirites?userId={userId}&FromDate={FromDate}&ToDate={ToDate}&StatusID={StatusID}&SectorID={SectorID}&PriorityID={priorityID}&AssigneeID={assigneeID}&projectID={projectID}")]
        Task<ObservableCollection<PrioritiesResponse>> GetFilterdPriorities([Header("Authorization")] string accessToken, string userId
            , string FromDate = "", string ToDate = "", int StatusID = 0, int SectorID = 0, int priorityID = 0, int assigneeID = 0, int projectID = 0);

        [Post("/Tasky/Task/getPriortyTasks?userId={userId}&priorityId={priorityId}&SectorID={SectorID}&StatusID={StatusID}&FromDate={FromDate}&ToDate={ToDate}&AssigneeID={AssigneeID}&projectID={projectID}")]
        Task<ObservableCollection<PriorirtiesDetailsResponse>> GetPrioritiesDetails([Header("Authorization")] string accessToken, string userId,string priorityId,
            int SectorID = 0, int StatusID = 0, string FromDate = "", string ToDate = "",  int AssigneeID = 0, int projectID = 0);
        #endregion
        #region add task
        [Post("/api/Task/Create")]
        Task<Result> newtask([Header("Authorization")] string accessToken, [Body]TaskDto request);

        [Post("/api/Task/UpdateTaskERCallStatus?ID={ID}&ERStatus={ERStatus}")]
        Task<Result> UpdateTaskERCallStatus([Header("Authorization")] string accessToken, Guid ID, bool ERStatus = false);

        [Post("/api/Task/UpdateTaskERCallEntityManagerRole?ID={ID}")]
        Task<Result> UpdateTaskERCallEntityManagerRole([Header("Authorization")] string accessToken, Guid ID, [Body]Value3 EntityManagerRole);

        [Get("/api/Task/GetEntitiesERCallList")]
        Task<Result> GetEntitiesERCallList([Header("Authorization")] string accessToken);


        [Get("/api/Task/GetTaskDueDateRequests?Page={page}&PageSize={pageSize}&TaskID={TaskID}")]
        Task<Result> GetTaskDueDateRequests([Header("Authorization")] string accessToken, int page, int pageSize, Guid TaskID);

        [Post("/api/Task/CreateDueDateRequest?TaskID={TaskID}")]
        Task<Result> CreateDueDateRequest([Header("Authorization")] string accessToken, [Body]TaskDueDateRequestDto taskDueDateRequestDto, Guid TaskID);

        [Post("/api/Task/UpdateDueDateRequest?DueDateRequestID={DueDateRequestID}&StatusID={StatusID}")]
        Task<Result> UpdateDueDateRequest([Header("Authorization")] string accessToken, Guid DueDateRequestID, int StatusID);
        #endregion
        #region upload task attachment
        [Multipart]
        [Post("/api/Task/Upload_Mobile?taskid={taskId}&IsEvidence={IsEvidence}")]
        Task<Result> UploadTaskAttachment([Header("Authorization")] string accessToken, string taskId,bool IsEvidence, [AliasAs("files")] IEnumerable<StreamPart> streams);
        #endregion
        #region search
        [Post("/api/Task/ReturnTask?ID={taskId}")]
        Task<Result> UpdateTaskReturned([Header("Authorization")] string accessToken, string taskId);
        #endregion

        #region Attachment
        [Post("/api/Attachment/Create")]
        Task<Result> AddAttachment([Header("Authorization")] string accessToken, [Body]AttachmentDto request);

        [Delete("/api/Attachment/Delete?ID={attachId}")]
        Task<Result> DeleteAttachment([Header("Authorization")] string accessToken, string attachId);
        #endregion
        #region update task
        [Post("/api/Task/Update")]
        Task<Result> UpdateTask([Header("Authorization")] string accessToken, [Body]TaskDto request);

        [Post("/api/Task/UpdateProgress?ID={taskId}&progress={progress}")]
        Task<Result> UpdateTaskProgress([Header("Authorization")] string accessToken, string taskId, int progress);

        [Post("/Tasky/Task/GetTasksBy?search={search}&userId={userId}")]
        Task<ObservableCollection<SearchResponse>> SearchTask([Header("Authorization")] string accessToken,string search,int userId);

        [Post("/api/Task/CloseTask?ID={taskId}")]
        Task<Result> UpdateTaskClosed([Header("Authorization")] string accessToken, string taskId);

        [Post("/Tasky/Task/UpdateTaskUrgent?taskId={taskId}&urgentSupport={urgentSupport}&CurrentUserID={CurrentUserID}")]
        Task<NewTaskResponse> UpdateTaskUrgent([Header("Authorization")] string accessToken, int taskId, bool urgentSupport, int CurrentUserID);
        #endregion
        #region gettaskbyid
        [Post("/Tasky/Task/GetTaskById?taskId={taskid}&userId={userId}")]
        Task<GetTaskByIdResponse> GetTaskById([Header("Authorization")] string accessToken, string taskid,string userId);

        [Get("/api/Attachment/DownloadFileMobile?ID={AttachmentID}")]
        Task<Result> DownloadFile([Header("Authorization")] string accessToken, string AttachmentID);

        [Get("/api/Task/Get?ID={taskid}&Language={language}")]
        Task<Result> GetTaskByIdForEdit([Header("Authorization")] string accessToken, string taskid, string language);

        [Post("/Tasky/Task/GetTaskHistory?taskId={taskid}")]
        Task<TaskChanges> GetTaskHistory([Header("Authorization")] string accessToken, string taskid);

        [Get("/api/Ability/Can?action={action}&module={module}")]
        Task<Result> Can([Header("Authorization")] string accessToken, string action, string module);

        [Get("/api/Task/RejectTask?ID={taskID}")]
        Task<string> RejectTask([Header("Authorization")] string accessToken, string taskID);
        #endregion
        #region get task attachhment
        [Get("/api/Task/GetAllAttachmentsByTaskID?taskid={taskid}")]
        Task<Result> GetTaskAttachment([Header("Authorization")] string accessToken, string taskid);
        #endregion
        #region gettaskcomment
        [Get("/api/Task/GetAllCommentsByTaskID?taskid={taskid}")]
        Task<Result> GetTaskComments([Header("Authorization")] string accessToken, string taskid);
        #endregion
        #region TasksRepresentationInfo
        [Post("/Tasky/Task/GetTasksRepresentationInfo?userId={userId}")]
        Task<ObservableCollection<TabsResponse>> GetTabs([Header("Authorization")] string accessToken, string userId);
        #endregion
        #region TasksRepresentationInfo
        [Get("/api/Dashboard/GetUserChartsCategories")]
        Task<Result> GetDashboardTabs([Header("Authorization")] string accessToken);
        #endregion
        #region addtaskcomment
        [Post("/api/Task/AddCommentGetAllComments?taskid={taskid}")]
        Task<Result> Addataskcomment([Header("Authorization")] string accessToken, [Body]CommentDto request, string taskid);
        #endregion
        #region refresh token
        [Post("/Tasky/Token")]
        Task<RefreshTokenResponse> RefreshToken([Body]LoginRequest request);
        #endregion
        #region UserTasks
        [Post("/Tasky/Task/GetUserTasks?userId={userId}&representationId={tabId}")]
        Task<ObservableCollection<UserTasksResponse>> GetUserTasks([Header("Authorization")] string accessToken, string userId, int tabId);
        #endregion
        #region DeleteTask
        [Post("/api/Task/DeleteTask?ID={taskId}")]
        Task<Result> DeleteTask([Header("Authorization")] string accessToken, string taskID);
        #endregion
        #region ChartsCategories
        //[Get("/api/Dashboard/GetCategoryCharts?categoryId={categoryId}&Filter={Filter}")]
        //Task<Result> GetCategoryCharts([Header("Authorization")] string accessToken, int categoryId,DashboardFilterDto Filter);
        //[Get("/api/Dashboard/GetCategoryCharts?categoryId={categoryId}&FromDate={FromDate}&ToDate={ToDate}&StatusId={StatusId}&PriorityId={PriorityId}&EntityId={EntityId}&ID={ID}&RoleID={RoleID}&Type={Type}&SourceId={SourceId}")]
        //Task<Result> GetCategoryCharts([Header("Authorization")] string accessToken, int categoryId, DateTime FromDate, DateTime ToDate, int StatusId, int PriorityId, Guid EntityId, string ID, string RoleID, int Type, int SourceId);
        //[Post("/Tasky/DashBoard/GetCategoryCharts?userId={userId}&categoryId={categoryId}&fromDate={fromDate}&toDate={toDate}&statusID={statusID}&employeeID={employeeID}&sectorID={sectorID}&priorityID={priorityID}&Mode={Mode}")]
        [Get("/api/Dashboard/GetCategoryCharts?categoryId={categoryId}&Filter={Filter}")]
        Task<Result> GetCategoryCharts([Header("Authorization")] string accessToken, int categoryId, string Filter);

        //[Post("/Tasky/DashBoard/GetCategoryCharts?userId={userId}&categoryId={categoryId}")]
        //Task<DashboardRootObjectModelApi> GetCategoryCharts([Header("Authorization")] string accessToken, string userId, string categoryId);
        #endregion
        #region Firebase

        [Post("/api/User/AddUserFireBaseToken")]
        Task<Result> AddUserFireBaseToken([Header("Authorization")]string accessToken, string prevToken, string nextToken, string userID, string lang);

        [Post("/api/User/DeleteUserFireBaseToken?userID={userId}&token={token}")]
        Task<Result> DeleteUserFireBaseToken([Header("Authorization")]string accessToken, string token, string userID);

        #endregion
        #region FilterRasks
        [Post("/Tasky/Task/GetUserTasks?userId={userId}&representationId={tabId}&FromDate={fromDate}&ToDate={toDate}&StatusID={statusID}&SectorID={sectorId}&PriorityID={priorityID}&AssigneeID={assigneeID}&projectID={projectID}")]
        Task<ObservableCollection<UserTasksResponse>> FilterTasks([Header("Authorization")] string accessToken, 
            string userId, int tabId, string fromDate = "", string toDate = "", int statusID = 0, int sectorId = 0, int priorityID = 0, int assigneeID = 0, int projectID = 0);
        #endregion
        #region Filter Tasks Params 
        [Get("/api/Entity/GetEntitiesHierarchy")]
        Task<ResultData<Entity>> GetEntities([Header("Authorization")] string accessToken);

        [Get("/api/Entity/GetEntitysList")]
        Task<Result> GetSectors([Header("Authorization")] string accessToken);

        //[Get("/api/UserGroup/GetUserGroupsList")]
        //Task<Result> GetUserGroupsList([Header("Authorization")] string accessToken);

        [Get("/api/TaskStatus/GetTaskStatusList")]
        Task<Result> GetStatus([Header("Authorization")] string accessToken);

        [Get("/Tasky/Projects/GetProjects")]
        Task<ObservableCollection<ListPopUpModel>> GetProjects([Header("Authorization")] string accessToken);

        [Get("/Tasky/Priority/GetPriority")]
        Task<ObservableCollection<ListPopUpModel>> GetPriorities([Header("Authorization")] string accessToken);
        #endregion

        #region Notification
        [Get("/api/Notification/GetNotificationHistory?userId={UserID}&pageNo={pageNo}")]
        Task<Result> GetNotificationHistory([Header("Authorization")]string accessToken, string UserID, int pageNo);

        [Post("/api/Notification/MarkNotificationAsRead?id={id}")]
        Task<Result> MarkNotificationAsRead([Header("Authorization")]string accessToken, string id);

        [Post("/api/Notification/SetPreferredLanguage?userId={userId}&deviceFireBaseToken={token}&lang={lang}")]
        Task<Result> SetPreferredLanguage([Header("Authorization")]string accessToken, string userId, string token, string lang);

        #endregion

        #region NewApis
        [Get("/api/View/GetAllTaskListViews")]
        Task<ResultData<TabsResponse>> GetAllTaskListViews([Header("Authorization")] string accessToken);

        [Get("/api/View/GetAllTaskListViewSections?ID={id}&Page={Page}&PageSize={PageSize}")]
        Task<Result> GetAllTaskListViewSections([Header("Authorization")] string accessToken, System.Guid id, int Page, int PageSize);

        //[Query(CollectionFormat.Multi)] List<Guid> not work
        //GetAllTaskListViewSectionData?Page=0&PageSize=10&ID=1F792981-545F-4BB1-AD33-036FEFFE35C1&UserID=74955b73-2f29-43e8-af68-6ff0c15e4e54&FullMode=false&StatusId=0&PriorityId=0&EntityId=00000000-0000-0000-0000-000000000000&SourceId=0&UserGroupId=00000000-0000-0000-0000-000000000000
        //[Get("/api/View/GetAllTaskListViewSectionData?Page={page}&PageSize={pageSize}&ID={sectionId}&FieldType={fieldType}&UserID={userId}&FullMode={fullMode}&StatusId={statusID}&PriorityId={priorityID}&SourceId={sourceId}&UserGroupId={userGroupId}&SearchTask={searchTitle}&FromDate={FromDate}&ToDate={ToDate}&ResponsibleID.ID={ResponsibleID}&ResponsibleID.RoleID={ResponsibleIDRoleID}&ResponsibleID.Type={ResponsibleIDType}&Entities={Entities}")]
        //Task<ResultData<Model.Response.Tasks.Task>> GetAllTaskListViewSectionData([Header("Authorization")] string accessToken,int page, int pageSize, Guid sectionId, string fieldType, string userId, bool fullMode, int statusID , int priorityID, int sourceId, Guid userGroupId, string searchTitle, string FromDate, string ToDate, string ResponsibleID, string ResponsibleIDRoleID, int ResponsibleIDType, [Query(CollectionFormat.Multi)] Guid[] Entities);

        [Post("/api/View/GetAllTaskListViewSectionData?Page={page}&PageSize={pageSize}&ID={sectionId}&FieldType={fieldType}&UserID={userId}&FullMode={fullMode}")]
        Task<ResultData<Model.Response.Tasks.Task>> GetAllTaskListViewSectionData([Header("Authorization")] string accessToken, [Body] FilterDto filterTask, int page, int pageSize, Guid sectionId, string fieldType, string userId, bool fullMode);


        // /api​/View​/GetAllTaskListUserGroupViews
        [Get("/api/View/GetAllTaskListUserGroupViews")]
        Task<ResultData<TabsResponse>> GetAllTaskListUserGroupViews([Header("Authorization")] string accessToken);

        [Get("/api/View/GetAllClosedTasksViews")]
        Task<ResultData<TabsResponse>> GetAllClosedTasks([Header("Authorization")] string accessToken);

        #endregion

        #region Edit task
        [Get("/api/Task/GetPositionReassign?Id={Id}")]
        Task<ResultData<Entity>> GetPositionReassign([Header("Authorization")] string accessToken, string Id);

        [Post("/api/Task/GetPositionReassignOnChange")]
        Task<Result> GetPositionReassignOnChange([Header("Authorization")] string accessToken, [Body] string[] IDs);

        [Get("/api/Entity/GetEntityIdByUserId?Id={Id}")]
        Task<Result> GetEntityIdByUserId([Header("Authorization")] string accessToken, string Id);
        #endregion
    }
}
