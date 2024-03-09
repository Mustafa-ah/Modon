using System;
using System.Collections.Generic;
using System.Text;

namespace Maham.Service.Model.Request.Dashboard
{
   public class Chart
    {
        public int chartTypeId { get; set; }
        public string chartType { get; set; }
        public string chartTitle { get; set; }
        public string chartTitleAr { get; set; }
        public string primaryAxisTitle { get; set; }
        public string primaryAxisTitleAr { get; set; }
        public string secondaryAxisTitle { get; set; }
        public string secondaryAxisTitleAr { get; set; }
        public List<ChartPlottingInfoModelApi> chartPlottingInfo { get; set; }
    }
}
