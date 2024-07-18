using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Windows.Input;

using Microsoft.AppCenter.Crashes;
using Plugin.Multilingual;
using Prism.Events;
using Prism.Navigation;
using Refit;
using Rg.Plugins.Popup.Services;
using Maham.Bases;
using Maham.Constants;
using Maham.Enums;
using Maham.Events;
using Maham.Extentions;
using Maham.Models;
using Maham.Resources;
using Maham.Service;
using Maham.Service.Model.Response.Tasks;
using Maham.Setting;
using Maham.Views;
using Xamarin.Forms;
using Maham.Service.Model.Response;
using Newtonsoft.Json;

namespace Maham.ViewModels
{
    public class FillterPopupViewModel : BaseViewModel
    {
        #region Public Properties
        //Priority
        
        public string priorityimage { get; set; }
        public bool clickedgridpriority { get; set; }
        public string priorityname { get; set; }
        public int priorityId { get; set; }
        public bool prioritylist { get; set; }
        public bool arrowprioritycheck { get; set; }

        public int PriortyListHeight { get; set; }
        public int StatusListHeight { get; set; }
        public int SectorListHeight { get; set; }
        public int EntitiesListHeight { get; set; }
        public int AssigneeListHeight { get; set; }
        public int SourceListHeight { get; set; }

        public ICommand CheckPriorityCommand { get; set; }
        public ICommand TogglePriorityCommand { get; set; }
        public ICommand ClearCommand { get; set; }
        public ICommand clickprioritygrid { get; set; }
        //Status
        public string Statusimage { get; set; }
        public bool clickedgridStatus { get; set; }
        public string Statusname { get; set; }
        public string Assigneename { get; set; }
        public string Sectorname { get; set; }
        public string Sourcename { get; set; }
        public string SearchTitle { get; set; }
        public int StatusId { get; set; }
        public Guid SectorId { get; set; }
        public Value2 AssigneeId { get; set; }
        public int SourceId { get; set; }
        public bool Statuslist { get; set; }
        public bool Sectorlist { get; set; }
        public bool Entitieslist { get; set; }
        public bool Assigneelist { get; set; }
        public bool Sourcelist { get; set; }
        public bool arrowStatuscheck { get; set; }
        public bool arrowSectorcheck { get; set; }
        public bool arrowAssigneecheck { get; set; }
        public bool arrowSourcecheck { get; set; }

        public ObservableCollection<ListPopUpModel> PriorityDataList { get; set; }
        public ObservableCollection<ListPopUpModel> StatusDataList { get; set; }
        public ObservableCollection<ListPopUpModel_Guid> SectorsDataList { get; set; }
        public List<ListPopUpModel_Guid> SectorsDataListSource { get; set; }
        public ObservableCollection<ResponsiblesDDL> AssigneeDataList { get; set; }
        public ObservableCollection<ListPopUpModel> SourcesDataList { get; set; }

        public ResponsiblesDDL lastitemAssignee { get; set; }


        public ICommand CheckStatusCommand { get; set; }
        public ICommand CheckSectorCommand { get; set; }
        public ICommand CheckEntityCommand { get; set; }
        public ICommand CheckAssigneeCommand { get; set; }
        public ICommand CheckSourceCommand { get; set; }
        public ICommand ToggleStatusCommand { get; set; }
        public ICommand ToggleSectorCommand { get; set; }
        public ICommand ToggleEntitesCommand { get; set; }
        public ICommand ToggleAssigneeCommand { get; set; }
        public ICommand ToggleSourceCommand { get; set; }
        public ICommand RemoveEntityCommand { get; set; }
        public ICommand clickStatusgrid { get; set; }

        public DateTime? MaxDate { get; set; }
        public DateTime StartDate { get; set; } = DateTime.Now;

        public DateTime EndDate { get; set; } = DateTime.Now;
        //Filter Excution 
        public ICommand FilterCommand { get; set; }
        FilterTask filterObj { get; set; }
        NotPrioritiesPageViewModel currentViewModel { get; set; }
        IEventAggregator eventAggregator;
        public ICommand CloseCommand { get; set; }
        public ICommand SearchSectorsCommand { get; set; }
        public ICommand LoadMoreAssigneeCommand { get; set; }


        public bool CanLoadMoreAssignee { get; set; }
        public bool IsDashboardFilter { get; set; }
        public bool IsTaskList { get; set; }
        public bool isBusy { get; set; }
        public bool isNotBusy { get; set; }
        public bool IsSectorsLoading { get; set; }

