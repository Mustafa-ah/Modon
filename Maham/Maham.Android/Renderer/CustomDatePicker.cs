using System;
using Android.Content;
using Java.Util;
using Maham.Droid.Renderer;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(DatePicker), typeof(CustomDatePicker))]
namespace Maham.Droid.Renderer
{
    public class CustomDatePicker : DatePickerRenderer
    {
        public CustomDatePicker(Context context):base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<DatePicker> e)
        {
            base.OnElementChanged(e);

            if (this.Control != null)
            {

                Control.Background = null;

                var layoutParams = new MarginLayoutParams(Control.LayoutParameters);
                layoutParams.SetMargins(0, 0, 0, 0);
                LayoutParameters = layoutParams;
                Control.LayoutParameters = layoutParams;
                Control.SetPadding(0, 0, 0, 0);
                SetPadding(0, 0, 0, 0);

                Locale locale = new Locale("EN");
                Control.TextLocale = locale;
            }
        }
    }
}
