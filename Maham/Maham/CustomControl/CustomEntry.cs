using System;
using System.Collections.Generic;
using System.Text;
using Maham.Enums;
using Xamarin.Forms;

namespace Maham.CustomControl
{
   public class CustomEntry : Entry
    {

        public CustomEntry()
        {
            this.HeightRequest = 50;
        }


        public static BindableProperty DisplaySuggestionsProperty = BindableProperty.Create(nameof(DisplaySuggestions), typeof(bool), typeof(CustomEntry), true);
        public static BindableProperty BorderThicknessProperty = BindableProperty.Create(nameof(BorderThickness), typeof(int), typeof(CustomEntry), 0);
        public static BindableProperty BorderRadiusProperty = BindableProperty.Create(nameof(BorderRadius), typeof(int), typeof(CustomEntry), 0);
        public static BindableProperty BorderColorProperty = BindableProperty.Create(nameof(BorderColor), typeof(Color), typeof(CustomEntry), Color.Default);
        public static BindableProperty EntryBackgroundColorProperty = BindableProperty.Create(nameof(EntryBackgroundColor), typeof(Color), typeof(CustomEntry), Color.Default);
        public static readonly BindableProperty ImageProperty = BindableProperty.Create(nameof(Image), typeof(string), typeof(CustomEntry), string.Empty);
        public static readonly BindableProperty ImageHeightProperty =BindableProperty.Create(nameof(ImageHeight), typeof(int), typeof(CustomEntry), 50);
        public static readonly BindableProperty ImageWidthProperty = BindableProperty.Create(nameof(ImageWidth), typeof(int), typeof(CustomEntry), 50);
        public static readonly BindableProperty ImageAlignmentProperty = BindableProperty.Create(nameof(ImageAlignment), typeof(ImageAlignmentEnum), typeof(CustomEntry), ImageAlignmentEnum.Left);
        public bool DisplaySuggestions
        {
            get { return (bool)GetValue(DisplaySuggestionsProperty); }
            set { SetValue(DisplaySuggestionsProperty, value); }
        }

        public int BorderThickness
        {
            get { return (int)GetValue(BorderThicknessProperty); }
            set { SetValue(BorderThicknessProperty, value); }
        }

        public int BorderRadius
        {
            get { return (int)GetValue(BorderRadiusProperty); }
            set { SetValue(BorderRadiusProperty, value); }
        }

        public Color BorderColor
        {
            get { return (Color)GetValue(BorderColorProperty); }
            set { SetValue(BorderColorProperty, value); }
        }
        public int ImageWidth

        {

            get { return (int)GetValue(ImageWidthProperty); }

            set { SetValue(ImageWidthProperty, value); }

        }



        public int ImageHeight

        {

            get { return (int)GetValue(ImageHeightProperty); }

            set { SetValue(ImageHeightProperty, value); }

        }



        public string Image

        {

            get { return (string)GetValue(ImageProperty); }

            set { SetValue(ImageProperty, value); }

        }



        public ImageAlignmentEnum ImageAlignment

        {

            get { return (ImageAlignmentEnum)GetValue(ImageAlignmentProperty); }

            set { SetValue(ImageAlignmentProperty, value); }

        }

    public Color EntryBackgroundColor
        {
            get { return (Color)GetValue(EntryBackgroundColorProperty); }
            set { SetValue(EntryBackgroundColorProperty, value); }
        }




        //public static readonly BindableProperty ReturnTypeProperty = BindableProperty.Create(
        //    nameof(ReturnType),
        //    typeof(ReturnType),
        //    typeof(CustomEntry),
        //    ReturnType.Done,
        //    BindingMode.OneWay
        //);

        //public ReturnType ReturnType
        //{
        //    get { return (ReturnType)GetValue(ReturnTypeProperty); }
        //    set { SetValue(ReturnTypeProperty, value); }
        //}

        public new event EventHandler Completed;
        public void InvokeCompleted()
        {
            if (this.Completed != null)
                this.Completed.Invoke(this, null);
        }


    }
    public enum ImageAlignmentEnum
    {

        Left,
        Right
    }
}