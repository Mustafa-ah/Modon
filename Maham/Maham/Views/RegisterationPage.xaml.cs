using Maham.Resources;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace Maham.Views
{
    public partial class RegisterationPage : ContentPage
    {
        bool isRTL;
        public RegisterationPage()
        {
            InitializeComponent();
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            username.Completed += (s, e) => email.Focus();
            email.Completed += (s, e) => password1.Focus();
            username.Focus();
            isRTL = new Helpers.Helper().CurrentLanguage() == 2;
            RootScroll.FlowDirection = isRTL ? FlowDirection.RightToLeft : FlowDirection.LeftToRight;
            if (isRTL)
            {
                password1.HorizontalTextAlignment = TextAlignment.End;
            }
        }

        private void TapGestureRecognizer_Tapped(object sender, System.EventArgs e)
        {
            password1.IsPassword = password1.IsPassword ? false : true;
            showlabel.Text = AppResource.showtext;
            showimage.Source = "show";
            if(password1.IsPassword==false)
            {
                showlabel.Text = AppResource.hidetext; 
                showimage.Source = "hideye";
            }
        }
    }
}