        public string SectorSearchWord { get; set; }
        #endregion

        private const int AssigneePageSize = 10;
        public int AssigneePage = 0;

        private int _filter_RowHeight = 45;
        public string SectorOrDepartmentString { get; set; }

        private ObservableCollection<object> checkedEntities;
        public ObservableCollection<object> CheckedEntities
        {
            get { return checkedEntities; }
            set { this.checkedEntities = value; }
        }

        private ObservableCollection<Entity> checkedEntitiesUI;
        public ObservableCollection<Entity> CheckedEntitiesUI
        {
            get { return checkedEntitiesUI; }
            set { this.checkedEntitiesUI = value; }
        }

        private ObservableCollection<Entity> entities;
        public ObservableCollection<Entity> Entities
        {
            get { return entities; }
            set { entities = value; RaisePropertyChanged(); }
        }

        public List<Entity> EntitiesSource { get; set; }

        //public FilterTask LastFilter { get; set; }

        public FillterPopupViewModel(IEventAggregator _eventAggregator, INavigationService _NavigationServices) : base(_NavigationServices)
        {
            isBusy = true;
            isNotBusy = false;
            MessagingCenter.Subscribe<MainTabbedPage, dynamic>(this, "FilterDashbord", (sender, arg) => {

                IsRTL = Settings.IsRtl;

                IsDashboardFilter = arg.IsDashboard;

                //IsTaskList = arg.TaskMode == (int)TasksMode.TaskList;
                //if (IsTaskList || IsDashboardFilter)
                //    SectorOrDepartmentString = AppResource.Sectortext;
                //else
                //    SectorOrDepartmentString = AppResource.usergrouptext;

                //SectorOrDepartmentString = AppResource.Sectortext;

                StartDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                EndDate = StartDate.AddMonths(1).AddDays(-1);

                isBusy = false;
                isNotBusy = true;


                if (arg.LastFilter != null)
                {
                    if (arg.LastFilter is FilterTask taskFilter)
                    {
                        filterObj = taskFilter;
                        SetLastFilter(filterObj);
                    }
                }

            });
            //Priority
            priorityimage = "";
            CheckPriorityCommand = new Command(CheckPriorityCommandExcute);
            TogglePriorityCommand = new Command(TogglePriorityCommandExcute);
            ClearCommand = new Command(ClearCommandExcute);
            clickprioritygrid = new Command(clickprioritygridExcute);
            priorityname = AppResource.All;


            Statusimage = "";
            CheckStatusCommand = new Command(CheckStatusCommandExcute);
            CheckSectorCommand = new Command(CheckSectorCommandExcute);
            CheckEntityCommand = new Command(CheckEntityCommandExcute);
            CheckAssigneeCommand = new Command(CheckAssigneeCommandExcute);
            CheckSourceCommand = new Command(CheckSourceCommandExcute);

            ToggleStatusCommand = new Command(ToggleStatusCommandExcute);
            ToggleAssigneeCommand = new Command(ToggleAssigneeCommandExcute);

            ToggleSectorCommand = new Command(ToggleSectorCommandExcute);
            ToggleEntitesCommand = new Command(ToggleEntitesCommandExcute);
            ToggleSourceCommand = new Command(ToggleSourceCommandExcute);

            clickStatusgrid = new Command(clickStatusgridExcute);
            Statusname = AppResource.All;
            Sectorname = AppResource.All;
            Assigneename = AppResource.All;
            Sourcename = AppResource.All;

            SectorOrDepartmentString = AppResource.BusinessUnit;

            RemoveEntityCommand = new Command(RemoveEntityCommandExcute);

            FilterCommand = new Command(Filter);
            CloseCommand = new Command(Close);

            SearchSectorsCommand = new Command(SearchSectorsCommandExcute);

            LoadMoreAssigneeCommand = new Command(LoadMoreAssigneeCommandExcute);

            CanLoadMoreAssignee = true;

            eventAggregator = _eventAggregator;

            //init heights
            PriortyListHeight = 280;
            StatusListHeight = 280;
            SectorListHeight = 280;
            AssigneeListHeight = 280;
            SourceListHeight = 280;
            EntitiesListHeight = 280;

            CheckedEntitiesUI = new ObservableCollection<Entity>(); 
            CheckedEntities = new ObservableCollection<object>();

            CheckedEntities.CollectionChanged += CheckedEntities_CollectionChanged;
        }


