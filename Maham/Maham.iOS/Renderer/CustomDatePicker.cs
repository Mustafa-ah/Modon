using System;
using Maham.iOS.Renderer;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(DatePicker), typeof(CustomDatePicker))]
namespace Maham.iOS.Renderer
{
    public class CustomDatePicker : DatePickerRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<DatePicker> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null && this.Control != null)
            {
                try
                {
                    if (UIDevice.CurrentDevice.CheckSystemVersion(13, 2))
                    {
                        UIDatePicker picker = (UIDatePicker)Control.InputView;
                        picker.PreferredDatePickerStyle = UIDatePickerStyle.Wheels;

                    }

                    var date = (UIDatePicker)Control.InputView;
                    date.Locale = new Foundation.NSLocale("EN");

                    Control.Layer.BorderWidth = 0;
                    Control.BorderStyle = UITextBorderStyle.None;
                }
                catch 
                {
                    // do nothing
                }
            }
        }
    }
}
