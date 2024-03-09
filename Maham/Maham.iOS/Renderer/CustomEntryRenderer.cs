using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using CoreGraphics;
using Foundation;
using Maham.CustomControl;
using Maham.iOS.Renderer;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomEntry), typeof(CustomEntryRenderer))]
namespace Maham.iOS.Renderer
{
   public class CustomEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                var roundedView = Element as CustomEntry;

                if (roundedView != null)
                {
                    NativeView.Layer.CornerRadius = roundedView.BorderRadius;
                    NativeView.Layer.BorderWidth = roundedView.BorderThickness;
                    NativeView.Layer.BorderColor = roundedView.BorderColor.ToCGColor();
                    NativeView.Layer.BackgroundColor = roundedView.EntryBackgroundColor.ToCGColor();
                }
                //if (!string.IsNullOrEmpty(roundedView.Image))

                //{

                //    switch (roundedView.ImageAlignment)

                //    {

                //        case ImageAlignmentEnum.Left:

                //            Control.LeftViewMode = UITextFieldViewMode.Always;

                //            Control.LeftView = GetImageView(roundedView.Image, roundedView.ImageHeight, roundedView.ImageWidth);

                //            break;

                //        case ImageAlignmentEnum.Right:

                //            Control.RightViewMode = UITextFieldViewMode.Always;

                //            Control.RightView = GetImageView(roundedView.Image, roundedView.ImageHeight, roundedView.ImageWidth);

                //            break;

                //    }

                //}
                if (roundedView != null && !roundedView.DisplaySuggestions)
                {
                    Control.AutocorrectionType = UITextAutocorrectionType.No;

                    SetReturnType(roundedView);

                    //Control.ShouldReturn += (UITextField tf) =>
                    //{
                    //    roundedView.InvokeCompleted();
                    //    return true;
                    //};
                }

                Control.LeftView = new UIView(new CGRect(0, 0, 15, Control.Frame.Height));
                Control.RightView = new UIView(new CGRect(0, 0, 15, Control.Frame.Height));
                Control.LeftViewMode = UITextFieldViewMode.Always;
                Control.RightViewMode = UITextFieldViewMode.Always;
                Control.BorderStyle = UITextBorderStyle.None;
            }
        }
        private UIView GetImageView(string imagePath, int height, int width)

        {

            var uiImageView = new UIImageView(UIImage.FromBundle(imagePath))

            {

                Frame = new RectangleF(0, 0, width, height)

            };

            UIView objLeftView = new UIView(new System.Drawing.Rectangle(0, 0, width, height));

            objLeftView.AddSubview(uiImageView);



            return objLeftView;

        }

        private void SetReturnType(CustomEntry entry)
        {
            ReturnType type = entry.ReturnType;

            switch (type)
            {
                case ReturnType.Go:
                    Control.ReturnKeyType = UIReturnKeyType.Go;
                    break;
                case ReturnType.Next:
                    Control.ReturnKeyType = UIReturnKeyType.Next;
                    break;
                case ReturnType.Send:
                    Control.ReturnKeyType = UIReturnKeyType.Send;
                    break;
                case ReturnType.Search:
                    Control.ReturnKeyType = UIReturnKeyType.Search;
                    break;
                case ReturnType.Done:
                    Control.ReturnKeyType = UIReturnKeyType.Done;
                    break;
                default:
                    Control.ReturnKeyType = UIReturnKeyType.Default;
                    break;
            }
        }
    }

}