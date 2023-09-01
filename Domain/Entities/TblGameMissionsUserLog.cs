using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class TblGameMissionsUserLog
    {
        public int IdMissionsUserLog { get; set; }
        public int? IdUser { get; set; }
        public int MissionNo { get; set; }
        public int? MissionPoints { get; set; }
        public int? IdOrganization { get; set; }
        public DateTime UpdatedDateTime { get; set; }
    }
}
