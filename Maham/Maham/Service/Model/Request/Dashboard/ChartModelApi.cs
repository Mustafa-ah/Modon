using System.Collections.ObjectModel;

namespace Maham.Service.Model.Request.Dashboard
{
    public class ChartModelApi
    {
        public int ChartId { get; set; }
        public string ChartType { get; set; }
        public string ChartTitle { get; set; }
        public string PrimaryAxisTitle { get; set; }
        public string SecondaryAxisTitle { get; set; }
        public ObservableCollection<ChartPlottingInfoModelApi> ChartPlottingInfo { get; set; }
    }
}
