using System;
using System.Collections.Generic;
using System.Text;

namespace Maham.Service.Model.Request.Registeration
{
   public class SendVerficationCodeRequest:RequestBase
    {
        public string userId { get; set; }
    }
}
