using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Maham.Models;

namespace Maham.Service.Model.Response.Notification
{
    public class NotificationHistoryResponse
    {
        [JsonProperty(PropertyName = "requestSuccess")]
        public bool RequestSuccess { get; set; }

        [JsonProperty(PropertyName = "errorMsg")]
        public string ErrorMsg { get; set; }

        [JsonProperty(PropertyName = "content")]
        public List<NotificationDTO> NotificationList { get; set; }

        [JsonProperty(PropertyName = "hasMorePages")]
        public bool HasMorePages { get; set; }

        [JsonProperty(PropertyName = "unreadCount")]
        public long UnreadCount { get; set; }
    }
}
