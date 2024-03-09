using System;
using System.Collections.Generic;
using System.Text;

namespace Maham.Service.Model.Response.Tasks
{
    public class SearchResponse
    {
        public int status_Id { get; set; }
        public string status { get; set; }
        public int priority_Id { get; set; }
        public string priority { get; set; }
        public int assignedTo_ID { get; set; }
        public string assignedTo { get; set; }
        public string sector { get; set; }
        public bool urgentSupport { get; set; }
        public int? fK_UrgentSupportID { get; set; }
        public double average_progress { get; set; }
        public int flag_id { get; set; }
        public string flag { get; set; }
        public string flagAr { get; set; }
        public int id { get; set; }
        public string title { get; set; }
        public double progress { get; set; }
        public string description { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
    }

  

}
