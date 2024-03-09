using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Maham.Models
{
    public class NotificationDTO
    {
        public Guid Id { get; set; }
        public string Message { get; set; }
        public Guid? TaskId { get; set; }
        public Guid ReceiverId { get; set; }
        public bool IsReaded { get; set; }
        public int TypeId { get; set; }
        public bool IsDeleted { get; set; }
        public string Title { get; set; }
        public string TitleAr { get; set; }
        public int? NotificationType { get; set; }
        public string MessageAr { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int PriorityId { get; set; }
    }
 
}
