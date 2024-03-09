using System;
using System.Collections.Generic;
using System.Text;
using Maham.Bases;
using Maham.ViewModels;
//using Maham.Extensions;
using System.Windows.Input;
using Xamarin.Forms;

namespace Maham.Models
{
    public class TaskTabbedPage : BaseModel
    {
        public TaskTabbedPage()
        {
            AppearingCommand = new Command(() => { ViewModel?.OnAppearing(); });
        }
        private string _tabName;
        private bool _isFirstTab;
        public string TabName
        {
            get { return _tabName; }
            set
            {
                _tabName = value;
                //ViewModel = ((App)App.Current).ExtResolve<NotPrioritiesPageViewModel>();
                //switch (_tabName)
                //{
                //    case "Priorities":
                //        ViewModel = ((App)App.Current).ExtResolve<PrioritiesPageViewModel>();
                //        ViewModel.tabName = _tabName;
                //        ViewModel.tabId = TabId;
                //        ViewModel.Id = Id;
                //        break;
                //    case "الاولويات":
                //        ViewModel = ((App)App.Current).ExtResolve<PrioritiesPageViewModel>();
                //        ViewModel.tabName = _tabName;
                //        ViewModel.tabId = TabId;
                //        ViewModel.Id = Id;
                //        break;
                //    default:
                //        ViewModel = ((App)App.Current).ExtResolve<NotPrioritiesPageViewModel>();
                //        ViewModel.tabName = _tabName;
                //        ViewModel.tabId = TabId;
                //        ViewModel.Id = Id;
                //        break;
                //}
            }
        }
        private Guid _id;
        public Guid Id
        {
            get { return _id; }
            set
            {
                _id = value;
                NotPrioritiesPageViewModel vm = ((App)App.Current).ExtResolve<NotPrioritiesPageViewModel>();
                vm.TabId = value;
                ViewModel = vm;
            }
        }

        public int TabId { get; set; }
        public BaseViewModel ViewModel { get; private set; }
        public ICommand AppearingCommand { get; }
        public bool IsFirstTab {
            get { return _isFirstTab; }
            set
            {
                _isFirstTab = value;
                if (_isFirstTab)
                {
                    //ViewModel = ((App)App.Current).ExtResolve<NotPrioritiesPageViewModel>();
                    ((NotPrioritiesPageViewModel)ViewModel).GetData(Id);

                }
            }
        }
    }
}
