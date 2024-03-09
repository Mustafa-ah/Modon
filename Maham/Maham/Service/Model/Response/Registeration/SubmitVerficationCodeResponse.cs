using System;
using System.Collections.Generic;
using System.Text;

namespace Maham.Service.Model.Response.Registeration
{
  public  class SubmitVerficationCodeResponse:ResponseBase
    {
        public bool verified { get; set; }
        public int userId { get; set; }
        public string token { get; set; }
    }
}
