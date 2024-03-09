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

[assembly: ExportRenderer(typeof(CustomEditor), typeof(CustomEditorRenderer))]
namespace Maham.iOS.Renderer
{
   public class CustomEditorRenderer : EditorRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
        {

            base.OnElementChanged(e);
            if (Control != null)
            {
               
                //Control.BorderStyle = UITextBorderStyle.None;
                //Control.Layer.CornerRadius = 10;
                //Control.TextColor = UIColor.White;
            }
        }
    }
}