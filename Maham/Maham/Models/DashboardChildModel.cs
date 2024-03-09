using System.Windows.Input;
using Maham.Bases;
using Maham.ViewModels;
using Xamarin.Forms;

namespace Maham.Models
{
    public class DashboardChildModel : BaseModel
    {

        public BaseViewModel ViewModel { get; private set; }
        public ICommand AppearingCommand { get; }

        public DashboardChildModel()
        {
            AppearingCommand = new Command(() => { ViewModel?.OnAppearing(); });
            ViewModel = ((App)App.Current).ExtResolve<DashboardChildPageViewModel>();

        }
        private string _tabName;
        public string TabName
        {
            get { return _tabName; }
            set
            {
                _tabName = value;
                ViewModel.tabName = _tabName;
            }
        }
        private int tabId;

        public int TabId
        {
            get { return tabId; }
            set
            {
                tabId = value;
                ViewModel.tabId = tabId;
            }
        }

    }
}
