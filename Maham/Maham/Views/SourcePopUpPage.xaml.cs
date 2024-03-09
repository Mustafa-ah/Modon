using Maham.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace Maham.Views
{
    public partial class SourcePopUpPage 
    {
        public SourcePopUpPage(string userId, int? privilege)
        {
            InitializeComponent();
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);

            (this.BindingContext as SourcePopUpPageViewModel).GetSource(userId, privilege);
        }
    }
}
