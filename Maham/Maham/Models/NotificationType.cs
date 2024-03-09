using System;
using System.Collections.Generic;
using System.Text;

namespace Maham.Models
{
    public enum NotificationType
    {
        AddTask = 1,
        UpdateTask = 2,
        Completed = 3,
        UrgentSupport = 4,
        TaskClosed = 5,
        TaskReturnd = 6,
        DeleteTask = 7,
        AddComment = 8,
        notUrgent = 9
    }
}
