using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class TblOrganizationHierarchy
    {
        public int IdOrgHierarchy { get; set; }
        public string HierarchyName { get; set; }
        public int ParentIdOrgHierarchy { get; set; }
        public int? IdOrganization { get; set; }
        public DateTime UpdatedDateTime { get; set; }
        public int? IdCmsUser { get; set; }
        public string IsActive { get; set; }
    }
}