        private async void LoadMoreAssigneeCommandExcute(object obj)
        {
            try
            {
                if (!CanLoadMoreAssignee)
                {
                    return;
                }

                CanLoadMoreAssignee = false;

                AssigneePage++;

                var assigneeList = await GetAssigneeDataList(AssigneePage, AssigneePageSize);

                if (assigneeList.Count> 0)
                {
                    foreach (var item in assigneeList)
                    {
                        item.Text = IsRTL ? item.ArabicText : item.Text;
                        AssigneeDataList.Add(item);
                    }
                    CanLoadMoreAssignee = true;

                    AssigneeListHeight += assigneeList.Count * _filter_RowHeight;
                }
                //Syncfusion.SfPicker.XForms.SfPicker sfPicker = new Syncfusion.SfPicker.XForms.SfPicker();
                
            }
            catch (Exception ex)
            {

            }

        }

        private void RemoveEntityCommandExcute(object obj)
        {
            if (obj is Entity entity)
            {
                CheckedEntitiesUI.Remove(entity);
                CheckedEntities.Remove(entity);
            }
            
        }

        private void CheckedEntities_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            try
            {
                Sectorname = "";// CheckedEntities.Count > 0 ? "" : AppResource.All;
                                //CheckedEntitiesUI = new ObservableCollection<Entity>();
                                //foreach (var entity in e.NewItems)
                                //{
                                //    CheckedEntitiesUI.Add(entity as Entity);
                                //}

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

                Sectorname = CheckedEntitiesUI.Count == 0 ? AppResource.All : "";
                //Sectorname = CheckedEntities.Count.ToString();
            }
            catch (Exception exception)
            {

                var properties = new Dictionary<string, string>
                       {
                             { "fillterpopupviewmodel", "CheckedEntities_CollectionChanged" },
                       };
                Crashes.TrackError(exception, properties);

            }

        }

        private void CheckEntityCommandExcute(object obj)
        {
            var sector = obj as Entity;
            if (sector == null) return;

            checkedEntities.Add(sector);
            //sector.IsChecked = !sector.IsChecked;
            //if (sector.IsChecked)
            //{
            //    Sectorname = Sectorname + "," + sector.name;
            //}

            //SectorId = sector.id;

            //arrowSectorcheck = false;
        }

