using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreAnimation;
using Foundation;
using Maham.CustomControl;
using Maham.iOS.Renderer;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
[assembly: ExportRenderer(typeof(DashedView), typeof(MyFrameRenderer))]
namespace Maham.iOS.Renderer
{
    public class MyFrameRenderer :  ViewRenderer<DashedView, FrameRenderer>
    {
        DashedView dashview;
        //protected override void OnElementChanged(ElementChangedEventArgs<DashedView> e)
        //{
        //    base.OnElementChanged(e);

        //    if (Element is DashedView roundedView)
        //    {
        //        CAShapeLayer viewBorder = new CAShapeLayer();
        //        viewBorder.StrokeColor = roundedView.BorderColor2.ToCGColor();
        //        viewBorder.FillColor = null;
        //        viewBorder.LineDashPattern = new NSNumber[] { new NSNumber(5), new NSNumber(2) };
        //        viewBorder.Frame = NativeView.Bounds;
        //        viewBorder.Path = UIBezierPath.FromRect(NativeView.Bounds).CGPath;
        //        Layer.AddSublayer(viewBorder);

        //        // If you don't want the shadow effect
        //        Element.HasShadow = false;
        //    }
        //}
        public override void LayoutSubviews()
        {
            base.LayoutSubviews();

            CAShapeLayer viewBorder = new CAShapeLayer();
            viewBorder.StrokeColor =viewBorder.BorderColor;
            viewBorder.FillColor =null;
            viewBorder.LineDashPattern = new NSNumber[] { new NSNumber(10), new NSNumber(10) };
            viewBorder.Frame = NativeView.Bounds;
            viewBorder.LineWidth = 0.2f;
            viewBorder.Path = UIBezierPath.FromRect(NativeView.Bounds).CGPath;
            Layer.AddSublayer(viewBorder);

            // If you don't want the shadow effect
            Element.HasShadow = false;
        }


    }
}