using System;
using System.Threading.Tasks;
using Naxam.Controls.Forms;
using Prism.Ioc;
using Prism.Navigation;
using Maham.Bases;
using Maham.CustomControl;
using Maham.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace Maham.Views
{
    public partial class TasksPage : TopTabbedPage
    {
        bool isRTL;
        public TasksPage()
        {
            InitializeComponent();
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            On<Xamarin.Forms.PlatformConfiguration.Android>().SetToolbarPlacement(ToolbarPlacement.Top);
            On<Xamarin.Forms.PlatformConfiguration.Android>().SetBarSelectedItemColor(Color.FromHex("#00ace6")); //#69CAF1
            On<Xamarin.Forms.PlatformConfiguration.Android>().SetBarItemColor(Color.FromHex("#303C56"));
            On<Xamarin.Forms.PlatformConfiguration.Android>().SetIsSwipePagingEnabled(true);
            isRTL = new Helpers.Helper().CurrentLanguage() == 2;
            FlowDirection = isRTL ? FlowDirection.RightToLeft : FlowDirection.LeftToRight;

            MessagingCenter.Subscribe<TasksPageViewModel>(this, "SetCurrentTaskPage", (sender) =>
            {
                SetCurrentTask();
            });
        }

        private async Task SetCurrentTask()
        {
            try
            {
                if (TasksTabs.Children.Count > 1)
                {
                   
                    if (isRTL)
                    {
                        int beforeLastPage = TasksTabs.Children.Count - 2;
                        int lastPage = TasksTabs.Children.Count - 1;
                        TasksTabs.CurrentPage = TasksTabs.Children[beforeLastPage];
                        await Task.Delay(500);
                        TasksTabs.CurrentPage = TasksTabs.Children[lastPage];
                    }
                    else
                    {
                        if (Device.RuntimePlatform == Device.iOS)
                        {
                            TasksTabs.CurrentPage = TasksTabs.Children[1];
                            TasksTabs.CurrentPage = TasksTabs.Children[0];
                        }

                    }
                }
               
            }
            catch (Exception ex)
            {

            }
           
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Unsubscribe<object>(this, "SetCurrentTaskPage");
        }

        private void TabbedPage_CurrentPageChanged(object sender, EventArgs e)
        {
        }
    }
}
