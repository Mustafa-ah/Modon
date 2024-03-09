using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace Maham.Views
{
    public partial class SetttingsPage : ContentPage
    {
        public SetttingsPage()
        {
            InitializeComponent();
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            FlowDirection = new Helpers.Helper().CurrentLanguage() == 1 ? FlowDirection.LeftToRight : FlowDirection.RightToLeft;
        }
    }
}
