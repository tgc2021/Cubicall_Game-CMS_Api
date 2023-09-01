using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class TblFacesAttemptnoMappingwithHierarchy
    {
        public int MapAttemptNoId { get; set; }
        public int AttemptNoMapId { get; set; }
        public int IdOrgHierarchy { get; set; }
        public int? IdOrganization { get; set; }
        public string IsActive { get; set; }
        public DateTime UpdatedDateTime { get; set; }
    }
}
