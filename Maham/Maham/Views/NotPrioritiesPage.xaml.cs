using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Maham.Models;
using Maham.Service.Model.Response.Tasks;
using Maham.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace Maham.Views
{
    
    public partial class NotPrioritiesPage : ContentPage
    {
        NotPrioritiesPageViewModel notPrioritiesPageViewModel;
        public NotPrioritiesPage()
        {
            InitializeComponent();
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            this.BindingContext = ((App)App.Current).ExtResolve<NotPrioritiesPageViewModel>();
            notPrioritiesPageViewModel = (this.BindingContext as NotPrioritiesPageViewModel);
            // this.listview.ItemsSource = ((App)App.Current).ExtResolve<NotPrioritiesPageViewModel>().lstNotPrioritiesTabContent;     

            FlowDirection = new Helpers.Helper().CurrentLanguage() == 1 ? FlowDirection.LeftToRight : FlowDirection.RightToLeft;
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            dataTemplateItem = null;
        }

        bool isLoading = false;
        private async void SectionData_ItemAppearing(object sender, ItemVisibilityEventArgs e)
        {
            bool success = false;

            if (isLoading)
            {
                return;
            }
            isLoading = true;

            notPrioritiesPageViewModel = ((NotPrioritiesPageViewModel)((TaskTabbedPage)this.BindingContext).ViewModel);

            var itemTypeObject = e.Item as TabContentViewModel;

            if (notPrioritiesPageViewModel.Items.Last() == itemTypeObject)
            {
                success = await notPrioritiesPageViewModel.LoadMoreSections();
            }

            else if (e.Item is TaskViewModel taskViewModel)
            {
                success = await notPrioritiesPageViewModel.LoadMoreTasks(taskViewModel);
            }

            isLoading = false;
        }
    }
}
