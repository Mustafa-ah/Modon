using System.Windows.Input;
using Maham.Bases;
using Maham.ViewModels;
using Xamarin.Forms;

namespace Maham.Models
{

    public class DashboardTabbedPage : BaseModel
    {
        #region Private Fields
        public ICommand AppearingCommand { get; }
        private string _tabName;
        #endregion
        public DashboardTabbedPage()
        {
            AppearingCommand = new Command(() => { ViewModel?.OnAppearing(); });
        }

        public string TabName
        {
            get { return _tabName; }
            set
            {
                _tabName = value;
                switch (_tabName)
                {
                    case "Priorities":
                        ViewModel = ((App)App.Current).ExtResolve<PrioritiesPageViewModel>();
                        ViewModel.tabName = _tabName;
                        break;
                    default:
                        ViewModel = ((App)App.Current).ExtResolve<NotPrioritiesPageViewModel>();
                        ViewModel.tabName = _tabName;
                        break;
                }
            }
        }
        public int TabId { get; set; }
        public BaseViewModel ViewModel { get; private set; }
    }
}
