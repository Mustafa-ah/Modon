using System;
using System.Collections.Generic;
using System.Text;

namespace Maham.Service.Model.Request.Login
{
   public class LoginRequest:RequestBase
    {
        public string email { get; set; }
        public string username { get; set; }
        public string password { get; set; }
    }
}
