
//using Plugin.Multilingual;
using Maham.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Plugin.Multilingual;

namespace Maham.Helpers
{
    public class Helper
    {
        public Helper()
        {

        }
        /// <summary>
        /// Return true if current language arabic
        /// </summary>
        public virtual bool IsRtl
        {
            get
            {
                var isRtl = new Helpers.Helper().CurrentLanguage() != 1;
                return isRtl;
            }
        }
        /// <summary>
        /// Gets the height of the propotional.
        /// </summary>
        /// <returns>The propotional height.</returns>
        /// <param name="page">Page.</param>
        /// <param name="heightFraction">Height fraction.</param>
        public double GetPropotionalHeight(double heightFraction)
        {
            return App.Current.MainPage.Height * heightFraction;
        }
        /// <summary>
        /// Gets the width of the proportional.
        /// </summary>
        /// <returns>The proportional width.</returns>
        /// <param name="page">Page.</param>
        /// <param name="widthFraction">Width fraction.</param>
        public double GetProportionalWidth(double widthFraction)
        {
            return App.Current.MainPage.Width * widthFraction;
        }
        public AbsoluteLayout GetAbsoluteLayout(Image img, View contentStackLayout)
        {

            var absolute = new AbsoluteLayout
            {
                Children = {
                        {img, new Rectangle(0,0,1,1), AbsoluteLayoutFlags.All},
                        { contentStackLayout,new Rectangle (0,0,1,1), AbsoluteLayoutFlags.All}
                },
                Padding = 0,
                Margin = 0
            };
            return absolute;
        }
        public string SetFontFamily(ExtFontAttributes fontAttributes, bool isNative = false)
        {
            string fontName = string.Empty;

            if (fontAttributes == ExtFontAttributes.Regular)
                fontName = "Cairo-Regular";
            else if (fontAttributes == ExtFontAttributes.Bold)
                fontName = "Cairo-Bold";
            else if (fontAttributes == ExtFontAttributes.Black)
                fontName = "Cairo-Black";
            else if (fontAttributes == ExtFontAttributes.ExtraLight)
                fontName = "Cairo-ExtraLight";
            else if (fontAttributes == ExtFontAttributes.Light)
                fontName = "Cairo-Light";
            else if (fontAttributes == ExtFontAttributes.SemiBold)
                fontName = "Cairo-SemiBold";

            if (Device.RuntimePlatform == Device.Android && !isNative)
                fontName = $"{fontName}.ttf#{fontName}";
            else if (Device.RuntimePlatform == Device.Android && isNative)
                fontName = $"{fontName}.ttf";

            return fontName;
        }
        public int CurrentLanguage()
        {
            int x = 1;
            var ci = CrossMultilingual.Current.CurrentCultureInfo;
            if (ci.ToString().Contains("ar") || ci.ToString().Contains("Ar"))
                x = 2;
            return x;
        }
    }
}
