using System;
using System.Collections.Generic;
using System.Text;

namespace Maham.Service.Model.Response.Tasks
{
    public class DeleteTaskResponse : ResponseBase
    {
        public bool requestSuccess { get; set; }
        public string errorMsg { get; set; }
        public string content { get; set; }
    }
}
