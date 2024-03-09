using System;
using System.Collections.Generic;
using System.Text;

namespace Maham.Models.User
{
   public class ListModel:BaseEntity
    {

        public string name { get; set; }
        public string id { get; set; }
        public string checkImage { get; set; }
        public bool IsCheckedRefe { get; set; }
    }
}
