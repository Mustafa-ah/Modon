using System;
using System.Collections.Generic;
using System.Text;
using Maham.Models;

namespace Maham.Service.Model.Response.orgnization
{
  public  class GetTenantResponse
    {
        public bool RequestSuccess { get; set; }
        public string ErrorMsg { get; set; }
        public Tenants Content { get; set; }
    }
}
