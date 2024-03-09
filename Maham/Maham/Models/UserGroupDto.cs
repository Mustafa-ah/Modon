using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Maham.Models
{
    public class UserGroupDto
    {

        public Guid Id { get; set; }
        public string Name { get; set; }
        //public int? GroupId { get; set; }
        //public string GroupName { get; set; }
        public bool IsActive { get; set; }

    }

}
