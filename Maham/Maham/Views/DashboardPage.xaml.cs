using Naxam.Controls.Forms;
using System;
using System.Threading.Tasks;
using Maham.Bases;
using Maham.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace Maham.Views
{
    public partial class DashboardPage : TopTabbedPage
    {
        bool isRTL;
        public DashboardPage()
        {
            Setting.Settings.Dashboardtabs.Clear();
            Setting.Settings.DashboardtabsPage.Clear();
            InitializeComponent();
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            On<Xamarin.Forms.PlatformConfiguration.Android>().SetToolbarPlacement(ToolbarPlacement.Top);
            On<Xamarin.Forms.PlatformConfiguration.Android>().SetBarSelectedItemColor(Color.FromHex("#00ace6")); //#69CAF1
            On<Xamarin.Forms.PlatformConfiguration.Android>().SetBarItemColor(Color.FromHex("#303C56"));
            //On<Xamarin.Forms.PlatformConfiguration.Android>().SetIsSwipePagingEnabled(false);
            isRTL = new Helpers.Helper().CurrentLanguage() == 2;
            FlowDirection = isRTL ? FlowDirection.RightToLeft : FlowDirection.LeftToRight;


            MessagingCenter.Subscribe<object>(this, "SetCurrentPage", (sender) =>
            {
                SetCurrentDash();
            });

        }
        private async void SetCurrentDash()
        {
            if (isRTL)
            {
                if (Device.RuntimePlatform == Device.iOS)
                {
                    if (DashTabs.Children.Count > 1)
                    {
                        int beforeLastPage = DashTabs.Children.Count - 2;
                        int lastPage = DashTabs.Children.Count - 1;
                        DashTabs.CurrentPage = DashTabs.Children[beforeLastPage];
                        await Task.Delay(500);
                        DashTabs.CurrentPage = DashTabs.Children[lastPage];
                    }
                }
            }
        }
           
        
        private void TabbedPage_CurrentPageChanged(object sender, EventArgs e)
        {
            foreach (var viewModel in Setting.Settings.Dashboardtabs)
            {
                try
                {
                    viewModel.currentpage = false;
                    string tabName = " " + viewModel.tabName;
                    if (this.CurrentPage != null)
                    {  
                        if (tabName == this.CurrentPage.Title)
                        {
                            viewModel.Appear();
                        }
                    }
                }
                catch (Exception ex)
                {
                }
            }
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Unsubscribe<object>(this, "SetCurrentPage");
        }
    }
}
