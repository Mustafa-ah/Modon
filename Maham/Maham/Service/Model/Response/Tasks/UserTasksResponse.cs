using System;
using System.Collections.Generic;
using System.Text;
using Maham.Models;

namespace Maham.Service.Model.Response.Tasks
{
    public class UserTasksResponse
    {
        public Guid sectionID { get; set; }
        public string sectionName { get; set; }
        public string sectionNameAr { get; set; }
        public string tasksCount { get; set; }
        public string FieldType { get; set; }
        public List<TaskDto> tasks { get; set; }

    }

    public class Section
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string TitleAr { get; set; }
        public string FieldType { get; set; }
        public int Order { get; set; }
        /*

        "id":"88ccaa25-7836-460a-8722-b53e34125c81",
         "viewId":"00000000-0000-0000-0000-000000000000",
         "title":"This Month Tasks",
         "titleAr":"الشهر الحالي",
         "fieldId":null,
         "fieldType":null,
         "condition":null,
         "createdBy":"00000000-0000-0000-0000-000000000000",
         "createdAt":"0001-01-01T00:00:00",
         "updatedBy":null,
         "updatedAt":null,
         "order":3,
         "createdByNavigation":null,
         "field":null,
         "updatedByNavigation":null,
         "view":null
         */
    }
}
