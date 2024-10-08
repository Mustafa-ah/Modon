﻿//using Messier16.Forms.Controls;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;

namespace Maham.CustomControl
{
    [Flags]
    public enum BarBackgroundApplyTo
    {
        None = 1,
        Android = 2,
        iOS = 4,
        Both = Android | iOS
    }

    public enum BarStyle
    {
        Default = 1,
        Black = 2
    }

    public class ExtCustomTabbedPage : TabbedPage
    {

        public static readonly BindableProperty SelectedColorProperty =
            BindableProperty.Create(nameof(SelectedColor), typeof(Color), typeof(ExtCustomTabbedPage), default(Color));

        public Color SelectedColor
        {
            get => (Color)GetValue(SelectedColorProperty);
            set => SetValue(SelectedColorProperty, value);
        }

        public static readonly BindableProperty UnselectedItemTintColorProperty =
            BindableProperty.Create(nameof(UnselectedItemTintColor), typeof(Color), typeof(ExtCustomTabbedPage), default(Color));

        public Color UnselectedItemTintColor
        {
            get => (Color)GetValue(UnselectedItemTintColorProperty);
            set => SetValue(UnselectedItemTintColorProperty, value);
        }


        public static readonly BindableProperty BarBackgroundApplyToProperty =
            BindableProperty.Create(nameof(BarBackgroundApplyTo), typeof(BarBackgroundApplyTo), typeof(ExtCustomTabbedPage), BarBackgroundApplyTo.Android);

        public BarBackgroundApplyTo BarBackgroundApplyTo
        {
            get => (BarBackgroundApplyTo)GetValue(BarBackgroundApplyToProperty);
            set => SetValue(BarBackgroundApplyToProperty, value);
        }

        public static readonly BindableProperty BarStyleProperty =
            BindableProperty.Create(nameof(BarStyle), typeof(BarStyle), typeof(ExtCustomTabbedPage), BarStyle.Default);

        public BarStyle BarStyle
        {
            get => (BarStyle)GetValue(BarStyleProperty);
            set => SetValue(BarStyleProperty, value);
        }

        public new static readonly BindableProperty BarBackgroundColorProperty =
            BindableProperty.Create(nameof(BarBackgroundColor), typeof(Color), typeof(ExtCustomTabbedPage), default(Color));


        public new Color BarBackgroundColor
        {
            get => (Color)GetValue(BarBackgroundColorProperty);
            set => SetValue(BarBackgroundColorProperty, value);
        }
    }
}
