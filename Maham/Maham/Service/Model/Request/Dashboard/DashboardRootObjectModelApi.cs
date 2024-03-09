using System;
using System.Collections.Generic;
using System.Text;

namespace Maham.Service.Model.Request.Dashboard
{
    public class DashboardRootObjectModelApi
    {
        public bool requestSuccess { get; set; }
        public string errorMsg { get; set; }
        public List<Chart> content { get; set; }
    }
}
