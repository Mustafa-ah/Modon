using Microsoft.AppCenter.Crashes;
using System;
using System.Collections.Generic;
using Maham.Bases;
using Xamarin.Forms;
using Maham.Service.Model.Response;

namespace Maham.Models
{
    public class TaskDto
    {
        public Guid Id { get; set; }
        public Value2 ResponsibleID { get; set; }//could be User/Usergroup"PMO"
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Progress { get; set; }
        public int SourceId { get; set; }
        public int StatusId { get; set; }
        public int PriorityId { get; set; }
        public bool IsUrgentSupport { get; set; }
        public string SName { get; set; }
        public string PName { get; set; }
        public string Creator { get; set; }
        public string As { get; set; }// as user or as usergroup
        public string Assignee { get; set; }
        public string AssigneeRoleName { get; set; }
        public string Sector { get; set; }
        public string SourceDisplayName { get; set; }
        public string SourceDisplayNameEn { get; set; }
        public bool? ReminderEnabled { get; set; }
        public DateTime? ReminderDate { get; set; }
        public Guid? AssignorUserGroupId { get; set; }
        public string AssignorUserGroup { get; set; }
        public Guid? AssignorEntityUserId { get; set; }
        public string AssignorEntityUser { get; set; }
        public bool NeedsErcall { get; set; }
        public Value3 TaskErcallDto { get; set; }
        public Guid CreatedBy { get; set; }
        public string CreatorArabicName { get; set; }
        public string ArabicName { get; set; }

        public ICollection<AttachmentDto> Attachment { get; set; }
        public ICollection<CommentDto> Comment { get; set; }
        //public ICollection<NotificationDto> Notification { get; set; }
        public ICollection<TaskAssignmentDto> TaskAssignment { get; set; }
    }

    public class TaskAssignmentDto
    {
        public string Id { get; set; }
        public DateTime CreatedAt { get; set; }
        /*
         "id": "8e8f3bff-1c75-431e-be73-3a434cb76974",
                "taskId": "fa308f37-17c5-4645-b690-ee2b759064bc",
                "assignedBy": "0160c8be-3605-4b7b-b754-1c70bdf81aaa",
                "assignorRoleId": null,
                "assignorUserGroupId": null,
                "assignedTo": "01c6ec24-5d0b-4a7f-a6d4-5b35220a1458",
                "assigneeRoleId": 8,
                "assigneeUserGroupId": null,
                "operationId": 1,
                "createdAt": "2020-08-20T14:30:14.8228429",
         */
    }
}
