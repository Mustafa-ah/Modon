using System;
using System.Collections.Generic;
using System.Text;

namespace Maham.Service.Model.Request.Registeration
{
   public class SubmitVerficationCodeRequest:RequestBase
    {
        public int verificationCode { get; set; }
        public int userId { get; set; }
    }
}
