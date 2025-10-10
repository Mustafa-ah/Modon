using System;
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
        private bool _isIpadBottomTabsApplied = false;

        public CustomTabBarRenderer()
        {
            // Early initialization for iPad
            if (UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Pad)
            {
                // Force compact traits early in the lifecycle
                ModalPresentationStyle = UIModalPresentationStyle.FullScreen;
            }
        }

        public override void ViewDidLoad()
        {
            // Apply trait override BEFORE calling base.ViewDidLoad
            if (UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Pad)
            {
                var compactTraits = UITraitCollection.FromHorizontalSizeClass(UIUserInterfaceSizeClass.Compact);
                SetOverrideTraitCollection(compactTraits, this);
            }

            base.ViewDidLoad();
            
            if (UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Pad)
            {
                // Additional setup
                TabBar.Translucent = false;
                EdgesForExtendedLayout = UIRectEdge.None;
                
                BeginInvokeOnMainThread(() =>
                {
                    ApplyIpadBottomTabs();
                });
            }
        }

        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            // Apply early in the renderer lifecycle
            if (e.NewElement != null && UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Pad)
            {
                var compactTraits = UITraitCollection.FromHorizontalSizeClass(UIUserInterfaceSizeClass.Compact);
                SetOverrideTraitCollection(compactTraits, this);
            }

            base.OnElementChanged(e);
        }

        public override void ViewWillAppear(bool animated)
        {
            // Re-apply trait collection before appearance
            if (UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Pad)
            {
                var compactTraits = UITraitCollection.FromHorizontalSizeClass(UIUserInterfaceSizeClass.Compact);
                SetOverrideTraitCollection(compactTraits, this);
            }

            base.ViewWillAppear(animated);

            if (UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Pad)
            {
                ApplyIpadBottomTabs();
                
                // Additional UI adjustments
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
                    
                    // Force update
                    TabBar.SetNeedsLayout();
                    TabBar.LayoutIfNeeded();
                }
            }
        }

        public override void TraitCollectionDidChange(UITraitCollection previousTraitCollection)
        {
            base.TraitCollectionDidChange(previousTraitCollection);

            if (UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Pad)
            {
                var compactTraits = UITraitCollection.FromHorizontalSizeClass(UIUserInterfaceSizeClass.Compact);
                SetOverrideTraitCollection(compactTraits, this);
                
                _isIpadBottomTabsApplied = false;
                ApplyIpadBottomTabs();
            }
        }

        public override UITraitCollection GetOverrideTraitCollectionForChildViewController(UIViewController childViewController)
        {
            if (UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Pad)
            {
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
                // Ensure tab bar is properly configured
                TabBar.Translucent = false;
                TabBar.Hidden = false;
                
                var tabBarHeight = TabBar.Frame.Height > 0 ? TabBar.Frame.Height : 49;
                TabBar.Frame = new CGRect(
                    0,
                    View.Frame.Height - tabBarHeight,
                    View.Frame.Width,
                    tabBarHeight
                );
                
                _isIpadBottomTabsApplied = true;
                
                System.Diagnostics.Debug.WriteLine("iPad bottom tabs applied successfully");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error applying iPad bottom tabs: {ex.Message}");
            }
        }
    }
}