using System;
using System.Collections.Generic;
using System.Text;

namespace Maham.Service.Model.Response.Priorities
{
    public class PrioritiesResponse
    {
        public string sectionName { get; set; }
        public int sectionId { get; set; }
        public int tasksCount { get; set; }
        public string notStarted { get; set; }
        public string inProgress { get; set; }
        public string returned { get; set; }
        public string completed { get; set; }
        public string delayed { get; set; }

    }
}
