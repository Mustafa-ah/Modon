using System;
using System.Collections.Generic;
using System.Text;
using Maham.Models.User;

namespace Maham.Service.Model.Response.User
{
    public class GetUserProfileResponse : ResponseBase
    {
        public string id { get; set; }
        public string name { get; set; }
        public string fullName { get; set; }
        public string arabicName { get; set; }
        public string email { get; set; }
        public string Mobile { get; set; }
        public byte[] photo { get; set; }
        public string photoUrl { get; set; }
    }
}
