using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class TblServiceLevelScoringDetails
    {
        public int ServiceLevelScoringId { get; set; }
        public int? CubesFacesId { get; set; }
        public string LevelEndsColour { get; set; }
        public int BonusPoints { get; set; }
        public int? IdOrganization { get; set; }
        public DateTime UpdatedDateTime { get; set; }
        public string IsActive { get; set; }
    }
}
