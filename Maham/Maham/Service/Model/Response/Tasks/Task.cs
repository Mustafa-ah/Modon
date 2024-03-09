using System;
using System.Collections.Generic;
using System.Text;

namespace Maham.Service.Model.Response.Tasks
{
    public class Task
    {
        public int status_Id { get; set; }
        public string status { get; set; }
        public int priority_Id { get; set; }
        public string priority { get; set; }
        public int assignedTo_ID { get; set; }
        public string assignedTo { get; set; }
        public string sector { get; set; }
        public string fK_UrgentSupportID { get; set; }
        public double average_progress { get; set; }
        public int flag_id { get; set; }
        public string flag { get; set; }
       // public int id { get; set; }
        public string title { get; set; }
        public string progress { get; set; }
        public string description { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public int sourceId { get; set; }
        public System.Guid id { get; set; }
        public int priorityId { get; set; }
        public int statusId { get; set; }
        public bool isUrgentSupport { get; set; }
        public string sName { get; set; }
        public string pName { get; set; }
        public string creator { get; set; }
  
        /*
        "id": "a4d304fe-70d5-44a9-bc68-9051b2672d73",
      "responsibleID": {
        "id": "aa9f0864-f8be-4593-81a7-4c9f00f81cb2",
        "roleID": null,
        "type": 2
      },


      "as": null,
      "assignee": null,
      "sector": null,
      "source": null,
      "reminderEnabled": null,
      "reminderDate": null,
      "assignorUserGroupId": null,
      "entityUserId": null,
      "assignorUserGroup": null,
      "assignorEntityUserId": null,
      "assignorEntityUser": null,
      "attachment": null,
      "comment": null,
      "notification": null,
      "taskAssignment": null

         */
    }
}
