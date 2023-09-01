using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class TblServiceLevelStreakScoringDetails
    {
        public int StreakId { get; set; }
        public int? CubesFacesId { get; set; }
        public int Streak { get; set; }
        public int StreakPoints { get; set; }
        public int? IdOrganization { get; set; }
        public DateTime UpdatedDateTime { get; set; }
        public string IsActive { get; set; }
    }
}
