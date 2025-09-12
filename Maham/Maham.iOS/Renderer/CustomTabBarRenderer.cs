using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using Maham.CustomControl;
using Maham.iOS.Renderer;
using Maham.Views;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
[assembly: ExportRenderer(typeof(ExtCustomTabbedPage), typeof(CustomTabBarRenderer))]
namespace Maham.iOS.Renderer
{
    public class CustomTabBarRenderer : TabbedRenderer
    {
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
        
            // Force tabs to bottom on iPad
            if (UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Pad)
            {
                TabBar.BackgroundColor = UIColor.White; // Set your desired color
            
                // This forces the tab bar to stay at bottom
                EdgesForExtendedLayout = UIRectEdge.None;
            }
        }
    
        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
        
            if (UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Pad)
            {
                // Additional positioning if needed
                TabBar.Frame = new CoreGraphics.CGRect(0, View.Frame.Height - TabBar.Frame.Height, View.Frame.Width, TabBar.Frame.Height);
            }
        }
        //public override void ViewWillAppear(bool animated)
        //{
        //    if (TabBar?.Items == null)
        //        return;

        //    // Go through our elements and change them
        //    var tabs = Element as TabbedPage;
        //    if (tabs != null)
        //    {
        //        for (int i = 0; i < TabBar.Items.Length; i++)
        //            UpdateTabBarItem(TabBar.Items[i]);
        //    }

        //    base.ViewWillAppear(animated);
        //}

        //private void UpdateTabBarItem(UITabBarItem item)
        //{
        //    if (item == null)
        //        return;

        //    // Set the font for the title.
        //    item.SetTitleTextAttributes(new UITextAttributes() { Font = UIFont.FromName("Your-Font", 10) }, UIControlState.Normal);
        //    item.SetTitleTextAttributes(new UITextAttributes() { Font = UIFont.FromName("Your-Font", 10) }, UIControlState.Selected);

        //    // Moves the titles up just a bit.
        //    item.TitlePositionAdjustment = new UIOffset(0, -2);
        //}
    }
}