        private async void SearchSectorsCommandExcute(object obj)
        {
            try
            {
                await System.Threading.Tasks.Task.Run(() =>
                {
                    Entities = new ObservableCollection<Entity>();
                    SearchSectors(EntitiesSource[0]);
                });
                
                //SectorListHeight = SectorsDataList.Count * _filter_RowHeight;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        private void SearchSectors(Entity entity)
        {
            if (entity == null)
            {
                return;
            }

            if (entity.Text.ToLower().Contains(SectorSearchWord.ToLower()))
            {
                Entities.Add(entity);
            }

            if (entity.Childern == null)
            {
                return;
            }

            foreach (var entity2 in entity.Childern)
            {
                SearchSectors(entity2);
            }
        }

        private void CheckSourceCommandExcute(object obj)
        {
            try
            {
                var _source = obj as ListPopUpModel;
                if (_source == null) return;

                foreach (var item in SourcesDataList)
                {
                    item.IsChecked = false;
                }

                _source.IsChecked = true;

                Sourcename = _source.name;

                Sourcelist = false;
                SourceId = _source.id;

                arrowSourcecheck = false;
            }
            catch (Exception exception)
            {

                var properties = new Dictionary<string, string>
                       {
                             { "fillterpopupviewmodel", "checksourcecommand" },
                       };
                Crashes.TrackError(exception, properties);

            }
        }

        private async void ToggleSourceCommandExcute(object obj)
        {
            if (obj.ToString() == "1")
            {
                arrowSourcecheck = !arrowSourcecheck;
            }

            Sourcelist = !Sourcelist;

            if (SourcesDataList == null)
            {
                SourcesDataList = await GetSourcesDataList();
                SourceListHeight = SourcesDataList.Count * _filter_RowHeight;
            }
        }

        private void CheckAssigneeCommandExcute(object obj)
        {
            try
            {
                var _assigne = obj as ResponsiblesDDL;
                if (_assigne == null) return;
                if (_assigne != lastitemAssignee)
                {
                    if (lastitemAssignee != null)
                    {
                        lastitemAssignee.IsCheckedemployee = false;
                    }
                    //else
                    //{
                    //    AssigneeDataList.First().IsCheckedemployee = false;
                    //}
                    _assigne.IsCheckedemployee = true;
                }
                else
                {
                    _assigne.IsCheckedemployee = !_assigne.IsCheckedemployee;
                }
                lastitemAssignee = _assigne;
                Assigneename = _assigne.Text;

                Assigneelist = false;
                AssigneeId = _assigne.Value2;

                arrowAssigneecheck = false;
            }
            catch (Exception exception)
            {

                var properties = new Dictionary<string, string>
                       {
                             { "fillterpopupviewmodel", "checkassignmentcommand" },
                       };
                Crashes.TrackError(exception, properties);

            }
        }

        private async void ToggleAssigneeCommandExcute(object obj)
        {
            if (obj.ToString() == "1")
            {
                arrowAssigneecheck = !arrowAssigneecheck;
            }

            Assigneelist = !Assigneelist;

            if (AssigneeDataList == null)
            {
                bool assignee = await GetAssigneeDataList();
                AssigneeListHeight = 10 * _filter_RowHeight;
            }
        }

        private async void ToggleSectorCommandExcute(object obj)
        {
            if (obj.ToString() == "1")
            {
                arrowSectorcheck = !arrowSectorcheck;
            }

            Sectorlist = !Sectorlist;

            if (SectorsDataList == null)
            {
                SectorsDataList = await GetSectorsDataList();
                SectorsDataListSource = new List<ListPopUpModel_Guid>(SectorsDataList);
                //if (IsTaskList || IsDashboardFilter)
                //    SectorsDataList = await GetSectorsDataList();
                //else
                //    SectorsDataList = await GetUserGroupsDataList();
                SectorListHeight = SectorsDataList.Count * _filter_RowHeight;
            }
        }

        private async void ToggleEntitesCommandExcute(object obj)
        {
            if (obj.ToString() == "1")
            {
                arrowSectorcheck = !arrowSectorcheck;
            }

            Entitieslist = !Entitieslist;

            if (Entitieslist)
            {
                if (EntitiesSource == null)
                {
                    EntitiesSource = await GetEntitiesDataList();
                    // Entities = new ObservableCollection<Entity>();
                    CheckedEntitiesUI = new ObservableCollection<Entity>();

                    ObservableCollection<Entity> entities = new ObservableCollection<Entity>();
                    foreach (var item in EntitiesSource)
                    {
                        entities.Add(item);
                    }

                    Entities = entities;

                    if (filterObj != null)
                    {
                        AddEntity(Entities[0]);

                        Sectorname = checkedEntities.Count > 0 ? "" : AppResource.All;
                    }


                    //SectorListHeight = EntitiesSource.Sum(e => e.Childern[1].Childern.Count) * _filter_RowHeight;
                }
            }

           
        }

        private void AddEntity(Entity entity)
        {
            if (entity == null)
            {
                return;
            }

            if (filterObj.Entities.Any(ent => ent.Value == entity.Value))
            {
                CheckedEntities.Add(entity);
            }

            if (entity.Childern == null)
            {
                return;
            }

            foreach (var item in entity.Childern)
            {
                AddEntity(item);
            }
        }

        private void CheckSectorCommandExcute(object obj)
        {
            var sector = obj as ListPopUpModel_Guid;
            if (sector == null) return;

            sector.IsChecked = !sector.IsChecked;
            if (sector.IsChecked)
            {
                Sectorname = Sectorname + "," + sector.name;
            }

            // Statusimage = sector.image2;
            //  prioritycheckedimage = "circlechecked.png";
            //Sectorlist = false;
            SectorId = sector.id;

            arrowSectorcheck = false;

        }
        private void ClearCommandExcute(object obj)
        {
            try
            {
                priorityname = AppResource.All;
                Statusname = AppResource.All;
                Sectorname = AppResource.All;
                Assigneename = AppResource.All;
                Sourcename = AppResource.All;

                priorityId = 0;
                SectorId = Guid.Empty;
                StatusId = 0;
                AssigneeId = new Value2() { ID = Guid.Empty.ToString(), RoleID = "0", Type = -1 };
                SourceId = 0;
                SearchTitle = "";


                if (EntitiesSource != null)
                {
                    //Entities = new ObservableCollection<Entity>();
                    ObservableCollection<Entity> entities = new ObservableCollection<Entity>();
                    foreach (var item in EntitiesSource)
                    {
                        entities.Add(item);
                    }

                    Entities = entities;
                }

                CheckedEntities.Clear();
                CheckedEntitiesUI.Clear();

                var firstDayOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

                StartDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                EndDate = StartDate.AddMonths(1).AddDays(-1);

                filterObj = null;

                ClearDrops();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
          

        }
        #region Priority Methods

        private void ClearDrops()
        {
            if (PriorityDataList != null)
            {
                foreach (var item in PriorityDataList)
                {
                    item.IsChecked = false;
                }
                PriorityDataList.First().IsChecked = true;
            }

            if (StatusDataList != null)
            {
                foreach (var item in StatusDataList)
                {
                    item.IsChecked = false;
                }
                StatusDataList.First().IsChecked = true;
            }

            if (SectorsDataList != null)
            {
                foreach (var item in SectorsDataList)
                {
                    item.IsChecked = false;
                }
                SectorsDataList.First().IsChecked = true;
            }

            if (AssigneeDataList != null)
            {
                foreach (var item in AssigneeDataList)
                {
                    item.IsCheckedemployee = false;
                }
                AssigneeDataList.First().IsCheckedemployee = true;
            }

            if (SourcesDataList != null)
            {
                foreach (var item in SourcesDataList)
                {
                    item.IsChecked = false;
                }
                SourcesDataList.First().IsChecked = true;
            }
            //
        }

        private async void Close(object obj)
        {
            await PopupNavigation.Instance.PopAsync();

            MessagingCenter.Unsubscribe<MainTabbedPage, dynamic>(this, "FilterDashbord");
        }

        private void CheckPriorityCommandExcute(object obj)
        {
            var ItemSelected = obj as ListPopUpModel;
            if (ItemSelected == null) return;

            foreach (var item in PriorityDataList)
            {
                item.IsChecked = false;
            }

            ItemSelected.IsChecked = true;

            priorityname = ItemSelected.name;
            priorityimage = ItemSelected.image2;

            prioritylist = false;
            priorityId = ItemSelected.id;

            arrowprioritycheck = false;
           
        }

        private async void TogglePriorityCommandExcute(object obj)
        {
            if (obj.ToString() == "1")
            {
                arrowprioritycheck = !arrowprioritycheck;
            }

            prioritylist = !prioritylist;

            if (PriorityDataList == null)
            {
                PriorityDataList = await GetPriorityDataList();
                PriortyListHeight = PriorityDataList.Count * _filter_RowHeight;
            }
            // PopupNavigation.Instance.PushAsync(new PrioritiesLIstPage());
        }
        private void clickprioritygridExcute(object obj)
        {
            var data = (ListPopUpModel)obj;
            clickedgridpriority = true;
            prioritylist = true;
            clickedgridpriority = false;
            //if(clickedgridpriority)
            //{
            //    prioritylist = true;
            //}
            //else
            //{
            //    prioritylist = false;
            //}
        }
        #endregion
        #region Status Methods
        private void CheckStatusCommandExcute(object obj)
        {
            var ItemSelectedStatus = obj as ListPopUpModel;
            if (ItemSelectedStatus == null) return;

            foreach (var item in StatusDataList)
            {
                item.IsChecked = false;
            }
            ItemSelectedStatus.IsChecked = true;

            Statusname = ItemSelectedStatus.name;
            Statusimage = ItemSelectedStatus.image2;
            //  prioritycheckedimage = "circlechecked.png";
            Statuslist = false;
            StatusId = ItemSelectedStatus.id;

            arrowStatuscheck = false;
            // MessagingCenter.Send(new ListPopUpModel() { id = ItemSelected.id, name = priorityname, image2 = ItemSelected.image2 }, "PopUpData");
            // PopupNavigation.Instance.PopAsync();
        }

        private async void ToggleStatusCommandExcute(object obj)
        {
            if (obj.ToString() == "1")
            {
                arrowStatuscheck = !arrowStatuscheck;
            }

            Statuslist = !Statuslist;

            if (StatusDataList == null)
            {
                StatusDataList = await GetStatusDataList();
                StatusListHeight = StatusDataList.Count * _filter_RowHeight;
            }
            // PopupNavigation.Instance.PushAsync(new PrioritiesLIstPage());
        }
        private void clickStatusgridExcute(object obj)
        {
            var dataStatus = (ListPopUpModel)obj;
            clickedgridStatus = true;
            prioritylist = true;
            clickedgridStatus = false;
            //if(clickedgridpriority)
            //{
            //    prioritylist = true;
            //}
            //else
            //{
            //    prioritylist = false;
            //}
        }
        #endregion
       

        #region Filter Excutions 
        private void Filter(object obj)
        {
            if (StartDate > EndDate)
            {
                Application.Current.MainPage.DisplayAlert(AppResource.alerttext, AppResource.FilteredDateIncorrect, AppResource.oktext);
                return;
            }

            if (AssigneeId == null)
            {
                AssigneeId = new Value2() { ID = Guid.Empty.ToString(), RoleID = "0", Type = -1 };
            }

            filterObj = new FilterTask
            {
                StartDate = StartDate,
                EndDate = EndDate,
                PriorityID = priorityId,
                StatusID = StatusId,
                EntityId = SectorId,
                ResponsibleID = AssigneeId,
                SourceId = SourceId,
                SearchTitle = SearchTitle,
                IsActive = true
            };

            filterObj.Entities.Clear();
            foreach (var item in CheckedEntities)
            {
                Entity entity = item as Entity;
                filterObj.Entities.Add(entity);
            }

            //to show it when user open the filter agian
            filterObj.Status = Statusname;
            filterObj.SectorName = Sectorname;
            filterObj.AssigneeName = Assigneename;
            filterObj.SourceName = Sourcename;
            filterObj.Priority = priorityname;

            Close(this);

            if (IsDashboardFilter)
            {
                eventAggregator.GetEvent<FilterDashboardEvent>().Publish(filterObj);
            }
            else
            {
                eventAggregator.GetEvent<FilterTasksEvent>().Publish(filterObj);
            }

        }

        #endregion

        #region API Calls  
        public bool IsLoadingPriority { get; set; }
        private async Task<ObservableCollection<ListPopUpModel>> GetPriorityDataList()
        {
            ListPopUpModel al = new ListPopUpModel() { name = AppResource.All, id = 0, IsChecked = true };
            ObservableCollection<ListPopUpModel> lst = new ObservableCollection<ListPopUpModel>();
            var api = RestService.For<ITaskyApi>(new System.Net.Http.HttpClient(new HttpLoggingHandler()) { BaseAddress = new Uri(Settings.ApiUrl) });
            try
            {
                IsLoadingPriority = true;
                var result = await api.GetPriority("Bearer " + Settings.AccessToken);
                List<ListPopUpModel> x = new List<ListPopUpModel>();
                //x = (result.Data).ToObject<List<ListPopUpModel>>();
                x = JsonConvert.DeserializeObject<List<ListPopUpModel>>(Convert.ToString(result.Data));
                lst.Add(al);
                foreach (var item in x)
                {
                    lst.Add(new ListPopUpModel { id = item.id, name = IsRTL ? item.nameAr : item.name });
                }
            }
            catch (Exception exception)
            {
                var properties = new Dictionary<string, string>
                       {
                             { "fillterpopviewmodel", "getprioritiesdatalist" },
                       };
                Crashes.TrackError(exception, properties);
            }
            IsLoadingPriority = false;
            return lst;
        }

        public bool IsLoadingStatus { get; set; }
        private async Task<ObservableCollection<ListPopUpModel>> GetStatusDataList()
        {
            ListPopUpModel al = new ListPopUpModel() { name = AppResource.All, id = 0, IsChecked = true };
            ObservableCollection<ListPopUpModel> lst = new ObservableCollection<ListPopUpModel>();
            var api = RestService.For<ITaskyApi>(new System.Net.Http.HttpClient(new HttpLoggingHandler()) { BaseAddress = new Uri(Settings.ApiUrl) });
            try
            {
                IsLoadingStatus = true;
                var result = await api.GetStatus("Bearer " + Settings.AccessToken);
                List<ListPopUpModel> x = new List<ListPopUpModel>();
                //x = (result.Data).ToObject<List<ListPopUpModel>>();
                x = JsonConvert.DeserializeObject<List<ListPopUpModel>>(Convert.ToString(result.Data));
                lst.Add(al);
                foreach (var item in x)
                {
                    if (item.id != 6)//(int)Enums.SatausEnum.Completed)
                    {
                        lst.Add(new ListPopUpModel { id = item.id, name = IsRTL ? item.nameAr : item.name });
                    }
                }
            }
            catch (Exception exception)
            {
                var properties = new Dictionary<string, string>
                       {
                             { "fillterpopupviewmodel", "getsatusdatalist" },
                       };
                Crashes.TrackError(exception, properties);
            }
            IsLoadingStatus = false;
            return lst;
        }

        private async Task<ObservableCollection<ListPopUpModel_Guid>> GetSectorsDataList()
        {
            IsSectorsLoading = true;
            ListPopUpModel_Guid al = new ListPopUpModel_Guid() { name = AppResource.All, id = Guid.Empty, IsChecked = true };
            ObservableCollection<ListPopUpModel_Guid> lst = new ObservableCollection<ListPopUpModel_Guid>();
            var api = RestService.For<ITaskyApi>(new System.Net.Http.HttpClient(new HttpLoggingHandler()) { BaseAddress = new Uri(Settings.ApiUrl) });
            try
            {
                var result = await api.GetSectors("Bearer " + Settings.AccessToken);
                List<ListPopUpModel_Guid> x = new List<ListPopUpModel_Guid>();
                //x = (result.Data).ToObject<List<ListPopUpModel_Guid>>();
                x = JsonConvert.DeserializeObject<List<ListPopUpModel_Guid>>(Convert.ToString(result.Data));
                lst.Add(al);
                foreach (var item in x)
                {
                    lst.Add(new ListPopUpModel_Guid { id = item.id, name = IsRTL? item.name : item.name });
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
            IsSectorsLoading = false;
            return lst;
        }
        private async Task<List<Entity>> GetEntitiesDataList()
        {
            IsSectorsLoading = true;
            ListPopUpModel_Guid al = new ListPopUpModel_Guid() { name = AppResource.All, id = Guid.Empty, IsChecked = true };
            List<Entity> lst = new List<Entity>();
            var api = RestService.For<ITaskyApi>(new System.Net.Http.HttpClient(new HttpLoggingHandler()) { BaseAddress = new Uri(Settings.ApiUrl) });
            try
            {
                var result = await api.GetEntities("Bearer " + Settings.AccessToken);
                
                var data = JsonConvert.DeserializeObject<ResultData<Entity>>(result);
                //List<ListPopUpModel_Guid> x = new List<ListPopUpModel_Guid>();
                IsSectorsLoading = false;
                return data.Data;
            }
            catch (Exception exception)
            {
                var properties = new Dictionary<string, string>
                       {
                             { "filterpopviewmodel", "getsectordatalist" },
                       };
                Crashes.TrackError(exception, properties);
            }
            IsSectorsLoading = false;
            return lst;
        }
        private async Task<ObservableCollection<ListPopUpModel_Guid>> GetUserGroupsDataList()
        {
            ListPopUpModel_Guid al = new ListPopUpModel_Guid() { name = AppResource.All, id = Guid.Empty, IsChecked = true };
            ObservableCollection<ListPopUpModel_Guid> lst = new ObservableCollection<ListPopUpModel_Guid>();
            var api = RestService.For<ITaskyApi>(new System.Net.Http.HttpClient(new HttpLoggingHandler()) { BaseAddress = new Uri(Settings.ApiUrl) });
            try
            {
                var result = await api.GetUserGroupsList("Bearer " + Settings.AccessToken, Guid.Empty);
                List<ListPopUpModel_Guid> x = new List<ListPopUpModel_Guid>();
                //x = (result.Data).ToObject<List<ListPopUpModel_Guid>>();
                x = JsonConvert.DeserializeObject<List<ListPopUpModel_Guid>>(Convert.ToString(result.Data));
                lst.Add(al);
                foreach (var item in x)
                {
                    lst.Add(new ListPopUpModel_Guid { id = item.id, name = IsRTL ? item.name : item.name });
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

            return lst;
        }
        private async Task<bool> GetAssigneeDataList()
        {
            AssigneeDataList = new ObservableCollection<ResponsiblesDDL>();
           
            try
            {
                List<ResponsiblesDDL> emps = await GetAssigneeDataList(0, AssigneePageSize);//0 get first page

                ResponsiblesDDL al = new ResponsiblesDDL() { Text = AppResource.All, Value2 = new Value2() { ID = Guid.Empty.ToString(), RoleID = "0", Type = -1 }, IsCheckedemployee = true };
                AssigneeDataList.Add(al);

                lastitemAssignee = al;

                foreach (var item in emps)
                {
                    string txt = IsRTL ? item.ArabicText : item.Text;
                    AssigneeDataList.Add(new ResponsiblesDDL { Text = txt, Value2 = item.Value2 });
                }

            }
            catch (Exception exception)
            {
                var properties = new Dictionary<string, string>
                       {
                             { "filterpopviewmodel", "getassigneedatalist" },
                       };
                Crashes.TrackError(exception, properties);
                return false;
            }

            return true;
        }

        public bool IsLoadingAssignee { get; set; }
        private async Task<List<ResponsiblesDDL>> GetAssigneeDataList(int page, int pageSize)
        {
            List<ResponsiblesDDL> responsiblesDDLs = new List<ResponsiblesDDL>();
            IsLoadingAssignee = true;
             var api = RestService.For<ITaskyApi>(new System.Net.Http.HttpClient(new HttpLoggingHandler()) { BaseAddress = new Uri(Settings.ApiUrl) });
            try
            {
                var result = await api.GetEmployees("Bearer " + Settings.AccessToken, page, pageSize);

                //responsiblesDDLs = (result.Data).ToObject<List<ResponsiblesDDL>>();
                responsiblesDDLs = JsonConvert.DeserializeObject<List<ResponsiblesDDL>>(Convert.ToString(result.Data));
            }
            catch (Exception exception)
            {
                var properties = new Dictionary<string, string>
                       {
                             { "filterpopviewmodel", "getassigneedatalist" },
                       };
                Crashes.TrackError(exception, properties);
                return responsiblesDDLs;
            }
            IsLoadingAssignee = false;
            return responsiblesDDLs;
        }

        public bool IsLoadingSource { get; set; }
        private async Task<ObservableCollection<ListPopUpModel>> GetSourcesDataList()
        {
            ListPopUpModel al = new ListPopUpModel() { name = AppResource.All, id = 0, IsChecked = true };
            ObservableCollection<ListPopUpModel> lst = new ObservableCollection<ListPopUpModel>();
            var api = RestService.For<ITaskyApi>(new System.Net.Http.HttpClient(new HttpLoggingHandler()) { BaseAddress = new Uri(Settings.ApiUrl) });
            try
            {
                IsLoadingSource = true;
                var result = await api.GetSource("Bearer " + Settings.AccessToken);
                List<ListPopUpModel> x = new List<ListPopUpModel>();
                //x = (result.Data).ToObject<List<ListPopUpModel>>();
                x = JsonConvert.DeserializeObject<List<ListPopUpModel>>(Convert.ToString(result.Data));
                lst.Add(al);
                foreach (var item in x)
                {
                    lst.Add(new ListPopUpModel { id = item.id, name = IsRTL ? item.nameAr : item.name });
                }
            }
            catch (Exception exception)
            {
                var properties = new Dictionary<string, string>
                       {
                             { "filterpopviewmodel", "getsourcedatalist" },
                       };
                Crashes.TrackError(exception, properties);
            }
            IsLoadingSource = false;
            return lst;
        }
        #endregion

        //public override System.Threading.Tasks.Task InitializeAsync(object data)
        //{
        //    if (data is FilterTask taskFilter)
        //    {
        //        filterObj = taskFilter;
        //        SetLastFilter(filterObj);
        //    }
        //    return base.InitializeAsync(data);
        //}

        private void SetLastFilter(FilterTask filter)
        {
            try
            {
                Statusname = filter.Status;
                Sectorname = filter.SectorName;
                Assigneename = filter.AssigneeName;
                Sourcename = filter.SourceName;
                priorityname = filter.Priority;

                SearchTitle = filter.SearchTitle;

                priorityId = filter.PriorityID;
                StatusId = filter.StatusID;
                SectorId = filter.EntityId;
                AssigneeId = filter.ResponsibleID;
                SourceId = filter.SourceId;

                foreach (var item in filter.Entities)
                {
                    CheckedEntities.Add(item);
                }

                StartDate = filter.StartDate;
                EndDate = filter.EndDate;
            }
            catch (Exception exception)
            {
                var properties = new Dictionary<string, string>
                       {
                             { "fillterpopupviewmodel", "SetLastFilter" },
                       };
                Crashes.TrackError(exception, properties);

            }
        }
    }
}
