using System;
namespace Maham.Enums
{
    public enum TasksMode
    {
        TaskList = 1,
        TaskListUserGroup = 2,
        ClosedTasksList = 3,
        ClosedTasksListUserGroup = 4
    }

    public enum DueDateRequestStatus
    {
        Pending = 0,
        Approved = 1,
        Rejected = 2,
    }

    public enum SelectListItemType
    {
        All = -1,
        User = 0,
        Role = 1,
        UserGroup = 2
    }
}
