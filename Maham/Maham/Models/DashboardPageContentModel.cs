using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Maham.Models
{
    public class DashboardPageContentModel
    {
        public int ChartId { get; set; }
        public string ChartType { get; set; }
        public string ChartTitle { get; set; }
        public string PrimaryAxisTitle { get; set; }
        public string SecondaryAxisTitle { get; set; }
        public ObservableCollection<ChartPlottingInfoContentModel> ChartPlottingInfo { get; set; }

    }
}
