using System;
using System.Collections.Generic;
using System.Text;

namespace Maham.Models.User
{
   public class PositionListModel:BaseEntity
    {

        public Guid Id { get; set; }
        public Guid EntityId { get; set; }
        public Guid UserId { get; set; }
        public int RoleId { get; set; }
        public int RelationId { get; set; }
        public string uName { get; set; }
        public string eName { get; set; }
        public string roledisplayName { get; set; }
        public string relationdisplayName { get; set; }
        public bool? IsActive { get; set; }
        public string checkImage { get; set; }
        public bool IsCheckedRefe { get; set; }
    }
}
