using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.Content;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;
using Maham.CustomControl;
using Maham.Droid.Renderer;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomEntry), typeof(CustomEntryRenderer))]
namespace Maham.Droid.Renderer
{
    class CustomEntryRenderer : EntryRenderer
    {
        public CustomEntryRenderer(Context context) : base(context)
        {
        }

        CustomEntry imageentry;
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                var gradient = new GradientDrawable();
                var roundedEntry = Element as CustomEntry;
                imageentry = roundedEntry;
                var padding = (int)Utils.ConvertDpToPixel(Context, 10);

                if (roundedEntry != null)
                {
                    gradient.SetStroke(roundedEntry.BorderThickness, roundedEntry.BorderColor.ToAndroid());
                    gradient.SetCornerRadius(Utils.ConvertDpToPixel(Context, roundedEntry.BorderRadius));
                    gradient.SetColor(roundedEntry.EntryBackgroundColor.ToAndroid());



                    SetReturnType(roundedEntry);

                    // Editor Action is called when the return button is pressed
                    Control.EditorAction += (object sender, TextView.EditorActionEventArgs args) =>
                    {
                        if (roundedEntry.ReturnType != ReturnType.Next)
                            roundedEntry.Unfocus();

                        // Call all the methods attached to base_entry event handler Completed
                        roundedEntry.InvokeCompleted();
                    };

                }

                if (!roundedEntry.DisplaySuggestions)
                {
                    Control.InputType = Android.Text.InputTypes.TextFlagNoSuggestions;
                }
                if (!string.IsNullOrEmpty(roundedEntry.Image))
                {
                    switch (roundedEntry.ImageAlignment)

                    {

                        case ImageAlignmentEnum.Left:

                            Control.SetCompoundDrawablesWithIntrinsicBounds(GetDrawable(roundedEntry.Image), null, null, null);

                            break;

                        case ImageAlignmentEnum.Right:

                            Control.SetCompoundDrawablesWithIntrinsicBounds(null, null, GetDrawable(roundedEntry.Image), null);

                            break;

                    }
                }
                // Control.CompoundDrawablePadding = 10;


                Control.Background = gradient;
                Control.SetPadding(5, 10, 5, 10);
            }
        }
        public BitmapDrawable GetDrawable(string imageEntryImage)

        {

            int resID = Resources.GetIdentifier(imageEntryImage, "drawable", this.Context.PackageName);
            if (resID == 0) return null;

            var drawable = ContextCompat.GetDrawable(this.Context, resID);

            var bitmap = ((BitmapDrawable)drawable).Bitmap;

            return new BitmapDrawable(Resources, Bitmap.CreateScaledBitmap(bitmap, imageentry.ImageWidth, imageentry.ImageHeight, true));

        }
        private void SetReturnType(CustomEntry entry)
        {
            ReturnType type = entry.ReturnType;

            switch (type)
            {
                case ReturnType.Go:
                    Control.ImeOptions = ImeAction.Go;
                    Control.SetImeActionLabel("Go", ImeAction.Go);
                    break;
                case ReturnType.Next:
                    Control.ImeOptions = ImeAction.Next;
                    Control.SetImeActionLabel("Next", ImeAction.Next);
                    break;
                case ReturnType.Send:
                    Control.ImeOptions = ImeAction.Send;
                    Control.SetImeActionLabel("Send", ImeAction.Send);
                    break;
                case ReturnType.Search:
                    Control.ImeOptions = ImeAction.Search;
                    Control.SetImeActionLabel("Search", ImeAction.Search);
                    break;
                default:
                    Control.ImeOptions = ImeAction.Done;
                    Control.SetImeActionLabel("Done", ImeAction.Done);
                    break;
            }
        }
    }

}