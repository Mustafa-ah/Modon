using System;
using Android.Graphics.Drawables;
using Maham.Effects;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ResolutionGroupName("Global")]
[assembly: ExportEffect(typeof(Maham.Droid.Renderer.EntryAndroidEffect), nameof(EntryEffect))]
namespace Maham.Droid.Renderer
{
    public class EntryAndroidEffect : PlatformEffect
    {
      

        protected override void OnAttached()
        {
            //Control.Background = null;
            this.Control.Background = new ColorDrawable(Android.Graphics.Color.Transparent);
        }

        protected override void OnDetached()
        {
            //throw new NotImplementedException();
        }
    }
}
