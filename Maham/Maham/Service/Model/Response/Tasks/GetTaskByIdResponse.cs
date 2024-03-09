using System;
using System.Collections.Generic;
using System.Text;
using Maham.Models;
using Xamarin.Forms;

namespace Maham.Service.Model.Response.Tasks
{
    public class GetTaskByIdResponse
    {
        public TaskModel task { get; set; }
        public Comment[] comments { get; set; }
        public Attachment[] attachments { get; set; }
        public Creator creator { get; set; }
        public Assignee assignee { get; set; }
    }





    public class EditTask
    {
        public string assignee { get; set; }
        public int assigneeID { get; set; }
        public string status { get; set; }
        public int fK_PriorityID { get; set; }
        public int? fk_StatusID { get; set; }


        public int fK_RefrenceID { get; set; }
        public string priority { get; set; }
        public string sector { get; set; }
        public string reference { get; set; }
        public bool urgentSupport { get; set; }
        public int id { get; set; }
        public string title { get; set; }
        public double progress { get; set; }
        public string description { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public DateTime ModificationDate
        { get; set; }
    }

   



   
   

   
    public class TaskModel
    {
        public string assigneeID { get; set; }
        public int creatorID { get; set; }
        public int fK_StatusID { get; set; }
        public string status { get; set; }
        public int fK_PriorityID { get; set; }
        public string priority { get; set; }
        public int? fK_RefrenceID { get; set; }
        public int? FK_ProjectID { get; set; }
          public string ProjectName { get; set; }
        public string reference { get; set; }
        public int? fK_SectorID { get; set; }
        public string sector { get; set; }
        public bool urgentSupport { get; set; }
        public bool canViewTask { get; set; }
        public bool canEditTask { get; set; }
        public bool canDeleteTask { get; set; }
        public bool canReAssignTask { get; set; }
        public bool canEditProgressBar { get; set; }
        public bool CanCloseTask { get; set; }
        public bool CanReturnTask { get; set; }
        public int id { get; set; }
        public string title { get; set; }
        public double progress { get; set; }
        public string description { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
    }

    public class Creator
    {
        public string role { get; set; }
        public int id { get; set; }
        public string email { get; set; }
        public string name { get; set; }
        public string empName { get; set; }
        public int roleID { get; set; }
        public string photoUrl { get; set; }
    }

    public class Assignee
    {
        public string role { get; set; }
        public int id { get; set; }
        public string email { get; set; }
        public string name { get; set; }
        public string empName { get; set; }
        public int roleID { get; set; }
        public string photoUrl { get; set; }
    }

    public class Comment
    {
        public int id { get; set; }
        public int taskID { get; set; }
        public int fK_CreatedBy { get; set; }
        public string description { get; set; }
        public string createdBy { get; set; }
        public string photoUrl { get; set; }
        public ImageSource backgroundimage { get; set; }
        public int CommentHeight { get; set; }
    }

    public class Attachment
    {
        public int id { get; set; }
        public int fK_TypeID { get; set; }
        public int fK_ItemID { get; set; }
        public string title { get; set; }
        public string content { get; set; }
        public int fK_CreatedBy { get; set; }
        public DateTime creationDate { get; set; }
    }


}
