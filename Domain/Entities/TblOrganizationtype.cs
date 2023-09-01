using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class TblOrganizationtype
    {
        public int IdOrganizationType { get; set; }
        public string OrganizationType { get; set; }
        public string IsActive { get; set; }
        public int? IdOrganization { get; set; }
        public DateTime UpdatedDateTime { get; set; }
    }
}
