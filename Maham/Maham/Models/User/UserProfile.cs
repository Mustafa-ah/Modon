using System;
using System.Collections.Generic;
using System.Text;

namespace Maham.Models.User
{
    public class UserProfile
    {
        public byte[] photo { get; set; }
        public int roleID { get; set; }
        public DateTime createdAt { get; set; }
        public bool isSysAdmin { get; set; }
        public bool isDeleted { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public string empName { get; set; }
    }
}
