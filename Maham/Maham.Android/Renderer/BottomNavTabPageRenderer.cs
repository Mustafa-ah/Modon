using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Opengl;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.View;
using Android.Views;
using Android.Widget;
using Messier16.Forms.Controls;
using Plugin.Badge.Droid;
using Maham.CustomControl;
using Maham.Droid.Renderers;
using Maham.Helpers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms.Platform.Android.AppCompat;
using View = Android.Views.View;
using Syncfusion.Android;

[assembly: ExportRenderer(typeof(ExtCustomTabbedPage), typeof(BottomNavTabPageRenderer))]

namespace Maham.Droid.Renderers
{
    public class BottomNavTabPageRenderer : BadgedTabbedPageRenderer, TabLayout.IOnTabSelectedListener
    {
        private bool _isShiftModeSet;
       
        public BottomNavTabPageRenderer(Context context)
            : base(context)
        {

        }
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            try
            {
                base.OnElementPropertyChanged(sender, e);
            }
            catch (Exception ex)
            {
                
            }
           
          
        }
      
        //protected override void OnMeasure(int widthMeasureSpec, int heightMeasureSpec)
        //{
        //    base.OnMeasure(widthMeasureSpec, heightMeasureSpec);
        //    try
        //    {
        //        Type myType = this.GetType();

        //        List<(string name, MethodInfo method)> publicMethods = myType.GetMethods(BindingFlags.Instance | BindingFlags.Public).Select(item => (item.Name, item)).Where(item => item.Name.ToLower().Contains("tab")).ToList();
        //        List<(string name, MethodInfo method)> nonPublicMethods = myType.GetMethods(BindingFlags.Instance | BindingFlags.NonPublic).Select(item => (item.Name, item)).Where(item => item.Name.ToLower().Contains("tab")).ToList();

        //        List<(string name, PropertyInfo property)> publicProperties = myType.GetProperties(BindingFlags.Instance | BindingFlags.Public).Select(item => (item.Name, item)).Where(item => item.Name.ToLower().Contains("tab")).ToList();
        //        List<(string name, PropertyInfo property)> nonPublicProperties = myType.GetProperties(BindingFlags.Instance | BindingFlags.NonPublic).Select(item => (item.Name, item)).Where(item => item.Name.ToLower().Contains("tab")).ToList();



        //        //IEnumerable<MethodInfo> methods = this.GetType().GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
        //        //IEnumerable<PropertyInfo> properties = this.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

        //    }
        //    catch (Exception e)
        //    {

        //    }
        //}
        protected override void OnLayout(bool changed, int l, int t, int r, int b)
        {
         
            try
            {
                base.OnLayout(changed, l, t, r, b);
                //if (!_isShiftModeSet)
                //{
                //    var children = GetAllChildViews(ViewGroup);
                //    if (children.SingleOrDefault(x => x is BottomNavigationView) is BottomNavigationView bottomNav)
                //    {
                //        bottomNav.SetShiftMode(false, false);
                //        _isShiftModeSet = true;
                //    }
                //}
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error setting ShiftMode: {e}");
            }
        }

        private List<View> GetAllChildViews(View view)
        {
            if (!(view is ViewGroup group))
            {
                return new List<View> { view };
            }

            var result = new List<View>();

            for (int i = 0; i < group.ChildCount; i++)
            {
                var child = group.GetChildAt(i);

                var childList = new List<View> { child };
                childList.AddRange(GetAllChildViews(child));

                result.AddRange(childList);
            }

            return result.Distinct().ToList();
        }
        public override bool OnTouchEvent(MotionEvent e)
        {
             //base.OnTouchEvent(e);
            return true;
        }

        public void OnTabReselected(TabLayout.Tab tab)
        {
            tab.SetText(tab.Text + "f");
            //throw new NotImplementedException();
        }

        public void OnTabSelected(TabLayout.Tab tab)
        {
            //throw new NotImplementedException();
        }

        public void OnTabUnselected(TabLayout.Tab tab)
        {
            //throw new NotImplementedException();
        }
    }
    public static class ViewGroupExtension
    {
        public static IEnumerable<T> Children<T>(this ViewGroup parent)
            where T : Android.Views.View
        {
            for (int x = 0; x < parent.ChildCount; ++x)
            {
                Android.Views.View child = parent.GetChildAt(x);
                if (child is T tChild)
                    yield return tChild;
                if (child is Android.Views.ViewGroup vg)
                {
                    foreach (T item in vg.Children<T>())
                        yield return item;
                }
            }
        }
    }
}