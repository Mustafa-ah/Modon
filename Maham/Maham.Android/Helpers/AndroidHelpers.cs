﻿using System;
using System.Collections.Generic;
using Android.Support.Design.Internal;
using Android.Support.Design.Widget;
using Microsoft.AppCenter.Crashes;

namespace Maham.Helpers
{
    public static class AndroidHelpers
    {
        public static void SetShiftMode(this BottomNavigationView bottomNavigationView, bool enableShiftMode, bool enableItemShiftMode)
        {
            try
            {
                var menuView = bottomNavigationView.GetChildAt(0) as BottomNavigationMenuView;
                if (menuView == null)
                {
                    System.Diagnostics.Debug.WriteLine("Unable to find BottomNavigationMenuView");
                    return;
                }


                var shiftMode = menuView.Class.GetDeclaredField("mShiftingMode");

                shiftMode.Accessible = true;
                shiftMode.SetBoolean(menuView, enableShiftMode);
                shiftMode.Accessible = false;
                shiftMode.Dispose();


                for (int i = 0; i < menuView.ChildCount; i++)
                {
                    var item = menuView.GetChildAt(i) as BottomNavigationItemView;
                    if (item == null)
                        continue;

                    //item.SetShiftingMode(enableItemShiftMode);
                    item.SetChecked(item.ItemData.IsChecked);

                }

                menuView.UpdateMenuView();
            }
          
            catch (Exception exception)
            {
                System.Diagnostics.Debug.WriteLine($"Unable to set shift mode: {exception}");
                var properties = new Dictionary<string, string>
                       {
                             { "androidhelper", "setshiftmode" },
                       };
                Crashes.TrackError(exception, properties);
            }
        }
    }
}
