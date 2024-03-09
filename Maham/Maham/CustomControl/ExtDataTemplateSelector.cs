using System;
using System.Collections.Generic;
using System.Text;
using Maham.Models;
using Xamarin.Forms;

namespace Maham.CustomControl
{
    public class ExtDataTemplateSelector : DataTemplateSelector
    {
        private bool isRtl;

        public DataTemplate Priorities { get; set; }
        public DataTemplate NotPriorities { get; set; }
        
        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            isRtl = new Helpers.Helper().IsRtl;
            if (isRtl)
            {
                return ((TaskTabbedPage)item).TabName == "الاولويات" ? Priorities : NotPriorities;
            }
            else
            {
                return ((TaskTabbedPage)item).TabName == "Priorities" ? Priorities : NotPriorities;
            }
        }
    }
}
