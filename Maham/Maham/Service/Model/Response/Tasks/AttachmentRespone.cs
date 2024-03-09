using System;
using System.Collections.Generic;
using System.Text;

namespace Maham.Service.Model.Response.Tasks
{
    public class AttachmentRespone : ResponseBase
    {
        public string ErrorMsg { get; set; }
        public string RequestSuccess { get; set; }
        public Attachment Content { get; set; }
    }

}
