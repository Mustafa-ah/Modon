using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace Maham.Bases
{
    public class BaseContentPage:ContentPage
    {
        public virtual bool IsRtl
        {
            get
            {
                var isRtl = new Helpers.Helper().CurrentLanguage() != 1;
                return isRtl;
            }
        }
        public BaseContentPage()
        {
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
        }
    }
}
