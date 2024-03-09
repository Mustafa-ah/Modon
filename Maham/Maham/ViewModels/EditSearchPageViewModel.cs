using Microsoft.AppCenter.Crashes;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Maham.Bases;
using Maham.Models;
using Maham.Service.General;
using Xamarin.Forms;

namespace Maham.ViewModels
{
	public class EditSearchPageViewModel : BaseViewModel
    {
        private readonly INavService navService;

        public int selecetedid { get; set; }
        public string Employeename { get; set; }
        public string projectid { get; set; }
        //public string projectname { get; set; }
        public EmployeeModel Lastemployee1 { get; set; }
        public ICommand chooseemp { get; set; }
       public ICommand back { get; set; }
        public ICommand Search { get; set; }
     //   public AddTaskModel editdata { get; set; }
        public ObservableCollection<EmployeeModel> Myemployeelist { get; set; }
        public ObservableCollection<EmployeeModel> TempemployeeDemoDataList { get; set; }
        public EditSearchPageViewModel(INavService _navService, INavigationService _NavigationServices) : base(_NavigationServices)
        {
             navService = _navService;
            Myemployeelist = new ObservableCollection<EmployeeModel>();
            TempemployeeDemoDataList = new ObservableCollection<EmployeeModel>();
            Search = new Command(SearchCommandExcute);
            back = new Command(backcommandExcute);
            chooseemp = new Command(chooseempExcute);
        }

        public void backcommandExcute(object obj)
        {
            //_navigation.NavigateAsync("EditTask");
            navService.NavigateBackAsync();
        }

        private void SearchCommandExcute(object obj)
        {
            Myemployeelist = new ObservableCollection<EmployeeModel>();
            TempemployeeDemoDataList = new ObservableCollection<EmployeeModel>();

            MessagingCenter.Send(Lastemployee1, "SelectedEmployeeEdited");

            navService.NavigateBackAsync();
        }
        public override void OnNavigatedTo(NavigationParameters parameters)
        {
            if (parameters != null&&parameters.Count>0)
            {
                var data = parameters["listofedittemp"] as ObservableCollection<EmployeeModel>;
                Myemployeelist = data;
                //projectname = parameters["projectnameedit"] as string;
                //editdata = parameters["editmodel"] as AddTaskModel;

                TempemployeeDemoDataList = Myemployeelist;
                var id = (int)parameters["empid"];

                TempemployeeDemoDataList = Myemployeelist;

                foreach (var item in Myemployeelist)
                {
                    item.IsCheckedemployee = false;
                }

                if (id != 0)
                {
                    selecetedid = id;
                    
                    Myemployeelist.FirstOrDefault(x => x.id == id).IsCheckedemployee = true;
                }
                else
                {
                    selecetedid = 0;
                }
            }
            ///base.OnNavigatedTo(parameters);
        }

        private void chooseempExcute(object obj)
        {
            var ItemSelected = obj as EmployeeModel;

            if (ItemSelected == null) return;

            foreach (var item in Myemployeelist)
            {
                item.IsCheckedemployee = false;
            }

            selecetedid = ItemSelected.id;
            Employeename = ItemSelected.name;
            Lastemployee1 = ItemSelected;

        }
        public void OnEmployeenameChanged()
        {

            if (string.IsNullOrEmpty(Employeename) || string.IsNullOrWhiteSpace(Employeename))
            {
                if(TempemployeeDemoDataList.Count>0)
                {
                    Myemployeelist = TempemployeeDemoDataList;
                }

                //var asignee = Myemployeelist.FirstOrDefault(x => x.id == selecetedid);
                //if (asignee != null)
                //{
                //    asignee.IsCheckedemployee = false;
                //}
            }
            else
            {
                if (Myemployeelist.Count>0)
                {
                    Myemployeelist = TempemployeeDemoDataList;
                    List<EmployeeModel> result = Myemployeelist.Where(x => !string.IsNullOrEmpty(x.name) && x.name.ToLower().Contains(Employeename.ToLower())).ToList();
                    if (result != null)
                    {
                        try
                        {
                            Myemployeelist = new ObservableCollection<EmployeeModel>(result);
                            Myemployeelist.FirstOrDefault(x => x.id == selecetedid).IsCheckedemployee = true;
                        }
                        catch (Exception exception)
                        {
                            // isBusy = false;
                            var properties = new Dictionary<string, string>
                       {
                             { "editsearchpageviewmodel", "onemployeenamechanged" },
                       };
                            Crashes.TrackError(exception, properties);
                        }

                    }

                }
            }
        }

        public override Task InitializeAsync(object data)
        {
            OnNavigatedTo((NavigationParameters)data);
            return base.InitializeAsync(data);
        }
    }
}