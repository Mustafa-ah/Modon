using Microsoft.AppCenter.Crashes;
using System;
using System.Collections.Generic;
using Maham.Bases;
using Xamarin.Forms;
using Maham.Service.Model.Response;

namespace Maham.Models
{
    public class TaskDueDateRequestDto
    {
        public Guid Id { get; set; }
        public Guid TaskId { get; set; }
        public DateTime DueDate { get; set; }
        public string DueDateString { get; set; }
        public Guid? RequestedByUserId { get; set; }
        public int? RequestedByRoleId { get; set; }
        public Guid? RequestedByUserGroupId { get; set; }
        public Guid? ManagedByUserId { get; set; }
        public int? ManagedByRoleId { get; set; }
        public Guid? ManagedByUserGroupId { get; set; }
        public int StatusId { get; set; }
        public string StatusName { get; set; }
        public Color StatusColor { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string Creator { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool HasButton { get; set; }
        public string Reason { get; set; }
    }
}
