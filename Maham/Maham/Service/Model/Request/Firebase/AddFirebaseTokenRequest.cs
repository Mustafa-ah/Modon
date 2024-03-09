using System;
using System.Collections.Generic;
using System.Text;

namespace Maham.Service.Model.Request.Firebase
{
    public class AddFirebaseTokenRequest : RequestBase
    {
        public int userId { get; set; }
        public string token { get; set; }
    }
}
