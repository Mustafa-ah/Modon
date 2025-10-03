using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreGraphics;
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


        public override void TraitCollectionDidChange(UITraitCollection previousTraitCollection)
        {
            base.TraitCollectionDidChange(previousTraitCollection);

            if (UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Pad)
            {
                // Always force compact width traits on iPad
                var compact = UITraitCollection.FromHorizontalSizeClass(UIUserInterfaceSizeClass.Compact);
                SetOverrideTraitCollection(compact, this);
            }
        }

        public override void ViewDidLayoutSubviews()
        {
            base.ViewDidLayoutSubviews();

            if (UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Pad)
            {
                if (TabBar != null && View != null)
                {
                    var tabBarHeight = TabBar.Frame.Height;
                    TabBar.Frame = new CGRect(
                        0,
                        View.Frame.Height - tabBarHeight,
                        View.Frame.Width,
                        tabBarHeight
                    );
                }
            }
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            if (UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Pad)
            {
                // Force iPhone-style bottom tabs
                if (ViewControllers != null)
                {
                    foreach (var vc in ViewControllers)
                    {
                        vc.TabBarItem.TitlePositionAdjustment = new UIOffset(0, 0);
                    }
                }
            }
        }



        //public override UITraitCollection OverrideTraitCollectionForChildViewController(UIViewController childViewController)
        //{
        //    if (UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Pad)
        //    {
        //        return UITraitCollection.FromHorizontalSizeClass(UIUserInterfaceSizeClass.Compact);
        //    }
        //    return base.OverrideTraitCollectionForChildViewController(childViewController);
        //}

        public override UITraitCollection GetOverrideTraitCollectionForChildViewController(UIViewController childViewController)
        {
            if (UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Pad)
            {
                // Force iPhone-like compact width
                return UITraitCollection.FromHorizontalSizeClass(UIUserInterfaceSizeClass.Compact);
            }
            return base.GetOverrideTraitCollectionForChildViewController(childViewController);
        }

        /*public override void ViewDidLoad()
        {
            base.ViewDidLoad();
        
            // Force tabs to bottom on iPad
            if (UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Pad)
            {
                TabBar.BackgroundColor = UIColor.Yellow; // Set your desired color
            
                // This forces the tab bar to stay at bottom
                EdgesForExtendedLayout = UIRectEdge.None;
            }
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            if (UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Pad)
            {
                TabBar.Translucent = false;
                TabBar.Hidden = false;
            }
        }

        public override void ViewDidLayoutSubviews()
        {
            base.ViewDidLayoutSubviews();

            if (UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Pad)
            {
                if (TabBar != null && View != null)
                {
                    var tabBarHeight = TabBar.Frame.Height;
                    TabBar.Frame = new CGRect(
                        0,
                        View.Frame.Height - tabBarHeight,
                        View.Frame.Width,
                        tabBarHeight
                    );
                }
            }
        }

        public override void TraitCollectionDidChange(UITraitCollection previousTraitCollection)
        {
            base.TraitCollectionDidChange(previousTraitCollection);

            if (UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Pad)
            {
                // Force compact width so iPad behaves like iPhone layout
                SetOverrideTraitCollection(
                    UITraitCollection.FromHorizontalSizeClass(UIUserInterfaceSizeClass.Compact),
                    this
                );
            }
        }*/

        /*public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            
            // Force tabs to bottom regardless of device
            if (TabBar != null)
            {
                TabBar.Frame = new CoreGraphics.CGRect(0, View.Frame.Height - TabBar.Frame.Height, View.Frame.Width, TabBar.Frame.Height);
            }
        }

        public override void ViewDidLayoutSubviews()
        {
            base.ViewDidLayoutSubviews();
            
            // Ensure tab bar stays at bottom
            if (TabBar != null && View != null)
            {
                var tabBarHeight = TabBar.Frame.Height;
                TabBar.Frame = new CoreGraphics.CGRect(0, View.Frame.Height - tabBarHeight, View.Frame.Width, tabBarHeight);
            }
        }*/

        //public override void ViewWillAppear(bool animated)
        //{
        //    base.ViewWillAppear(animated);

        //    if (UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Pad)
        //    {
        //        // Additional positioning if needed
        //        TabBar.Frame = new CoreGraphics.CGRect(0, View.Frame.Height - TabBar.Frame.Height, View.Frame.Width, TabBar.Frame.Height);
        //    }
        //}
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