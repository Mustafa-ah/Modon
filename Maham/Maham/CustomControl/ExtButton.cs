
using System;
using Maham.Constants;
using Maham.Enums;
using Xamarin.Forms;
namespace Maham.CustomControl
{
    public class ExtButton : Button
    {
        /// <summary>
        /// The font attributes property.
        /// </summary>
        public new static readonly BindableProperty FontAttributesProperty = BindableProperty.Create(
            "FontAttributes",
            typeof(ExtFontAttributes),
            typeof(Button),
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
            if (bindable is ExtButton label) label.FontFamily = SetFontFamily(fontAttribute);
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
        /// The horizontal text alignment property.
        /// </summary>
        public static readonly BindableProperty HorizontalTextAlignmentProperty =
            BindableProperty.Create(nameof(HorizontalTextAlignment), typeof(TextAlignment), typeof(ExtButton), TextAlignment.Center);
        /// <summary>
        /// Gets or sets the horizontal text alignment.
        /// </summary>
        /// <value>The horizontal text alignment.</value>
        public TextAlignment HorizontalTextAlignment
        {
            get { return (TextAlignment)GetValue(HorizontalTextAlignmentProperty); }
            set { SetValue(HorizontalTextAlignmentProperty, value); }
        }
        /// <summary>
        /// The padding property.
        /// </summary>
        public static readonly BindableProperty PaddingProperty =
            BindableProperty.Create(nameof(Padding), typeof(Thickness), typeof(ExtButton), new Thickness(0));
        /// <summary>
        /// Gets or sets the padding.
        /// </summary>
        /// <value>The padding.</value>
        public Thickness Padding
        {
            get { return (Thickness)GetValue(PaddingProperty); }
            set { SetValue(PaddingProperty, value); }
        }
        /// <summary>
        /// The background image property.
        /// </summary>
        public static readonly BindableProperty BackgroundImageProperty =
            BindableProperty.Create(nameof(BackgroundImage), typeof(string), typeof(ExtButton), string.Empty);
        /// <summary>
        /// Gets or sets the background image.
        /// </summary>
        /// <value>The background image.</value>
        public string BackgroundImage
        {
            get { return (string)GetValue(BackgroundImageProperty); }
            set { SetValue(BackgroundImageProperty, value); }
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
