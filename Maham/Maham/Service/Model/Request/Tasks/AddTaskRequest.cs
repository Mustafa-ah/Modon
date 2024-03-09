using System;
using System.Collections.Generic;
using System.Text;
using Maham.Models;

namespace Maham.Service.Model.Request.Tasks
{
   public class AddTaskRequest
    {
        public AddTaskModel task { get; set; }
        public List< AttachmentModel>attachment { get; set; }
    }
}
