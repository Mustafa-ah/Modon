﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using Maham.iOS.Renderer;
using Maham.Views;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
//[assembly: ExportRenderer(typeof(TasksPage), typeof(CustomTabBarRenderer))]
namespace Maham.iOS.Renderer
{
    public class CustomTabBarRenderer : TabbedRenderer
    {
        public override void ViewWillAppear(bool animated)
        {
            if (TabBar?.Items == null)
                return;

            // Go through our elements and change them
            var tabs = Element as TabbedPage;
            if (tabs != null)
            {
                for (int i = 0; i < TabBar.Items.Length; i++)
                    UpdateTabBarItem(TabBar.Items[i]);
            }

            base.ViewWillAppear(animated);
        }

        private void UpdateTabBarItem(UITabBarItem item)
        {
            if (item == null)
                return;

            // Set the font for the title.
            item.SetTitleTextAttributes(new UITextAttributes() { Font = UIFont.FromName("Your-Font", 10) }, UIControlState.Normal);
            item.SetTitleTextAttributes(new UITextAttributes() { Font = UIFont.FromName("Your-Font", 10) }, UIControlState.Selected);

            // Moves the titles up just a bit.
            item.TitlePositionAdjustment = new UIOffset(0, -2);
        }
    }
}