using System;
using System.Collections.Generic;
using System.Text;
using Maham.Models;

namespace Maham.Service.Model.Response.Priorities
{
    public class PriorityListResponse : ResponseBase
    {
        public List<ListPopUpModel> Mylist { get; set; }
    }


    public class Priorities
    {
        public int id { get; set; }
        public string name { get; set; }
    }
}
