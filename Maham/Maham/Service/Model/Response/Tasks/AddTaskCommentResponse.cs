using System;
using System.Collections.Generic;
using System.Text;

namespace Maham.Service.Model.Response.Tasks
{
    public class AddTaskCommentResponse:ResponseBase
    {
        public bool requestSuccess { get; set; }
        public string errorMsg
        {
            get; set;
        }
        public CommentIdReturnModel content { get; set; }
    }
    public class CommentIdReturnModel
    {
        public int commentId { get; set; }
    }



}
