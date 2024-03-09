using Prism.Navigation;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Maham.Bases;
using Maham.Models;
using Maham.Resources;
using Maham.Views;
using Xamarin.Forms;
using System.Threading.Tasks;
using Refit;
using Maham.Setting;
using Maham.Service;
using Microsoft.AppCenter.Crashes;

namespace Maham.ViewModels
{
    //TODO: delete this class
    public class DashboardFiltterPopupViewModel : BaseViewModel
    {
        #region Public Properties
        //Priority
        public string startdatebind { get; set; }
        public string enddatebind { get; set; }
        public string priorityimage { get; set; }
        public bool clickedgridpriority { get; set; }
        public string priorityname { get; set; }
        public int priorityId { get; set; }
        public bool prioritylist { get; set; }
        public bool arrowprioritycheck { get; set; }
        public ObservableCollection<ListPopUpModel> MYlistData { get; set; }
        public ListPopUpModel lastitem { get; set; }
        public ICommand CheckPriorityCommand { get; set; }
        public ICommand selectpriorityCommand { get; set; }
        public ICommand ClearCommand { get; set; }
        public ICommand clickprioritygrid { get; set; }
        //Status
        public string Statusimage { get; set; }
        public bool clickedgridStatus { get; set; }
        public string Statusname { get; set; }
        public int StatusId { get; set; }
        public bool Statuslist { get; set; }
        public bool arrowStatuscheck { get; set; }
        public ObservableCollection<ListPopUpModel> MYlistDataStatus { get; set; }
        public ListPopUpModel lastitemStatus { get; set; }
        public ICommand CheckStatusCommand { get; set; }
        public ICommand selectStatusCommand { get; set; }
        public ICommand ClearCommandStatus { get; set; }
        public ICommand clickStatusgrid { get; set; }
        //Start Date
        public ICommand OnSelectStarDateCommand { get; set; }
        public DateTime? MaxDate { get; set; }
        public string StartDate { get; set; }
        //End Date 
        public ICommand OnSelectEndDateCommand { get; set; }
        public string EndDate { get; set; }
        //Filter Excution 
        public ICommand FilterCommand { get; set; }
        FilterTask filterObj { get; set; }
        NotPrioritiesPageViewModel currentViewModel { get; set; }


        #endregion
        public DashboardFiltterPopupViewModel(INavigationService _NavigationServices) : base(_NavigationServices)
        {
            //Priority
            priorityimage = "";
            CheckPriorityCommand = new Command(CheckPriorityCommandExcute);
            selectpriorityCommand = new Command(selectpriorityCommandExcute);
            ClearCommand = new Command(ClearCommandExcute);
            clickprioritygrid = new Command(clickprioritygridExcute);
            priorityname = AppResource.All;
            MYlistData = new ObservableCollection<ListPopUpModel>();
            GetPriority();
            //Status
            Statusimage = "";
            CheckStatusCommand = new Command(CheckStatusCommandExcute);
            selectStatusCommand = new Command(selectStatusCommandExcute);
            ClearCommandStatus = new Command(ClearCommandExcuteStatus);
            clickStatusgrid = new Command(clickStatusgridExcute);
            Statusname = AppResource.All;
            MYlistDataStatus = new ObservableCollection<ListPopUpModel>();
            GetTaskStatus();

            ////Start Date 
            //OnSelectStarDateCommand = new Command(ShowDatePicker);
            ////End Date 
            //OnSelectEndDateCommand = new Command(ShowEndDatePicker);
            //Filter Excutions 
            FilterCommand = new Command(Filter);
            getLastFiltterStatus();

        }


        public override void OnAppearing()
        {
            base.OnAppearing();
            getLastFiltterStatus();
        }

        #region Priority Methods

