using Maham.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace Maham.Views
{
    public partial class EditSearchPage : ContentPage
    {
        public EditSearchPage()
        {
            InitializeComponent();
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            App.Current.On<Xamarin.Forms.PlatformConfiguration.Android>().UseWindowSoftInputModeAdjust(WindowSoftInputModeAdjust.Pan);
        }

        protected override bool OnBackButtonPressed()
        {
            ((App)App.Current).ExtResolve<EditSearchPageViewModel>().backcommandExcute(null);
            return true;// base.OnBackButtonPressed();
        }
    }
}
