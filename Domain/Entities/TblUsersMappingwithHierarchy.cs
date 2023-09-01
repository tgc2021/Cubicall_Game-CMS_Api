using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class TblUsersMappingwithHierarchy
    {
        public int UserMapId { get; set; }
        public int UserId { get; set; }
        public int IdOrgHierarchy { get; set; }
        public string IsActive { get; set; }
        public DateTime UpdatedDateTime { get; set; }
    }
}
