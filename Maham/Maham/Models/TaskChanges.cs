using System;
using System.Collections.Generic;
using System.Text;
using Maham.Bases;

namespace Maham.Models
{
    public class TaskChanges : BaseModel
    {
        public int TaskId { get; set; }
        public string TaskName { get; set; }
        public List<ChangePerDate> ChangesPerDate { get; set; }
    }

    public class ChangePerDate
    {
        public string Day { get; set; }
        public string MonthEn { get; set; }
        public string MonthAr { get; set; }
        public string Year { get; set; }

        public List<ChangePerTime> ChangesPerTime { get; set; }  
    }

    public class ChangePerTime
    {
        public string ChangeTime { get; set; }
        public int ChangeType { get; set; }
        public string ChangeEn { get; set; }
        public string ChangeAr { get; set; }
        public int ChangedById { get; set; }
        public string ChangedByName { get; set; }
    }
}
