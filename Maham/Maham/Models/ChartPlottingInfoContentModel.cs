using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Maham.Models
{
    public class ChartPlottingInfoContentModel
    {
        public string SeriesName { get; set; }
        public string SeriesColor { get; set; }
        ObservableCollection<ChartDataModel> Data { get; set; }
    }
}
