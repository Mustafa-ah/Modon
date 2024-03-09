using System;
using System.Collections.Generic;
using System.Text;

namespace Maham.Service.Model.Request.newclient
{
   public class NewClientRequest
    {
       public string userName { get; set; }
       public string organizationName { get; set; }
        public string email { get; set; }
       public string phoneNumber { get; set; }
    }
}
