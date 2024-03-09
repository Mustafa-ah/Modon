using System;
using System.Collections.Generic;
using System.Text;

namespace Maham.Service.Model.Request.User
{
    public class UploadProfileImageRequest:RequestBase
    {
        public byte[] photo { get; set; }
        public string id { get; set; }
    }
}
