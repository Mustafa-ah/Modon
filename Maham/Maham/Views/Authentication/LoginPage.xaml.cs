using System;
using Maham.Resources;
using Maham.ViewModels.Authentication;
using Xamarin.Forms;

namespace Maham.Views.Authentication
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            SetEntryFocus();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        private void TapGestureRecognizer_Tapped(object sender, System.EventArgs e)
        {
            password.IsPassword = password.IsPassword ? false : true;
            show1label.Text = AppResource.showtext; ;
            hideimage.Source = "show";
            if (password.IsPassword==false)
            {
                show1label.Text = AppResource.hidetext; ;
                hideimage.Source = "hideye";
            }
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();

          
        }
       
       private void SetEntryFocus()
        {
            if (Device.RuntimePlatform == Device.iOS)
            {
                login.Unfocused += (s, e) => password.Focus();
            }
            else if (Device.RuntimePlatform == Device.Android)
            {
                login.Completed += (s, e) => password.Focus();
            }
        }
    }
}
