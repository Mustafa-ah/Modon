using Plugin.Settings;
using Plugin.Settings.Abstractions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Maham.Enums;
using Maham.ViewModels;
using Maham.Views;
using Maham.Models;
using Maham.Service.Model.Response;
using Maham.Service.Model.Response.Tasks;
using Newtonsoft.Json;

namespace Maham.Setting
{
    public static class Settings
    {

        public static int NotificationCount { get; set; }
        private static ISettings AppSettings
        {
            get
            {
                return CrossSettings.Current;
            }
        }

        #region Setting Constants
        const string SettingVr = "3";
        private const string SettingsKey = "settings_key" + SettingVr;
        private const string UserIdKey = "userid" + SettingVr;
        private const string AccesstokenKey = "token" + SettingVr;
        private const string usernamekey = "unsername" + SettingVr;
        private const string EmailKey = "email" + SettingVr;
        private const string isloginkey = "loged" + SettingVr;
        private const string issuperadminkey = "issuperadmin" + SettingVr;
        private const string IsEntityManagerKey = "IsEntityManager" + SettingVr;
        private const string TaskIdKey = "taskid" + SettingVr;
        private const string passwordkey = "password" + SettingVr;
        private const string FirebaseTokenKey = "FirebaseTokenKey" + SettingVr;
        private const string HasTenantskey = "tenants" + SettingVr;
        private const string apiurlkey = "apiurl" + SettingVr;
        private const string TenantTypeKey = "tenanttype" + SettingVr;
        private const string isSelfAssignedkey = "selfassign" + SettingVr;
        private const string projectidkey = "projectid" + SettingVr;
        private const string AppLangKey = "AppLang" + SettingVr;
        private const string AbilityKey = "Ability" + SettingVr;
        private const string creatorPrivilegeKey = "creatorPrivilege" + SettingVr;
        private const string assigneePrivilegeKey = "assigneePrivilege" + SettingVr;
        private const string UserGroupListKey = "UserGroupList" + SettingVr;
        private const string GeneralIdKey = "GeneralId" + SettingVr;
        private const string GeneralIdStringKey = "GeneralIdString" + SettingVr;
        private const string PositionKey = "Position" + SettingVr;
        private const string UserGroupKey = "UserGroup" + SettingVr;
        private const string AllowPushNotificationKey = "AllowPushNotification" + SettingVr;
        private const string AllowSignUpKey = "AllowSignUp" + SettingVr;
        private const string HasPositionskey = "HasPositionskey" + SettingVr;
        private const string HasUserGroupskey = "HasUserGroups" + SettingVr;
        private static readonly string SettingsDefault = string.Empty;

        #endregion

        private static List<NotPrioritiesPageViewModel> _tabs;

        public static List<NotPrioritiesPageViewModel> Tabs
        {
            get {
                if (_tabs == null)
                    return _tabs = new List<NotPrioritiesPageViewModel>();
                return _tabs; }
            set { _tabs = value; }
        }

        private static List<DashboardChildPageViewModel> _dashboardtabs;
        public static List<DashboardChildPageViewModel> Dashboardtabs
        {
            get
            {
                if (_dashboardtabs == null)
                    return _dashboardtabs = new List<DashboardChildPageViewModel>();
                return _dashboardtabs;
            }
            set { _dashboardtabs = value; }
        }
        private static List<DashboardChildPage> _dashboardtabsPage;
        public static List<DashboardChildPage> DashboardtabsPage
        {
            get
            {
                if (_dashboardtabsPage == null)
                    return _dashboardtabsPage = new List<DashboardChildPage>();
                return _dashboardtabsPage;
            }
            set { _dashboardtabsPage = value; }
        }
        //Dashboard filtter 
        private static Dictionary<string, string> _dashboardFiltter;

        public static Dictionary<string, string> DashboardFiltter
        {
            get
            {
                if (_dashboardFiltter == null)
                    return _dashboardFiltter = new Dictionary<string, string>();
                return _dashboardFiltter;
            }
            set
            {
                _dashboardFiltter = value;
            }
        }

