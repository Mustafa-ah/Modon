using Plugin.Multilingual;
using System;
using System.Collections.Generic;
using System.Text;
using Maham.Constants;

namespace Maham.Models
{
    public partial class Module
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }

    }
    public partial class Privilege
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
    }
    public partial class RoleModulePrivilege
    {
        public Guid Id { get; set; }
        public int FkPrivilegeId { get; set; }
        public int FkModuleId { get; set; }
        public int FkRoleId { get; set; }
        public bool? Up { get; set; }
        public bool? Below { get; set; }
        public bool? Same { get; set; }
        public int? UpDirection { get; set; }
        public int? UpDirectionValue { get; set; }
        public int? BelowDirection { get; set; }
        public int? BelowDirectionValue { get; set; }
        public bool IsActive { get; set; }

        public virtual Module FkModule { get; set; }
        public virtual Privilege FkPrivilege { get; set; }
    }
}
