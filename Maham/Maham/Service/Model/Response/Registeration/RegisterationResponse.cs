using System;
using System.Collections.Generic;
using System.Text;

namespace Maham.Service.Model.Response.Registeration
{
   public class RegisterationResponse:ResponseBase
    {
        public string userId { get; set; }
        public bool verified { get; set; }
        public bool RequestSuccess { get; set; }
    }
}
