using System;
using System.Collections.Generic;
using System.Text;

namespace Tasky.Service.Model.Response.Tasks
{
    public class FilterTasksRespons
    {
        public string sectionName { get; set; }
        public string sectionNameAr { get; set; }
        public int tasksCount { get; set; }
        List<Task> tasks;

        public FilterTasksRespons()
        {
            tasks = new List<Task>();
        }
    }
}
