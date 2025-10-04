using System;
using CoreGraphics;
using Maham.CustomControl;
using Maham.iOS.Renderer;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ExtCustomTabbedPage), typeof(CustomTabBarRenderer))]
namespace Maham.iOS.Renderer
{
    public class CustomTabBarRenderer : TabbedRenderer
    {
        private bool _isIpadBottomTabsApplied = false;

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            
            if (UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Pad)
            {
                // Apply initial setup as soon as possible
                // Just ensure it runs on main thread in next cycle
                BeginInvokeOnMainThread(() => 
                {
                    ApplyIpadBottomTabs();
                });
            }
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            if (UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Pad)
            {
                ApplyIpadBottomTabs();
                
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

        public override void ViewDidLayoutSubviews()
        {
            base.ViewDidLayoutSubviews();

            if (UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Pad)
            {
                ApplyIpadBottomTabs();
                
                if (TabBar != null && View != null)
                {
                    var tabBarHeight = TabBar.Frame.Height;
                    TabBar.Frame = new CGRect(
                        0,
                        View.Frame.Height - tabBarHeight,
                        View.Frame.Width,
                        tabBarHeight
                    );
                    
                    // Ensure tab bar is visible and properly positioned
                    TabBar.Hidden = false;
                }
            }
        }

        public override void TraitCollectionDidChange(UITraitCollection previousTraitCollection)
        {
            base.TraitCollectionDidChange(previousTraitCollection);

            if (UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Pad)
            {
                // Always force compact width traits on iPad
                var compact = UITraitCollection.FromHorizontalSizeClass(UIUserInterfaceSizeClass.Compact);
                SetOverrideTraitCollection(compact, this);
                
                _isIpadBottomTabsApplied = false; // Reset to reapply
                ApplyIpadBottomTabs();
            }
        }

        public override UITraitCollection GetOverrideTraitCollectionForChildViewController(UIViewController childViewController)
        {
            if (UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Pad)
            {
                // Force iPhone-like compact width
                return UITraitCollection.FromHorizontalSizeClass(UIUserInterfaceSizeClass.Compact);
            }
            return base.GetOverrideTraitCollectionForChildViewController(childViewController);
        }

        private void ApplyIpadBottomTabs()
        {
            if (_isIpadBottomTabsApplied || TabBar == null || View == null) 
                return;

            try
            {
                // Force tab bar to bottom
                if (TabBar != null)
                {
                    TabBar.Translucent = false;
                    
                    // Ensure tab bar stays at bottom with proper sizing
                    var tabBarHeight = TabBar.Frame.Height > 0 ? TabBar.Frame.Height : 49; // Default height
                    TabBar.Frame = new CGRect(
                        0,
                        View.Frame.Height - tabBarHeight,
                        View.Frame.Width,
                        tabBarHeight
                    );
                    
                    // Force layout if needed
                    TabBar.SetNeedsLayout();
                    TabBar.LayoutIfNeeded();
                }
                
                _isIpadBottomTabsApplied = true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error applying iPad bottom tabs: {ex.Message}");
            }
        }
    }
}