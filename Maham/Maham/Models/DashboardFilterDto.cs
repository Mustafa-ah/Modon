using Plugin.Multilingual;
using System;
using System.Collections.Generic;
using System.Text;
using Maham.Constants;
using Maham.Service.Model.Response;

namespace Maham.Models
{
    public class DashboardFilterDto
    {
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public int StatusId { get; set; }
        public int PriorityId { get; set; }
        public Guid EntityId { get; set; }
        public Value2 ResponsibleID { get; set; }
        public int SourceId { get; set; }
        public string title { get; set; }

    }
}
