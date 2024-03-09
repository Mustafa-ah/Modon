using System;
using System.Collections.Generic;
using System.Text;
using Maham.Models;

namespace Maham.Service.Model.Response.Tasks
{
    public class GetCommentsResponse
    {
        public List<CommentsList> list { get; set; }
    }



    public class Rootobject
    {
        public int id { get; set; }
        public int taskID { get; set; }
        public int fK_CreatedBy { get; set; }
        public string description { get; set; }
        public string createdBy { get; set; }
        public string photoUrl { get; set; }
    }

    public class CommentsList : BaseEntity
    {
        public int id { get; set; }
        public int taskID { get; set; }
        public int fK_CreatedBy { get; set; }
        public string description { get; set; }
        public string createdBy { get; set; }
        public string photoUrl { get; set; }
    }

}
