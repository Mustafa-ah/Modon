using Maham.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Maham.Service.Model.Response.Login
{
   public class LoginResponse:ResponseBase
    {
        public bool Authenticated { get; set; }
        public string ArabicName { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string Token { get; set; }
        public string Id { get; set; }
        public string roleId { get; set; }
        public bool HasUserGroups { get; set; }
        public Guid? UserGroup { get; set; }
        public bool HasPositions { get; set; }
        public Guid? Position { get; set; }
        public bool IsSuperAdmin { get; set; }
        public List<RoleModulePrivilege> Ability { get; set; }
        public List<Guid> UserGroupList { get; set; }
        public bool IsEntityManager { get; set; }
    }
}
