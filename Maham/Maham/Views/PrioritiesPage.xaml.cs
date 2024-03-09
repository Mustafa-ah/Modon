using Maham.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace Maham.Views
{
    public partial class PrioritiesPage : ContentPage
    {
        public PrioritiesPage()
        {
            InitializeComponent();
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            var x = this.BindingContext;
            FlowDirection = new Helpers.Helper().CurrentLanguage() == 1 ? FlowDirection.LeftToRight : FlowDirection.RightToLeft;

        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            //this.Content = null;
        }
    }
}
