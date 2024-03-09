using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Maham.Models;
using Maham.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace Maham.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NotificationsPage : ContentPage
	{
        NotificationsPageViewModel notificationsPageViewModel;
        public NotificationsPage ()
		{
			InitializeComponent ();
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            FlowDirection = new Helpers.Helper().CurrentLanguage() == 1 ? FlowDirection.LeftToRight : FlowDirection.RightToLeft;

        }

        
	    private void ListView_OnItemTapped(object sender, ItemTappedEventArgs e)
	    {
            //((ListView)sender).SelectedItem = null;
	        if (BindingContext == null) return;
            notificationsPageViewModel.SelectedNotification = e.Item as NotificationDTO;
	        ((Xamarin.Forms.ListView)sender).SelectedItem = null;


        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            notificationsPageViewModel = (BindingContext as NotificationsPageViewModel);
            notificationsPageViewModel.OnAppearing();
        }
     //   protected override bool OnBackButtonPressed()
	    //{
	    //    base.OnBackButtonPressed();
	    //    return true;
            
	    //}

        bool isLoading = false;
        private async void NotificationListView_ItemAppearing(object sender, ItemVisibilityEventArgs e)
        {
            if (isLoading)
            {
                return;
            }
            isLoading = true;
            var itemTypeObject = e.Item as NotificationDTO;
            if (notificationsPageViewModel.NotificationCollection.Last() == itemTypeObject)
            {
              await notificationsPageViewModel.LoadMoreItems();
            }
            isLoading = false;
        }
    }
}