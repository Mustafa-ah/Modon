using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace Maham.Views
{
    public partial class NewTaskPage : ContentPage
    {
        bool isRTL;
        public NewTaskPage()
        {
            InitializeComponent();
            //TasknameEntry.Completed += (s, e) => AssignEntry.Focus();
            // assignlable.Completed += (s, e) => refrencelable.Focus();
            //refrencelable.Completed += (s, e) => describtionlable.Focus();
            isRTL = new Helpers.Helper().CurrentLanguage() == 2;
            RootScrollView.FlowDirection = isRTL ? FlowDirection.RightToLeft : FlowDirection.LeftToRight;
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);

            if (isRTL)
            {
                if (Device.RuntimePlatform == Device.iOS)
                {
                    //TasknameEntry.HorizontalTextAlignment = TextAlignment.End;
                   // AssignEntry.HorizontalTextAlignment = TextAlignment.End;
                }
                else
                {
                 //   AssignEntry.HorizontalTextAlignment = TextAlignment.Start;
                   // TasknameEntry.HorizontalTextAlignment = TextAlignment.Start;
                }
            }
           
        }
    }
}
