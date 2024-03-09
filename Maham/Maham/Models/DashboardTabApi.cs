using System;
using System.Collections.Generic;
using System.Text;

namespace Maham.Models
{
    public class DashboardTabApi
    {
        public bool requestSuccess { get; set; }
        public string errorMsg { get; set; }
        public List<DashboardContentApi> content { get; set; }
    }
}
