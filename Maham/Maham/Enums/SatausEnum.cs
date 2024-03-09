using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Maham.Enums
{
    public enum SatausEnum
    {
        [Description("#98aab4")]
        NotStarted = 1,
        [Description("#1dacfb")]
        InProgress,
        [Description("#d73952")]
        Delayed,
        [Description("#8dcb50")]
        Completed,
        [Description("ffffff")]
        Launched,
        [Description("#4682b4")]
        Closed,
        [Description("#7CB5EC")]
        Returned,
        [Description("")]
        Deleted,
    }
}
