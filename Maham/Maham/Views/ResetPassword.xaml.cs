using Maham.Resources;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace Maham.Views
{
    public partial class ResetPassword : ContentPage
    {

        public ResetPassword()
        {
            InitializeComponent();
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            //name.Focus();
            //name.Completed += (s, e) => oldpassword.Focus();
            //oldpassword.Completed += (s, e) => newpass.Focus();
            //resetstack.FlowDirection = new Helpers.Helper().CurrentLanguage() == 1 ? FlowDirection.LeftToRight : FlowDirection.RightToLeft;
            SetEntryFocus();
            //name.ReturnCommand = new Command(() => oldpassword.Focus());
            //oldpassword.ReturnCommand = new Command(() => newpass.Focus());
        }

        private void TapGestureRecognizer_Tapped(object sender, System.EventArgs e)
        {
            oldpassword.IsPassword = oldpassword.IsPassword ? false : true;
            oldpasslable.Text = AppResource.showtext;
            oldpassimage.Source = "show";
            if (oldpassword.IsPassword == false)
            {
                oldpasslable.Text = AppResource.hidetext;
                oldpassimage.Source = "hideye";
            }
        }

        private void TapGestureRecognizer_Tapped_1(object sender, System.EventArgs e)
        {
            newpass.IsPassword = newpass.IsPassword ? false : true;
            newpasslable.Text = AppResource.showtext;
            newpassimage.Source = "show";
            if (newpass.IsPassword == false)
            {
                newpasslable.Text = AppResource.hidetext;
                newpassimage.Source = "hideye";
            }
        }

        private void SetEntryFocus()
        {
            if (Device.RuntimePlatform == Device.iOS)
            {
                name.Unfocused += (s, e) => oldpassword.Focus();
                oldpassword.Unfocused += (s, e) => newpass.Focus();
            }
            else if (Device.RuntimePlatform == Device.Android)
            {
                name.Completed += (s, e) => oldpassword.Focus();
                oldpassword.Completed += (s, e) => newpass.Focus();
            }
        }
    }
}
