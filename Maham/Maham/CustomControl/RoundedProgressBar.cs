using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Maham.CustomControl
{
   public class RoundedProgressBar:ProgressBar
    {
        public static BindableProperty BorderRadiusProperty = BindableProperty.Create(nameof(BorderRadius), typeof(int), typeof(RoundedProgressBar), 0);
        public static BindableProperty BorderThicknessProperty = BindableProperty.Create(nameof(BorderThickness), typeof(int), typeof(RoundedProgressBar), 0);
        public static BindableProperty BorderColorProperty = BindableProperty.Create(nameof(BorderColor), typeof(Color), typeof(RoundedProgressBar), Color.Default);
        public RoundedProgressBar()
        {
            var tapInterceptGesture = new TapGestureRecognizer();
            GestureRecognizers.Add(tapInterceptGesture);
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
    }

}