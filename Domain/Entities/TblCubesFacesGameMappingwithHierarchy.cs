using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class TblCubesFacesGameMappingwithHierarchy
    {
        public int MapFacesGid { get; set; }
        public int CubesFacesMapId { get; set; }
        public int IdOrgHierarchy { get; set; }
        public int? IdOrganization { get; set; }
        public string IsActive { get; set; }
        public DateTime UpdatedDateTime { get; set; }
    }
}
