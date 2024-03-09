using Maham.Bases;
using Maham.Models;
using Maham.Resources;
using Maham.Service;
using Maham.Service.General;
using Maham.Setting;
using Microsoft.AppCenter.Crashes;
using Prism.Navigation;
using Refit;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Maham.ViewModels
{
    public class DepartmentsViewModel : BaseViewModel
    {

        //private ObservableCollection<object> checkedEntities;
        //public ObservableCollection<object> CheckedEntities
        //{
        //    get { return checkedEntities; }
        //    set { this.checkedEntities = value; }
        //}

        private ObservableCollection<Entity> entities;
        public ObservableCollection<Entity> Entities
        {
            get { return entities; }
            set { entities = value; RaisePropertyChanged(); }
        }

        public List<Entity> EntitiesSource { get; set; }

        public object SelectedEntity { get; set; }
        public List<string> SelectedIDs { get; set; }

        public string DepartmentName { get; set; }
        public string SearchWord { get; set; }

        public bool IsBusy { get; set; }

        public bool AsUser { get; set; }
        public bool ForEdit { get; set; }
        public string UserId { get; set; }

        #region Commands
        public ICommand SearchCommand { get; set; }
        public ICommand OkCommand { get; set; }
        #endregion

        public DepartmentsViewModel(INavigationService _NavigationServices) : base(_NavigationServices)
        {
            SearchCommand = new Command(SearchCommandExcute);
            OkCommand = new Command(OkCommandExcute);

            //CheckedEntities = new ObservableCollection<object>();
            SelectedEntity = Settings.SelectedEntity;

            //GetData();

            SelectedIDs = new List<string>();
           // CheckedEntities.CollectionChanged += CheckedEntities_CollectionChanged;
        }

        public async Task GetData()
        {
            try
            {
                int Privilege = ForEdit ? (int)Enums.Privilege.Reassign : (int)Enums.Privilege.Assign;

                if (AsUser)
                {
                    EntitiesSource = await GetEntitiesDataListForPosition(UserId);
                }
                //else if (ForEdit)
                //{
                //    EntitiesSource = await GetEntitiesDataListForEdit(UserId);
                //}
                else
                {
                    EntitiesSource = await GetEntitiesDataListForUserGroup(UserId, Privilege);
                }
                
                ObservableCollection<Entity> entities = new ObservableCollection<Entity>();
                foreach (var item in EntitiesSource)
                {
                    entities.Add(item);
                    FindSelected(item);
                }

                Entities = entities;

            }
            catch (Exception exception)
            {
                var properties = new Dictionary<string, string>
                       {
                             { "DepartmentsViewModel", "GetData" },
                       };
                Crashes.TrackError(exception, properties);
            }
        }

        private void OkCommandExcute(object obj)
        {
            FillIDs(SelectedEntity as Entity);
            MessagingCenter.Send(this, "EntitySelected", SelectedEntity);
            PopupNavigation.Instance.PopAsync();
        }

       private void FillIDs(Entity entity)
        {
            if (entity == null)
            {
                return;
            }
            if (entity.Active)
            {
                SelectedIDs.Add(entity.Value.ToString());
            }

            if (entity.Childern == null)
            {
                return;
            }

            foreach (var item in entity.Childern)
            {
                FillIDs(item);
            }
        }

        private void FindSelected(Entity entity)
        {
            if (entity == null)
            {
                return;
            }

            if (entity.Value == ((Entity)SelectedEntity).Value)
            {
                entity.LabelColor = Color.Red;
                return;
            }

            if (entity.Childern == null)
            {
                return;
            }

            foreach (var item in entity.Childern)
            {
                FindSelected(item);
            }
            
        }
       // public List<Entity> PreviousCheckedEntities { get; set; }
        //private void AddEntity(Entity entity)
        //{
        //    if (entity == null)
        //    {
        //        return;
        //    }

        //    if (PreviousCheckedEntities.Any(ent => ent.Value == entity.Value))
        //    {
        //        CheckedEntities.Add(entity);
        //    }

        //    if (entity.Childern == null)
        //    {
        //        return;
        //    }

        //    foreach (var item in entity.Childern)
        //    {
        //        AddEntity(item);
        //    }
        //}

        /*
        private void CheckedEntities_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            try
            {
                DepartmentName = "";

                if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
                {
                    foreach (var item in e.NewItems)
                    {
                        if (item is Entity entity)
                        {
                            if (!CheckedEntitiesUI.Any(en => en.Value == entity.Value))
                            {
                                CheckedEntitiesUI.Add(entity);
                            }
                        }

                    }
                }
                else if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Remove)
                {
                    foreach (var item in e.OldItems)
                    {
                        if (item is Entity entity)
                        {
                            CheckedEntitiesUI.Remove(entity);
                        }

                    }
                }

                DepartmentName = CheckedEntitiesUI.Count == 0 ? AppResource.All : "";
            }
            catch (Exception exception)
            {

                var properties = new Dictionary<string, string>
                       {
                             { "DepartmentsViewModel", "CheckedEntities_CollectionChanged" },
                       };
                Crashes.TrackError(exception, properties);

            }

        }
        */
        private async void SearchCommandExcute(object obj)
        {
            try
            {
                await System.Threading.Tasks.Task.Run(() =>
                {
                    Entities = new ObservableCollection<Entity>();
                    Search(EntitiesSource[0]);
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        private void Search(Entity entity)
        {
            if (entity == null)
            {
                return;
            }

            if (entity.Text.ToLower().Contains(SearchWord.ToLower()))
            {
                Entities.Add(entity);
            }

            if (entity.Childern == null)
            {
                return;
            }

            foreach (var entity2 in entity.Childern)
            {
                Search(entity2);
            }
        }

        private async Task<List<Entity>> GetEntitiesDataListForPosition(string userId)
        {
            IsBusy = true;
            List<Entity> lst = new List<Entity>();
            var api = RestService.For<ITaskyApi>(new System.Net.Http.HttpClient(new HttpLoggingHandler()) { BaseAddress = new Uri(Settings.ApiUrl) });
            try
            {
                var result = await api.GetPositionAssign("Bearer " + Settings.AccessToken, userId);
                //List<ListPopUpModel_Guid> x = new List<ListPopUpModel_Guid>();
                IsBusy = false;
                return result.Data;
            }
            catch (Exception exception)
            {
                var properties = new Dictionary<string, string>
                       {
                             { "filterpopviewmodel", "getsectordatalist" },
                       };
                Crashes.TrackError(exception, properties);
            }
            IsBusy = false;
            return lst;
        }
        private async Task<List<Entity>> GetEntitiesDataListForUserGroup(string userId, int privilege)
        {
            IsBusy = true;
            List<Entity> lst = new List<Entity>();
            var api = RestService.For<ITaskyApi>(new System.Net.Http.HttpClient(new HttpLoggingHandler()) { BaseAddress = new Uri(Settings.ApiUrl) });
            try
            {
                var result = await api.GetUserGroupAssignees("Bearer " + Settings.AccessToken, userId, privilege);
                //List<ListPopUpModel_Guid> x = new List<ListPopUpModel_Guid>();
                IsBusy = false;
                return result.Data;
            }
            catch (Exception exception)
            {
                var properties = new Dictionary<string, string>
                       {
                             { "filterpopviewmodel", "getsectordatalist" },
                       };
                Crashes.TrackError(exception, properties);
            }
            IsBusy = false;
            return lst;
        }

        private async Task<List<Entity>> GetEntitiesDataListForEdit(string userId)
        {
            IsBusy = true;
            List<Entity> lst = new List<Entity>();
            var api = RestService.For<ITaskyApi>(new System.Net.Http.HttpClient(new HttpLoggingHandler()) { BaseAddress = new Uri(Settings.ApiUrl) });
            try
            {
                var result = await api.GetPositionReassign("Bearer " + Settings.AccessToken, userId);
                //List<ListPopUpModel_Guid> x = new List<ListPopUpModel_Guid>();
                IsBusy = false;
                return result.Data;
            }
            catch (Exception exception)
            {
                var properties = new Dictionary<string, string>
                       {
                             { "filterpopviewmodel", "getsectordatalist" },
                       };
                Crashes.TrackError(exception, properties);
            }
            IsBusy = false;
            return lst;
        }
    }
}
