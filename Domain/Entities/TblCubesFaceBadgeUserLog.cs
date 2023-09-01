using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class TblCubesFaceBadgeUserLog
    {
        public int BadgeIdLog { get; set; }
        public int? IdGame { get; set; }
        public int? CubesFacesGameId { get; set; }
        public int? BadgeId { get; set; }
        public int? Points { get; set; }
        public DateTime? UpdatedDateTime { get; set; }
    }
}
