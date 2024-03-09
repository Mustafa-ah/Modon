using System;
using System.Collections.Generic;
using System.Text;
using Maham.Models;
using Xamarin.Forms;

namespace Maham.CustomControl
{
    public class ExtDashboardDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate Overall { get; set; }
        public DataTemplate Performance{ get; set; }
        public DataTemplate Sectors { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            return ((DashboardTabbedPage)item).TabName == "Overall" ? Overall : Performance;
        }
    }
}