        private void CheckPriorityCommandExcute(object obj)
        {
            var ItemSelected = obj as ListPopUpModel;
            if (ItemSelected == null) return;
            if (ItemSelected != lastitem)
            {
                if (lastitem != null)
                {
                    lastitem.IsChecked = false;
                }

                ItemSelected.IsChecked = true;
            }
            else
            {
                ItemSelected.IsChecked = !ItemSelected.IsChecked;
            }
            lastitem = ItemSelected;
            priorityname = ItemSelected.name;
            priorityimage = ItemSelected.image2;
            //  prioritycheckedimage = "circlechecked.png";
            prioritylist = false;
            if (priorityname == "All")
            {
                priorityId = 0;
            }
            else
            {
                priorityId = ItemSelected.id;
            }
            arrowprioritycheck = false;
            // MessagingCenter.Send(new ListPopUpModel() { id = ItemSelected.id, name = priorityname, image2 = ItemSelected.image2 }, "PopUpData");
            // PopupNavigation.Instance.PopAsync();
        }
        private void ClearCommandExcute(object obj)
        {
            priorityname = AppResource.crtiticlenumtext;
            priorityimage = "darkorange";
        }
        private void selectpriorityCommandExcute(object obj)
        {
            if (arrowprioritycheck)
            {
                prioritylist = true;
            }
            else
            {
                prioritylist = false;
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
            if (ItemSelectedStatus != lastitemStatus)
            {
                if (lastitemStatus != null)
                {
                    lastitemStatus.IsChecked = false;
                }

                ItemSelectedStatus.IsChecked = true;
            }
            else
            {
                ItemSelectedStatus.IsChecked = !ItemSelectedStatus.IsChecked;
            }
            lastitemStatus = ItemSelectedStatus;
            Statusname = ItemSelectedStatus.name;
            Statusimage = ItemSelectedStatus.image2;
            //  prioritycheckedimage = "circlechecked.png";
            Statuslist = false;
            if (Statusname == "All")
            {
                StatusId = 0;
            }
            else
            {
                StatusId = ItemSelectedStatus.id;
            }
            arrowStatuscheck = false;
            // MessagingCenter.Send(new ListPopUpModel() { id = ItemSelected.id, name = priorityname, image2 = ItemSelected.image2 }, "PopUpData");
            // PopupNavigation.Instance.PopAsync();
        }
        private void ClearCommandExcuteStatus(object obj)
        {
            Statusname = AppResource.All;
            Statusimage = "";
        }
        private void selectStatusCommandExcute(object obj)
        {
            if (arrowStatuscheck)
            {
                Statuslist = true;
            }
            else
            {
                Statuslist = false;
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

        #region Start Date  Methods
        //private void ShowDatePicker(object obj)
        //{
        //    UserDialogs.Instance.DatePrompt(new DatePromptConfig { MinimumDate = DateTime.Now, MaximumDate = MaxDate, OnAction = (result) => SetStartDate(result), IsCancellable = true });
        //}
        //private void SetStartDate(DatePromptResult result)
        //{
        //    if (result.Ok)
        //    {
        //        StartDate = result.SelectedDate.ToShortDateString();
        //        startdatebind = result.SelectedDate.ToString("yyyy-MM-dd h:mm tt");
        //    }
        //}

        #endregion
        #region End Date 
        //private void ShowEndDatePicker(object obj)
        //{
        //    UserDialogs.Instance.DatePrompt(new DatePromptConfig { MinimumDate = DateTime.Now, MaximumDate = MaxDate, OnAction = (result) => SetEndDate(result), IsCancellable = true });
        //}
        //private void SetEndDate(DatePromptResult result)
        //{
        //    if (result.Ok)
        //    {

        //        EndDate = result.SelectedDate.ToShortDateString();
        //        enddatebind = result.SelectedDate.ToString("yyyy-MM-dd h:mm tt");
        //    }
        //}
        #endregion
        #region Filter Excutions 
        private async void Filter(object obj)
        {
            DashboardChildPage currentpage = null;
            foreach (var item in Setting.Settings.DashboardtabsPage)
            {
                if (item.currentPage == true)
                {
                    currentpage = item;
                }
                item.isFiltter = false;
            }
            Setting.Settings.DashboardFiltter.Clear();
            if (priorityId != 0)
            {
                Setting.Settings.DashboardFiltter.Add("priorityID", priorityId.ToString());
            }
            else
            {
                Setting.Settings.DashboardFiltter.Add("priorityID", "0");
            }
            if (StatusId != 0)
            {
                Setting.Settings.DashboardFiltter.Add("StatusID", StatusId.ToString());
            }
            else
            {
                Setting.Settings.DashboardFiltter.Add("StatusID", "0");
            }
            if (StartDate != null)
            {
                //var newStartDate = StartDate.Replace("/", "-");
                //DateTime fDate = Convert.ToDateTime(newStartDate);
                //var fromDate = StartDate.ToString("yyyy-MM-dd");
                Setting.Settings.DashboardFiltter.Add("fromDate", startdatebind);
            }
            if (EndDate != null)
            {
                //var newEndDate = EndDate.Replace("/", "-");
                //DateTime tDate = Convert.ToDateTime(newEndDate);
                //var toDate = tDate.ToString("yyyy-MM-dd");
                Setting.Settings.DashboardFiltter.Add("toDate", enddatebind);
            }
            if (StartDate == null)
            {
                StartDate = DateTime.MinValue.ToString("yyyy-MM-dd h:mm tt");
            }
            if (EndDate == null)
            {
                EndDate = DateTime.MinValue.ToString("yyyy-MM-dd h:mm tt");
            }
            if (currentpage != null)
            {
                currentpage.Appear();
                currentpage.isFiltter = true;
            }
            await PopupNavigation.Instance.PopAsync();
        }
        #endregion

        public void getLastFiltterStatus()
        {
            if (Setting.Settings.DashboardFiltter.ContainsKey("fromDate"))
            {
                StartDate = Setting.Settings.DashboardFiltter["fromDate"];
            }
            if (Setting.Settings.DashboardFiltter.ContainsKey("toDate"))
            {
                EndDate = Setting.Settings.DashboardFiltter["toDate"];
            }
            if (Setting.Settings.DashboardFiltter["priorityID"] != "0")
            {
                int selectedPriorityID = Convert.ToInt32(Setting.Settings.DashboardFiltter["priorityID"]);
                ListPopUpModel item = MYlistData.Where(x => x.id == selectedPriorityID).FirstOrDefault();
                if (item != null)
                {
                    item.IsChecked = true;
                    priorityname = item.name;
                }
            }
            if (Setting.Settings.DashboardFiltter["StatusID"] != "0")
            {
                int selectedStatusIDID = Convert.ToInt32(Setting.Settings.DashboardFiltter["StatusID"]);
                ListPopUpModel item = MYlistDataStatus.Where(x => x.id == selectedStatusIDID).FirstOrDefault();
                if (item != null)
                {
                    item.IsChecked = true;
                    Statusname = item.name;
                }
            }
        }


        public async Task GetTaskStatus()
        {
            var api = RestService.For<ITaskyApi>(new System.Net.Http.HttpClient(new HttpLoggingHandler()) { BaseAddress = new Uri(Settings.ApiUrl) });
            try
            {


                var result = await api.GetStatus("Bearer " + Settings.AccessToken);
                List<ListPopUpModel> x = new List<ListPopUpModel>();
                x = (result.Data).ToObject<List<ListPopUpModel>>();
                foreach (var item in x)
                {
                    MYlistDataStatus.Add(new ListPopUpModel { id = item.id, name = Settings.IsRtl ? item.nameAr : item.name, image2 = item.image2 });
                }
            }
            catch (Exception exception)
            {
                var properties = new Dictionary<string, string>
                       {
                             { "DashboardFiltterPopupViewModel", "GetTaskStatus" },
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
                x = (result.Data).ToObject<List<ListPopUpModel>>();
                foreach (var item in x)
                {
                    MYlistData.Add(new ListPopUpModel { id = item.id, name = Settings.IsRtl ? item.nameAr : item.name, image2 = item.image2 });
                }
            }
            catch (Exception exception)
            {
                var properties = new Dictionary<string, string>
                       {
                             { "DashboardFiltterPopupViewModel", "GetPriority" },
                       };
                Crashes.TrackError(exception, properties);
            }
        }
    }
}
