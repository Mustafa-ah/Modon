using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using Maham.CustomControl;
using Maham.iOS.Renderer;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(RoundedView), typeof(RoundedViewRenderers))]
namespace Maham.iOS.Renderer
{
  public  class RoundedViewRenderers : ViewRenderer<RoundedView, UIView>
    {
        protected override void OnElementChanged(ElementChangedEventArgs<RoundedView> e)
        {
            base.OnElementChanged(e);

            if (Element is RoundedView roundedView)
            {
                NativeView.Layer.CornerRadius = roundedView.BorderRadius;
                NativeView.Layer.BorderWidth = roundedView.BorderThickness;
                NativeView.Layer.BorderColor = roundedView.BorderColor.ToCGColor();
                NativeView.Layer.BackgroundColor = roundedView.BackgroundColor.ToCGColor();
            }
        }
    }
}