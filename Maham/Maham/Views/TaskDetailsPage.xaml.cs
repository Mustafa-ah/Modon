using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace Maham.Views
{
    public partial class TaskDetailsPage : ContentPage
    {
        public TaskDetailsPage()
        {
            InitializeComponent();
            int lang = new Helpers.Helper().CurrentLanguage();
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            App.Current.On<Xamarin.Forms.PlatformConfiguration.Android>().UseWindowSoftInputModeAdjust(WindowSoftInputModeAdjust.Resize);
            RootScroll.FlowDirection = lang == 1 ? FlowDirection.LeftToRight : FlowDirection.RightToLeft;
            //toolbar.FlowDirection = lang == 1 ? FlowDirection.LeftToRight : FlowDirection.RightToLeft;
            TaskProgress.Rotation = lang == 1 ? 0 : 180;
            TaskSlider.Rotation = lang == 1 ? 0 : 180;
        }

        private async void CustomEditor_Focused(object sender, FocusEventArgs e)
        {
            await System.Threading.Tasks.Task.Delay(400);
           await RootScroll.ScrollToAsync(CommentBtn, ScrollToPosition.MakeVisible, true);
        }

        protected override void OnAppearing()
        {
            ViewModels.TaskDetailsPageViewModel vm = this.BindingContext as ViewModels.TaskDetailsPageViewModel;
            vm?.OnAppearing();
            base.OnAppearing();
        }
    }
}
