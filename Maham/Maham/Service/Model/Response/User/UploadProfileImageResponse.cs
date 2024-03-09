using System;
using System.Collections.Generic;
using System.Text;
using Maham.Models;

namespace Maham.Service.Model.Response.User
{
   public class UploadProfileImageResponse:BaseEntity
    {
        public bool requestSuccess { get; set; }
        public string errorMsg
        {
            get; set;
        }
        public PhotoUrlModel Content { get; set; }

    }
    public class BaseClass
    {
    }
    
        
  

    public class PhotoUrlModel :BaseEntity
    {
        public string photoUrl { get; set; }
    }
}
