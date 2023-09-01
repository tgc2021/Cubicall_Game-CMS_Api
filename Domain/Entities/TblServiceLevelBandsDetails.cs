using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class TblServiceLevelBandsDetails
    {
        public int ServiceLevelId { get; set; }
        public int? CubesFacesId { get; set; }
        public int? Percentage { get; set; }
        public string Colour { get; set; }
        public int? IdOrganization { get; set; }
        public DateTime UpdatedDateTime { get; set; }
        public string IsActive { get; set; }
    }
}
