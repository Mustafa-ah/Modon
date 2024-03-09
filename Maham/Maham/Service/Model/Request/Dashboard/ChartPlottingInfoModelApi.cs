using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Maham.Service.Model.Request.Dashboard
{
    public class ChartPlottingInfoModelApi
    {
        public int id { get; set; }
        public string name { get; set; }
        public string nameAr { get; set; }
        public int y { get; set; }
        public string color { get; set; }
        public int percentage { get; set; }
        public string groupingName { get; set; }
        public string seriesName { get; set; }
        public string seriesNameAr { get; set; }
        public string seriesColor { get; set; }
        public List<SeriesDataModelApi> seriesData { get; set; }
    }
}
