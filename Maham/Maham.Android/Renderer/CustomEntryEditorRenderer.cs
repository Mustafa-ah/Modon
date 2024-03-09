using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Text;
using Android.Text.Method;
using Android.Views;
using Android.Widget;
using Maham.CustomControl;
using Maham.Droid.Renderer;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;


[assembly: ExportRenderer(typeof(CustomEditor), typeof(CustomEntryEditorRenderer))]
namespace Maham.Droid.Renderer
{
    public class CustomEntryEditorRenderer : EditorRenderer
    {
        

      public CustomEntryEditorRenderer(Context context):base(context)
        {

        }
        protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                GradientDrawable gd = new GradientDrawable();
                gd.SetColor(global::Android.Graphics.Color.Transparent);
                this.Control.SetBackground(gd);

               // this.Control.SetRawInputType(InputTypes.TextFlagNoSuggestions);
                //  Control.SetHintTextColor(ColorStateList.ValueOf(global::Android.Graphics.Color.White));

                var nativeEditText = (global::Android.Widget.EditText)Control;

                //While scrolling inside Editor stop scrolling parent view.
                nativeEditText.OverScrollMode = OverScrollMode.Always;
                nativeEditText.ScrollBarStyle = ScrollbarStyles.InsideInset;
                nativeEditText.SetOnTouchListener(new DroidTouchListener());

                //For Scrolling in Editor innner area
                Control.VerticalScrollBarEnabled = true;
                Control.MovementMethod = ScrollingMovementMethod.Instance;
                Control.ScrollBarStyle = Android.Views.ScrollbarStyles.InsideInset;
                //Force scrollbars to be displayed
                Android.Content.Res.TypedArray a = Control.Context.Theme.ObtainStyledAttributes(new int[0]);
                InitializeScrollbars(a);
                a.Recycle();

            }
        }
    }


    public class DroidTouchListener : Java.Lang.Object, Android.Views.View.IOnTouchListener
    {
        public bool OnTouch(Android.Views.View v, MotionEvent e)
        {
            v.Parent?.RequestDisallowInterceptTouchEvent(true);
            if ((e.Action & MotionEventActions.Up) != 0 && (e.ActionMasked & MotionEventActions.Up) != 0)
            {
                v.Parent?.RequestDisallowInterceptTouchEvent(false);
            }
            return false;
        }
    }
}