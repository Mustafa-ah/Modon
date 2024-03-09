
using System;
using System.Collections.Generic;
using System.Text;

namespace Maham.Models
{
    public class AddTaskModel
    {
        public string creationDate { get; set; }
        public int? fK_CreatedBy { get; set; }
        public int? FK_ModifiedBy { get; set; }
        public int? fK_ProjectID { get; set; }
        public bool isActive { get; set; }
        public string fK_ResponsibleID { get; set; }
        public int? fK_RefrenceID { get; set; }
        public int? fK_PriorityID { get; set; }
        public int fK_SectorID { get; set; }
        public int fK_StatusID { get; set; }
        public bool urgentSupport { get; set; }
        public int id { get; set; }
        public string title { get; set; }
        public int progress { get; set; }
        public string description { get; set; }
        public string startDate { get; set; }
        public string endDate { get; set; }
        public string ModificationDate { get; set; }
        public List<AttachmentModel> attachment { get; set; }
        public string reminderDate { get; set; }
        public bool reminderEnabled { get; set; }
        public bool userGroupValue { get; set; }
        public int fK_SourceID { get; set; }
    }
}
