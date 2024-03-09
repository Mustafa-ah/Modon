using System;
using System.Collections.Generic;
using System.Text;

namespace Maham.Service.Model.Request.Tasks
{
    public class AddTaskCommentRequest
    {
        public int id { get; set; }
        public string description { get; set; }
        public DateTime creationDate { get; set; }
        public int fK_ItemID { get; set; }
        public int fK_CreatedBy { get; set; }
        public int fK_TypeID { get; set; }
    }
}
