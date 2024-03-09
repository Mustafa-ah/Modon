using System;
using System.Collections.Generic;
using System.Text;

namespace Maham.Service.Model.Response.Notification
{
    public class MarkNotificationReadedResponse
    {
        public bool requestSuccess { get; set; }
        public string errorMsg { get; set; }
        public object content { get; set; }

    }
}
