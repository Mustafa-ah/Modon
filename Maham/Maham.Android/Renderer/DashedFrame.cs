using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Maham.CustomControl;
using Maham.Droid.Renderer;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
[assembly: ExportRenderer(typeof(DashedView), typeof(DashedFrame))]
namespace Maham.Droid.Renderer
{
   public class DashedFrame : ViewRenderer<DashedView, Android.Views.View>
    {
        public DashedFrame(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<DashedView> e)
        {
            base.OnElementChanged(e);

            var gradient = new GradientDrawable();

            if (Element is DashedView roundedView)
            {
                gradient.SetStroke(1, roundedView.BorderColor.ToAndroid(),15,20);
                gradient.SetCornerRadius(roundedView.CornerRadius);
                gradient.SetColor(roundedView.BackgroundColor.ToAndroid());
            }

            Background = gradient;
        }
    }
}