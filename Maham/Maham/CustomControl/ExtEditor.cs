
using System;
using System.Collections.Generic;
using System.Text;
using Maham.Constants;
using Maham.Enums;
using Xamarin.Forms;

namespace Maham.CustomControl
{
    public class ExtEditor : Editor
    {
        /// <summary>
        /// The has under line property.
        /// </summary>
        public static readonly BindableProperty HorizontalTextAlignmentProperty = BindableProperty.Create(
            "HorizontalTextAlignment",
            typeof(TextAlignment),
            typeof(ExtEditor),
            TextAlignment.Start);
        /// <summary>
        /// Gets or sets the font attributes.
        /// </summary>
        /// <value>The font attributes.</value>
        public TextAlignment HorizontalTextAlignment
        {
            get => (TextAlignment)GetValue(HorizontalTextAlignmentProperty);
            set => SetValue(HorizontalTextAlignmentProperty, value);
        }
        /// <summary>
        /// The has under line property.
        /// </summary>
        public static readonly BindableProperty HasUnderLineProperty = BindableProperty.Create(
            "HasUnderLine",
            typeof(bool),
            typeof(ExtEditor),
            true);
        /// <summary>
        /// Gets or sets the font attributes.
        /// </summary>
        /// <value>The font attributes.</value>
        public bool HasUnderLine
        {
            get => (bool)GetValue(HasUnderLineProperty);
            set => SetValue(HasUnderLineProperty, value);
        }
        /// <summary>
        /// The Placeholder attributes property.
        /// </summary>
        public static readonly BindableProperty PlaceholderProperty = BindableProperty.Create(
            nameof(Placeholder),
            typeof(string),
            typeof(ExtEditor),
            string.Empty);
        /// <summary>
        /// Gets or sets the Placeholder attributes.
        /// </summary>
        /// <value>The font attributes.</value>
        public string Placeholder
        {
            get => (string)GetValue(PlaceholderProperty);
            set => SetValue(PlaceholderProperty, value);
        }

        //////////////////////////////////////////////////

        //////////////////////////////////////////////////
        /// <summary>
        /// The PlaceholderColor attributes property.
        /// </summary>
        public static readonly BindableProperty PlaceholderColorProperty = BindableProperty.Create(
            nameof(PlaceholderColor),
            typeof(Color),
            typeof(ExtEditor),
            Color.Gray);
        /// <summary>
        /// Gets or sets the PlaceholderColor attributes.
        /// </summary>
        /// <value>The font attributes.</value>
        public Color PlaceholderColor
        {
            get => (Color)GetValue(PlaceholderColorProperty);
            set => SetValue(PlaceholderColorProperty, value);
        }
        /// <summary>
        /// The font attributes property.
        /// </summary>
        public new static readonly BindableProperty FontAttributesProperty = BindableProperty.Create(
            "FontAttributes",
            typeof(ExtFontAttributes),
            typeof(ExtEditor),
            ExtFontAttributes.Regular, propertyChanged: HandleBindingPropertyChangedDelegate);
        /// <summary>
        /// Handles the binding property changed delegate.
        /// </summary>
        /// <param name="bindable">Bindable.</param>
        /// <param name="oldValue">Old value.</param>
        /// <param name="newValue">New value.</param>
        private static void HandleBindingPropertyChangedDelegate(BindableObject bindable, object oldValue, object newValue)
        {
            if (newValue == null)
                return;
            var fontAttribute = (ExtFontAttributes)newValue;
            if (bindable is ExtEditor entry) entry.FontFamily = SetFontFamily(fontAttribute);
        }
        /// <summary>
        /// Gets or sets the font attributes.
        /// </summary>
        /// <value>The font attributes.</value>
        public new ExtFontAttributes FontAttributes
        {
            get => (ExtFontAttributes)GetValue(FontAttributesProperty);
            set => SetValue(FontAttributesProperty, value);
        }
        /// <summary>
        /// Sets the font family.
        /// </summary>
        /// <returns>The font family.</returns>
        /// <param name="fontAttributes">Font attributes.</param>
        private static string SetFontFamily(ExtFontAttributes fontAttributes)
        {
            string fontName = string.Empty;

            if (fontAttributes == ExtFontAttributes.Regular)
                fontName = Fonts.Regular;
            else if (fontAttributes == ExtFontAttributes.Bold)
                fontName = Fonts.Bold;
            else if (fontAttributes == ExtFontAttributes.Black)
                fontName = Fonts.Regular;
            else if (fontAttributes == ExtFontAttributes.ExtraLight)
                fontName = Fonts.ExtraLight;
            else if (fontAttributes == ExtFontAttributes.Light)
                fontName = Fonts.Light;
            else if (fontAttributes == ExtFontAttributes.SemiBold)
                fontName = Fonts.SemiBold;

            if (Device.RuntimePlatform == Device.Android)
                fontName = $"{fontName}.ttf#{fontName}";

            return fontName;
        }

    }
}
