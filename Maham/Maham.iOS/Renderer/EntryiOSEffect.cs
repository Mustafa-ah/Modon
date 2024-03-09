using System;
using Maham.Effects;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ResolutionGroupName("Global")]
[assembly: ExportEffect(typeof(Maham.iOS.Renderer.EntryiOSEffect), nameof(EntryEffect))]
namespace Maham.iOS.Renderer
{
    public class EntryiOSEffect : PlatformEffect
    {

        protected override void OnAttached()
        {
            var textField = (UITextField)this.Control;
            textField.BorderStyle = UITextBorderStyle.None;
        }

        protected override void OnDetached()
        {
            //throw new NotImplementedException();
        }
    }
}
