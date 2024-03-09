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

[assembly: ExportRenderer(typeof(RoundedProgressBar), typeof(RoundedProgressBarRenderer))]
namespace Maham.Droid.Renderer
{
    public class RoundedProgressBarRenderer : ViewRenderer<RoundedProgressBar, Android.Views.View>
    {
        public RoundedProgressBarRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<RoundedProgressBar> e)
        {
            base.OnElementChanged(e);

            var gradient = new GradientDrawable();


            if (Element is RoundedProgressBar roundedView)
            {
                gradient.SetStroke(roundedView.BorderThickness, roundedView.BorderColor.ToAndroid());
                gradient.SetCornerRadius(roundedView.BorderRadius);
                gradient.SetColor(roundedView.BackgroundColor.ToAndroid());
            }

            Background = gradient;
        }
    }
}