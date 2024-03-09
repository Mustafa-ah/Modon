using System;
using System.Collections.Generic;
using System.Text;

namespace Maham.Service.Model.Response.newclient
{
  public class NewClientResponse
    {
        public bool RequestSuccess { get; set; }
        public string ErrorMsg { get; set; }
        public newclientmodel Content { get; set; }
    }
    public class newclientmodel
    {

    }
}
