    using System;
using System.Collections.Generic;
using System.Text;

namespace Maham.Models
{
   public class ProjectListModel:BaseEntity
    {
      public  int? id { get; set; }
      public string name { get; set; }
        public bool isProjectChecked { get; set; }
    }
}