        private static Dictionary<string, string> _lastdashboardFiltter;

        public static Dictionary<string, string> LastDashboardFiltter
        {
            get
            {
                if (_lastdashboardFiltter == null)
                    return _lastdashboardFiltter = new Dictionary<string, string>();
                return _lastdashboardFiltter;
            }
            set
            {
                _lastdashboardFiltter = value;
            }
        }


        public static string GeneralSettings
        {
            get
            {
                return AppSettings.GetValueOrDefault(SettingsKey, SettingsDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(SettingsKey, value);
            }
        }
        public static string ApiUrl
        {
            get
            {
                return AppSettings.GetValueOrDefault(apiurlkey, SettingsDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(apiurlkey, value);
            }
        }
        public static string Email
        {
            get => AppSettings.GetValueOrDefault(EmailKey, SettingsDefault);
            set => AppSettings.AddOrUpdateValue(EmailKey, value);

        }
        public static string projectId
        {
            get => AppSettings.GetValueOrDefault(projectidkey, SettingsDefault);
            set => AppSettings.AddOrUpdateValue(projectidkey, value);

        }
        public static string TaskId
        {
            get => AppSettings.GetValueOrDefault(TaskIdKey, SettingsDefault);
            set => AppSettings.AddOrUpdateValue(TaskIdKey, value);

        }
        public static string TenantTypes
        {
            get => AppSettings.GetValueOrDefault(TenantTypeKey, SettingsDefault);
            set => AppSettings.AddOrUpdateValue(TenantTypeKey, value);

        }
        public static string PasswordData
        {
            get => AppSettings.GetValueOrDefault(passwordkey, SettingsDefault);
            set => AppSettings.AddOrUpdateValue(passwordkey, value);

        }
        public static string UserName
        {
            get => AppSettings.GetValueOrDefault(usernamekey, SettingsDefault);
            set => AppSettings.AddOrUpdateValue(usernamekey, value);
        }
        public static string ArabicUserName
        {
            get => AppSettings.GetValueOrDefault(nameof(ArabicUserName), SettingsDefault);
            set => AppSettings.AddOrUpdateValue(nameof(ArabicUserName), value);
        }
        public static string UserId
        {
            get => AppSettings.GetValueOrDefault(UserIdKey, SettingsDefault);
            set => AppSettings.AddOrUpdateValue(UserIdKey, value);
        }
        public static bool IsLoged
        {
            get => AppSettings.GetValueOrDefault(isloginkey, false);
            set => AppSettings.AddOrUpdateValue(isloginkey, value);
        }
        public static string AccessToken
        {
            get => AppSettings.GetValueOrDefault(AccesstokenKey, SettingsDefault);
            set => AppSettings.AddOrUpdateValue(AccesstokenKey, value);
        }
        public static bool HasTenants
        {
            get => AppSettings.GetValueOrDefault(HasTenantskey, false);
            set => AppSettings.AddOrUpdateValue(HasTenantskey, value);
        }
        public static bool IsRtl
        {
            get
            {
                var isRtl = new Helpers.Helper().CurrentLanguage() != 1;
                return isRtl;
            }
        }
        public static bool IsSelfAssigned
        {
            get => AppSettings.GetValueOrDefault(isSelfAssignedkey, false);
            set => AppSettings.AddOrUpdateValue(isSelfAssignedkey, value);
        }
        public static string FirebaseToken
        {
            get => AppSettings.GetValueOrDefault(FirebaseTokenKey, SettingsDefault);
            set => AppSettings.AddOrUpdateValue(FirebaseTokenKey, value);
        }

        public static string AppLang
        {
            get => AppSettings.GetValueOrDefault(AppLangKey, "");
            set => AppSettings.AddOrUpdateValue(AppLangKey, value);
        }

        public static int GeneralId
        {
            get => AppSettings.GetValueOrDefault(GeneralIdKey, 0);
            set => AppSettings.AddOrUpdateValue(GeneralIdKey, value);
        }

        public static string GeneralId_string
        {
            get => AppSettings.GetValueOrDefault(GeneralIdStringKey, "");
            set => AppSettings.AddOrUpdateValue(GeneralIdStringKey, value);
        }
        public static bool AllowPushNotification
        {
            get => AppSettings.GetValueOrDefault(AllowPushNotificationKey, true);
            set => AppSettings.AddOrUpdateValue(AllowPushNotificationKey, value);
        }

        public static bool AllowSignUp
        {
            get => AppSettings.GetValueOrDefault(AllowSignUpKey, true);
            set => AppSettings.AddOrUpdateValue(AllowSignUpKey, value);
        }

        public static bool FullMode
        {
            get => AppSettings.GetValueOrDefault(nameof(FullMode), false);
            set => AppSettings.AddOrUpdateValue(nameof(FullMode), value);
        }

        public static int TasksMode
        {
            get => AppSettings.GetValueOrDefault(nameof(TasksMode), 1);
            set => AppSettings.AddOrUpdateValue(nameof(TasksMode), value);
        }
        public static bool HasUserGroups
        {
            get => AppSettings.GetValueOrDefault(HasUserGroupskey, false);
            set => AppSettings.AddOrUpdateValue(HasUserGroupskey, value);
        }
        public static bool HasPositions
        {
            get => AppSettings.GetValueOrDefault(HasPositionskey, false);
            set => AppSettings.AddOrUpdateValue(HasPositionskey, value);
        }
        public static string Position
        {
            get => AppSettings.GetValueOrDefault(PositionKey, "");
            set => AppSettings.AddOrUpdateValue(PositionKey, value);
        }
        public static string UserGroup
        {
            get => AppSettings.GetValueOrDefault(UserGroupKey, "");
            set => AppSettings.AddOrUpdateValue(UserGroupKey, value);
        }
        public static Value3 EmergencyCall
        { get; set; }
        public static List<EmergencyCallsDDL> EmergencyCallList
        { get; set; }

        public static bool IsSuperAdmin
        {
            get => AppSettings.GetValueOrDefault(issuperadminkey, false);
            set => AppSettings.AddOrUpdateValue(issuperadminkey, value);
        }

        public static bool IsEntityManager
        {
            get => AppSettings.GetValueOrDefault(IsEntityManagerKey, false);
            set => AppSettings.AddOrUpdateValue(IsEntityManagerKey, value);
        }
        public static List<RoleModulePrivilege> Ability
        {
            get { return JsonConvert.DeserializeObject<List<RoleModulePrivilege>>(AppSettings.GetValueOrDefault(AbilityKey, "[{}]")); }
            set => AppSettings.AddOrUpdateValue(AbilityKey, JsonConvert.SerializeObject(value));
        }

        public static object SelectedEntity
        {
            get { return JsonConvert.DeserializeObject<Entity>(AppSettings.GetValueOrDefault(nameof(SelectedEntity), "{}")); }
            set => AppSettings.AddOrUpdateValue(nameof(SelectedEntity), JsonConvert.SerializeObject(value));
        }

        public static List<Guid> UserGroupList
        {
            get { return JsonConvert.DeserializeObject<List<Guid>>(AppSettings.GetValueOrDefault(UserGroupListKey, "[{}]")); }
            set => AppSettings.AddOrUpdateValue(UserGroupListKey, JsonConvert.SerializeObject(value));
        }

        public static List<string> creatorPrivilege
        {
            get { return JsonConvert.DeserializeObject<List<string>>(AppSettings.GetValueOrDefault(creatorPrivilegeKey, "[{}]")); }
            set => AppSettings.AddOrUpdateValue(creatorPrivilegeKey, JsonConvert.SerializeObject(value));
        }

        public static List<string> assigneePrivilege
        {
            get { return JsonConvert.DeserializeObject<List<string>>(AppSettings.GetValueOrDefault(assigneePrivilegeKey, "[{}]")); }
            set => AppSettings.AddOrUpdateValue(assigneePrivilegeKey, JsonConvert.SerializeObject(value));
        }
    }
}

