using System;
using System.Collections.Generic;
using System.Text;

namespace Maham.Service.Model.Response.Tasks
{
    public class TabsResponse
    {
       // public string tabName { get; set; }
      //  public string tabNameAr { get; set; }
       // public int tabId { get; set; }

        public Guid Id { get; set; }
        public int Order { get; set; }
        
        public string Title { get; set; }
        public string TitleAr { get; set; }
        public string TypeId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool Closed { get; set; }
        
        /*


         "id":"0fcc6086-3da8-4dcb-a441-78ed60b9ee7c",
         "title":"Timeline View",
         "titleAr":"عرض المخطط الزمني",
         "typeId":2,
         "createdBy":"00000000-0000-0000-0000-000000000000",
         "createdAt":"0001-01-01T00:00:00",
         "updatedBy":null,
         "updatedAt":null,
         "closed":false,
         "order":0,
         "createdByNavigation":null,
         "type":null,
         "updatedByNavigation":null,
         "viewSection":null*/
    }
}
