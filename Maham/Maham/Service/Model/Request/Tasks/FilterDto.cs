using System;
using System.Collections.Generic;
using Maham.Service.Model.Response;

namespace Maham.Service.Model.Request.Tasks
{
    public class FilterDto
    {
        public int StatusId { get; set; }
        public int PriorityId { get; set; }
        public List<Guid> Entities { get; set; }
        public int SourceId { get; set; }
        public Guid UserGroupId { get; set; }
        public string SearchTask { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public Value2 ResponsibleID { get; set; }
    }
}
