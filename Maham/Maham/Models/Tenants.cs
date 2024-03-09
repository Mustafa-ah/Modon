using System;
using System.Collections.Generic;
using System.Text;

namespace Maham.Models
{
   public class Tenants
    {
      public int id { get; set; }
        public string userName { get; set; }
          public string organizationName { get; set; }
          public string type { get; set; }
          public string code { get; set; }
           public string  email { get; set; }
            public string phoneNumber { get; set; }
        public string apiUrl { get; set; }
        public bool AllowSignUp { get; set; }
    }
}
