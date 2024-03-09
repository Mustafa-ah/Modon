using System;
using System.Collections.Generic;
using System.Text;

namespace Maham.Service.Model.Request.User
{
   public class GetUserProfileRequest:RequestBase
    {
        public string userId { get; set; }
    }
}
