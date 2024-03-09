using System;
using System.Collections.Generic;
using System.Text;

namespace Maham.Models
{
   public class EmployeeModel:BaseEntity
    {
        public int id { get; set; }
        public string name { get; set; }
        public string Description { get; set; }
        public string EmployeeImage { get; set; }
        public string Imagechecked { get; set; }
        public bool IsCheckedemployee { get; set; }
    }
}
