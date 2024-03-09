using System;
using System.Collections.Generic;
using System.Text;

namespace Maham.Models
{
    public class ListPopUpModel_Guid : BaseEntity
    {
       

        public Guid id { get; set; }
        public string name { get; set; }
        public string nameAr { get; set; }
        public string checkImage { get; set; }
        public string image2 { get; set; }
        public bool IsChecked { get; set; }
        public bool IsActive { get; set; }
    }
}
