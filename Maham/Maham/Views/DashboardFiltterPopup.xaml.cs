using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace Maham.Views
{
    public partial class DashboardFiltterPopup
    {
        public DashboardFiltterPopup()
        {
            InitializeComponent();
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            //ObservableCollection<TabContentViewModel> items
            //((popupViewModel)this.BindingContext).file = list;
            //BindingContext=  ((App)App.Current).ExtResolve<FillterPopupViewModel>();
            FlowDirection = new Helpers.Helper().CurrentLanguage() == 1 ? FlowDirection.LeftToRight : FlowDirection.RightToLeft;
        }
    }
}