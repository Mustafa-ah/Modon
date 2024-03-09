using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Maham.AttachedProperties
{
    public static class ElementExt
    {
        #region RelativeSource Property
        public static Element GetRelativeSource(Element obj)
        {
            return obj.GetValue(RelativeSourceProperty) as Element;
        }
        public static void SetRelativeSource(Element obj, Element value)
        {
            obj.SetValue(RelativeSourceProperty, value);
        }
        public static readonly BindableProperty RelativeSourceProperty =
            BindableProperty.CreateAttached("RelativeSource", typeof(Element), typeof(ElementExt), null);
        #endregion

        #region RelativeSourceMode Property
        public static RelativeSourceMode GetRelativeSourceMode(Element obj)
        {
            return (RelativeSourceMode)obj.GetValue(RelativeSourceModeProperty);
        }
        public static void SetRelativeSourceMode(Element obj, RelativeSourceMode value)
        {
            obj.SetValue(RelativeSourceModeProperty, value);
        }
        public static readonly BindableProperty RelativeSourceModeProperty =
            BindableProperty.CreateAttached("RelativeSourceMode", typeof(RelativeSourceMode), typeof(ElementExt), null, propertyChanged: RelativeSourceModePropertyChanged);

        public enum RelativeSourceMode
        {
            FindAncestor = 1
        }
        private static void RelativeSourceModePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (CanFetchRelativeSource(bindable as Element))
                FetchRelativeSource(bindable as Element);
        }
        #endregion

        #region RelativeSourceType Property
        public static Type GetRelativeSourceType(Element obj)
        {
            return obj.GetValue(RelativeSourceTypeProperty) as Type;
        }
        public static void SetRelativeSourceType(Element obj, Type value)
        {
            obj.SetValue(RelativeSourceTypeProperty, value);
        }
        public static readonly BindableProperty RelativeSourceTypeProperty =
            BindableProperty.CreateAttached("RelativeSourceType", typeof(Type), typeof(ElementExt), null, propertyChanged: RelativeSourceTypePropertyChanged);

        private static void RelativeSourceTypePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (CanFetchRelativeSource(bindable as Element))
                FetchRelativeSource(bindable as Element);
        }
        #endregion

        private static bool CanFetchRelativeSource(Element elm)
        {
            return (GetRelativeSourceMode(elm) == RelativeSourceMode.FindAncestor && GetRelativeSourceType(elm) != null && typeof(Element).IsAssignableFrom(GetRelativeSourceType(elm)));
        }

        private static void FetchRelativeSource(Element elm)
        {
            Type ancestorType = GetRelativeSourceType(elm);
            if (ancestorType.Equals(elm.GetType()))
            {
                SetRelativeSource(elm, elm);
                return;
            }

            Element parent;
            while((parent = elm.Parent) != null)
            {
                if (ancestorType.Equals(parent.GetType()))
                {
                    SetRelativeSource(elm, parent);
                    return;
                }
            }

            SetRelativeSource(elm, null);
        }
    }
}
