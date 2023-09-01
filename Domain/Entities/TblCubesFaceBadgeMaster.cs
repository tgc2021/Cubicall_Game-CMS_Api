using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class TblCubesFaceBadgeMaster
    {
        public int BadgeId { get; set; }
        public int? IdGame { get; set; }
        public int? CubesFacesGameId { get; set; }
        public int? CubesFaceCompleteTime { get; set; }
        public int? IdMasterBadge { get; set; }
        public string BadgeName { get; set; }
        public int BadgePoints { get; set; }
        public string BadgeImgUrl { get; set; }
        public DateTime? UpdatedDateTime { get; set; }
        public int? IdOrganization { get; set; }
        public string IsActive { get; set; }
    }
}
