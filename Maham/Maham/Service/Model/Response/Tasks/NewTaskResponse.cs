using System;
using System.Collections.Generic;
using System.Text;

namespace Maham.Service.Model.Response.Tasks
{
   public class NewTaskResponse:ResponseBase
    {
        public bool requestSuccess { get; set; }
        public string errorMsg
        {
            get; set;
        }
       public AddTaskReturnModel content { get; set; }
    }
    public class AddTaskReturnModel
    {
        public int taskId { get; set; }
    }

}
