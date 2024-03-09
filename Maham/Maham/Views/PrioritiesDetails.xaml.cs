using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace Maham.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PrioritiesDetails : ContentPage
	{
		public PrioritiesDetails ()
		{
			InitializeComponent ();
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            FlowDirection = new Helpers.Helper().CurrentLanguage() == 1 ? FlowDirection.LeftToRight : FlowDirection.RightToLeft;
        }
    }
}