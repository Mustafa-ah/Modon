using System;
using System.Collections.Generic;
using System.Text;

namespace Maham.Service.Model.Request.Registeration
{
    public class RegisterationRequest:RequestBase
    {
        public string email { get; set; }
        public string userName { get; set; }
       
        public string password { get; set; }
    }
}
